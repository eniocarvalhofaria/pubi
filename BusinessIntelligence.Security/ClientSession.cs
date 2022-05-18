using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
namespace BusinessIntelligence.Security
{
    public class ClientSession
    {
        static private RSACryptoServiceProvider RSAClient = new RSACryptoServiceProvider(2048);
        RSACryptoServiceProvider RSAServer;
        public ClientSession()
        {
            RSAServer = new RSACryptoServiceProvider();
        }
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private static string _ClientPublicKey;

        public static string ClientPublicKey
        {
            get
            {

                if (string.IsNullOrEmpty(_ClientPublicKey))
                {
                    _ClientPublicKey = RSAClient.ToXmlString(false);
                }

                return _ClientPublicKey;



            }

        }
        public string ServerPublicKey
        {
            set
            {
                this.RSAServer.FromXmlString(value);
            }
        }

        private static string _ClientPrivateKey;
        private static string ClientPrivateKey
        {
            get
            {
                if (string.IsNullOrEmpty(_ClientPrivateKey))
                {
                    _ClientPrivateKey = RSAClient.ToXmlString(true);
                }
                return _ClientPrivateKey;
            }
            set { _ClientPrivateKey = value; }
        }
        public static string DecryptFromServer(string encryptedText)
        {
            byte[] Byte_DecryptedMensagem = RSAClient.Decrypt(Convert.FromBase64String(encryptedText), false);
            return Convert.ToBase64String(Byte_DecryptedMensagem).Replace("+", " ").TrimEnd();
        }

        public static string GenerateToken()
        {
            var r = new Random();
            byte[] bt = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                bt[i] = (byte)r.Next(65, 90);
            }

            byte[] Byte_EncryptedMensagem = RSAClient.Encrypt(bt, false);
            return Encoding.ASCII.GetString(bt) + System.Convert.ToBase64String(Byte_EncryptedMensagem);


        }

        public string EncryptToServer(string text)
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

            byte[] Byte_EncryptedMensagem = RSAServer.Encrypt(Convert.FromBase64String(text.Replace(" ", "+")), false);
            return System.Convert.ToBase64String(Byte_EncryptedMensagem);
        }
    }
}