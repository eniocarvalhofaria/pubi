using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
using BusinessIntelligence.Members.Financial;
using System.Data;
using System.Data.SqlClient;
namespace BusinessIntelligence.App.Financial
{
    public class Data
    {
        System.Data.Common.DbConnection cn;
        BusinessIntelligence.Data.QueryExecutor ex;
        SqlServerPersistenceEngine pe;
        public Data()
        {
            cn = BusinessIntelligence.Data.Connections.GetNewConnection("APPPROD");
            ex = new BusinessIntelligence.Data.QueryExecutor(cn);
            pe = new BusinessIntelligence.Persistence.SqlServerPersistenceEngine((SqlConnection)cn, "appprod");

            Tax IPI = new Tax();

            //    IPI.AccountsAffected



        }
        public bool TryConnect()
        {
            try
            {
                ex.Execute("select 1");
                return true;
            }
            catch (Exception exc)
            {
                return false;

            }
        }
        public DataTable GetAccountNotRegistered()
        {
            return ex.ReturnData("select * from  fin.AccountNotRegistered order by Total desc, Cod");
        }
    }
}
