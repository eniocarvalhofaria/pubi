using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net.Mail;
using System.Net;
using DotNetOpenAuth.ApplicationBlock;
using DotNetOpenAuth.OAuth;
using BusinessIntelligence.Security;
namespace BusinessIntelligence
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        public Service()
        {

        }

        
        private Dictionary<int, ServerSession> sessions = new Dictionary<int, ServerSession>();


        [WebMethod]
        public int RequestSession(string clientPublicKey)
        {
            int number = sessions.Count + 1;
            var nss = new ServerSession();
            nss.ClientPublicKey = clientPublicKey;
            sessions.Add(number, nss);
            return number;
        }

        [WebMethod]
        public string RequestServerPublicKey()
        {
            return ServerSession.ServerPublicKey;
        }
       
        public bool Authenticate(string encryptedUserEmailAddress, string encryptedPassword)
        {
            return false;
        }


        [WebMethod]
        public string HelloWorld()
        {
            MailMessage mail = new MailMessage();

            mail.To.Add(new MailAddress("enio.carvalho.faria@gmail.com"));

            mail.From = new MailAddress("enio.faria@peixeurbano.com");

            mail.Subject = "Teste web service";
            mail.Body = "Alô meu chapa!";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;   // [1] You can try with 465 also, I always used 587 and got success 25
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // [2] Added this
            smtp.UseDefaultCredentials = false; // [3] Changed this
            smtp.Credentials = new NetworkCredential(mail.From.Address, "Sissa198501");
            smtp.Host = "smtp.gmail.com";
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateMessageWithAttachment(): {0}",
                      ex.ToString());
            }


            return "Hello World";
        }
    }
}