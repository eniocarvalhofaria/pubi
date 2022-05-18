using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BusinessIntelligence.Data;
using CommandLine;
using CommandLine.Parsing;
using CommandLine.Text;
using System.IO;
using OpenPop.Mime;
using OpenPop.Mime.Decode;
using OpenPop.Mime.Header;
using OpenPop.Pop3;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.ETL.Email.ReturnPathReport
{
    class Program:BusinessIntelligence.ETL.CommonETL
    {

        private sealed class Options
        {

            [Option("redshiftuid", Required = false, HelpText = "User Id RedShift")]
            public string RedShiftUID { get; set; }

            [Option("redshiftpwd", Required = false, HelpText = "Password RedShift")]
            public string RedShiftPWD { get; set; }

            [Option("s3accesskey", Required = false, HelpText = "Access Key S3")]
            public string S3AccessKey { get; set; }

            [Option("s3secretkey", Required = false, HelpText = "Secret Key S3")]
            public string S3SecretKey { get; set; }

            [Option("filesdirectory", Required = true, HelpText = "Directory of the files")]
            public string FilesDirectory { get; set; }

            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }
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
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");



          
            if (!string.IsNullOrEmpty(options.RedShiftUID))
            {
                EnvironmentParameters.RedshiftUserId = options.RedShiftUID;
                EnvironmentParameters.RedshiftPwd = options.RedShiftPWD;
            }
            System.Data.Common.DbConnection cn = Connections.GetNewConnection(Database.REDSHIFT);
            var loader = new BusinessIntelligence.Data.Redshift.RedshiftLoader(cn, "reports", "returnpathreportstage");
            var dt = loader.GetNewDataTable();

            var columnsIndex = new List<int>();
            var tableColumnsIndex = new Dictionary<string, int>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                tableColumnsIndex.Add(dt.Columns[i].ColumnName, i);
                columnsIndex.Add(-1);
            }

            var executor = new QueryExecutor(cn);
            executor.Execute("delete from reports.returnpathreportstage");

            foreach (var fileName in Directory.GetFiles(GetFullPath(Environment.CurrentDirectory,options.FilesDirectory), "*.csv"))
            {
                Console.WriteLine("Reading " + fileName);
                DateTime fileDate;

                DateTime.TryParse(fileName.Substring(fileName.Length - 12, 4) + "-" + fileName.Substring(fileName.Length - 8, 2) + "-" + fileName.Substring(fileName.Length - 6, 2), out fileDate);

                columnsIndex.Clear();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    columnsIndex.Add(-1);
                }

                using (var sr = new System.IO.StreamReader(fileName))
                {
                    bool isFirst = true;
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Replace("\"", "").Replace("!", "").Split(',');
                        if (line.Length > 5)
                        {
                            if (isFirst)
                            {
                                int columnIndexInFile = -1;
                                foreach (var item in line)
                                {
                                    columnIndexInFile++;
                                    string columnNameInFile = item.ToLower().Replace(" ", "");
                                    int columnIndexInTable = -1;
                                    if (tableColumnsIndex.TryGetValue(columnNameInFile, out columnIndexInTable))
                                    {
                                        columnsIndex[columnIndexInTable] = columnIndexInFile;
                                    }
                                }
                                isFirst = false;
                            }
                            else
                            {
                                var row = dt.NewRow();

                                for (int i = 0; i < columnsIndex.Count; i++)
                                {

                                    if (columnsIndex[i] > -1 && !string.IsNullOrEmpty(line[columnsIndex[i]]))
                                    {
                                        if (dt.Columns[i].DataType.ToString().Equals("System.String"))
                                        {
                                            row[i] = line[columnsIndex[i]];
                                        }
                                        else if (dt.Columns[i].DataType.ToString().Equals("System.Decimal"))
                                        {
                                            if (line[columnsIndex[i]].IndexOf("%") > -1)
                                            {
                                                row[i] = Convert.ToDecimal(line[columnsIndex[i]].Replace("%", "")) / 100;
                                            }
                                            else if (line[columnsIndex[i]].Length > 1 && line[columnsIndex[i]].Substring(0, 1) == ".")
                                            {
                                                row[i] = Convert.ToDecimal("0" + line[columnsIndex[i]]);
                                            }
                                            else
                                            {
                                                row[i] = Convert.ToDecimal(line[columnsIndex[i]]);
                                            }
                                        }
                                        else if (dt.Columns[i].DataType.ToString().Equals("System.Int32"))
                                        {
                                            row[i] = Convert.ToInt32(line[columnsIndex[i]]);
                                        }
                                        else if (dt.Columns[i].DataType.ToString().Equals("System.DateTime"))
                                        {
                                            DateTime date;
                                            DateTime.TryParse(line[columnsIndex[i]].Substring(6, 4) + "-" + line[columnsIndex[i]].Substring(0, 2) + "-" + line[columnsIndex[i]].Substring(3, 2), out date);

                                            row[i] = date;
                                        }
                                    }

                                }
                                row[row.ItemArray.Length - 1] = fileDate;
                                dt.Rows.Add(row);
                            }

                        }
                    }
                    sr.Close();
                    sr.Dispose();
                }

            }
            if (dt.Rows.Count > 0)
            {
                loader.Load(dt);

                executor.Execute(".RefreshReturnPathReport.txt");

            }
            Directory.CreateDirectory(GetFullPath(Environment.CurrentDirectory, options.FilesDirectory) + "\\loaded");
            foreach (var fileName in Directory.GetFiles(GetFullPath(Environment.CurrentDirectory, options.FilesDirectory), "*.csv"))
            {
                string loadedFile = fileName.Replace(options.FilesDirectory, options.FilesDirectory + "\\loaded");
                if (File.Exists(loadedFile))
                {
                    File.Delete(loadedFile);
                }
                File.Move(fileName, loadedFile);
            }
            var cnS = BusinessIntelligence.Data.Connections.GetNewConnection(BusinessIntelligence.Data.Database.REPORTS);
            var ex = new BusinessIntelligence.Data.QueryExecutor(cnS);
            ex.Execute("exec msdb..sp_start_job 'Carregas tabelas do Redshift'");

        }
    }
}
