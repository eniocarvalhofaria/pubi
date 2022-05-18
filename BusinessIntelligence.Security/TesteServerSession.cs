using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
namespace BusinessIntelligence.Security
{
    public class TesteServerSession
    {
        public TesteServerSession()
        {
            //Mensagem a ser criptografada
            string strMensagem = "Criptografia";
            //instancia de RSACryptoServiceProvider para geração
            //da chave pública e privada
            /*
            CspParameters cspParam = new CspParameters();
            cspParam.Flags = CspProviderFlags.UseMachineKeyStore;*/
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048);

            //Obtem a chave pública e armazena em formato XML
            string publicKey = RSA.ToXmlString(false);

            //Obtem a chave privada e armazena em formato XML
            string privateKey = RSA.ToXmlString(true);

            //instancia de RSACryptoServiceProvider para
            //criptografar mensagem
            RSACryptoServiceProvider RSA2 = new RSACryptoServiceProvider();

            // Carrega a chave pública
            RSA2.FromXmlString(publicKey);

            //criptografa a mensagem
            byte[] Byte_EncryptedMensagem = RSA2.Encrypt(Convert.FromBase64String(strMensagem), false);
            string String_EncryptedMensagem = System.Convert.ToBase64String(Byte_EncryptedMensagem);

            Console.WriteLine("Texto original criptografado: '{0}'", strMensagem);
            Console.WriteLine("Pressione  para continuar....");
            Console.ReadLine();

            //instancia de RSACryptoServiceProvider para
            //criptografar mensagem
            RSACryptoServiceProvider RSA3 = new RSACryptoServiceProvider();

            //Carrega Chave Privada
            RSA3.FromXmlString(privateKey);

            //Descriptografa a mensagem
            byte[] Byte_DecryptedMensagem = RSA.Decrypt(Convert.FromBase64String(String_EncryptedMensagem), false);
            string String_DecryptedMensagem = Convert.ToBase64String(Byte_DecryptedMensagem);

            Console.WriteLine("Texto criptografado / descriptografado: '{0}'", String_DecryptedMensagem);

        }
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        string _ClientPublicKey;

        public string ClientPublicKey
        {
            get { return _ClientPublicKey; }
            set { _ClientPublicKey = value; }
        }
        string _ServerPublicKey;

        public string ServerPublicKey
        {
            get { return _ServerPublicKey; }
            set { _ServerPublicKey = value; }
        }
        string _ServerPrivateKey;

        public string ServerPrivateKey
        {
            get { return _ServerPrivateKey; }
            set { _ServerPrivateKey = value; }
        }

    }
}