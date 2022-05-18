using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using BusinessIntelligence.MIME;
using System.Net.Mail;
using System.Net;
using BusinessIntelligence.Configurations;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.Listening
{
    public abstract class Listener
    {

        static string GetFullPath(string currentPath, string relativePathTarget)
        {
            if (string.IsNullOrEmpty(relativePathTarget))
            {
                return currentPath;
            }
            string p = currentPath;
            if (relativePathTarget.Contains(":\\"))
            {
                return relativePathTarget;
            }
            foreach (string level in relativePathTarget.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (level == "..")
                {
                    p = Directory.GetParent(p).FullName;
                }
                else
                {
                    p += "\\" + level;
                }
            }
            return p;

        }
        public string EmailAddress { get; set; }
        public List<ExecutionStep> StepsExecuted = new List<ExecutionStep>();
        public string EmailPWD { get; set; }
        public abstract bool Listen(string applicationDirectory);
        private void GetStepsExecutedFromFile(string applicationDirectory)
        {
            if (File.Exists(applicationDirectory + "\\steps.txt"))
            {
                using (var sr = new StreamReader(applicationDirectory + "\\steps.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        StepsExecuted.Add((ExecutionStep)Enum.Parse(typeof(ExecutionStep), sr.ReadLine()));
                    }
                }
            }
        }
        private void SetStepOk(ExecutionStep step, string applicationDirectory)
        {
            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Setting Step Executed: " + step.ToString());
            if (step == ExecutionStep.End)
            {
                if (File.Exists(applicationDirectory + "\\steps.txt"))
                {
                    File.Delete(applicationDirectory + "\\steps.txt");
                }
            }
            else
            {
                if (!StepsExecuted.Contains(step))
                {
                    StepsExecuted.Add(step);
                    using (var sw = new StreamWriter(applicationDirectory + "\\steps.txt"))
                    {
                        foreach (var item in StepsExecuted)
                        {
                            sw.WriteLine(item.ToString());
                        }
                        sw.Close();
                        sw.Dispose();
                    }
                }
            }
        }
        public bool StepExecuted(ExecutionStep step)
        {
            return StepsExecuted.Contains(step);

        }
        private DateTime _StartTime;
        public DateTime StartTime
        {
            get { return _StartTime; }
        }
        private DateTime _LastTimeChecked;

        public DateTime LastTimeChecked
        {
            get { return _LastTimeChecked; }
            set { _LastTimeChecked = value; }
        }
        private string _ApplicationsDirectoriesFileName;

        public string ApplicationsDirectoriesFileName
        {
            get { return _ApplicationsDirectoriesFileName; }
            set { _ApplicationsDirectoriesFileName = value; }
        }
        public virtual void Start()
        {
            while (true)
            {
                Check();
                int secondsToWait = 60;
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Waiting " + secondsToWait.ToString() + " seconds...");
                System.Threading.Thread.Sleep(secondsToWait * 1000);
            }
        }
        public virtual void Check()
        {
            string dir = Environment.CurrentDirectory;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            string thisDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(Listener)).CodeBase).Replace("file:\\", "");
            //         BusinessIntelligence.Util.Cryptography.CheckKey();


            string applicationDirectoryRoot = GetFullPath(thisDirectory, "..\\..\\..\\Applications");
            string applicationsDirectoriesFile = applicationDirectoryRoot + "\\" + ApplicationsDirectoriesFileName;



            string[] applicationsDirectories = null;

            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + " Reading " + applicationsDirectoriesFile);

            using (var sr = new StreamReader(applicationsDirectoriesFile))
            {
                applicationsDirectories = sr.ReadToEnd().Replace("\r", "").Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                sr.Close();
                sr.Dispose();
            }

            foreach (string applicationDirectoryItem in applicationsDirectories)
            {

                ListenerConfigInfo config = null;
                //   Log log = Log.GetInstance(applicationDirectoryItem);
                Log log = Log.GetInstance();
                try
                {
                    this.StepsExecuted.Clear();
                    var applicationDirectory = applicationDirectoryRoot + "\\" + applicationDirectoryItem;
                    log.FileName = applicationDirectory + "\\" + applicationDirectoryItem + "." + DateTime.Now.ToString("yyyy-MM-dd") + ".log.txt";
                    log.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Selecting application directory: " + applicationDirectoryItem + "...");
                    this.GetStepsExecutedFromFile(applicationDirectory);
                    config = new ListenerConfigInfo(applicationDirectory + "\\config.xml");
                    log.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + applicationDirectory + " selected.");

                    Environment.CurrentDirectory = applicationDirectory;
                    _StartTime = DateTime.Now;

                    DateTime.TryParse("2015-01-01", out _LastTimeChecked);
                    string LastTimeReadFile = (applicationDirectory + "\\LastTimeRead.txt").Replace("\\\\", "\\");
                    if (File.Exists(LastTimeReadFile))
                    {
                        using (var sr = new StreamReader(LastTimeReadFile))
                        {
                            DateTime.TryParse(sr.ReadToEnd(), out _LastTimeChecked);
                            sr.Close();
                            sr.Dispose();
                        }
                    }
                    bool dayToUpdate = true;
                    if (!config.DaysOfWeek.Contains(DateTime.Now.DayOfWeek))
                    {
                        log.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Today is " + DateTime.Now.DayOfWeek.ToString() + ". The application will not run");
                        dayToUpdate = false;
                    }
                    bool ListenResult = Listen(applicationDirectory);

                    if (ListenResult)
                    {
                        StepsExecuted.Clear();
                        SetStepOk(ExecutionStep.Listen, applicationDirectory);
                    }
                    else if (StepsExecuted.Count > 0)
                    {
                        log.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Steps executed before:");
                        foreach (var item in StepsExecuted)
                        {
                            log.WriteLine("\t" + item.ToString());
                        }
                    }
                    if ((dayToUpdate && ListenResult) || StepsExecuted.Count >= 1)
                    {
                        if (!string.IsNullOrEmpty(config.ExecutableFile))
                        {
                            if (!StepExecuted(ExecutionStep.Executable))
                            {
                                string executableFullDirectory = GetFullPath(applicationDirectory, config.ExecutableDirectory);
                                string currentDirectory = Environment.CurrentDirectory;
                                callProgram(config.ExecutableDirectory, config.ExecutableFile, config.ExecutableArguments);
                                SetStepOk(ExecutionStep.Executable, applicationDirectory);
                            }
                        }



                        if (!string.IsNullOrEmpty(config.EtlName))
                        {
                            if (!StepExecuted(ExecutionStep.ETLProgram))
                            {

                                callProgram(GetFullPath(thisDirectory, @"..\..\..\" + config.EtlName + @"\bin\Debug"), config.EtlName + ".exe", "--filesdirectory " + GetFullPath(applicationDirectory, config.FilesDirectory));
                                SetStepOk(ExecutionStep.ETLProgram, applicationDirectory);
                            }
                        }
                        if (config.CallReportRefresh)
                        {

                            var exc = new BusinessIntelligence.Report.ExcelReport(applicationDirectory);
                            //          exc.IsTest = true;

                            if (StepExecuted(ExecutionStep.Refresh) || exc.Refresh())
                            {
                                SetStepOk(ExecutionStep.Refresh, applicationDirectory);
                                if (exc.Publish())
                                {
                                    SetStepOk(ExecutionStep.Publish, applicationDirectory);
                                    SetStepOk(ExecutionStep.End, applicationDirectory);
                                }
                            }

                        }
                        else
                        {
                            SetStepOk(ExecutionStep.End, applicationDirectory);
                        }


                    }
                    log.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Setting LastTimeReadFile: " + StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    using (var sw = new StreamWriter(LastTimeReadFile))
                    {
                        sw.Write(StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        sw.Close();
                        sw.Dispose();
                    }

                }
                catch (Exception ex)
                {
                    log.WriteLine(ex.Message);
                    log.WriteLine(ex.StackTrace);
                    sendErrorMessage(config.AdminEmailAddress, ex, applicationDirectoryItem);
                }
            }



        }
        static string tryGetParameter(XmlElement xe, string path)
        {
            string ret;
            try
            {
                ret = xe.SelectNodes(path)[0].InnerText;
                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        static void callProgram(string executableDirectory, string executableFile, string arguments)
        {
            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Calling " + executableFile);
            string appDir = Environment.CurrentDirectory;
            if (!string.IsNullOrEmpty(executableDirectory))
            {
                Environment.CurrentDirectory = executableDirectory;
            }
            var a = new ProcessExecutor(executableFile, arguments);

            try
            {
                a.Process.StartInfo.EnvironmentVariables.Add("KEY", EnvironmentParameters.Key);
            }
            catch (Exception ex)
            {

            }
            a.Execute();
            Environment.CurrentDirectory = appDir;


            if (a.ReturnCode != 0)
            {
                throw new Exception(a.GetOutputFromApplication());
            }
        }
        void sendErrorMessage(string adminEmailAddress, Exception exception, string applicationName)
        {
            var fromAddress = new MailAddress(EmailAddress);
            var fromPassword = EmailPWD;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            var message = new MailMessage();
            message.From = fromAddress;
            if (!string.IsNullOrEmpty(adminEmailAddress))
            {
                message.To.Add(new MailAddress(adminEmailAddress));
            }
            message.To.Add(new MailAddress(EnvironmentParameters.EmailAddress));
            message.To.Add(new MailAddress("businessintelligence@peixeurbano.com"));
            smtp.Timeout = 0;
            message.Subject = "Erro em :" + applicationName;
            message.IsBodyHtml = false;
            message.Body = "A aplicação " + applicationName + " apresentou o seguinte erro:\r\n " +
                exception.Message + "\r\n" +
                "Rastreamento:\r\n" +
                exception.StackTrace;

            try
            {
                smtp.Send(message);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
