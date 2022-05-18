using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesForceCopyLoader
{
    public class OdbcConnectionFactory:DbConnectionFactory
    {
        public OdbcConnectionFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }
        protected override System.Data.Common.DbConnection GetNewConnection()
        {
            return new System.Data.Odbc.OdbcConnection(ConnectionString);
        }


    }
}
