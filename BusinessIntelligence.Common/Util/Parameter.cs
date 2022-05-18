using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.Util
{
    public class Parameter
    {
        public Parameter(string name)
        {
            _Name = name.ToLower();
            if (name != "key")
            {
                allParameters.Add(name.ToLower(), this);
            }
        }
        static private Dictionary<string, Parameter> allParameters = new Dictionary<string, Parameter>();

        string _Name;
        public string Name
        {
            get { return _Name; }
        }
        string _EncryptedValue;

        public string EncryptedValue
        {
            get { return _EncryptedValue; }
            set { _EncryptedValue = value; }
        }
        string _Value;

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        bool _IsEncrypted;

        public bool IsEncrypted
        {
            get { return _IsEncrypted; }
            set { _IsEncrypted = value; }
        }

        public void Store()
        {
            string thisDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(Parameter)).CodeBase).Replace("file:\\", "");
            string parametersDirectory = (GetLocalParameterDirectory() != null) ? GetLocalParameterDirectory() : GetGlobalParameterDirectory();
            string fileName;
            fileName = parametersDirectory + "\\" + Name + ".par";


            using (var sw = new StreamWriter(fileName))
            {
                if (IsEncrypted)
                {
                    sw.Write("1" + Cryptography.Encrypt(Value));
                }
                else
                {
                    sw.Write("0" + Value);
                }

                sw.Close();
                sw.Dispose();
            }

        }

        public static string GetCommandLineParameter(string name)
        {
            string[] p = Environment.GetCommandLineArgs();
            for (int i = 1; i < p.Length; i += 2)
            {
                if (p[i] == "--" + name)
                {
                    if (i + 1 < p.Length)
                    {
                        return p[i + 1];
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            return null;
        }
        public static string GetMainFullFileName(string filename)
        {
            var local = GetLocalParameterDirectory() + "\\" + filename;
            var global = GetGlobalParameterDirectory() + "\\" + filename;
            if (File.Exists(local))
            {
                return local;
            }
            else if (File.Exists(global))
            {
                return global;
            }
            else
            {
                return null;
            }

        }
        public static string GetStoredValue(string name, out bool isEncrypted)
        {
            string thisDirectory = IOFunctions.GetExecutableDirectory();
            string content = null;
            var fullFileName = GetMainFullFileName(name + ".par");

            if (fullFileName != null)
            {
                using (var sr = new StreamReader(fullFileName))
                {
                    content = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                }
                if (content.Length > 1)
                {
                    if (content.Substring(0, 1) == "1")
                    {
                        isEncrypted = true;
                    }
                    else
                    {
                        isEncrypted = false;
                    }
                    return content.Substring(1);
                }
            }
            isEncrypted = false;
            return null;
        }
        static string localParametersDirectory = null;
        static bool localParametersDirectoryChecked = false;
        public static string GetLocalParameterDirectory()
        {
            if (!localParametersDirectoryChecked)
            {
                string thisDirectory = IOFunctions.GetExecutableDirectory();
                if (Directory.Exists(thisDirectory + "\\" + "Parameters"))
                {
                    localParametersDirectory = thisDirectory + "\\" + "Parameters";
                }
            }
            return localParametersDirectory;
        }
        static string globalParametersDirectory = null;
        static bool globalParametersDirectoryChecked = false;
        public static string GetGlobalParameterDirectory()
        {
            if (!globalParametersDirectoryChecked)
            {
                string thisDirectory = IOFunctions.GetExecutableDirectory();
                string parent = "..\\";
                string testDirectory = "..\\Parameters";
                int i = 0;
                while (i < 3)
                {
                    if (Directory.Exists(IOFunctions.GetFullPath(thisDirectory,testDirectory)))
                    {
                        globalParametersDirectory = IOFunctions.GetFullPath(thisDirectory,testDirectory);
                        return globalParametersDirectory;

                    }

                    testDirectory = parent + testDirectory;
                    i++;
                }

            }
            return globalParametersDirectory;
        }
        public static string ReplaceParameters(string text)
        {
            string oldText = text;
            string newText = text;
            var hasmore = true;
            int nextIndex = 1;
            int begin = 0;
            int end = 0;
            while (hasmore)
            {
                hasmore = false;
                oldText = newText;
                string param = null;
                begin = oldText.IndexOf("{", nextIndex - 1);
                if (begin > -1)
                {
                    end = oldText.IndexOf("}", begin);
                    if (end > -1)
                    {
                        param = oldText.Substring(begin + 1, end - begin - 1);
                        var paramvalue = Parameter.GetParameter(param).Value;
                        if (!string.IsNullOrEmpty(paramvalue))
                        {
                            newText = oldText.Replace("{" + param + "}", paramvalue);
                            nextIndex = newText.IndexOf("{", begin);
                        }
                        else
                        {
                            newText = oldText;
                            nextIndex = newText.IndexOf("{", end);
                        }

                        hasmore = nextIndex > -1;
                    }
                }

            }
            return newText;
        }
        public static Parameter GetParameter(string name)
        {
            if (allParameters.ContainsKey(name.ToLower()))
            {
                return allParameters[name.ToLower()];
            }
            Parameter ret = new Parameter(name);
            string cl = GetCommandLineParameter(name);
            if (!string.IsNullOrEmpty(cl))
            {
                ret.Value = cl;
                return ret;
            }
            bool isEncrypted;
            var storedValue = GetStoredValue(name, out isEncrypted);
            ret.IsEncrypted = isEncrypted;


            if (ret.IsEncrypted)
            {
                try
                {
                    ret.EncryptedValue = storedValue;
                    ret.Value = Cryptography.Decrypt(storedValue);
                    return ret;
                }
                catch (Exception ex)
                {
                    Log.GetInstance().WriteLine("Não foi possível descriptografar o parâmetro: " + name);
                }

            }
            else
            {
                ret.Value = storedValue;
                return ret;
            }


            if (name != "key")
            {
                Console.WriteLine("Digite o valor para a variável: " + name);
                Console.ForegroundColor = ConsoleColor.Black;
                ret.Value = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            return ret;
        }
        public override string ToString()
        {
            return Value;
        }
    }
}
