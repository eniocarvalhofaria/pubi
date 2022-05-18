using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesForceCopyLoader
{
    public class SqlServerConnectionFactory:DbConnectionFactory
    {
        public SqlServerConnectionFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }
        protected override System.Data.Common.DbConnection GetNewConnection()
        {
            var a = new System.Data.SqlClient.SqlConnection(ConnectionString);
           
            return a;
        }


    }
}
