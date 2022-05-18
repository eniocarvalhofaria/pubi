using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
using BusinessIntelligence.Members.Marketing;
using System.IO;
using System.Web.Script.Serialization;
using System.IO;
using System.Data;
using System.Data.SqlClient;
namespace BusinessIntelligence.App.Marketing
{
    public class Data
    {

        BusinessIntelligence.Data.QueryExecutor mailingSelectorex;
        System.Data.Common.DbConnection cn;
        //     System.Data.Common.DbConnection cnEmail;
        BusinessIntelligence.Data.QueryExecutor ex;
        //     BusinessIntelligence.Data.QueryExecutor exEmail;
        SqlServerPersistenceEngine pe;
        static Data data;
        private Data(string[] databases)
        {

            cn = BusinessIntelligence.Data.Connections.GetNewConnection("APPPROD");
            //      cnEmail = BusinessIntelligence.Data.Connections.GetNewConnection(BusinessIntelligence.Data.Database.EMAILDELIVERY);
            ex = new BusinessIntelligence.Data.QueryExecutor(cn);
            //        exEmail = new BusinessIntelligence.Data.QueryExecutor(cnEmail);
            pe = new BusinessIntelligence.Persistence.SqlServerPersistenceEngine((System.Data.SqlClient.SqlConnection)BusinessIntelligence.Data.Connections.GetNewConnection("APPPROD"), "appprod");
        }
        public static Data GetInstance()
        {
            return GetInstance("APPPROD");
        }
        public static Data GetInstance(string[] databases)
        {
            if (data == null)
            {
                data = new Data(databases);
            }
            return data;
        }
        public static Data GetInstance(string database)
        {
            List<string> d = new List<string>();
            d.Add(database);
            return GetInstance(d.ToArray());
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
        public string Execute(string commandText)
        {
            ex.Execute(commandText);
            if (ex.ReturnCode > 0)
            {
                return ex.DatabaseMessage;
            }
            return "Comando executado com sucesso!";
        }

        public string CreateInMailfish(Campaign c)
        {
            // TODO Escrever código de integração. de preferência uma chamada de store procedure.
            return null;
        }
        public int GetMailingId(string campaignName)
        {
            var dt = ex.ReturnData(string.Format("select MailingId from dbo.EmailInfo where Campaign like '{0}%'", campaignName));
            if (dt.Rows.Count == 0)
            {
                return -1;
            }
            else return Convert.ToInt32(dt.Rows[0][0]);
        }
        public string LoadFile(string fileName, Campaign c)
        {
            var dt = new DataTable();
            dt.Columns.Add("UserAccountId", typeof(int));
            dt.Columns.Add("MailingUserTypeId", typeof(int));
            dt.Columns.Add("CampaignId", typeof(int));
            int CampaignId = c.Id;
            bool IsFirstLine = true;

            int mCount = 0;
            int cCount = 0;
            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(',');
                    DataRow r = dt.NewRow();
                    if (!IsFirstLine)
                    {
                        r["UserAccountId"] = Convert.ToInt32(line[1].Replace(".000000", ""));
                        if (line[2] != "Mailing" && line[2] != "Controle")
                        {
                            sr.Close();
                            sr.Dispose();
                            return "O conteudo da 3a coluna devem ser as palavras Mailing ou Controle.\r\nO arquivo não será carregado.";
                        }
                        if (line[2] == "Mailing")
                        {
                            r["MailingUserTypeId"] = 1;
                            mCount++;
                        }
                        else
                        {
                            r["MailingUserTypeId"] = 2;
                            cCount++;
                        }
                        if (line[3].Substring(0, 5) != "CRMBR")
                        {
                            sr.Close();
                            sr.Dispose();
                            return "O conteudo da 4a coluna devem ser o nome da campanha começando com CRMBR.\r\nO arquivo não será carregado.";
                        }
                        if (!line[3].Equals(c.Name))
                        {
                            return line[3] + " não se refere a campanha desta tela";
                        }


                        r["CampaignId"] = CampaignId;
                        dt.Rows.Add(r);
                    }
                    else
                    {
                        IsFirstLine = false;
                    }


                }
                sr.Close();
                sr.Dispose();


            }
            var cn = BusinessIntelligence.Data.Connections.GetNewConnection("APPPROD");
            var ex = new BusinessIntelligence.Data.QueryExecutor(cn);
            ex.Execute("delete from dbo.MailingUserAccount_stage");
            if (ex.ReturnCode != 0)
            {
                return ex.DatabaseMessage;
            }
            using (SqlBulkCopy bc = new SqlBulkCopy((SqlConnection)cn))
            {

                bc.DestinationTableName = "dbo.MailingUserAccount_stage";
                bc.BatchSize = 100000;
                bc.BulkCopyTimeout = 0;
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + string.Format(" Loading {0} rows in {1}.", dt.Rows.Count.ToString(), bc.DestinationTableName));
                bc.WriteToServer(dt);
            }
            ex.Execute("exec dbo.RefreshMailingUserAccount");
            if (ex.ReturnCode != 0)
            {
                return ex.DatabaseMessage;
            }
            c.MailingUsersCount = mCount;
            c.ControlUsersCount = cCount;
            c.Update();
            return "Arquivo carregado com sucesso!";
        }
        public List<Deal> GetDiscounts(string pageUrl, DateTime date)
        {
            StringBuilder sb = new StringBuilder();
            List<Deal> ret = new List<Deal>();
            int offSetPage = 0;
            int limit = 1000;
            using (var webClient = new System.Net.WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                while (true)
                {
                     var json = webClient.DownloadString(@"https://api.peixeurbano.com.br/v3/deals/" + pageUrl + @"/" + date.Year.ToString() + @"/" + date.Month.ToString("D2") + @"/" + date.Day.ToString("D2") + @"?limit="+limit.ToString()+"&offset=" + ( offSetPage * limit).ToString());
                    var serializer = new JavaScriptSerializer();
                    serializer.MaxJsonLength = int.MaxValue;
                    
                    DealsRootObject deserializedResult = serializer.Deserialize<DealsRootObject>(json);
                    ret.AddRange(deserializedResult.response.deals);
               //     if (deserializedResult.response.deals.Count < limit)
                    if (deserializedResult.response.deals.Count == 0)
                    {
                        break;
                    }
          
                    offSetPage++;
                }

            }
            return ret;
        }
        public List<Page> GetPages()
        {
            List<Discount> ret = new List<Discount>();
            StringBuilder sb = new StringBuilder();
            using (var webClient = new System.Net.WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                var json = webClient.DownloadString(@"http://api.peixeurbano.com.br/v3/pages");
                var serializer = new JavaScriptSerializer();
                PagesRootObject deserializedResult = serializer.Deserialize<PagesRootObject>(json);


                return deserializedResult.response;

            }
        }

    }
}
