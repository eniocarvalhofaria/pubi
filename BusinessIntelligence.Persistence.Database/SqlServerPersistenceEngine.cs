using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Reflection;
namespace BusinessIntelligence.Persistence
{
    public class SqlServerPersistenceEngine : DatabasePersistenceEngine
    {
        public SqlServerPersistenceEngine(System.Data.Common.DbConnection connection, String schemaName)
            : base(connection, schemaName)
        {
        }
        public override string GetCreateTableText(Type persistentObjectType)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("create table {0}\r\n(\r\n", GetTableName(persistentObjectType.Name)));
            sb.Append("\t" + GetIdentityColumnDDLText("Id") + "\r\n");
            foreach (KeyValuePair<string, PersistentProperty> item in this.GetPropertiesAttributtes(persistentObjectType))
            {
                if (!item.Key.Equals("Id"))
                {
                
                    //          string columnDDLText = GetColumnDDLText(persistentObjectType, item.Key, item.Value);
                    string columnDDLText = GetColumnDDLText(item.Key, item.Value);
                    if (!string.IsNullOrEmpty(columnDDLText) && !item.Value.IsList)
                    {
                       sb.Append(",\t" + columnDDLText + "\r\n");
                    }
                }
            }
            sb.Append(GetPrimaryKeyDDLText("Id"));
            sb.Append("\r\n)\r\n");
            return sb.ToString();
        }
        public override string GetTableName(string objectType)
        {
            if (string.IsNullOrEmpty(SchemaName))
            {
                return string.Format("[{0}]", objectType);
            }
            else
            {
                return string.Format("[{0}].[{1}]", SchemaName, objectType);
            }
        }
        public override string GetIdentityColumnDDLText(string columnName)
        {
            return string.Format("{0} int identity(1,1) not null", columnName);
        }

        public override string GetCurrentDateTimeText()
        {
            return "getdate()";
        }

        public override string GetCurrentDateText()
        {
            return "cast(getdate() as date)";
        }
        public override string GetPrimaryKeyDDLText(string columnName)
        {
            return string.Format("primary key ({0})", columnName);
        }
        public override string GetPrimaryKeyDDLText(string[] columnNames)
        {

            var sb = new StringBuilder();
            foreach (string c in columnNames)
            {
                sb.Append("," + c);
            }
            sb.Remove(0, 1);
            return string.Format("primary key ({0})", sb.ToString());
        }
        //      public override string GetColumnDDLText(Type persistentObjectType, String columnName, PersistentProperty pp)
        public override string GetColumnDDLText(String columnName, PersistentProperty pp)
        {
            var attribute = pp.DataTypeAttribute;
            string typeDefinition;
            switch (attribute.GetType().Name)
            {
                case "NumericAttribute":
                    {
                        NumericAttribute na = (NumericAttribute)attribute;
                        if (na.MinValue >= 0 && na.MaxValue <= ((Math.Pow(2, 8)) - 1))
                        {
                            typeDefinition = " tinyint";
                            break;
                        }
                        else if (na.MinValue >= ((Math.Pow(2, 16) / 2) * -1) && na.MaxValue <= ((Math.Pow(2, 16) / 2) - 1))
                        {
                            typeDefinition = " smallint";
                            break;
                        }
                        else if (na.MinValue >= ((Math.Pow(2, 32) / 2) * -1) && na.MaxValue <= ((Math.Pow(2, 32) / 2) - 1))
                        {
                            typeDefinition = " int";
                            break;
                        }
                        else if (na.MinValue >= ((Math.Pow(2, 64) / 2) * -1) && na.MaxValue <= ((Math.Pow(2, 64) / 2) - 1))
                        {
                            typeDefinition = " bigint";
                            break;
                        }
                        else
                        {
                            typeDefinition = " decimal(38,0)";
                            break;
                        }

                    }
                case "DecimalAttribute":
                    {
                        typeDefinition = string.Format("decimal({0},{1})", ((DecimalAttribute)attribute).TotalDigits, ((DecimalAttribute)attribute).DecimalPlaces);
                        break;
                    }
                case "BooleanAttribute":
                    typeDefinition = "bit";
                    break;
                case "DateTimeAttribute":
                    DateTimeAttribute ta = (DateTimeAttribute)attribute;
                    if (!ta.HasTimeDetail)
                    {
                        return "date";
                    }
                    else if (0 > 1 && ta.MaxDateTime <= DateTime.Parse("2079-06-06 23:59:59") && ta.MinDateTime >= DateTime.Parse("1900-01-01 00:00:00"))
                    {
                        typeDefinition = "smalldatetime";
                    }
                    else
                    {
                        typeDefinition = "datetime";
                    }
                    break;
                case "CharAttribute":
                    {
                        CharAttribute ca = (CharAttribute)attribute;
                        string type;
                        if (ca.HasFixedLength)
                        {
                            type = "nchar";
                        }
                        else
                        {
                            type = "nvarchar";
                        }
                        if (ca.MaxLength <= 4000)
                        {
                            typeDefinition = string.Format("{0}({1})", type, ca.MaxLength);
                        }
                        else {
                            typeDefinition = string.Format("{0}({1})", type, "max");
                        }
                        break;
                    }
                default:
                    return null;
            }

            string nullDefinition = "";
            if (pp.NotNull)
            {
                nullDefinition = " not null";
            }
            string uniqueDefinition = "";
            if (pp.IsUnique)
            {
                uniqueDefinition = " unique";
            }
            return columnName + " " + typeDefinition + nullDefinition + uniqueDefinition;
        }

        public override string GetRemoveFieldsText(Type persistentObjectType, string[] fieldsNames)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("alter table {0}", GetTableName(persistentObjectType.Name)));
            sb.Append("drop column\r\n");
            bool isFirst = true;
            foreach (string field in fieldsNames)
            {
                if (!isFirst)
                {
                    sb.Append(",");
                }

                sb.Append(string.Format("\t{0}\r\n", field));
                isFirst = false;
            }

            return sb.ToString();
        }

        public override string GetAddFieldsText(Type persistentObjectType, Dictionary<string, PersistentProperty> pps)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("alter table {0}\r\n", GetTableName(persistentObjectType.Name)));
            sb.Append("add\r\n");
            bool isFirst = true;
            foreach (KeyValuePair<string, PersistentProperty> item in pps)
            {
                if (!isFirst)
                {
                    sb.Append(",");
                }
                //   string columnDDLText = GetColumnDDLText(persistentObjectType, item.Key, item.Value);
                string columnDDLText = GetColumnDDLText(item.Key, item.Value);
                if (!string.IsNullOrEmpty(columnDDLText))
                {
                    sb.Append("\t" + columnDDLText + "\r\n");
                }
                isFirst = false;
            }
            return sb.ToString();
        }



        public override string GetLastRowText(string objectType)
        {
            return string.Format("select * from {0} where Id =  @@IDENTITY", GetTableName(objectType));
        }

        public override void CheckPersistenceStructure(Type persistentObjectType)
        {
            var dr = ExecuteRead("select top 1 * from " + this.GetTableName(persistentObjectType.Name), true);
            if (dr == null)
            {
                CreateTable(persistentObjectType);
            }
            else
            {

                // criar rotina para caso de mudança de estrutura
                var atts = GetPropertiesAttributtes(persistentObjectType);
                List<string> removable = new List<string>();
                var schemaTable = dr.GetSchemaTable();
                // Checa se algumas colunas foram retiradas da classe
                foreach (DataRow r in schemaTable.Rows)
                {
                    if (!r["ColumnName"].ToString().Equals("Id") && (!atts.ContainsKey(r["ColumnName"].ToString()) || atts[r["ColumnName"].ToString()].IsList || atts[r["ColumnName"].ToString()].IsArray))
                    {
                        removable.Add(r["ColumnName"].ToString());
                    }
                }
                dr.Close();
                isInExecution = false;
                var rem = new Dictionary<string, PersistentProperty>();

                PersistentProperty IdProperty  = atts["Id"];
                
                // Checa se algumas colunas devem ser adicionadas a tabela
          
          
                foreach (KeyValuePair<string, PersistentProperty> item in atts)
                {

                    if (!item.Value.IsArray && !item.Value.IsList && !TableHasColumn(schemaTable, item.Key))
                    {
                        rem.Add(item.Key, item.Value);
                    }
                   if (item.Value.IsArray || item.Value.IsList)
                    {
                        var dr2 = ExecuteRead("select top 1 * from " + this.GetTableName(persistentObjectType.Name + "_" + item.Key), true);
                        if (dr2 == null)
                        {
                            AddArrayField(persistentObjectType, IdProperty, item.Value);
                        }
                        else
                        {
                            dr2.Close();
                            isInExecution = false;
                        }
                    }
                }
             
                if (removable.Count > 0)
                {
    //                RemoveField(persistentObjectType, removable.ToArray());
                }
                if (rem.Count > 0)
                {
                    AddField(persistentObjectType, rem);
                }

            }
        }

        private bool TableHasColumn(DataTable schemaTable, string columnName)
        {
            foreach (DataRow r in schemaTable.Rows)
            {
                if (columnName.Equals(r["ColumnName"].ToString()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
