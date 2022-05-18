using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;
using BusinessIntelligence.MIME;
using BusinessIntelligence.Util;
namespace BusinessIntelligence
{
    public class IMAPClient
    {
        public void Logout()
        {
            receiveResponse("$ LOGOUT\r\n");
        }
        public IMAPClient(string username, string password)
        {
            this.username = username;
            this.password = password;

            /*
            path = Environment.CurrentDirectory + "\\emailresponse.txt";

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
           
            sw = new System.IO.StreamWriter(System.IO.File.Create(path));
             */

            tcpc = new System.Net.Sockets.TcpClient("imap.gmail.com", 993);

            ssl = new System.Net.Security.SslStream(tcpc.GetStream());
            ssl.AuthenticateAsClient("imap.gmail.com");
          var resp =   receiveResponse("$ LOGIN " + username + " " + password + "  \r\n");

            if (resp.ToUpper().Contains("FAIL"))
            {
                throw new Exception("resp");
            }

            //            receiveResponse("$ LIST " + "\"\"" + " \"*\"" + "\r\n");
            //    receiveResponse("$ SELECT INBOX\r\n (UTF8)");
            receiveResponse("$ SELECT INBOX\r\n");
        }

        //    System.IO.StreamWriter sw = null;
        System.Net.Sockets.TcpClient tcpc = null;
        System.Net.Security.SslStream ssl = null;
        string username, password;
        string path;
        int bytes = -1;
        byte[] buffer;
        StringBuilder sb = new StringBuilder();
        byte[] dummy;
        public MimeMessage GetMIMEMessage(int id)
        {
            string message = receiveResponse("$ FETCH " + id.ToString() + " BODY[]\r\n");
            MimeMessage aMimeMessage = new MimeMessage();
            aMimeMessage.LoadBody(message);
            return aMimeMessage;
            var bodylist = aMimeMessage.GetBodyPartList();

            for (int i = 0; i < bodylist.Count; i++)
            {
                MimeBody ab = (MimeBody)bodylist[i];
                if (ab.IsText())
                {
                    string m = ab.GetText();
                }
                else if (ab.IsAttachment())
                {
                    ab.WriteToFile(ab.GetName());
                }
            }


        }
        string commandText = null;
        public int[] SearchSubject(string criterial)
        {


            var retorno = receiveResponse("$ SEARCH SUBJECT \"" + criterial + "\"\r\n").Replace("\r\n", " ").Substring(8);
            List<int> ret = new List<int>();
            foreach (var item in retorno.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (item == "$")
                {
                    break;
                }
                try
                {
                    ret.Add(Convert.ToInt32(item));
                }
                catch (Exception ex)
                {
                    Log.GetInstance().WriteLine("Erro ao converter: " + item);
                    Log.GetInstance().WriteLine("O comando foi: " + commandText);
                    Log.GetInstance().WriteLine("O retorno foi: " + retorno);
                    Log.GetInstance().WriteLine(ex.Message);
                    Log.GetInstance().WriteLine(ex.StackTrace);
                }

            }

            return ret.ToArray();
        }
        public int[] Search(string commandText)
        {


            //var retorno = receiveResponse(commandText.Replace("SEARCH","SEARCH CHARSET UTF-8" )).Replace("\r\n", " ").Substring(8);
            var retorno = receiveResponse(commandText).Replace("\r\n", " ");
            List<int> ret = new List<int>();
            if (retorno.Length > 10)
            {
                retorno = retorno.Substring(8);

                foreach (var item in retorno.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (item == "$")
                    {
                        break;
                    }
                    try
                    {
                        ret.Add(Convert.ToInt32(item));
                    }
                    catch (Exception ex)
                    {
                        Log.GetInstance().WriteLine("Erro ao converter: " + item);
                        Log.GetInstance().WriteLine("O comando foi: " + commandText);
                        Log.GetInstance().WriteLine("O retorno foi: " + retorno);
                        Log.GetInstance().WriteLine(ex.Message);
                        Log.GetInstance().WriteLine(ex.StackTrace);
                    }
                }

            }

            return ret.ToArray();

        }
        public void execute()
        {
            try
            {




                receiveResponse("$ STATUS INBOX (MESSAGES)\r\n");


                Console.WriteLine("enter the email number to fetch :");
                int number = int.Parse(Console.ReadLine());

                receiveResponse("$ FETCH " + number + " body[header]\r\n");
                receiveResponse("$ FETCH " + number + " body[text]\r\n");


                receiveResponse("$ LOGOUT\r\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.Message);
            }
            finally
            {
                /*
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
                 */
                if (ssl != null)
                {
                    ssl.Close();
                    ssl.Dispose();
                }
                if (tcpc != null)
                {
                    tcpc.Close();
                }
            }


            Console.ReadKey();
        }
        public string receiveResponse(string command)
        {

            commandText = command;
            try
            {
                if (command != "")
                {
                    sb = new StringBuilder();
                    if (tcpc.Connected)
                    {

                        //   dummy = Encoding.UTF8.GetBytes(command);
                        dummy = Encoding.ASCII.GetBytes(command);
                        ssl.Write(dummy, 0, dummy.Length);
                    }
                    else
                    {
                        throw new ApplicationException("TCP CONNECTION DISCONNECTED");
                    }
                }
                ssl.Flush();

                buffer = new byte[2048];
                ssl.ReadTimeout = 60000;

                while (ssl.Read(buffer, 0, 2048) > 0)
                {

                    sb.Append(Encoding.ASCII.GetString(buffer).Replace("\0", ""));
                    if (Encoding.ASCII.GetString(buffer).Contains("$ OK") || Encoding.ASCII.GetString(buffer).Contains("$ BAD") || Encoding.ASCII.GetString(buffer).Contains("$ NO"))
                    {
                        break;
                    }
                    /*
                    sb.Append(Encoding.UTF8.GetString(buffer).Replace("\0", ""));
                    if (Encoding.UTF8.GetString(buffer).Contains("$ OK") || Encoding.UTF8.GetString(buffer).Contains("$ BAD"))
                    {
                        break;
                    }
                    */
                    buffer = new byte[2048];
                    //              ssl.ReadTimeout = 1;
                }



                //             ssl.ReadTimeout = -1;
                var RETORNO = sb.ToString();

                return RETORNO;
            }
            catch (Exception ex)
            {
                if (ssl.ReadTimeout == 1)
                {
                    ssl.ReadTimeout = -1;
                    var RETORNO = sb.ToString();
                    return RETORNO;
                }
                else
                {
                    throw new ApplicationException(ex.Message);
                }
            }
        }

    }
}
