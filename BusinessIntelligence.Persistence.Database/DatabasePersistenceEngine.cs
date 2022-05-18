using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Reflection;
using System.Collections;
namespace BusinessIntelligence.Persistence
{
    public abstract class DatabasePersistenceEngine : PersistenceEngine
    {
        public DatabasePersistenceEngine(System.Data.Common.DbConnection connection, String schemaName)
            : base()
        {
            Connection = connection;
            this._SchemaName = schemaName;
            Type[] types = null;
            /*
            if (assembly == null)
            {
 
                types = Assembly.GetEntryAssembly().GetTypes();
             
            }
            else {
                types = assembly.GetTypes();
            }
             */

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly ass in assemblies)
            {

                try
                {
                    foreach (Type type in ass.GetTypes())
                    {
                        if (type.IsSubclassOf(typeof(StoredObject)) && !type.IsAbstract)
                        {
                            CheckPersistenceStructure(type);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            /*
            foreach (Type type in types)
            {
                if (type.IsSubclassOf(typeof(StoredObject)) && !type.IsAbstract)
                {
                    CheckPersistenceStructure(type);
                }
            }
             * */

        }
        private string _SchemaName;

        public string SchemaName
        {
            get { return _SchemaName; }
            set { _SchemaName = value; }
        }

        public bool ExecuteStatement(string commandText)
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                {
                    Connection.Open();
                }
                var cmd = Connection.CreateCommand();
                cmd.CommandTimeout = 0;
                cmd.CommandText = commandText;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + e.StackTrace);
                return false;
            }



        }
        public DbDataReader ExecuteRead(string commandText)
        {
            return ExecuteRead(commandText, false);
        }
       protected bool isInExecution = false;
        public DbDataReader ExecuteRead(string commandText, bool ErrorExpected)
        {

            try
            {
                int wait = 0;
                while (isInExecution )
                {
                    System.Threading.Thread.Sleep(1000);
                    wait++;
                    if (wait > 10)
                    {
                        return null;
                    }
                }

                isInExecution = true;
                if (Connection.State != System.Data.ConnectionState.Open)
                {
                    Connection.Open();
                }
                var cmd = Connection.CreateCommand();
                cmd.CommandTimeout = 60;
                cmd.CommandText = commandText;
                return cmd.ExecuteReader();

            }
            catch (Exception e)
            {
                if (!ErrorExpected)
                {
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + e.StackTrace);
                    throw e;
                }
                else
                {
                    return null;
                }
            }



        }
        public override bool Refresh(StoredObject obj)
        {
            Dictionary<int, Dictionary<string, object>> objectList;
            objectList = LoadFromTexts(GetLoadText(obj.GetType(), FilterExpressions.Equal("Id", obj.Id.ToString()), null), false);
            if (objectList != null && objectList.ContainsKey(obj.Id))
            {
                obj.Fill(objectList[obj.Id]);
                return true;
            }

            return false;

        }
        public override bool Update(PersistentObject obj)
        {
            return ExecuteStatement(GetUpdateText(obj));

        }
        private string GetUpdateText(PersistentObject obj)
        {
            var sb = new StringBuilder();
            var arrayText = new StringBuilder();
            sb.Append(string.Format("update {0}\r\nset\r\n", GetTableName(obj.ObjectTypeName)));
            bool isFirst = true;
            string whereId = null;
            string Id = null;
            foreach (KeyValuePair<string, object> item in obj.GetValues())
            {
                if (item.Key.Equals("Id"))
                {
                    Id = item.Value.ToString();
                    whereId = string.Format("\t{0} = {1}\r\n", item.Key, item.Value.ToString());
                }
                else if (this.GetPropertiesAttributtes(obj.GetType())[item.Key].IsArray || this.GetPropertiesAttributtes(obj.GetType())[item.Key].IsList)
                {

                    arrayText.Append(string.Format("delete from {0} where Id = @@ID\r\n", GetTableName(obj.ObjectTypeName + "_" + item.Key)));
                    foreach (object o in (IList)item.Value)
                    {
                        arrayText.Append(string.Format("insert into {0}\r\n values(", GetTableName(obj.ObjectTypeName + "_" + item.Key)));
                        arrayText.Append("@@ID,");
                        arrayText.Append("'" + o.ToString().Replace("'", "''") + "'");
                        arrayText.Append(")\r\n");
                    }
                }
                else
                {
                    string content = null;
                    if (item.Value == null)
                    {
                        content = "null";
                    }
                    else
                    {
                        if (item.Key.Equals("LastUpdateDate"))
                        {
                            content = GetCurrentDateTimeText();
                        }
                        else
                            if (item.Key.Equals("CreatedDate"))
                        {
                            content = item.Key;
                        }
                        else
                        {
                            switch (item.Value.GetType().Name)
                            {

                                case "Int32":
                                case "Int16":
                                case "Int64":
                                case "Long":
                                case "Double":
                                case "Decimal":
                                    content = item.Value.ToString().Replace(",", ".");
                                    break;
                                case "Boolean":
                                    if ((bool)item.Value)
                                    {
                                        content = "1";
                                    }
                                    else
                                    {
                                        content = "0";
                                    }
                                    break;
                                case "DateTime":
                                    if (((DateTime)item.Value).Equals(DateTime.MinValue))
                                    {
                                        content = "null";
                                    }
                                    else
                                    {
                                        content = "'" + ((DateTime)item.Value).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                                    }
                                    break;
                                default:
                                    content = "'" + item.Value.ToString().Replace("'", "''") + "'";
                                    break;
                            }
                        }
                    }
                    if (!item.Key.Equals(content))
                    {
                        if (!isFirst)
                        {
                            sb.Append(",");
                        }
                        sb.Append(string.Format("\t{0} = {1}\r\n", item.Key, content));
                        isFirst = false;
                    }

                }
            }
            sb.Append("where\r\n" + whereId);
            sb.Append("\r\n");
            sb.Append(arrayText.Replace("@@ID", Id));
            return sb.ToString();
        }
        private DbConnection _Connection;
        public DbConnection Connection
        {
            get { return _Connection; }
            set { _Connection = value; }
        }



        public override bool Create(PersistentObject obj)
        {
            var objects = LoadFromText(GetInsertText(obj) + "\r\n" + GetLastRowText(obj.GetType().Name));

            foreach (KeyValuePair<int, Dictionary<string, object>> ob in objects)
            {
                var o = new Dictionary<string, object>();
                o.Add("Id", ob.Value["Id"]);
                o.Add("LastUpdateDate", ob.Value["LastUpdateDate"]);
                o.Add("CreatedDate", ob.Value["CreatedDate"]);
                obj.Fill(o);
                Add((int)ob.Value["Id"], obj);
                return true;
            }
            return false;



        }
        public virtual string GetInsertText(PersistentObject obj)
        {
            var sb = new StringBuilder();
            var values = new StringBuilder();
            var arrayText = new StringBuilder();
            sb.Append(string.Format("insert into {0}\r\n(", GetTableName(obj.ObjectTypeName)));
            values.Append("values\r\n(");
            bool isFirst = true;


            foreach (KeyValuePair<string, object> item in obj.GetValues())
            {

                if (item.Key.Equals("Id"))
                {

                }
                else if (this.GetPropertiesAttributtes(obj.GetType())[item.Key].IsArray || this.GetPropertiesAttributtes(obj.GetType())[item.Key].IsList)
                {
                    foreach (var o in (Array)item.Value)
                    {
                        arrayText.Append(string.Format("insert into {0}\r\n values(", GetTableName(obj.ObjectTypeName + "_" + item.Key)));
                        arrayText.Append("@@IDENTITY,");
                        arrayText.Append("'" + item.Value.ToString().Replace("'", "''") + "'");
                        arrayText.Append(")\r\n");
                    }
                }
                else
                {
                    if (!isFirst)
                    {
                        sb.Append(",");
                        values.Append(",");
                    }
                    string content = null;
                    sb.Append(item.Key);
                    if (item.Value == null)
                    {
                        content = "null";
                    }
                    else if (item.Key.Equals("LastUpdateDate"))
                    {
                        content = GetCurrentDateTimeText();
                    }
                    else if (item.Key.Equals("CreatedDate"))
                    {
                        content = GetCurrentDateTimeText();
                    }
                    else
                    {
                        switch (item.Value.GetType().Name)
                        {
                            case "Int32":
                            case "Int16":
                            case "Int64":
                            case "Long":
                            case "Double":
                            case "Decimal":
                                content = item.Value.ToString().Replace(",", ".");
                                break;
                            case "Boolean":
                                if ((bool)item.Value)
                                {
                                    content = "1";
                                }
                                else
                                {
                                    content = "0";
                                }
                                break;
                            case "DateTime":
                                if (((DateTime)item.Value).Equals(DateTime.MinValue))
                                {
                                    content = "null";
                                }
                                else
                                {
                                    content = "'" + ((DateTime)item.Value).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                                }
                                break;
                            default:
                                content = "'" + item.Value.ToString().Replace("'", "''") + "'";

                                break;
                        }
                    }
                    values.Append(content);
                    isFirst = false;
                }
            }
            sb.Append(")\r\n");
            values.Append(")");
            sb.Append(values);
            sb.Append("\r\n");
            sb.Append(arrayText);
            return sb.ToString();
        }

        public virtual bool CreateTable(Type persistentObjectType)
        {
            if (!persistentObjectType.IsSubclassOf(typeof(PersistentObject)))
            {
                throw new Exception("Este tipo não deriva de [PersistentObject]");
            }
            CreateArrayFields(persistentObjectType);
            return ExecuteStatement(GetCreateTableText(persistentObjectType));
        }
        public void CreateArrayFields(Type persistentObjectType)
        {
            var f = this.GetPropertiesAttributtes(persistentObjectType);
            PersistentProperty IdProperty = f["Id"];
            foreach (KeyValuePair<string, PersistentProperty> item in f)
            {
                if (item.Value.IsList)
                {
                    AddArrayField(persistentObjectType, IdProperty, item.Value);
                }
            }
        }


        public virtual string GetCreateTableText(Type persistentObjectType)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("create table {0}\r\n(\r\n", GetTableName(persistentObjectType.Name)));
            sb.Append("\t" + GetIdentityColumnDDLText("Id") + "\r\n");

            foreach (KeyValuePair<string, PersistentProperty> item in this.GetPropertiesAttributtes(persistentObjectType))
            {
                string columnDDLText;

                if (!item.Key.Equals("Id"))
                {
                    sb.Append(",");
                    columnDDLText = GetColumnDDLText(item.Key, item.Value);
                    if (!string.IsNullOrEmpty(columnDDLText))
                    {
                        sb.Append("\t" + columnDDLText + "\r\n");
                    }
                }

            }
            sb.Append(GetPrimaryKeyDDLText("Id"));
            sb.Append("\r\n)\r\n");

            return sb.ToString();
        }
        public virtual string GetTableName(string objectType)
        {
            if (string.IsNullOrEmpty(SchemaName))
            {
                return string.Format("{0}", objectType);
            }
            else
            {
                return string.Format("{0}.{1}", SchemaName, objectType);
            }
        }
        public abstract string GetIdentityColumnDDLText(string columnName);
        public abstract string GetLastRowText(string objectType);
        public abstract string GetCurrentDateTimeText();
        public abstract string GetCurrentDateText();
        public virtual string GetPrimaryKeyDDLText(string columnName)
        {
            return string.Format("primary key ({0})", columnName);
        }
        public virtual string GetPrimaryKeyDDLText(string[] columnNames)
        {

            var sb = new StringBuilder();
            foreach (string c in columnNames)
            {
                sb.Append("," + c);
            }
            sb.Remove(0, 1);
            return string.Format("primary key ({0})", sb.ToString());
        }
        //     public abstract string GetColumnDDLText(Type persistentObjectType, String columnName, PersistentProperty pp);
        public abstract string GetColumnDDLText(String columnName, PersistentProperty pp);

        public override bool Delete(PersistentObject obj)
        {
            return ExecuteStatement(GetDeleteText(obj));
        }
        private string GetDeleteText(PersistentObject obj)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("delete from {0}\r\n", GetTableName(obj.ObjectTypeName)));
            sb.Append(string.Format("\twhere {0} = {1}\r\n", "Id", obj.Id.ToString()));
            return sb.ToString();
        }
        private string GetRefreshText(PersistentObject obj)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("select *  from {0}\r\n", GetTableName(obj.ObjectTypeName)));
            sb.Append(string.Format("\twhere {0} = {1}\r\n", "Id", obj.Id.ToString()));
            return sb.ToString();
        }
        public virtual string[] GetLoadText(Type objectType, IFilterExpression filterExpression, SortExpression sortExpression)
        {
            List<string> texts = new List<string>();
            string objectName = GetTableName(objectType.Name);
            var atts = GetPropertiesAttributtes(objectType);
            string expression = string.Format("select * from {0}", objectName);

            if (filterExpression != null && filterExpression.GetText() != null)
            {
                expression = string.Format(expression + " where {0}", filterExpression.GetText());
            }

            if (sortExpression != null && sortExpression.GetText() != null)
            {
                expression = expression + " " + sortExpression.GetText();
            }
            texts.Add(expression);
            foreach (KeyValuePair<string, PersistentProperty> item in atts)
            {
                if (item.Value.IsArray || item.Value.IsList)
                {
                    if (filterExpression != null && filterExpression.GetText() != null)
                    {
                        texts.Add(string.Format("select * from {0} where Id in (select Id from {1} where {2})", GetTableName(objectType.Name + "_" + item.Value.PropertyInfo.Name), objectName, filterExpression.GetText()));
                    }
                    else
                    {
                        texts.Add(string.Format("select * from {0}", GetTableName(objectType.Name + "_" + item.Value.PropertyInfo.Name)));

                    }
                }
            }

            return texts.ToArray();
        }
        public override T[] FindObjects<T>(IFilterExpression filterExpression, SortExpression sortExpression)
        {
            var objects = LoadFromTexts(GetLoadText(typeof(T), filterExpression, sortExpression), filterExpression == null);

            var ret = CreateObjects<T>(objects);

            return ret;
        }
        private Dictionary<int, Dictionary<string, object>> LoadFromText(string loadText)
        {
            string[] s = { loadText };
            return LoadFromTexts(s, false);
        }
        private Dictionary<int, Dictionary<string, object>> LoadFromTexts(string[] loadTexts, bool all)
        {
            DbDataReader dr = null;
            var objects = new Dictionary<int, Dictionary<string, object>>();
            int i = 0;
            foreach (string commandText in loadTexts)
            {
                dr = ExecuteRead(commandText);
                if (dr != null)
                {
                    if (i == 0)
                    {
                        if (dr.HasRows)
                        {
                            Dictionary<int, string> fieldsIndex = new Dictionary<int, string>();
                            int index = 0;
                            foreach (DataRow sr in dr.GetSchemaTable().Rows)
                            {
                                fieldsIndex.Add(index, sr["ColumnName"].ToString());
                                index++;
                            }
                            while (dr.Read())
                            {
                                var row = new Dictionary<string, object>();
                                foreach (KeyValuePair<int, string> item in fieldsIndex)
                                {
                                    if (dr[item.Key] != DBNull.Value)
                                    {
                                        row.Add(item.Value, dr[item.Key]);
                                    }
                                }
                                try
                                {
                                    objects.Add(Convert.ToInt32(dr["Id"]), row);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }

                        }
                        dr.Close();
                        isInExecution = false;
                    }
                    else
                    {
                        if (objects.Count > 0)
                        {
                            var arrayFields = new Dictionary<int, List<object>>();
                            while (dr.Read())
                            {
                                List<object> a;
                                if (arrayFields.TryGetValue((int)(dr[0]), out a))
                                {
                                    a.Add(dr[1]);
                                }
                                else
                                {
                                    a = new List<object>();
                                    a.Add(dr[1]);
                                    arrayFields.Add((int)(dr[0]), a);
                                }
                            }
                            foreach (KeyValuePair<int, List<object>> item in arrayFields)
                            {
                                Dictionary<string, object> row;
                                if (objects.TryGetValue((int)(item.Key), out row))
                                {
                                    //adicionar toArray qd ISArray = true
                                    row.Add(dr.GetSchemaTable().Rows[1]["ColumnName"].ToString(), item.Value);
                                }
                            }
                        }
                        dr.Close();
                        isInExecution = false;
                    }
                }
                i++;
            }
            return objects;
        }





        public void RemoveField(Type persistentObjectType, string[] fieldsNames)
        {
            ExecuteStatement(GetRemoveFieldsText(persistentObjectType, fieldsNames));

        }
        public abstract string GetRemoveFieldsText(Type persistentObjectType, string[] fieldsNames);


        public void AddField(Type persistentObjectType, Dictionary<string, PersistentProperty> pps)
        {
            ExecuteStatement(GetAddFieldsText(persistentObjectType, pps));

        }
        public abstract string GetAddFieldsText(Type persistentObjectType, Dictionary<string, PersistentProperty> pps);

        public void AddArrayField(Type persistentObjectType, PersistentProperty idProperty, PersistentProperty arrayFieldProperty)
        {
            ExecuteStatement(GetAddArrayFieldText(persistentObjectType, idProperty, arrayFieldProperty));

        }
        public string GetAddArrayFieldText(Type persistentObjectType, PersistentProperty idProperty, PersistentProperty arrayFieldProperty)
        {
            var sbIsArray = new StringBuilder();
            var columnDDLText = GetColumnDDLText(arrayFieldProperty.PropertyInfo.Name, arrayFieldProperty);
            if (!string.IsNullOrEmpty(columnDDLText))
            {
                sbIsArray.Append(string.Format("\r\ncreate table {0}\r\n(\r\n", GetTableName(persistentObjectType.Name + "_" + arrayFieldProperty.PropertyInfo.Name)));
                sbIsArray.Append("\r\n\t");
                sbIsArray.Append(GetColumnDDLText("Id", idProperty));
                sbIsArray.Append("\r\n,\t");
                sbIsArray.Append(columnDDLText);
                sbIsArray.Append("\r\n)\r\n");
                return sbIsArray.ToString();
            }
            return null;
        }
    }
}
