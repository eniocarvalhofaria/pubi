using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
namespace BusinessIntelligence.Security
{
    public class ServerSession
    {
        static private RSACryptoServiceProvider RSAServer = new RSACryptoServiceProvider(2048);
        RSACryptoServiceProvider RSAClient;
        public ServerSession()
        {
            RSAClient = new RSACryptoServiceProvider();
        }
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private static string _ServerPublicKey;

        public static string ServerPublicKey
        {
            get
            {

                if (string.IsNullOrEmpty(_ServerPublicKey))
                {
                    _ServerPublicKey = RSAServer.ToXmlString(false);
                }

                return _ServerPublicKey;



            }

        }
        public string ClientPublicKey
        {
            set
            {
                this.RSAClient.FromXmlString(value);
            }
        }
        private bool _IsAuthenticated;

        public bool IsAuthenticated
        {
            get { return _IsAuthenticated; }
            set { _IsAuthenticated = value; }
        }
        private static string _ServerPrivateKey;
        private static string ServerPrivateKey
        {
            get
            {
                if (string.IsNullOrEmpty(_ServerPrivateKey))
                {
                    _ServerPrivateKey = RSAServer.ToXmlString(true);
                }
                return _ServerPrivateKey;
            }
            set { _ServerPrivateKey = value; }
        }

        public static string DecryptFromClient(string encryptedText)
        {
            byte[] Byte_DecryptedMensagem = RSAServer.Decrypt(Convert.FromBase64String(encryptedText), false);
            return Convert.ToBase64String(Byte_DecryptedMensagem).Replace("+", " ").TrimEnd();
        }
        public bool ValidateToken(string token)
        {

            byte[] Byte_DecryptedMensagem = RSAClient.Decrypt(Convert.FromBase64String(token.Substring(8)), false);

     
            return Convert.ToBase64String(Byte_DecryptedMensagem).Equals(token.Substring(0,8));

        }

        public string EncryptToClient(string text)
        {
            switch (text.Length % 4)
            {
                case 1:
                    {
                        text = text + "   ";
                        break;
                    }
                case 2:
                    {
                        text = text + "  ";
                        break;
                    }
                case 3:
                    {
                        text = text + " ";
                        break;
                    }
            }

            byte[] Byte_EncryptedMensagem = RSAClient.Encrypt(Convert.FromBase64String(text.Replace(" ", "+")), false);
            return System.Convert.ToBase64String(Byte_EncryptedMensagem);
        }
    }
}