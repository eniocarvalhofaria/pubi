using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Reflection;
namespace BusinessIntelligence.Reports
{
    public class EmailPublisher : IPublisher
    {
        private string _ReportFile;

        public string ReportFile
        {
            get { return _ReportFile; }
            set { _ReportFile = value; }
        }

        private string _ReportName;
        public string ReportName
        {
            get { return _ReportName; }
            set { _ReportName = value; }
        }

        private string[] _Recipients;
        public string[] Recipients
        {
            get { return _Recipients; }
            set { _Recipients = value; }
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

        public void Publish()
        {

            var a = ConfigurationManager.AppSettings["fromAddress"];
            var fromAddress = new MailAddress(ConfigurationManager.AppSettings["fromAddress"], ConfigurationManager.AppSettings["fromAlias"]);
            var fromPassword = ConfigurationManager.AppSettings["fromPwd"];
            var smtp = new SmtpClient
                       {
                           Host = ConfigurationManager.AppSettings["smtpHost"],
                           Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]),
                           EnableSsl = true,
                           DeliveryMethod = SmtpDeliveryMethod.Network,
                           UseDefaultCredentials = false,
                           Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                       };

            var message = new MailMessage();
            message.From = fromAddress;
            foreach (string email in Recipients)
            {
                message.To.Add(email);
            }
            smtp.Timeout = 0;
            Attachment att = null;
            if (!string.IsNullOrEmpty(ReportFile))
            {
                att = new Attachment(ReportFile);
                message.Attachments.Add(att);
            }
            message.Subject = ReportName + " " + ReportDate.ToString("yyyy-MM-dd");
            String strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            message.IsBodyHtml = true;
            message.Body = fileContent(strAppDir + @"\Resources\Html\Body.htm").Replace("@Content", Content).Replace("@Name", ReportName).Replace("@Date", ReportDate.ToString("yyyy-MM-dd"));
            try
            {
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                if (att != null)
                {
                    att.Dispose();
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
