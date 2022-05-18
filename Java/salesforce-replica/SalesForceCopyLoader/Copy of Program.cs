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
    class ProgramBackup
    {
        private static string CONNECTION_STRING = @"Server={0};Database={1};Trusted_Connection=true";
        private static string CONNECTION_STRING_REDSHIFT = @"Driver={PostgreSQL Unicode(x64)}; Server=pu-dw-1.cphgrk2t7oss.us-east-1.redshift.amazonaws.com; Database=dw; DS=salesforce; UID={0}; PWD={1}; Port=5439";

        private static int DATA_TABLE_MAX_ROWS = 10000;
        private static int XML_BUFFER_SIZE = 100 * 1024 * 1024;
        private static System.Data.Odbc.OdbcConnection cnRedShift = null;
        private static DataTable schemaDefRedShift = null;
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

            [Option("s3path", Required = true, HelpText = "Path S3 for windows")]
            public string S3Path { get; set; }

            [Option("javapath", Required = true, HelpText = "Path JRE")]
            public string JavaPath { get; set; }

            [Option("target", Required = false, HelpText = "em qual database devemos carregar")]
            public string Target { get; set; }

            [Option("redshiftloaderpath", Required = true, HelpText = "Path .jar loader ")]
            public string RedShitLoaderPath { get; set; }

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
        static void Main2(string[] args)
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

            DirectoryListener dl = new DirectoryListener((options.ListenerDirectory.Trim() + "\\xml").Replace("\\\\", "\\"), "*.xml");
            string csvDirectory = (options.ListenerDirectory.Trim() + "\\csv\\").Replace("\\\\", "\\");
            foreach (var csvfile in Directory.GetFiles(csvDirectory))
            {
                File.Delete(csvfile);
            }
            SqlConnection cn = null;
            if (options.GetTarget.Equals("Redshift"))
            {
                //          DATA_TABLE_MAX_ROWS = int.MaxValue;
            }
            if (!options.GetTarget.Equals("Redshift"))
            {
                cn = new SqlConnection(options.ConnectionString);
                try
                {
                    cn.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (!options.GetTarget.Equals("Sqlserver"))
            {
                cnRedShift = new System.Data.Odbc.OdbcConnection(options.ConnectionStringRedShift);
                try
                {
                    cnRedShift.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
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
                        if (!options.GetTarget.Equals("Redshift"))
                        {
                            MergeTables(cn);
                        }

                        if (!options.GetTarget.Equals("Sqlserver"))
                        {
                            MergeTables(cnRedShift);
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
                            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " File " + file.Name + " will be loaded.");
                            bool errorInLoad = false;
                            //    List<string> truncatedTables = new List<string>();

                            string tableName = ConvertKey2TableName(file.Name);
                            string redShiftTableName = "salesforce." + tableName.Replace("[", "").Replace("]", "").Replace(".", "");
                            //      string csvFileName = file.Name.Replace(".xml", "." + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv");
                            string csvFileName = file.Name.Split('.')[0] + file.Name.Split('.')[1] + ".csv";
                            string stageTableName = tableName.Replace("[cf].", "[cfstage].").Replace("[ct].", "[ctstage].").Replace("[sf].", "[sfstage].");
                            string redShiftStageTableName = "salesforce." + stageTableName.Replace("[", "").Replace("]", "").Replace(".", "");


                            FileInfo ddlFileInfo = new FileInfo(file.FullName.Substring(0, file.FullName.Length - 7).Replace("\\xml\\", "\\ddl\\") + ".ddl.sql");
                            FileInfo ddlFileInfoRedShift = new FileInfo(file.FullName.Substring(0, file.FullName.Length - 7).Replace("\\xml\\", "\\ddlRedShift\\") + ".ddl.sql");
                            DataTable schemaDef = null;

                            if (!options.GetTarget.Equals("Redshift"))
                            {
                                CreateTable(cn, tableName, stageTableName, file, ddlFileInfo);
                                schemaDef = GetSchema(cn, stageTableName);
                            }

                            if (!options.GetTarget.Equals("Sqlserver"))
                            {
                                CreateTable(cnRedShift, tableName.Replace("[", "").Replace("]", "").Replace(".", ""), stageTableName.Replace("[", "").Replace("]", "").Replace(".", ""), file, ddlFileInfoRedShift);
                                schemaDefRedShift = GetSchema(cnRedShift, redShiftStageTableName);
                            }






                            // READ XML, DESERIALIZE AND LOAD ON DATABASE

                            int rw_cnt = 0;

                            DataTable errorDataTable;
                            try
                            {
                                XmlReaderSettings xrs = new XmlReaderSettings();
                                xrs.IgnoreWhitespace = true;
                                xrs.IgnoreComments = true;
                                xrs.ValidationFlags = XmlSchemaValidationFlags.None;
                                xrs.ValidationType = ValidationType.None;

                                xrs.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(
                                    (object sender, System.Xml.Schema.ValidationEventArgs e) =>
                                    {
                                        Console.WriteLine("\n{0}", e.Message);
                                    }
                                );


                                using (XmlReader xr = new XmlTextReader(file.FullName))
                                {
                                    Xml2DataTable(xr, (schemaDef == null) ? schemaDefRedShift : schemaDef, out errorDataTable, (DataTable dt) =>
                 {
                     try
                     {

                         if (!options.GetTarget.Equals("Redshift"))
                         {
                             using (SqlBulkCopy bc = new SqlBulkCopy(cn))
                             {

                                 bc.DestinationTableName = stageTableName;
                                 bc.BatchSize = options.BatchSize;
                                 bc.BulkCopyTimeout = 0;
                                 Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + string.Format(" Loading {0} rows in {1}.", dt.Rows.Count.ToString(), bc.DestinationTableName));
                                 bc.WriteToServer(dt);
                             }

                         }
                         if (!options.GetTarget.Equals("Sqlserver"))
                         {
                             Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Generating csv file " + csvDirectory + csvFileName);
                             DataTableToCsv(dt, csvDirectory + csvFileName, false, schemaDefRedShift);
                         }
                         rw_cnt += dt.Rows.Count;
                     }
                     catch (Exception e)
                     {
                         errorInLoad = true;
                         Console.WriteLine(" ({0}) ", e.Message);
                         StringBuilder sb = new StringBuilder();
                         foreach (DataRow r in dt.Rows)
                         {
                             for (int i = 0; i < dt.Columns.Count; i++)
                             {
                                 if (r[i] != DBNull.Value)
                                 {
                                     sb.Append(string.Format("{0} = {1}", dt.Columns[i].ColumnName, r[i].ToString()) + "\r\n");
                                 }
                             }

                         }
                         //                   Console.WriteLine(sb.ToString());
                         Console.WriteLine(e.StackTrace);
                         Environment.Exit(1);

                     }

                 });
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(" error: {0}", e.Message);
                                Console.WriteLine(e.StackTrace);
                                return;
                            }

                            if (!options.GetTarget.Equals("Redshift") && errorDataTable != null && errorDataTable.Rows.Count > 0)
                            {
                                string errorTableName = (stageTableName + "_error]").Replace("]_", "_");
                                StringBuilder createText = new StringBuilder();
                                createText.Append(string.Format("IF object_id(N'{0}') IS NOT NULL \r\n", errorTableName));
                                createText.Append("begin\r\n");
                                createText.Append(string.Format("drop table {0} \r\n", errorTableName));
                                createText.Append("end\r\n");
                                createText.Append(string.Format("create table {0} \r\n", errorTableName));
                                createText.Append(string.Format("( Id varchar(100) \r\n"));
                                createText.Append(string.Format(", ColumnName varchar(100) \r\n"));
                                createText.Append(string.Format(", Content varchar(8000) \r\n"));
                                createText.Append(string.Format(", ErrorMessage varchar(8000) \r\n"));
                                createText.Append(")");
                                SqlCommand cmd = cn.CreateCommand();
                                cmd.CommandTimeout = 0;
                                cmd.CommandText = createText.ToString();
                                cmd.ExecuteNonQuery();
                                using (SqlBulkCopy bc = new SqlBulkCopy(cn))
                                {

                                    bc.DestinationTableName = errorTableName;
                                    bc.BatchSize = options.BatchSize;
                                    bc.BulkCopyTimeout = 0;
                                    bc.WriteToServer(errorDataTable);
                                }
                                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Error content loaded in " + errorTableName + ".");

                            }
                            if (!errorInLoad)
                            {
                                if (!options.GetTarget.Equals("Redshift"))
                                {
                                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Table " + stageTableName + " loaded.");
                                }
                                if (file.Name.IndexOf(".LA.xml") > -1 || file.Name.IndexOf(".UN.xml") > -1)
                                {
                                    if (!options.GetTarget.Equals("Sqlserver") && rw_cnt > 0)
                                    {
                                        string bucket = @"pu-redshift-workarea/input/subject=salesforce/data/object=" + tableName.Replace("[", "").Replace("]", "").Replace(".", "") + "/";
                                        if (!loadToRedShift(bucket, csvDirectory, csvFileName, redShiftStageTableName))
                                        {
                                            Environment.Exit(5);
                                        }
                                    }
                                    if (!File.Exists((options.ListenerDirectory.Trim() + "\\merge").Replace("\\\\", "\\") + "\\" + tableName + ".merge.xml"))
                                    {
                                        if (!options.GetTarget.Equals("Redshift"))
                                        {
                                            UpsertTable(cn, tableName, stageTableName, schemaDef);
                                        }
                                        if (!options.GetTarget.Equals("Sqlserver"))
                                        {
                                            UpsertTable(cnRedShift, redShiftTableName, redShiftStageTableName, schemaDefRedShift);
                                        }
                                    }
                                }
                                File.Move(file.FullName, file.FullName.Replace("xml\\", "loaded\\"));
                                //    File.Delete(file.FullName);
                            }
                            else
                            {
                                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Table " + stageTableName + " not loaded correctly.");
                                Environment.Exit(1);
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

        private static string ConvertKey2TableName(string objectKey)
        {
            objectKey = objectKey.Substring(objectKey.LastIndexOf('/') + 1);

            string[] parts = objectKey.Split('.');

            if (parts.Length >= 2)
            {
                return EncodeTableName(parts[0] + '.' + parts[1]);
            }

            return "";
        }

        private static string EncodeTableName(string tableName)
        {
            string[] splitted = tableName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < splitted.Length; i++)
            {
                splitted[i] = "[" + splitted[i].Trim(' ', '[', ']') + "]";
            }

            return string.Join(".", splitted);
        }

        private static string Escape(string value)
        {
            return string.IsNullOrEmpty(value) ? "" : "\"" + value.Replace("\"", "\"\"") + "\"";
        }


        private static void ExecuteQuery(Options options, Action<SqlConnection, SqlCommand> TransactionBlock)
        {
            using (SqlConnection cn = new SqlConnection(options.ConnectionString))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    try
                    {
                        TransactionBlock(cn, cmd);
                    }
                    catch (Exception)
                    {
                        throw;
                    }


                }


                cn.Close();
            }
        }
        private static void Xml2DataTable(XmlReader xr, DataTable schemaDef, out DataTable errorDataTable, Action<DataTable> onDataTableClose)
        {
            var numberFormatInfo = new System.Globalization.NumberFormatInfo();
            numberFormatInfo.NumberDecimalSeparator = ".";

            errorDataTable = new DataTable();
            errorDataTable.Columns.Add("Id", "".GetType());
            errorDataTable.Columns.Add("ColumnName", "".GetType());
            errorDataTable.Columns.Add("Content", "".GetType());
            errorDataTable.Columns.Add("ErrorMessage", "".GetType());

            Dictionary<string, string> keyPair = null;

            DataTable dataTable = null;

            Stack<string> hierarchy = new Stack<string>();
            int totalRows = 0;
            while (xr.Read())
            {
                switch (xr.NodeType)
                {
                    case XmlNodeType.Element:
                        hierarchy.Push(xr.Name);

                        if ("records".Equals(xr.Name))
                        {
                            keyPair = new Dictionary<string, string>();
                        }
                        break;

                    case XmlNodeType.Text:
                        if (keyPair != null)
                        {
                            string columnName = Regex.Replace(hierarchy.Peek(), "__c$", "");
                            keyPair[columnName] = System.Net.WebUtility.HtmlDecode(xr.Value);
                        }
                        break;

                    case XmlNodeType.EndElement:
                        hierarchy.Pop();

                        if ("records".Equals(xr.Name))
                        {
                            if (dataTable == null)
                            {
                                dataTable = new DataTable();

                                foreach (DataRow schemaRow in schemaDef.Rows)
                                {
                                    if (schemaRow["DataType"].ToString().Equals("System.Boolean"))
                                    {
                                        dataTable.Columns.Add(schemaRow["ColumnName"].ToString(), new Int16().GetType());
                                    }
                                    else
                                    {
                                        dataTable.Columns.Add(schemaRow["ColumnName"].ToString(), (Type)schemaRow["DataType"]);
                                    }

                                }
                            }

                            DataRow dr = dataTable.NewRow();
                            try
                            {
                                foreach (DataColumn dc in dataTable.Columns)
                                {
                                    string v = null;

                                    if (keyPair.TryGetValue(dc.ColumnName, out v))
                                    {
                                        try
                                        {

                                            switch (dc.DataType.ToString())
                                            {
                                                case "System.DateTime":
                                                    dr.SetField(dc, DateTime.Parse(v.Replace("T", " ").Replace("Z", "")));
                                                    break;
                                                case "System.Decimal":
                                                    if (v.IndexOf("E") > -1)
                                                    {
                                                        throw (new Exception("number very big"));
                                                        //           dr.SetField(dc, Decimal.Parse(v, System.Globalization.NumberStyles.Float, numberFormatInfo));
                                                    }
                                                    else if (Decimal.Parse(v, System.Globalization.NumberStyles.Float, numberFormatInfo) > 100000000)
                                                    {
                                                        throw (new Exception("number very big"));
                                                    }
                                                    else
                                                    {
                                                        dr.SetField(dc, v);
                                                    }
                                                    break;
                                                case "System.Int16":
                                                    if (v.Equals("true"))
                                                    {
                                                        dr.SetField(dc, 1);
                                                    }
                                                    else if (v.Equals("false"))
                                                    {
                                                        dr.SetField(dc, 0);
                                                    }
                                                    else
                                                    {
                                                        dr.SetField(dc, v);
                                                    }

                                                    break;
                                                default:
                                                    dr.SetField(dc, v);
                                                    break;
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            DataRow r = errorDataTable.NewRow();
                                            r.SetField("Id", dr["Id"]);
                                            r.SetField("ColumnName", dc.ColumnName);
                                            r.SetField("Content", v.ToString());
                                            r.SetField("ErrorMessage", e.Message);
                                            errorDataTable.Rows.Add(r);
                                            dr.SetField(dc, DBNull.Value);
                                        }

                                    }
                                    else
                                    {
                                        dr.SetField(dc, DBNull.Value);
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            totalRows++;

                            dataTable.Rows.Add(dr);
                            if (dataTable.Rows.Count >= DATA_TABLE_MAX_ROWS)
                            {
                                onDataTableClose(dataTable);
                                dataTable.Dispose();
                                dataTable = null;
                            }

                            keyPair = null;
                        }
                        break;

                    default:
                        break;
                }
            }

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                onDataTableClose(dataTable);
                dataTable.Dispose();
                dataTable = null;
            }
        }
        static Dictionary<string, DataTable> schemas = new Dictionary<string, DataTable>();
        static DataTable GetSchema(System.Data.Common.DbConnection cn, string tableName)
        {
            if (schemas.ContainsKey(tableName))
            {
                return schemas[tableName];
            }
            DataTable schemaDef;
            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
            var cmd = cn.CreateCommand();
            cmd.CommandTimeout = 0;
            cmd.CommandText = string.Format("SELECT TOP 0 * FROM {0}", tableName);
            try
            {
                cmd.ExecuteNonQuery();
                using (var dr = cmd.ExecuteReader())
                {
                    schemaDef = dr.GetSchemaTable();
                }
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Schema from " + tableName + " getted.");
                schemas.Add(tableName, schemaDef);
                return schemaDef;
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Table " + tableName + " does not exists.");
                return null;
            }
        }
        static List<string> stagesCreated = new List<string>();
        static void CreateTable(System.Data.Common.DbConnection cn, string tableName, string stageTableName, FileInfo file, FileInfo ddlFile)
        {
            if (file.Name.IndexOf(".00.xml") > -1 || file.Name.IndexOf(".UN.xml") > -1)
            {
                if (cn.ConnectionString.Contains("redshift"))
                {
                    string bucket = @"pu-redshift-workarea/input/subject=salesforce/data/object=" + tableName.Replace("[", "").Replace("]", "").Replace(".", "") + "/";
                }
                string createText;
                if (!stagesCreated.Contains(stageTableName))
                {
                    using (StreamReader sr = new StreamReader(ddlFile.FullName))
                    {
                        createText = sr.ReadToEnd().Trim();
                    }
                    createText = createText.Replace(tableName, stageTableName);
                    createText = createText.Replace("\r\ngo\r\n", "\r\n");
                    if (createText.ToUpper().EndsWith("GO"))
                    {
                        createText = createText.Substring(0, createText.Length - 2);
                    }
                }
                else
                {
                    createText = "delete from " + stageTableName;
                }

                try
                {
                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    foreach (String t in createText.Split(';'))
                    {
                        var cmd = cn.CreateCommand();
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = t;
                        cmd.ExecuteNonQuery();
                    }
                    if (!stagesCreated.Contains(stageTableName))
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Table " + tableName.Replace("[cf].", "[cfstage].").Replace("[ct].", "[ctstage].").Replace("[sf].", "[sfstage].") + " created.");
                        stagesCreated.Add(stageTableName);
                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Table " + tableName.Replace("[cf].", "[cfstage].").Replace("[ct].", "[ctstage].").Replace("[sf].", "[sfstage].") + " cleaned.");

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("error: {0}", e.Message);
                    return;
                }
            }
        }

        private static void DatabaseTransaction(System.Data.Common.DbConnection cn, System.Data.Common.DbCommand cmd)
        {


            using (var tran = cn.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }

                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.CommandTimeout = 0;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }

                tran.Commit();

            }



        }
        static List<string> tablesLoaded = new List<string>();
        static private void MergeTables(System.Data.Common.DbConnection cn)
        {
            string dir = (options.ListenerDirectory.Trim() + "\\merge").Replace("\\\\", "\\");

            if (cn.ConnectionString.Contains("redshift"))
            {
                dir = dir.Replace("\\merge", "\\mergeRedshift");
            }
            foreach (String f in Directory.GetFiles(dir, "*.sql"))
            {
                FileInfo fi = new FileInfo(f);
                string mergeText;

                String stageTableName = fi.Name.Split('.')[0] + "stage." + fi.Name.Split('.')[1];
                String tableName = fi.Name.Split('.')[0] + "." + fi.Name.Split('.')[1];

                if (cn.ConnectionString.Contains("redshift"))
                {
                    stageTableName = "salesforce." + stageTableName.Replace(".", "");
                    tableName = "salesforce." + tableName.Replace(".", "");
                }



                using (StreamReader sr = new StreamReader(f))
                {
                    mergeText = sr.ReadToEnd().Trim();
                }
                try
                {
                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    foreach (String t in mergeText.Split(';'))
                    {
                        var cmd = cn.CreateCommand();
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = t;
                        cmd.ExecuteNonQuery();
                    }
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Table " + stageTableName + " merged.");
                    var sc = GetSchema(cn, stageTableName);

                    UpsertTable(cn, tableName, stageTableName, sc);
                }
                catch (Exception e)
                {
                    Console.WriteLine("error: {0}", e.Message);
                    return;
                }

            }
        }
        static void DataTableToCsv(DataTable dt, string csvFileName, bool hasColumnNames, DataTable schemaDef)
        {
            //        using (var sw = new System.IO.StreamWriter(csvFileName, true, Encoding.GetEncoding("iso-8859-1")))

            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            using (var sw = new System.IO.StreamWriter(csvFileName, true, utf8WithoutBom))
            {
                bool isFirst = true;
                StringBuilder sb = new StringBuilder();
                if (hasColumnNames)
                {
                    foreach (var c in dt.Columns)
                    {
                        if (!isFirst)
                        {
                            sb.Append(",");
                        }
                        sb.Append(c.ToString().Replace(",", " ").Replace("\r", " ").Replace("\n", " "));
                        isFirst = false;
                    }
                    sw.WriteLine(sb.ToString());
                    sb.Clear();
                }

                int linha = 0;
                foreach (DataRow r in dt.Rows)
                {
                    linha++;
                    isFirst = true;
                    int columnIndex = 0;

                    foreach (var c in r.ItemArray)
                    {

                        if (!isFirst)
                        {
                            sb.Append(",");
                        }
                        if (dt.Columns[columnIndex].DataType.ToString() == "System.DateTime" && c != DBNull.Value)
                        {
                            if (schemaDef.Rows[columnIndex]["DataTypeName"].ToString() == "date")
                            {
                                sb.Append(((DateTime)c).ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                sb.Append(((DateTime)c).ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                        }
                        else
                        {
                            /*    if (utf8WithoutBom.GetByteCount(c.ToString().Replace(",", " ").Replace("\r", "").Replace("\n", " ").Replace("\"", "").Replace("'", "")) > (int)(schemaDef.Rows[columnIndex]["ColumnSize"]))
                                {

                                    var bytes = new byte[(int)(schemaDef.Rows[columnIndex]["ColumnSize"]) - 3];
                                    int byteIndex = 0;
                                    foreach (byte b in utf8WithoutBom.GetBytes(c.ToString().Replace(",", " ").Replace("\r", "").Replace("\n", " ").Replace("\"", "").Replace("'", "").ToCharArray()))
                                    {
                                        bytes[byteIndex] = b;
                                        byteIndex++;
                                        if (byteIndex >= bytes.Length)
                                        {
                                            break;
                                        }
                                    }
                                    sb.Append(utf8WithoutBom.GetString(bytes));

                                }
                                else
                                {*/
                            sb.Append(c.ToString().Replace(",", " ").Replace("\r", "").Replace("\n", " ").Replace("\"", "").Replace("'", ""));
                            //      }
                        }

                        isFirst = false;
                        columnIndex++;
                    }
                    sw.WriteLine(sb.ToString());
                    sb.Clear();
                }
                sw.Close();
                sw.Dispose();
            }
        }

        static bool moveToS3(string bucket, string csvFileName)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Moving " + csvFileName + " to s3.");
            var p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.FileName = "cmd";
            p.Start();
            p.StandardInput.WriteLine(options.S3Path.Replace("\\\\", "\\") + " " + " auth " + options.S3AccessKey + " " + options.S3SecretKey + "");
            p.StandardInput.WriteLine(options.S3Path.Replace("\\\\", "\\") + " " + " put " + bucket + " " + csvFileName);
            p.StandardInput.WriteLine("exit %errorlevel%");
            var sb = new StringBuilder();
            while (!p.HasExited)
            {
                sb.Append(p.StandardOutput.ReadToEnd());

            }
            p.WaitForExit();
            if (p.ExitCode == 0)
            {
                File.Delete(csvFileName);
                return true;
            }
            else
            {
                Console.Write(sb.ToString());
                return false;
            }

        }
        static bool bulkLoadToRedShift(string bucket, string csvFileName, string tableName)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Loading " + csvFileName + " in Redshift.");

            var p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.FileName = "cmd";
            p.Start();

            p.StandardInput.WriteLine("set AWS_ACCESS_KEY_ID=" + options.S3AccessKey);
            p.StandardInput.WriteLine("set AWS_SECRET_KEY=" + options.S3SecretKey);
            p.StandardInput.WriteLine("set AWS_REDSHIFT_USER=" + options.RedShiftUID);
            p.StandardInput.WriteLine("set AWS_REDSHIFT_PASS=" + options.RedShiftPWD);
            p.StandardInput.WriteLine("\"" + options.JavaPath.Replace("\\\\", "\\") + "\" -jar " + options.RedShitLoaderPath + " --load --cluster pu-dw-1 --from s3://" + bucket + csvFileName + " --to " + tableName + "  --option \"delimiter ','\" --option removequotes --discard-source");
            p.StandardInput.WriteLine("exit  %errorlevel%");

            var sb = new StringBuilder();
            while (!p.HasExited)
            {
                sb.Append(p.StandardOutput.ReadToEnd());

            }

            p.WaitForExit();
            if (p.ExitCode == 0)
            {
                return true;
            }
            else
            {
                Console.Write(sb.ToString());
                Environment.Exit(5);
                return false;
            }
        }
        private static bool odbcLoadToRedShift(string csvDirectory, string csvFileName, string tableName)
        {
            DataTable schemaDef;

            if (cnRedShift.State != ConnectionState.Open)
            {
                cnRedShift.Open();
            }
            var cmd = cnRedShift.CreateCommand();
            cmd.CommandTimeout = 0;
            string values = "";
            foreach (var f in schemaDefRedShift.Rows)
            {
                values += ",?";
            }
            values = values.Substring(1);
            cmd.CommandText = string.Format("insert into {0} values ({1})", tableName, values);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + e.Message + " " + e.StackTrace);
                return false;
            }
            return false; //TODO
        }
        static bool loadToRedShift(string bucket, string csvDirectory, string csvFileName, string tableName)
        {
            FileInfo fi = new FileInfo(csvDirectory + csvFileName);
            if (fi.Length > 10 * 1024 * 1024)
            {
                if (!moveToS3(bucket, csvDirectory + csvFileName))
                {
                    return false;
                }
                return bulkLoadToRedShift(bucket, csvFileName, tableName);

            }
            else
            {
                return odbcLoadToRedShift(csvDirectory, csvFileName, tableName);
            }
        }
        static void UpsertTable(System.Data.Common.DbConnection cn, string tableName, string stageTableName, DataTable NewSchemaDef)
        {
            DataTable oldSchemaDef = GetSchema(cn, tableName);

            List<string> ColumnsKept = new List<string>();

            //         bool hasSchemaChanges = false;
            bool hasSchemaChanges = true;
            if (oldSchemaDef != null)
            {
                foreach (DataRow oldSchemaRow in oldSchemaDef.Rows)
                {
                    if (oldSchemaRow["ColumnName"].ToString().ToLower().Equals("Comiss_o_Fracionamento_Pre_o".ToLower()))
                    {
                        Console.WriteLine("");
                    }
                    foreach (DataRow schemaRow in NewSchemaDef.Rows)
                    {
                        if (oldSchemaRow["ColumnName"].ToString().ToLower().Equals(schemaRow["ColumnName"].ToString().ToLower()))
                        {
                            ColumnsKept.Add(oldSchemaRow["ColumnName"].ToString().ToLower());
                            break;
                        }
                    }
                    if (!ColumnsKept.Contains(oldSchemaRow["ColumnName"].ToString().ToLower()))
                    {
                        hasSchemaChanges = true;
                    }
                }
                foreach (DataRow schemaRow in NewSchemaDef.Rows)
                {
                    if (!ColumnsKept.Contains(schemaRow["ColumnName"].ToString().ToLower()))
                    {
                        hasSchemaChanges = true;
                        break;
                    }
                }
            }
            else
            {
                hasSchemaChanges = true;
            }
            StringBuilder upsertText = new StringBuilder();
            StringBuilder upsertTextRedShift = new StringBuilder();
            StringBuilder listField = new StringBuilder();

            bool isFirst = true;
            foreach (string column in ColumnsKept)
            {
                if (isFirst)
                {
                    listField.Append("\t");
                }
                else
                {
                    listField.Append(",\t");
                }
                listField.Append(column + "\r\n");
                isFirst = false;
            }
            if (hasSchemaChanges)
            {

                if (oldSchemaDef != null)
                {

                    upsertText.Append(string.Format("insert into {0} \r\n(\r\n", stageTableName));

                    upsertText.Append(listField);

                    upsertText.Append(")\r\n");
                    upsertText.Append("select\r\n");
                    upsertText.Append(listField);
                    upsertText.Append(string.Format("from {0} \r\n", tableName));
                    upsertText.Append(string.Format("where Id not in (select Id from {0});\r\n", stageTableName));
                }
                if (cn.ConnectionString.ToLower().Contains("redshift"))
                {
                    upsertText.Append(string.Format("drop table if exists {0};", tableName));
                    upsertText.Append(string.Format("create table {0} as select * from  {1}", tableName, stageTableName));
                }
                else
                {
                    upsertText.Append(string.Format("IF object_id(N'{0}') IS NOT NULL \r\n", tableName));
                    upsertText.Append("begin\r\n");
                    upsertText.Append(string.Format("drop table {0}\r\n", tableName));
                    upsertText.Append("end\r\n");
                    upsertText.Append(string.Format("select * into {0} from  {1}", tableName, stageTableName));
                }

            }
            else
            {
                upsertText.Append(string.Format("delete from {0} \r\n", tableName));
                upsertText.Append(string.Format("where Id  in (select Id from {0} );\r\n", stageTableName));
                upsertText.Append(string.Format("insert into {0} \r\n(\r\n", tableName));
                upsertText.Append(listField);

                upsertText.Append(")\r\n");
                upsertText.Append("select\r\n");
                upsertText.Append(listField);
                upsertText.Append(string.Format("from {0} \r\n", stageTableName));
            }
            try
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }

                foreach (String t in upsertText.ToString().Split(';'))
                {
                    var cmd = cn.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = t;
                    if (hasSchemaChanges)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        DatabaseTransaction(cn, cmd);
                    }
                }

                if (!tablesLoaded.Contains(tableName))
                {
                    tablesLoaded.Add(tableName);
                }

                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Table " + tableName + " upserted.");
            }
            catch (Exception e)
            {
                Console.WriteLine("error: {0}", e.Message);
                return;
            }

        }
    }
}
