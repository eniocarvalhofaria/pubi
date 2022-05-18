using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Parsing;
using CommandLine.Text;
using System.IO;
using BusinessIntelligence.Util;
using System.Xml;
using System.Net;
using System.Net.Mail;
namespace BusinessIntelligence.CheckConsistency
{
    class Program
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

        private class Options
        {
            [Option("directory", Required = true, HelpText = "Directory with configuration file")]
            public string Directory { get; set; }

            [Option("redshiftuserid", Required = false, HelpText = "Redshift User Id")]
            public string RedshiftUserId { get; set; }

            [Option("redshiftpwd", Required = false, HelpText = "Redshift User Pwd")]
            public string RedshiftPassword { get; set; }
            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }
        }
        private static Options options = new Options();
    static    List<string> emails = new List<string>();
        static void Main(string[] args)
        {
            try
            {
                Parser parser = new Parser
                (
                    delegate (ParserSettings settings)
                    {
                        settings.CaseSensitive = true;
                        settings.HelpWriter = Console.Out;
                    }
                );

                if (!parser.ParseArguments(args, options))
                {
                    throw new ParserException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Environment.Exit(-1);
            }


            List<ConsistencyTest> list = new List<ConsistencyTest>();
            ConsistencyTest test = null;
            if (!options.Directory.Contains("\\"))
            {
                options.Directory = "..\\..\\..\\Alerts\\" + options.Directory;
            }
            string filename = GetFullPath(Environment.CurrentDirectory, options.Directory + "\\config.xml");

            
            using (var sr = new System.IO.StreamReader(GetFullPath(Environment.CurrentDirectory, options.Directory + "\\recipients.txt")))
            {
                while (!sr.EndOfStream)
                {
                    var l = sr.ReadLine();
                    if (!string.IsNullOrEmpty(l) && l.IndexOf("@") > 1)
                    {
                        emails.Add(l);
                    }
                }
                sr.Close();
                sr.Dispose();
            }
            Directory.CreateDirectory(options.Directory + "\\log");
            Log.GetInstance().FileName = options.Directory + "\\log\\log." + DateTime.Now.ToString("yyyyMMdd.HHmmss") + ".log";


            if (File.Exists(filename))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                XmlElement root = doc.DocumentElement;


                for (int i = 0; i < root.SelectNodes("/root/test").Count; i++)
                //     foreach (XmlNode xtest in root.SelectNodes("/root/test"))
                {
                    var xtest = new XmlDocument();
                    xtest.InnerXml = root.SelectNodes("/root/test")[i].OuterXml;

                    test = new ConsistencyTest();
                    test.Xml = root.SelectNodes("/root/test")[i].InnerXml;
                    test.Name = xtest.SelectNodes("/test/name")[0].InnerText;

                    test.Connection1Name = xtest.SelectNodes("/test/query/connectionName")[0].InnerText;
                    test.Sql1 = xtest.SelectNodes("/test/query/sql")[0].InnerText;
                    try
                    {
                        test.Type = TestType.Comparison;
                        test.Connection2Name = xtest.SelectNodes("/test/query/connectionName")[1].InnerText;
                        test.Sql2 = xtest.SelectNodes("/test/query/sql")[1].InnerText;
                    }
                    catch (Exception ex)
                    {
                        test.Type = TestType.Simple;
                    }
                    try
                    {
                        test.ErrorTolerance = Convert.ToInt32(xtest.SelectNodes("/test/errorTolerance")[0].InnerText);
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        test.WarningTolerance = Convert.ToInt32(xtest.SelectNodes("/test/warningTolerance")[0].InnerText);
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        test.ErrorMinValue = Convert.ToDouble(xtest.SelectNodes("/test/errorMinValue")[0].InnerText);
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        test.WarningMinValue = Convert.ToDouble(xtest.SelectNodes("/test/warningMinValue")[0].InnerText);
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        test.ErrorMaxValue = Convert.ToDouble(xtest.SelectNodes("/test/errorMaxValue")[0].InnerText);
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        test.WarningMaxValue = Convert.ToDouble(xtest.SelectNodes("/test/warningMaxValue")[0].InnerText);
                    }
                    catch (Exception ex)
                    {

                    }
                    list.Add(test);
                }
            }

            foreach (var item in list)
            {
                try
                {
                    System.Data.Common.DbConnection conn1 = getConnection(item.Connection1Name);
                    var ex1 = new BusinessIntelligence.Data.QueryExecutor(conn1);

                    var dt1 = ex1.ReturnData(item.Sql1);
                    if (ex1.ReturnCode > 0)
                    {
                        FailTest(item, ex1.DatabaseMessage,1);
                    }
                    double retorno1 = 0;
                    if (dt1.Rows.Count == 0)
                    {
                        FailTest(item, "The query 1 not returned result.",1);

                    }
                    else if (dt1.Rows[0][0].Equals(DBNull.Value))
                    {
                        FailTest(item, "The query 1 returned null.",1);
                    }
                    else
                    {
                        retorno1 =  Convert.ToDouble(dt1.Rows[0][0]);
                        Log.GetInstance().WriteLine("Value founded: " + retorno1.ToString());
                        if (item.ErrorMaxValue < double.MaxValue && retorno1 > item.ErrorMaxValue)
                        {
                            FailTest(item, "The query 1 value was " + retorno1.ToString() + " and the max value allowed is: " + item.ErrorMaxValue,1);
                        }
                        else if (item.WarningMaxValue < double.MaxValue && retorno1 > item.WarningMaxValue)
                        {
                            FailTest(item, "The query 1 value was " + retorno1.ToString() + " and " + item.WarningMaxValue + " is the max value with no warning.", 0);
                        }
                        if (item.ErrorMinValue > double.MinValue && retorno1 < item.ErrorMinValue)
                        {
                            FailTest(item, "The query 1 value was " + retorno1.ToString() + " and the min value allowed is: " + item.ErrorMinValue,1);
                        }
                        else if (item.WarningMinValue > double.MinValue && retorno1 < item.WarningMinValue)
                        {
                            FailTest(item, "The query 1 value was " + retorno1.ToString() + " and " + item.ErrorMinValue + " is the min value with no warning.", 0);
                        }
                        if (item.Type == TestType.Comparison)
                        {
                            System.Data.Common.DbConnection conn2 = getConnection(item.Connection2Name);
                            var ex2 = new BusinessIntelligence.Data.QueryExecutor(conn2);
                            var dt2 = ex2.ReturnData(item.Sql2);
                            if (ex2.ReturnCode > 0)
                            {
                                FailTest(item, ex2.DatabaseMessage,1);
                            }
                            double retorno2 = 0;
                            if (dt2.Rows.Count == 0)
                            {
                                FailTest(item, "The query 2 not returned result.",1);

                            }
                            else if (dt2.Rows[0][0].Equals(DBNull.Value))
                            {
                                FailTest(item, "The query 2 returned null.",1);
                            }
                            else
                            {
                                retorno2 = Convert.ToDouble(dt2.Rows[0][0]);
                                Log.GetInstance().WriteLine("Value founded: " + retorno2.ToString());
                                if ((retorno1 > retorno2 && retorno1 - retorno2 > item.ErrorTolerance) || (retorno2 > retorno1 && retorno2 - retorno1 > item.ErrorTolerance))
                                {
                                    FailTest(item, "The query 1 value was " + retorno1.ToString() + " and query 2 value was " + retorno2.ToString() + " the difference tolerated is: " + item.ErrorTolerance,1);
                                }
                                else if ((retorno1 > retorno2 && retorno1 - retorno2 > item.WarningTolerance) || (retorno2 > retorno1 && retorno2 - retorno1 > item.WarningTolerance))
                                {
                                    FailTest(item, "The query 1 value was " + retorno1.ToString() + " and query 2 value was " + retorno2.ToString() + " the difference tolerated with no warning is: " + item.WarningTolerance, 1);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    FailTest(item, ex.Message,1);

                }
            }
            Environment.Exit(returnCode);
        }
        static int  returnCode = 0;
        static void FailTest(ConsistencyTest test, string Message,int returncode)
        {
            returnCode = returncode;
            Attachment att = null;

            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Declaring SMTP Client ");

            using (var smtp = new SmtpClient())
            {
                if (Environment.MachineName.Contains("PEIXE-NB"))
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(EnvironmentParameters.EmailAddress, EnvironmentParameters.EmailPwd);
                   
                }
                else
                {

                    smtp.Host = "mailrelay02.peixeurbano.local";
                    smtp.Port = 25;
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = true;
                }
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Creating Mail Message ");
                using (var message = new MailMessage())
                {
                    try
                    {
                        message.From = new MailAddress(EnvironmentParameters.EmailAddress, "Informações Gerenciais");
                        foreach (string email in emails)
                        {
                            if (email.Contains("@") && email.Contains("."))
                            {
                                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Adding address " + email);
                                message.To.Add(email);
                            }
                        }
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Setting SMTP Timeout ");
                        smtp.Timeout = int.MaxValue;

                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Setting subject");
                        if (returncode > 0)
                        {
                            message.Subject = "Erro no teste " + test.Name + " " + DateTime.Today.ToString("yyyy-MM-dd");
                        }
                        else {
                            message.Subject = "Alerta no teste " + test.Name + " " + DateTime.Today.ToString("yyyy-MM-dd");

                        }

                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Setting body");

                        StringBuilder body = new StringBuilder();
                        body.Append(Message + "\r\n");
                        body.Append(test.Xml + "\r\n");

                        body.Replace("<query>", "\r\n<query>");
                        body.Replace("<query>", "\r\n<query>");
                        body.Replace("<connectionName>", "\r\n\t<connectionName>");
                        body.Replace("<sql>", "\r\n\t<sql>");
                        body.Replace("<minValue>", "\r\n<minValue>");
                        body.Replace("<maxValue>", "\r\n<maxValue>");
                        body.Replace("<tolerance>", "\r\n<tolerance>");
                        message.Body = body.ToString();
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Sending email");
                        smtp.Send(message);
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Email sended");
                    }
                    catch (SmtpException ex)
                    {
                        if (att != null)
                        {
                            att.Dispose();
                        }
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Smtp Exception.");
                        Log.GetInstance().WriteLine("\tMessage: " + ex.Message);
                        Log.GetInstance().WriteLine("\tStatus Code: " + ex.StatusCode.ToString());
                        Log.GetInstance().WriteLine("\tStack Trace: " + ex.StackTrace.ToString());
                        foreach (KeyValuePair<string, string> item in ex.Data)
                        {
                            Log.GetInstance().WriteLine("\t" + item.Key + ": " + item.Value);
                        }
                        smtp.Dispose();
                        message.Dispose();
                        throw new SmtpException(ex.Message, ex);
                    }
                }
            }


        }
        static Dictionary<string, System.Data.Common.DbConnection> connections = new Dictionary<string, System.Data.Common.DbConnection>();

        private static System.Data.Common.DbConnection getConnection(string name)
        {
            if (connections.ContainsKey(name))
            {
                return connections[name];
            }
            System.Data.Common.DbConnection conn2 = null;
            switch (name.ToUpper())
            {
                case "REPORTS":
                    {
                        conn2 = BusinessIntelligence.Data.Connections.GetNewConnection(Data.Database.REPORTS);
                        break;
                    }
                case "DW":
                    {
                        conn2 = BusinessIntelligence.Data.Connections.GetNewConnection(Data.Database.DW);
                        break;
                    }
                case "EMAILDELIVERY":
                    {
                        conn2 = BusinessIntelligence.Data.Connections.GetNewConnection(Data.Database.EMAILDELIVERY);
                        break;
                    }
                case "PEIXEURBANO":
                    {
                        conn2 = BusinessIntelligence.Data.Connections.GetNewConnection(Data.Database.PEIXEURBANO);
                        break;
                    }
                case "REDSHIFT":
                    {
                        if (!string.IsNullOrEmpty(options.RedshiftUserId))
                        {
                            EnvironmentParameters.RedshiftUserId = options.RedshiftUserId;
                            EnvironmentParameters.RedshiftPwd = options.RedshiftPassword;
                        }
                        conn2 = BusinessIntelligence.Data.Connections.GetNewConnection(Data.Database.REDSHIFT);
                        break;
                    }
                default:
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " This connection name is not a valid option.");
                        Environment.Exit(3);
                        break;
                    }
            }
            connections.Add(name, conn2);
            return conn2;

        }

        public string fileContent(string path)
        {
            string retorno;
            using (System.IO.StreamReader sr = new System.IO.StreamReader(path.Replace("file:\\", "")))
            {
                retorno = sr.ReadToEnd();
                sr.Close();
            }

            return retorno;
        }
    }
}

