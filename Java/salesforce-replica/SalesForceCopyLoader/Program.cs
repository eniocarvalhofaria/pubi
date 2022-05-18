using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Schema;
using System.Net;
using CommandLine;
using CommandLine.Parsing;
using CommandLine.Text;

namespace SalesForceCopyLoader
{
    class Program
    {
        private static string CONNECTION_STRING = @"Server={0};Database={1};Trusted_Connection=true;Pooling=false; Connection Timeout=120";
            private static string CONNECTION_STRING_REDSHIFT = @"Driver={Amazon Redshift (x64)}; Server=pu-dw-1.cphgrk2t7oss.us-east-1.redshift.amazonaws.com; Database=dw; DS=salesforce; UID={0}; PWD={1}; Port=5439";
 
        private static List<Loader> LoaderList = new List<Loader>();
        private static List<Loader> MergeList = new List<Loader>();
        private static OdbcConnectionFactory cnRedShift = null;
        private sealed class Options
        {
            [Option("address", Required = true, HelpText = "Target address")]
            public string Address { get; set; }

            [Option("database", Required = true, HelpText = "Target database name")]
            public string Database { get; set; }

            [Option("redshiftuid", Required = true, HelpText = "User Id RedShift")]
            public string RedShiftUID { get; set; }

            [Option("redshiftpwd", Required = true, HelpText = "Password RedShift")]
            public string RedShiftPWD { get; set; }

            [Option("s3accesskey", Required = true, HelpText = "Access Key S3")]
            public string S3AccessKey { get; set; }

            [Option("s3secretkey", Required = true, HelpText = "Secret Key S3")]
            public string S3SecretKey { get; set; }

            [Option("listenerdirectory", Required = true, HelpText = "Xml Directory Name")]
            public string ListenerDirectory { get; set; }

                [Option("target", Required = false, HelpText = "em qual database devemos carregar")]
            public string Target { get; set; }

            [Option("truncate", DefaultValue = false, HelpText = "Truncate target object before load?")]
            public bool TruncateTarget { get; set; }

            [Option("batchsize", DefaultValue = 10000, HelpText = "Batch size")]
            public int BatchSize { get; set; }

            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }
            public string GetTarget
            {
                get
                {
                    if (Target == null)
                    {
                        return "Both";
                    }
                    if (Target.ToLower().Equals("redshift"))
                    {
                        return "Redshift";
                    }
                    if (Target.ToLower().Contains("sql"))
                    {
                        return "Sqlserver";
                    }
                    return "Both";

                }
            }
            public string ConnectionString
            {
                get { return string.Format(CONNECTION_STRING, Address, Database); }
            }
            public string ConnectionStringRedShift
            {
                get
                {
                    //   return string.Format(CONNECTION_STRING_REDSHIFT, RedShiftUID, RedShiftPWD); 
                    return CONNECTION_STRING_REDSHIFT.Replace("{0}", RedShiftUID).Replace("{1}", RedShiftPWD);
                }
            }
        }

        static Options options = new Options();
        static void Main(string[] args)
        {

            try
            {
                Parser parser = new Parser
                (
                    delegate (ParserSettings settings)
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

            DirectoryListener dl = new DirectoryListener((options.ListenerDirectory.Trim() + "\\xml").Replace("\\\\", "\\"), "*.xml");
            string csvDirectory = (options.ListenerDirectory.Trim() + "\\csv\\").Replace("\\\\", "\\");
            if (!Directory.Exists(csvDirectory))
            {
                Directory.CreateDirectory(csvDirectory);
            }
            else
            {

                foreach (var csvfile in Directory.GetFiles(csvDirectory))
                {
                    File.Delete(csvfile);
                }
            }
            SqlServerConnectionFactory cn = null;
            if (!options.GetTarget.Equals("Redshift"))
            {
                cn = new SqlServerConnectionFactory(options.ConnectionString);
                cn.CloseOnRelease = true;
                cn.LimitSessions = 5;
            }

            if (!options.GetTarget.Equals("Sqlserver"))
            {
                cnRedShift = new OdbcConnectionFactory(options.ConnectionStringRedShift);
                cnRedShift.CloseOnRelease = true;
                cnRedShift.LimitSessions = 1;
            }
            int contLoop = 0;
            bool endFiles = false;

            while (true)
            {

                FileInfo file = dl.GetNextFile();
                if (file == null)
                {
                    if (endFiles)
                    {
                        Thread.Sleep(1000);
                        while (true)
                        {
                            bool IsRunning = false;
                            bool anyEnd = false;
                            string processes = "";
                            foreach (var l in LoaderList)
                            {
                                if (l.IsRunning)
                                {
                                    IsRunning = true;
                                    processes += ", " + l.FileInfo.Name;
                                }
                                else
                                {
                                    anyEnd = true;
                                }
                            }
                            if (anyEnd && IsRunning)
                            {
                                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Waiting " + processes.Substring(1) + " end the load.");
                            }

                            if (IsRunning)
                            {
                                Thread.Sleep(5000);
                            }
                            else
                            {
                                break;
                            }
                        }
                        foreach (var l in LoaderList)
                        {
                            if (l.ReturnCode != 0)
                            {
                                Environment.Exit(l.ReturnCode);
                            }
                        }
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " The merge process will begin.");
                        if (!options.GetTarget.Equals("Redshift"))
                        {
                            string dir = (options.ListenerDirectory + "\\merge").Replace("\\\\", "\\");
                            foreach (String f in Directory.GetFiles(dir, "*.sql"))
                            {
                                FileInfo fi = new FileInfo(f);
                                SqlServerLoader sl = new SqlServerLoader();
                                sl.ConnectionFactory = cn;
                                sl.Connection = cn.GetConnection();
                                sl.MergeDirectory = options.ListenerDirectory + "\\merge";
                                sl.DdlDirectory = options.ListenerDirectory + "\\ddl";
                                sl.MergeFileInfo = fi;
                                MergeList.Add(sl);
                                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Merge " + f + " started in SqlServer.");

                                new Thread(sl.MergeTable).Start();
                            }
                        }

                        if (!options.GetTarget.Equals("Sqlserver"))
                        {
                            string dir = (options.ListenerDirectory + "\\mergeRedshift").Replace("\\\\", "\\");
                            foreach (String f in Directory.GetFiles(dir, "*.sql"))
                            {
                                FileInfo fi = new FileInfo(f);
                                RedshiftLoader rl = new RedshiftLoader();
                                rl.S3AccessKey = options.S3AccessKey;
                                rl.S3SecretKey = options.S3SecretKey;
                                                        //             rl.RedshiftLoaderPath = options.RedShitLoaderPath;
                                //            rl.JavaPath = options.JavaPath;
                                rl.UserId = options.RedShiftUID;
                                rl.Password = options.RedShiftPWD;
                                rl.ConnectionFactory = cnRedShift;
                                rl.Connection = cnRedShift.GetConnection();
                                rl.CsvDirectory = options.ListenerDirectory + "\\csv";
                                rl.MergeDirectory = options.ListenerDirectory + "\\mergeRedshift";
                                rl.DdlDirectory = options.ListenerDirectory + "\\ddlRedshift";
                                rl.MergeFileInfo = fi;
                                MergeList.Add(rl);
                                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Merge " + f + " started in Redshift.");

                                new Thread(rl.MergeTable).Start();
                            }
                        }
                        Thread.Sleep(1000);
                        while (true)
                        {
                            bool IsRunning = false;
                            bool anyEnd = false;
                            string processes = "";
                            foreach (var l in MergeList)
                            {
                                if (l.IsRunning)
                                {
                                    IsRunning = true;
                                    processes += ", " + l.MergeFileInfo.Name;
                                }
                                else
                                {
                                    anyEnd = true;
                                }
                            }
                            if (anyEnd && IsRunning)
                            {
                                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Waiting " + processes.Substring(1) + " end the merge.");
                            }

                            if (IsRunning)
                            {
                                Thread.Sleep(5000);
                            }
                            else
                            {
                                break;
                            }
                        }
                        foreach (var l in MergeList)
                        {
                            if (l.ReturnCode != 0)
                            {
                                Environment.Exit(l.ReturnCode);
                            }
                        }
                        Environment.Exit(0);
                    }

                    if (contLoop % 10 == 0)
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Waiting for a XML file in directory.");
                    }
                    contLoop++;
                    Thread.Sleep(1000);
                }
                else
                {
                    contLoop = 0;
                    try
                    {
                        if (file.Name.Equals("End.xml"))
                        {
                            endFiles = true;
                        }
                        else
                        {
                            if (!options.GetTarget.Equals("Redshift"))
                            {
                                SqlServerLoader sl = new SqlServerLoader();

                                sl.ConnectionFactory = cn;
                                sl.Connection = cn.GetConnection();
                                sl.MergeDirectory = options.ListenerDirectory + "\\merge";
                                sl.DdlDirectory = options.ListenerDirectory + "\\ddl";
                                sl.FileInfo = file;
                                LoaderList.Add(sl);
                                new Thread(sl.Load).Start();

                            }

                            if (!options.GetTarget.Equals("Sqlserver"))
                            {
                                RedshiftLoader rl = new RedshiftLoader();
                                rl.S3AccessKey = options.S3AccessKey;
                                rl.S3SecretKey = options.S3SecretKey;
                                rl.UserId = options.RedShiftUID;
                                rl.Password = options.RedShiftPWD;
                                rl.ConnectionFactory = cnRedShift;
                                rl.Connection = cnRedShift.GetConnection();
                                rl.CsvDirectory = options.ListenerDirectory + "\\csv";
                                rl.MergeDirectory = options.ListenerDirectory + "\\mergeRedshift";
                                rl.DdlDirectory = options.ListenerDirectory + "\\ddlRedshift";
                                rl.FileInfo = file;
                                LoaderList.Add(rl);
                                new Thread(rl.Load).Start();
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("error: ");
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                        Environment.Exit(1);
                    }
                }
            }
        }

        /*
                private static string Escape(string value)
                {
                    return string.IsNullOrEmpty(value) ? "" : "\"" + value.Replace("\"", "\"\"") + "\"";
                }

        */

    }
}
