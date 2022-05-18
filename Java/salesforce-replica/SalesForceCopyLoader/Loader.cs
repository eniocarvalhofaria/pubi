using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Schema;
using System.Net;
using System.Text.RegularExpressions;
namespace SalesForceCopyLoader
{
    public abstract class Loader
    {
        public string DdlDirectory { get; set; }
        public string MergeDirectory { get; set; }
        public int DataTableMaxRows { get; set; }
        public DataTable SchemaTableDefinition;
        public System.Data.Common.DbConnection Connection { get; set; }
        public DbConnectionFactory ConnectionFactory { get; set; }
        private int _ReturnCode = 0;
        public int ReturnCode { get { return _ReturnCode; } }
        private bool _IsRunning;

        public bool IsRunning
        {
            get { return _IsRunning; }
        }

        public abstract string GetStageTableName(string fileName);
        public abstract string GetTableName(string fileName);
        public abstract void ConsumeData(DataTable dt);
        public abstract void EndOfFileRead();
        public abstract void LoadErrors(DataTable errorDataTable);
        public FileInfo FileInfo { get; set; }
        public FileInfo MergeFileInfo { get; set; }
        protected FileInfo DdlFileInfo { get; set; }
        //     List<string> stagesCreated = new List<string>();
        private string _TableName;

        public string TableName
        {
            get
            {
                if (string.IsNullOrEmpty(_TableName))
                {
                    _TableName = GetTableName(FileInfo.Name);
                }
                return _TableName;
            }
            set
            {
                _TableName = value;
            }
        }
        private string _StageTableName;

        public string StageTableName
        {
            get
            {
                if (string.IsNullOrEmpty(_StageTableName))
                {
                    _StageTableName = GetStageTableName(FileInfo.Name);
                }
                return _StageTableName;

            }
            set { _StageTableName = value; }
        }

        public void Load()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            _IsRunning = true;
            try
            {
        /*
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " File " + FileInfo.Name + " requesting connection from " + this.ConnectionFactory.GetType().ToString()+ "." );
                Connection = ConnectionFactory.GetConnection();
         */
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " File " + FileInfo.Name + " will be loaded.");
                bool errorInLoad = false;
                //    List<string> truncatedTables = new List<string>();
                /*
                string csvFileName = FileInfo.Name.Split('.')[0] + FileInfo.Name.Split('.')[1] + ".csv";
                  * */
                DdlFileInfo = new FileInfo(DdlDirectory + "\\" + FileInfo.Name.Substring(0, FileInfo.Name.Length - 7) + ".ddl.sql");
                DataTableMaxRows = 10000;
                SchemaTableDefinition = null;


                CreateTable();
                SchemaTableDefinition = GetSchema( StageTableName);

                int rw_cnt = 0;

                DataTable errorDataTable = null;

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


                    using (XmlReader xr = new XmlTextReader(FileInfo.FullName))
                    {
                        Xml2DataTable(xr, SchemaTableDefinition, out errorDataTable, (DataTable dt) =>
                        {
                            ConsumeData(dt);
                        });
                    }
                    EndOfFileRead();

                if (errorDataTable != null)
                {
                    LoadErrors(errorDataTable);
                }
                if (!errorInLoad)
                {
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Table " + StageTableName + " loaded.");

                    if (FileInfo.Name.IndexOf(".LA.xml") > -1 || FileInfo.Name.IndexOf(".UN.xml") > -1)
                    {
                        if (!File.Exists(MergeDirectory + "\\" + FileInfo.Name.Substring(0, FileInfo.Name.Length - 10) + ".merge.sql"))
                        {
                            UpsertTable(Connection, TableName, StageTableName, SchemaTableDefinition);

                        }
                    }
            //        File.Move(FileInfo.FullName, FileInfo.FullName.Replace("xml\\", "loaded\\"));
                    //    File.Delete(file.FullName);
                }
                else
                {
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Table " + StageTableName + " not loaded correctly.");
                    Environment.Exit(1);
                }
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " File " + FileInfo.Name + " releasing connection.");
                ConnectionFactory.ReleaseConnection(Connection);
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " File " + FileInfo.Name + " connection relesead.");
                _ReturnCode = 0;
                _IsRunning = false;
            }
            catch (Exception e)
            {

                Console.WriteLine("error: ");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                try
                {
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " File " + FileInfo.Name + " releasing connection.");
                    ConnectionFactory.ReleaseConnection(Connection);
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " File " + FileInfo.Name + " connection relesead.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                _ReturnCode = 1;
                _IsRunning = false;
            }

        }
        List<string> stagesCreated = new List<string>();
        public virtual DataTable GetSchema(string tableName)
        {
            if (schemas.ContainsKey(tableName))
            {
                return schemas[tableName];
            }
          
            DataTable SchemaTableDefinition;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            var cmd = Connection.CreateCommand();
            cmd.CommandTimeout = 0;
            cmd.CommandText = string.Format("SELECT TOP 0 * FROM {0}", tableName);
            try
            {
                cmd.ExecuteNonQuery();
                using (var dr = cmd.ExecuteReader())
                {
                    SchemaTableDefinition = dr.GetSchemaTable();
                }
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Schema from " + tableName + " getted.");
                schemas.Add(tableName, SchemaTableDefinition);
                return SchemaTableDefinition;
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Table " + tableName + " does not exists.");
                return null;
            }
        }

        void CreateTable()
        {
            if (FileInfo.Name.IndexOf(".00.xml") > -1 || FileInfo.Name.IndexOf(".UN.xml") > -1)
            {
                string createText;
                if (!stagesCreated.Contains(StageTableName))
                { 
                    using (StreamReader sr = new StreamReader(DdlFileInfo.FullName))
                    {
                        createText = sr.ReadToEnd().Trim();
                    }
                    createText = createText.Replace(TableName, StageTableName);
                    createText = createText.Replace("\r\ngo\r\n", "\r\n");
                    if (createText.ToUpper().EndsWith("GO"))
                    {
                        createText = createText.Substring(0, createText.Length - 2);
                    }
                }
                else
                {
                    createText = "delete from " + StageTableName;
                }

                try
                {
                    if (Connection.State != ConnectionState.Open)
                    {
                        Connection.Open();
                    }
                    foreach (String t in createText.Split(';'))
                    {
                        var cmd = Connection.CreateCommand();
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = t;
                        DatabaseTransaction(Connection, cmd);
                      }
                    
                    if (!stagesCreated.Contains(StageTableName))
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Table " + TableName.Replace("[cf].", "[cfstage].").Replace("[ct].", "[ctstage].").Replace("[sf].", "[sfstage].") + " created.");
                        stagesCreated.Add(StageTableName);
                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Table " + TableName.Replace("[cf].", "[cfstage].").Replace("[ct].", "[ctstage].").Replace("[sf].", "[sfstage].") + " cleaned.");

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("error: {0}", e.Message);
                    return;
                }
            }
        }/*
        private static void ExecuteQuery( Action<SqlConnection, SqlCommand> TransactionBlock)
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
        }*/
        private void Xml2DataTable(XmlReader xr, DataTable SchemaTableDefinition, out DataTable errorDataTable, Action<DataTable> onDataTableClose)
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
                            string columnName = Regex.Replace(hierarchy.Peek(), "__c$", "").ToLower();
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

                                foreach (DataRow schemaRow in SchemaTableDefinition.Rows)
                                {
                                    if (schemaRow["DataType"].ToString().Equals("System.Boolean"))
                                    {
                                        dataTable.Columns.Add(schemaRow["ColumnName"].ToString().ToLower(), new Int16().GetType());
                                    }
                                    else
                                    {
                                        dataTable.Columns.Add(schemaRow["ColumnName"].ToString().ToLower(), (Type)schemaRow["DataType"]);
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
                                                case "System.Double":
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
                            if (dataTable.Rows.Count >= DataTableMaxRows)
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
        Dictionary<string, DataTable> schemas = new Dictionary<string, DataTable>();
        private void DatabaseTransaction(System.Data.Common.DbConnection cn, System.Data.Common.DbCommand cmd)
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
        List<string> tablesLoaded = new List<string>();
     

        void UpsertTable(System.Data.Common.DbConnection cn, string tableName, string stageTableName, DataTable NewSchemaDef)
        {
            DataTable oldSchemaDef = GetSchema( tableName);

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
            //        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + t);
                    DatabaseTransaction(cn, cmd);
                    /*
                    if (hasSchemaChanges)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        DatabaseTransaction(cn, cmd);
                    }*/
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

        public void MergeTable()
        {
            _IsRunning = true;
            string mergeText;

            String stageTableName = GetStageTableName(MergeFileInfo.Name);
            String tableName = GetTableName(MergeFileInfo.Name);
            if (Connection == null)
            {
                Connection = ConnectionFactory.GetConnection();
            }
            using (StreamReader sr = new StreamReader(MergeFileInfo.FullName))
            {
                mergeText = sr.ReadToEnd().Trim();
            }
            try
            {
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }
                foreach (String t in mergeText.Split(';'))
                {
                    var cmd = Connection.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = t;
           //         Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + t);
              
                    DatabaseTransaction(Connection, cmd);
                }
                var sc = GetSchema(stageTableName);

                UpsertTable(Connection, tableName, stageTableName, sc);
                _ReturnCode = 0;
                _IsRunning = false;
            }
            catch (Exception e)
            {
                Console.WriteLine("error: {0}", e.Message);
                _ReturnCode = 2;
                _IsRunning = false;
                return;
            }

        }
    }
}
