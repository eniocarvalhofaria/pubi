using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Reflection;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.Data
{
    public abstract class DatabaseLoader
    {
        public DatabaseLoader(System.Data.Common.DbConnection connection, string schemaName, string tableName)
        {
            this.Connection = connection;
            _SchemaName = schemaName;
            _TableName = tableName;
        }

        private string GetExecutingDirectory
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetAssembly(typeof(DatabaseLoader)).CodeBase).Replace("file:\\", "");
            }
        }
        private string UserId { get; set; }
        private string Password { get; set; }
        private DataTable schemaTable = null;
        public void ClearDestinationTable()
        {
            var e = new QueryExecutor(Connection);
            e.Execute(string.Format("delete from \"{0}\".\"{1}\"", SchemaName, TableName), false);
        }
        public virtual DataTable GetNewDataTable()
        {
            var e = new QueryExecutor(Connection);
            /*
            foreach (var item in GetSchemaTable().Rows)
            { 
            
            
            }
              */
            var ret = e.ReturnData(string.Format("select top 0 * from \"{0}\".\"{1}\"", SchemaName, TableName), false);
            foreach (DataColumn item in ret.Columns)
            {
                item.DataType = Type.GetType("System.Object");
            }
            return ret;
        }
        public virtual bool TestIfTableExist()
        {
            var e = new QueryExecutor(Connection);
            var ret = e.ReturnData(string.Format("select top 0 1 from \"{0}\".\"{1}\"", SchemaName, TableName), false);
            if (e.ReturnCode != 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private string _SchemaName;
        public string SchemaName
        {
            get { return _SchemaName; }
        }
        private string _TableName;
        public string TableName { get { return _TableName; } }

        private System.Data.Common.DbConnection _Connection = null;

        public System.Data.Common.DbConnection Connection
        {
            get { return _Connection; }
            set { _Connection = value; }
        }
        protected DataTable GetSchemaTable()
        {
            if (schemaTable == null)
            {
                var e = new QueryExecutor(Connection);
                schemaTable = e.ReturnData(string.Format("select column_name as ColumnName, data_type as DataTypeName from INFORMATION_SCHEMA.COLUMNS where table_name = '{0}' and table_schema = '{1}'  order by ordinal_position", TableName, _SchemaName), false);

            }
            return schemaTable;
        }
        public abstract bool Load(DataTable dt);
        public void CreateTable(DataTable dt)
        {
            var e = new QueryExecutor(Connection);
       
            var text = GetCreateTableText(dt);
            e.Execute(text);

        }
        public string ErrorMessage { get; protected set; }
        public string GetCreateTableText(DataTable dt)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("create table \"{0}\".\"{1}\"\r\n(\r\n", SchemaName, TableName));
            bool isFirst = true;

            foreach (DataColumn c in dt.Columns)
            {
                if (!isFirst)
                {
                    sb.Append(",");
                }
                sb.Append("\t\"" + c.ColumnName + "\" varchar(1000)" + "\r\n");
                isFirst = false;
            }
            sb.Append(")");

            return sb.ToString();
        }

    }
}
