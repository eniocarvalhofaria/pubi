using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Reflection;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.Report
{
    public class EmailPublisher : IPublisher
    {
        private string _ReportFileName;

        public string ReportFileName
        {
            get { return _ReportFileName; }
            set { _ReportFileName = value; }
        }

        private string _ReportName;
        public string ReportName
        {
            get { return _ReportName; }
            set { _ReportName = value; }
        }
        public string EmailAddress { get; set; }
        public string EmailPWD { get; set; }

        private string[] _Recipients;
        public string[] Recipients
        {
            get { return _Recipients; }
            set { _Recipients = value; }
        }

        private string _DownloadUrl;
        public string DownloadUrl
        {
            get
            {
                return _DownloadUrl;
            }

            set
            {
                _DownloadUrl = value;
            }
        }
        private string _Content;

        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }
        private DateTime _ReportDate;

        public DateTime ReportDate
        {
            get { return _ReportDate; }
            set { _ReportDate = value; }
        }
        private string _Subject;

        public string Subject
        {
            get
            {
                if (string.IsNullOrEmpty(_Subject))
                {
                    return ReportName + " " + ReportDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    return _Subject;
                }
            }
            set { _Subject = value; }
        }

        public string AdminEmailAddress { get; set; }

        public bool Publish()
        {
            Attachment att = null;

            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Declaring SMTP Client ");

            using (var smtp = new SmtpClient())
            {
                if (Environment.MachineName.Contains("PEIXE-NB") || 0 == 0)
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(EmailAddress, this.EmailPWD);
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
                        message.From = new MailAddress(EmailAddress, "Informações Gerenciais");
                   
                        if (!this.Subject.ToLower().StartsWith("test"))
                        {
                            message.To.Add(new MailAddress("businessintelligence@peixeurbano.com"));
                            foreach (string email in Recipients)
                            {
                                if (email.Contains("@") && email.Contains("."))
                                {
                                    Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Adding address " + email);
                                    message.To.Add(email);
                                }
                            }
                        }
                        else {
                            if(!string.IsNullOrEmpty(AdminEmailAddress))
                            message.To.Add(AdminEmailAddress);
                        }
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Setting SMTP Timeout ");
                        smtp.Timeout = 600000;

                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Setting subject");
                        
                        message.Subject = this.Subject;
                        String strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                        message.IsBodyHtml = true;
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Setting body");

                        message.Body = fileContent(strAppDir + @"\Resources\Html\Body.htm").Replace("@Content", Content).Replace("@Name", ReportName).Replace("@Date", ReportDate.ToString("yyyy-MM-dd"));
                      
                        string downloadButtonContent = null;
                        if (!string.IsNullOrEmpty(this.DownloadUrl))
                        {
                            downloadButtonContent = fileContent(strAppDir + @"\Resources\Html\DownloadButton.htm").Replace("@ReportUrl", this.DownloadUrl);
                        }
                        else if (!string.IsNullOrEmpty(ReportFileName))
                        {
                            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Adding attachment " + ReportFileName);
                            att = new Attachment(ReportFileName);
                            message.Attachments.Add(att);
                        }
                        message.Body = message.Body.Replace("@DownloadButton", downloadButtonContent);

                        using (var sw = new StreamWriter(new FileInfo(Log.GetInstance().FileName).DirectoryName + "\\EmailBody.htm"))
                        {
                            sw.WriteLine(message.Body);
                            sw.Close();
                            sw.Dispose();
                        }
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Sending email");
                        DateTime sendDate = DateTime.Now;
                        int contError = 0;
                        while (true)
                        {
                            try
                            {
                                smtp.Send(message);
                                break;
                            }
                            catch (SmtpException ex)
                            {
                                             contError++;
                                if (DateTime.Now.Subtract(sendDate).Milliseconds < smtp.Timeout || contError > 3)
                                {
                                    throw ex;
                                }
                            }
                        }
                    
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Email sended");
                        return true;
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
