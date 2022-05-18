using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BusinessIntelligence.Persistence;
using BusinessIntelligence.Members.Marketing;
using BusinessIntelligence.Authentication;
using BusinessIntelligence.Data.Redshift;
using BusinessIntelligence.Data;
using System.IO;
namespace BusinessIntelligence.App.Marketing
{
    public class MailingSelectorData
    {
        public struct Result
        {
            public bool Sucess;
            public string Message;
        }
        static Dictionary<string, MailingSelectorData> instances = new Dictionary<string, MailingSelectorData>();
        /*
          public static MailingSelectorData GetInstance()
          {
              if (instances.Count > 0)
              { 
              return 
              }

                  return GetInstance(BusinessIntelligence.Data.Database.REDSHIFT);
            
          }
         * */
        public static MailingSelectorData GetInstance(string mailingDatabase)
        {
            if (instances.ContainsKey(mailingDatabase))
            {
                return instances[mailingDatabase];
            }
            else
            {

                return new MailingSelectorData(mailingDatabase);
            }
        }
            SqlServerPersistenceEngine pe;
        private MailingSelectorData(string mailingDatabase)
        {
            MailingDatabase = mailingDatabase;
            var mailingConnection = BusinessIntelligence.Data.Connections.GetNewConnection(MailingDatabase);
            MailingConnection = mailingConnection;
            MailingSelectorQueryExecutor = new BusinessIntelligence.Data.QueryExecutor(MailingConnection);
            pe = new BusinessIntelligence.Persistence.SqlServerPersistenceEngine((System.Data.SqlClient.SqlConnection)BusinessIntelligence.Data.Connections.GetNewConnection("APPPROD"), "appprod");
            instances.Add(MailingDatabase, this);
        }
        public Result CreateListFromFile(string fileName, CustomizedList customizedList)
        {
            Result ret = new Result();
            DataTable dt = new DataTable();
            bool typeDefined = false;
            bool isEmail = false;
            string fieldName = null;
            string fieldDataType = null;
            using (StreamReader sr = new StreamReader(fileName))
            {

                while (!sr.EndOfStream)
                {
                    int UserAccountId;
                    string linha = sr.ReadLine();
                    if (!string.IsNullOrEmpty(linha.Trim()))
                    {
                        if (!typeDefined)
                        {
                            if (linha.Contains("@"))
                            {
                                isEmail = true;
                                typeDefined = true;
                                fieldName = "emailAddress";
                                fieldDataType = " varchar(100) ";
                                dt.Columns.Add(new DataColumn("emailAddress", typeof(string)));
                                DataRow r = dt.NewRow();
                                r[0] = linha;
                                dt.Rows.Add(r);
                            }
                            else if (Int32.TryParse(linha, out UserAccountId))
                            {
                                isEmail = false;
                                typeDefined = true;
                                fieldName = "userAccountId";
                                fieldDataType = " int ";
                                dt.Columns.Add(new DataColumn("userAccountID", typeof(int)));
                                DataRow r = dt.NewRow();
                                r[0] = UserAccountId;
                                dt.Rows.Add(r);
                            }
                        }
                        else
                        {
                            if (isEmail)
                            {
                                if (linha.Contains("@"))
                                {
                                    DataRow r = dt.NewRow();
                                    r[0] = linha;
                                    dt.Rows.Add(r);
                                }
                                else
                                {
                                    ret.Sucess = false;
                                    ret.Message = linha + " não é um email válido.";
                                    return ret;
                                }
                            }
                            else
                            {
                                if (Int32.TryParse(linha, out UserAccountId))
                                {
                                    DataRow r = dt.NewRow();
                                    r[0] = UserAccountId;
                                    dt.Rows.Add(r);
                                }
                                else
                                {
                                    ret.Sucess = false;
                                    ret.Message = linha + " não é um UserAccountId válido.";
                                    return ret;
                                }
                            }
                        }
                    }
                }
                sr.Close();
            }
            string tableName = "cmAuxListTemp";
            string fieldExpression = fieldName + " " + fieldDataType;
            MailingSelectorQueryExecutor.Execute("drop table if exists reports." + tableName);
            if (MailingSelectorQueryExecutor.ReturnCode > 0)
            {
                ret.Sucess = false;
                ret.Message = MailingSelectorQueryExecutor.DatabaseMessage;
            }
            MailingSelectorQueryExecutor.Execute("create table reports." + tableName + " (" + fieldExpression + " ) distkey(" + fieldName + ")" + " sortkey(" + fieldName + ")");
            if (MailingSelectorQueryExecutor.ReturnCode > 0)
            {
                ret.Sucess = false;
                ret.Message = MailingSelectorQueryExecutor.DatabaseMessage;
            }
            MailingSelectorQueryExecutor.Execute("grant all  privileges on  table reports." + tableName + " to group campaignmaker");
            if (MailingSelectorQueryExecutor.ReturnCode > 0)
            {
                ret.Sucess = false;
                ret.Message = MailingSelectorQueryExecutor.DatabaseMessage;
            }
            try
            {
                var loader = new RedshiftLoader(Connections.GetNewConnection("REDSHIFT"), "reports", tableName);
                loader.Load(dt);

                customizedList.Create();

                string newTableName = "cmAuxList" + (customizedList.Id + 100000).ToString().Substring(1, 5);
                MailingSelectorQueryExecutor.Execute("alter table reports." + tableName + " rename to " + newTableName);
                if (isEmail)
                {
                    customizedList.SqlText = "EmailAddress in (select EmailAddress from reports." + newTableName + " )";
                }
                else
                {
                    customizedList.SqlText = "UserAccountId in (select UserAccountId from reports." + newTableName + " )";

                }
                customizedList.Update();

                ret.Sucess = true;
                ret.Message = "Lista criada.";
                return ret;
            }
            catch (Exception ex)
            {
                ret.Sucess = false;
                ret.Message = ex.Message;
                return ret;
            }
        }
        public Result TestSqlList(string sqlText)
        {
            var ret = new Result();
            var dt = MailingSelectorQueryExecutor.ReturnData("select count(*) qty from (" + sqlText + ") x");
            if (MailingSelectorQueryExecutor.ReturnCode > 0)
            {
                ret.Sucess = false;
                ret.Message = MailingSelectorQueryExecutor.DatabaseMessage;
            }
            else
            {
                if (dt != null && dt.Rows.Count == 1)
                {
                    ret.Sucess = true;
                    ret.Message = "No momento a sua query retornaria " + dt.Rows[0][0].ToString() + " registros.";
                }
                else
                {
                    ret.Sucess = true;
                    ret.Message = "No momento a sua query não retornaria registros.";

                }

            }
            return ret;
        }
        private System.Data.Common.DbConnection _MailingConnection;

        public System.Data.Common.DbConnection MailingConnection
        {
            get { return _MailingConnection; }
            set { _MailingConnection = value; }
        }
        private string _MailingDatabase;

        public string MailingDatabase
        {
            get { return _MailingDatabase; }
            set { _MailingDatabase = value; }
        }
        private BusinessIntelligence.Data.QueryExecutor _MailingSelectorQueryExecutor;

        public BusinessIntelligence.Data.QueryExecutor MailingSelectorQueryExecutor
        {
            get { return _MailingSelectorQueryExecutor; }
            set { _MailingSelectorQueryExecutor = value; }
        }
        public string GenerateMailing(string commandText)
        {

            BusinessIntelligence.Controls.LogWindow log = new BusinessIntelligence.Controls.LogWindow();
            var queries = commandText.Split(';');
            log.Show();
            foreach (var query in queries)
            {
                log.WriteMessage(query);
                System.Windows.Forms.Application.DoEvents();
                MailingSelectorQueryExecutor.Execute(query);
                if (MailingSelectorQueryExecutor.ReturnCode > 0)
                {
                    log.WriteMessage(MailingSelectorQueryExecutor.DatabaseMessage + "\r\n");
                    return MailingSelectorQueryExecutor.DatabaseMessage;
                }
                else
                {
                    log.WriteMessage(MailingSelectorQueryExecutor.RecordsAffected.ToString() + " Registros afetados.\r\n");
                }
            }
            return "Mailing gerado com sucesso!";
        }
        public string ExportMailing(string commandText, string fileName, List<string> seededs)
        {
            int qty = 0;
            qty = MailingSelectorQueryExecutor.ExportFile(MailingSelectorQueryExecutor.ReturnDataReader(commandText), fileName, true, "\r\n", ",");
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, true))
            {
                foreach (string s in seededs)
                {
                    if (s.Contains("@"))
                    {
                        sw.WriteLine(s + ",0,Prezado Semeado");
                    }
                }
                sw.Close();
                sw.Dispose();
            }
            if (MailingSelectorQueryExecutor.ReturnCode != 0)
            {
                return "Erro na geração e exportação\r\n" + MailingSelectorQueryExecutor.DatabaseMessage;
            }

            return "Mailing exportado com sucesso (" + qty.ToString("#,###.") + " registros).";
        }
        public void RefreshCounts(string countCommandText, List<Campaign> campaigns)
        {
            var counts = MailingSelectorQueryExecutor.ReturnData(countCommandText);
            foreach (var c in campaigns)
            {
                c.MailingUsersCount = 0;
                c.ControlUsersCount = 0;
                foreach (DataRow r in counts.Rows)
                {
                    if (Convert.ToInt32(r[0]) == c.Id)
                    {
                        c.MailingUsersCount = Convert.ToInt32(r[1]);
                        c.ControlUsersCount = Convert.ToInt32(r[2]);
                        c.Update();
                        break;
                    }
                }
            }

        }
        public bool TryConnect()
        {
            try
            {
                MailingSelectorQueryExecutor.Execute("select 1");
                return true;
            }
            catch (Exception exc)
            {
                return false;

            }
        }
        public string DeleteMailing(List<Campaign> campaigns)
        {
            string templateDelete = "delete from dbo.MailingUserAccount where CampaignId in (<@CampaignIds@>)";
            if (MailingDatabase == "REDHIFT")
            {
                templateDelete = templateDelete.Replace("dbo", "reports");
            }

            string campaignIds = null;
            foreach (Campaign c in campaigns)
            {
                campaignIds += "," + c.Id.ToString();
            }
            campaignIds = campaignIds.Substring(1);
            MailingSelectorQueryExecutor.Execute(templateDelete.Replace("<@CampaignIds@>", campaignIds));
            if (MailingSelectorQueryExecutor.ReturnCode == 0)
            {
                return "Mailing deletado com sucesso!";
            }
            else
            {
                return MailingSelectorQueryExecutor.DatabaseMessage;

            }
        }

    }
}
