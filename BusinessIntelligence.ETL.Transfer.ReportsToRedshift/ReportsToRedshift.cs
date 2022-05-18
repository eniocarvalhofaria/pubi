using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Data;
using CommandLine;
using CommandLine.Parsing;
using CommandLine.Text;
using BusinessIntelligence.Util;
using System.IO;
namespace BusinessIntelligence.ETL.Transfer
{
    class RedshiftToReports
    {
        private sealed class Options
        {

            [Option("schema", Required = false, HelpText = "Schema que recebera os dados")]
            public string SchemaName { get; set; }
            [Option("table", Required = true, HelpText = "Tabela que recebera os dados")]
            public string TableName { get; set; }
            [Option("readcommand", Required = false, HelpText = "Comando de leitura no Redshift")]
            public string ReadCommand { get; set; }
            [Option("readcommandfile", Required = false, HelpText = "Arquivo com comando de leitura no Redshift")]
            public string ReadCommandFile { get; set; }

            [Option("redshiftuid", Required = false, HelpText = "User Id RedShift")]
            public string RedShiftUID { get; set; }
            [Option("redshiftpwd", Required = false, HelpText = "Password RedShift")]
            public string RedShiftPWD { get; set; }
            [Option("refreshprocedure", Required = false, HelpText = "Procedure para atualizar os dados")]
            public string RefreshProcedure { get; set; }
            [Option("cleartable", Required = false, HelpText = "Informa se a tabela deve ser limpa")]
            public bool ClearTable { get; set; }
            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }
        }
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
        static Options options = new Options();
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
                Log.GetInstance().WriteLine(e.Message);
                Environment.Exit(-1);
            }
            try
            {
                if (!string.IsNullOrEmpty(options.RedShiftUID))
                {
                    EnvironmentParameters.RedshiftUserId = options.RedShiftUID;
                    EnvironmentParameters.RedshiftPwd = options.RedShiftPWD;
                }
                string schemaName;
                if (string.IsNullOrEmpty(options.SchemaName))
                {
                    schemaName = "reports";
                }
                else
                {
                    schemaName = options.SchemaName;
                }
                string scriptPath;
                Log.GetInstance().FileName = GetFullPath(Environment.CurrentDirectory, "..\\..\\..\\Scripts\\log\\Load_" + schemaName + "." + options.TableName + "." + DateTime.Now.ToString("yyyyMMdd.HHmmss") + ".log" );

                if (!string.IsNullOrEmpty(options.ReadCommandFile))
                {
                    if (options.ReadCommandFile.Contains("\\"))
                    {

                        scriptPath = GetFullPath(Environment.CurrentDirectory, options.ReadCommandFile);
                        FileInfo fi = new FileInfo(scriptPath);
                        Directory.CreateDirectory(fi.DirectoryName + "\\log");
                        Log.GetInstance().FileName = fi.DirectoryName + "\\log\\" + fi.Name + "." + DateTime.Now.ToString("yyyyMMdd.HHmmss") + ".log";
                    }
                    else
                    {
                        string thisDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(RedshiftToReports)).CodeBase).Replace("file:\\", "");
                        scriptPath = GetFullPath(thisDirectory, "..\\..\\..\\Scripts\\" + options.ReadCommandFile);
                    }

                    if (!File.Exists(scriptPath))
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + scriptPath + " not found.");
                        Environment.Exit(2);
                    }
                    else
                    {
                        options.ReadCommand = new StreamReader(scriptPath, Encoding.Default).ReadToEnd();
                    }
                }
                var loader = new Data.Redshift.RedshiftLoader(Connections.GetNewConnection("REDSHIFT"), schemaName, options.TableName);


            //    var transfer = new DataTransfer(Connections.GetNewConnection(Database.REPORTS), options.ReadCommand, loader);
                var transfer = new DataTransfer(Connections.GetNewConnection("REPORTS"), options.ReadCommand, loader);
               
              
                transfer.Execute(options.ClearTable);
                if (!string.IsNullOrEmpty(options.RefreshProcedure))
                {
                    var qex = new QueryExecutor(loader.Connection);
                    qex.Execute(options.RefreshProcedure);
                }
            }
            catch (Exception ex)
            {
                Log.GetInstance().Write(ex);
                Environment.Exit(1);
            }
        }
    }
}
