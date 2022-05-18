using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.Data
{
   public class SqlServerLoader: DatabaseLoader
    {
       public SqlServerLoader(System.Data.Common.DbConnection connection, string schemaName, string tableName)
           : base(connection, schemaName, tableName)
        {
        }
       public override bool Load(DataTable dt)
       {
           using (SqlBulkCopy bc = new SqlBulkCopy((SqlConnection)this.Connection))
           {

               bc.DestinationTableName = SchemaName + "." + TableName;
               bc.BatchSize = 10000;
               bc.BulkCopyTimeout = 0;
               Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + string.Format(" Loading {0} rows in {1}.", dt.Rows.Count.ToString(), bc.DestinationTableName));
               bc.WriteToServer(dt);
           }
           return true;
       }
    }
}
