using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using CommandLine.Parsing;
using CommandLine.Text;
using System.IO;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.ETL
{

    class ScriptExecutor
    {
        private class Options
        {
            [Option("script", Required = true, HelpText = "Script name or script full or relative path.")]
            public string Script { get; set; }
            [Option("connection", Required = true, HelpText = "Connection Name. Valid options: REDSHIFT, REPORTS, DW, EMAILDELIVERY, PEIXEURBANO")]
            public string ConnectionName { get; set; }
            [Option("userid", Required = false, HelpText = "Database User Id")]
            public string UserId { get; set; }
            [Option("pwd", Required = false, HelpText = "User Pwd")]
            public string Password { get; set; }
            [Option("parameters", Required = false, HelpText = "Parametros para o script")]
            public string Parameters { get; set; }

            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }
        }
        private static Options options = new Options();

        public static BusinessIntelligence.Data.QueryExecutor GetQueryExecutor(string connectionName)
        {
            if (QueryExecutorList.ContainsKey(connectionName.ToUpper()))
            {
                return QueryExecutorList[connectionName.ToUpper()];
            }
            else {
                var ret = new BusinessIntelligence.Data.QueryExecutor(GetConnection(connectionName));

                foreach (var item in QueryExecutorList.Values)
                {
                    foreach (var p in item.Parameters)
                    {
                        ret.AddTextParameter(p.Key, p.Value);

                    }
                    break;
                }
                 QueryExecutorList.Add(connectionName.ToUpper(), ret);

                return ret;
            }

        }


        public static Dictionary<string, BusinessIntelligence.Data.QueryExecutor> QueryExecutorList = new Dictionary<string, Data.QueryExecutor>();
        public static System.Data.Common.DbConnection GetConnection(string connectionName)
        {
            switch (connectionName)
            {
                case "REDSHIFT":
                    {
                        if (!string.IsNullOrEmpty(options.UserId))
                        {
                            EnvironmentParameters.RedshiftUserId = options.UserId;
                            EnvironmentParameters.RedshiftPwd = options.Password;
                        }
                       return BusinessIntelligence.Data.Connections.GetNewConnection("REDSHIFT");
            
                    }
                default:
                    {
                        return BusinessIntelligence.Data.Connections.GetNewConnection(connectionName);
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " This connection name is not a valid option.");
                        Environment.Exit(3);
                        return null;
                    }
            }


        }
        static void Main(string[] args)
        {
            try
            {
                Parser parser = new Parser
                (
                    delegate(ParserSettings settings)
                    {
                        settings.CaseSensitive = true;
                        settings.HelpWriter = Console.Out;
                    }
                );

                if (!parser.ParseArguments(args, options))
                {
                    throw new ParserException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Environment.Exit(-1);
            }
            string scriptPath = null;
            string scriptContent = null;
            if (options.Script.Contains("\\"))
            {

                scriptPath = IOFunctions.GetFullPath(options.Script);
    
            }
            else
            {
                //   string thisDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(ScriptExecutor)).CodeBase).Replace("file:\\", "");
                string thisDirectory = IOFunctions.GetExecutableDirectory();

                scriptPath = IOFunctions.GetFullPath(thisDirectory, "..\\..\\..\\Scripts\\" + options.Script + ".txt");
            }
            FileInfo fi = new FileInfo(scriptPath);
            Directory.CreateDirectory(fi.DirectoryName + "\\log");
            Log.GetInstance().FileName = fi.DirectoryName + "\\log\\" + fi.Name + "." + DateTime.Now.ToString("yyyyMMdd.HHmmss") + ".log";

            if (!File.Exists(scriptPath))
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + scriptPath + " not found.");
                Environment.Exit(2);
            }
            else
            {
                scriptContent = new StreamReader(scriptPath,Encoding.Default).ReadToEnd();
            }
       
           
            var ex = GetQueryExecutor(options.ConnectionName.ToUpper());

       
            try
            {
       
                if (options.Parameters != null)
                {
                    foreach (string item in options.Parameters.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (item.Contains("="))
                        {
                            Script.AddParameter(item.Split('=')[0].ToLower(), item.Split('=')[1]);
                        }
                    }
                }
                var script = new Script(ex, scriptContent);
                script.Execute();
                foreach (var qex in ScriptExecutor.QueryExecutorList.Values)
                {
                    qex.Close();
                }
                Log.GetInstance().WriteLine("EXECUÇÃO TERMINADA. CÓDIGO DE RETORNO " + script.ReturnCode);
                if (script.ReturnCode != 0)
                {
                
                    Environment.Exit(script.ReturnCode);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Environment.Exit(2);
            }
         
        }
    }
}
