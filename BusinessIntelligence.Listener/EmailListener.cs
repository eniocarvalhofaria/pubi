using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using BusinessIntelligence.MIME;
using BusinessIntelligence.Configurations;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.Listening
{
    public class EmailListener : Listener
    {
        static string GetFullPath(string currentPath, string relativePathTarget)
        {
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
        List<int> _MessagesReaded = new List<int>();

        public List<int> MessagesReaded
        {
            get { return _MessagesReaded; }
            set { _MessagesReaded = value; }
        }

        public override bool Listen(string applicationDirectory)
        {
            MessagesReaded.Clear();
            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Accessing " + EmailAddress + " mailbox.");
            bool ret = false;
            var im = new IMAPClient(EmailAddress, EmailPWD);



            MessagesReadedFileName = (applicationDirectory + "\\messagesReaded.txt").Replace("\\\\", "\\");

            if (File.Exists(MessagesReadedFileName))
            {
                using (var sr = new StreamReader(MessagesReadedFileName))
                {
                    var content = sr.ReadToEnd();
                    foreach (var item in content.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        MessagesReaded.Add(Convert.ToInt32(item));
                    }
                    sr.Close();
                    sr.Dispose();
                }
            }

            var config = new EmailListenerConfigurationInfo(applicationDirectory + "\\config.xml");

            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Searching subject: " + config.SearchPattern + "...");
            int[] messagesID = im.Search("$ SEARCH SUBJECT \"" + config.SearchPattern + "\" SINCE " + base.LastTimeChecked.ToString("d-MMM-yyyy") + "\r\n");

            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + messagesID.Length + " messages finded.");
            string attachmentDirectory = config.AttachmentDirectory;

            if (!string.IsNullOrEmpty(attachmentDirectory))
            {
                attachmentDirectory = GetFullPath(applicationDirectory, attachmentDirectory);
            }
            //    bool attachmentSaved = false;
            //    bool messageFinded = false;
            foreach (var messageId in messagesID)
            {
                if (!MessagesReaded.Contains(messageId))
                {
                    ret = true;
                    bool attachmentThisEmailSaved = false;
                    Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Message " + messageId.ToString() + " is a unreaded message.");
                    //        messageFinded = true;
                    List<string> attachmentNames = new List<string>();
                    MessagesReaded.Add(messageId);
                    var message = im.GetMIMEMessage(messageId);
                    Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Saving attachments in " + attachmentDirectory);
                    foreach (var att in message.GetAttachments())
                    {
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Attachment " + att.GetName() + " finded.");
                        if (config.SearchExtensions == null || checkExtensions(att.GetName().Replace("\r\n", ""), config.SearchExtensions))
                        {
                            att.WriteToFile(attachmentDirectory + "\\" + att.GetName().Replace("\r", "").Replace("\n", ""));
                            attachmentNames.Add(att.GetName().Replace("\r\n", ""));
                            //attachmentSaved = true;
                            attachmentThisEmailSaved = true;
                        }
                    }
                    if (attachmentThisEmailSaved)
                    {
                        sendMessageProcessStart(message, attachmentNames);
                    }
                    else
                    {
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " No attachment finded.");
                    }
                }
                else
                {
                    Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Message " + messageId.ToString() + " is a readed message.");

                }
            }


            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Leaving " + EmailAddress + " mailbox.");
            im.Logout();

            MessagesReaded.Clear();
            foreach (var messageId in messagesID)
            {
                MessagesReaded.Add(messageId);
            }
            if (ret)
            {
                SetListenOk();
            }
            return ret;

        }
        private string _MessagesReadedFileName;

        public string MessagesReadedFileName
        {
            get { return _MessagesReadedFileName; }
            set { _MessagesReadedFileName = value; }
        }


        public  void SetListenOk()
        {
            using (var sw = new StreamWriter(MessagesReadedFileName))
            {
                var sb = new StringBuilder();
                bool isFirst = true;
                foreach (var mReaded in MessagesReaded)
                {
                    if (!isFirst)
                    {
                        sb.Append(",");
                    }
                    sb.Append(mReaded.ToString());
                    isFirst = false;
                }
                sw.Write(sb.ToString());
                sw.Close();
                sw.Dispose();
            }



        }

        static bool checkExtensions(string fileName, string[] extensions)
        {
            foreach (var extension in extensions)
            {
                if (fileName.EndsWith("." + extension))
                {
                    return true;
                }
            }
            return false;
        }
        static void callProgram(string executablePath, string arguments)
        {

            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Calling " + executablePath);


            var p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            if (executablePath.EndsWith(".bat"))
            {
                p.StartInfo.FileName = "cmd";
                p.Start();
                p.StandardInput.WriteLine(executablePath + " " + arguments);
                p.StandardInput.WriteLine("exit");
            }
            else
            {
                p.StartInfo.FileName = executablePath;
                p.StartInfo.Arguments = arguments;
                p.Start();
            }


            var sb = new StringBuilder();
            sb.Append(p.StandardOutput.ReadToEnd());
            while (!p.HasExited)
            {
                sb.Append(p.StandardOutput.ReadToEnd());
            }
            Log.GetInstance().WriteLine(sb.ToString());
            p.WaitForExit();
        }
        void sendMessageProcessStart(MimeMessage mimeMessage, List<string> attachmentNames)
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
            message.To.Add(mimeMessage.GetTo());

            foreach (var item in mimeMessage.GetTo().Split(new char[] { ',', '<', '>' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (item.IndexOf("@") > -1 && item != EmailAddress)
                {
                    message.CC.Add(item);
                }
            }

            if (mimeMessage.GetCC() != null)
            {
                foreach (var item in mimeMessage.GetCC().Split(new char[] { ',', '<', '>' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (item.IndexOf("@") > -1)
                    {
                        message.CC.Add(item);
                    }
                }
            }


            /*
            foreach (string email in mimeMessage.GetCC())
            {
                message.To.Add(email);
            }
            */
            smtp.Timeout = 0;
            Attachment att = null;
            if (attachmentNames.Count > 1)
            {
                bool isFirst = true;
                string attachments = null;
                foreach (var item in attachmentNames)
                {
                    if (!isFirst)
                    {
                        attachments += ",";
                    }
                    attachments += item.Replace("\r\n", "");
                    isFirst = false;
                }
                message.Subject = "Os anexos " + attachments + " serão carregados";

            }
            else
            {
                message.Subject = "O anexo " + attachmentNames[0] + " será carregado";
            }
            message.IsBodyHtml = true;
            message.Body = "Seu relatório será processado e enviado aos recebedores deste relatório";
            try
            {
                //    Log.GetInstance().WriteLine("Mensagem substituindo envio");
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
    }
}