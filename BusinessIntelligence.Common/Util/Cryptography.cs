using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
namespace BusinessIntelligence.Util
{
    public class Cryptography
    {
        /// <summary>     
        /// Vetor de bytes utilizados para a criptografia (Chave Externa)     
        /// </summary>     
        private static byte[] bIV =
    { 0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18,
        0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC };

        public static string Encrypt(string text)
        {

            var base64EncodedBytes = System.Convert.FromBase64String(Environment.GetEnvironmentVariable("KEY_C"));
            var key = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            return Encrypt(text, key);

        }
        public static string Encrypt(string text, string key)
        {
            try
            {
                if (key.Length != 16)
                {
                    key = (key + "0123456789ABCDEFabcdefghijklmnop").Substring(0, 32);
                }
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(key);
                string cryptoKey = System.Convert.ToBase64String(plainTextBytes);


                // Se a string não está vazia, executa a criptografia
                if (!string.IsNullOrEmpty(text))
                {
                    // Cria instancias de vetores de bytes com as chaves                
                    byte[] bKey = Convert.FromBase64String(cryptoKey);
                    byte[] bText = new UTF8Encoding().GetBytes(text);

                    // Instancia a classe de criptografia Rijndael
                    Rijndael rijndael = new RijndaelManaged();

                    // Define o tamanho da chave "256 = 8 * 32"                
                    // Lembre-se: chaves possíves:                
                    // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
                    rijndael.KeySize = 256;

                    // Cria o espaço de memória para guardar o valor criptografado:                
                    MemoryStream mStream = new MemoryStream();
                    // Instancia o encriptador                 
                    CryptoStream encryptor = new CryptoStream(
                        mStream,
                        rijndael.CreateEncryptor(bKey, bIV),
                        CryptoStreamMode.Write);

                    // Faz a escrita dos dados criptografados no espaço de memória
                    encryptor.Write(bText, 0, bText.Length);
                    // Despeja toda a memória.                
                    encryptor.FlushFinalBlock();
                    // Pega o vetor de bytes da memória e gera a string criptografada                
                    return Convert.ToBase64String(mStream.ToArray());
                }
                else
                {
                    // Se a string for vazia retorna nulo                
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Se algum erro ocorrer, dispara a exceção            
                throw new ApplicationException("Erro ao criptografar", ex);
            }
        }

        public static void SetKey(string key)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(key);
            string cryptoKey = System.Convert.ToBase64String(plainTextBytes);
            Environment.SetEnvironmentVariable("KEY_C", cryptoKey);
        }
        public static void CreateKey()
        {
            string key = null;
            Console.WriteLine("Digite a chave para a Criptografia:");
            Console.ForegroundColor = ConsoleColor.Black;
            key = Console.ReadLine();
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(key);
            string cryptoKey = System.Convert.ToBase64String(plainTextBytes);
            Environment.SetEnvironmentVariable("KEY_C", cryptoKey);
            Console.ForegroundColor = ConsoleColor.Gray;
            var p = new Parameter("key");
            p.Value = key;
            p.IsEncrypted = true;
            p.Store();
        }

        public static bool CheckKey(string key)
        {
            if (Environment.GetEnvironmentVariable("KEY_C") == null)
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(key);
                string cryptoKey = System.Convert.ToBase64String(plainTextBytes);
                Environment.SetEnvironmentVariable("KEY_C", cryptoKey);
                Environment.SetEnvironmentVariable("KEY", key);
            }
            if (key == Parameter.GetParameter("key").Value)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private static bool _KeyIsChecked;

        public static bool KeyIsChecked
        {
            get { return _KeyIsChecked; }
        }
        public static void CheckKey()
        {


            while (true)
            {
                string key = Environment.GetEnvironmentVariable("KEY");
                string keyc = Environment.GetEnvironmentVariable("KEY_C");
                bool isEncrypted;
                string encryptedKey = Parameter.GetStoredValue("key", out isEncrypted);
                if (string.IsNullOrEmpty(keyc))
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        Console.WriteLine("Digite a chave para a descriptografia:");
                        Console.ForegroundColor = ConsoleColor.Black;
                        key = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(key);
                    keyc = System.Convert.ToBase64String(plainTextBytes);
                    Environment.SetEnvironmentVariable("KEY_C", keyc);
                }
                if (encryptedKey == Encrypt(key, key))
                {
                    Environment.SetEnvironmentVariable("KEY", "");
                    _KeyIsChecked = true;
                    break;
                }
                else
                {
                    Log.GetInstance().WriteLine("Chave de descriptografia incorreta.");
                    Environment.SetEnvironmentVariable("KEY_C", "");
                    Environment.SetEnvironmentVariable("KEY", "");
                }
            }
        }
        public static bool TryDecrypt(string encryptedValue, out string decryptedValue, string key)
        {
            try
            {
                decryptedValue = Decrypt(encryptedValue, key);
                return true;
            }
            catch (Exception ex)
            {
                decryptedValue = null;
                return false;
            }

        }
        public static string Decrypt(string text)
        {
            if (!KeyIsChecked)
            {
                CheckKey();
            }
            var base64EncodedBytes = System.Convert.FromBase64String(Environment.GetEnvironmentVariable("KEY_C"));
            var key = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            return Decrypt(text, key);

        }

        public static string Decrypt(string text, string key)
        {
            try
            {
                if (key.Length != 32)
                {
                    key = (key + "0123456789ABCDEFabcdefghijklmnop").Substring(0, 32);
                }
                // Se a string não está vazia, executa a criptografia           
                if (!string.IsNullOrEmpty(text))
                {
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(key);
                    string cryptoKey = System.Convert.ToBase64String(plainTextBytes);

                    // Cria instancias de vetores de bytes com as chaves                
                    byte[] bKey = Convert.FromBase64String(cryptoKey);
                    byte[] bText = Convert.FromBase64String(text);

                    // Instancia a classe de criptografia Rijndael                
                    Rijndael rijndael = new RijndaelManaged();

                    // Define o tamanho da chave "256 = 8 * 32"                
                    // Lembre-se: chaves possíves:                
                    // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
                    rijndael.KeySize = 256;

                    // Cria o espaço de memória para guardar o valor DEScriptografado:               
                    MemoryStream mStream = new MemoryStream();

                    // Instancia o Decriptador                 
                    CryptoStream decryptor = new CryptoStream(
                        mStream,
                        rijndael.CreateDecryptor(bKey, bIV),
                        CryptoStreamMode.Write);

                    // Faz a escrita dos dados criptografados no espaço de memória   
                    decryptor.Write(bText, 0, bText.Length);
                    // Despeja toda a memória.                
                    decryptor.FlushFinalBlock();
                    // Instancia a classe de codificação para que a string venha de forma correta         
                    UTF8Encoding utf8 = new UTF8Encoding();
                    // Com o vetor de bytes da memória, gera a string descritografada em UTF8       
                    return utf8.GetString(mStream.ToArray());
                }
                else
                {
                    // Se a string for vazia retorna nulo                
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Se algum erro ocorrer, dispara a exceção            
                throw new ApplicationException("Erro ao descriptografar", ex);
            }
        }
    }
}

