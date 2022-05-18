using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using BusinessIntelligence.App.Marketing;
namespace BusinessIntelligence.App.Marketing.SmartEmail
{
    public class CampaignLoader
    {

        public List<EmailCampaign> EmailCampaignList = new List<EmailCampaign>();

        public void Load()
        {
            FillDeals();
        }


        private void FillDeals()
        {
            DbTools.Data.QueryExecutor q;
            System.Data.DataTable dt;
            EmailCampaignList.Clear();

            string connectionString = ConfigurationManager.ConnectionStrings["biConnection"].ConnectionString;

            System.Data.Common.DbConnection conn = new System.Data.SqlClient.SqlConnection(connectionString);

            q = new DbTools.Data.QueryExecutor(conn);

            String strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

            dt = q.ReturnData(fileContent(strAppDir + @"\Resources\Sql\ListCampaigns.sql"));

            foreach (DataRow cp in dt.Rows)
            {

                EmailCampaign ec1 = new EmailCampaign();
                ec1.emailBodyTemplate = new CommonEmailBodyTemplate();
                ec1.UsersAffected = Convert.ToInt32(cp["qty"]);
                if (Convert.ToInt32(cp["NeighborhoodGroup"]) == 0)
                {
                    ec1.GetUsersSqlCommand = "select * from ##GeneralUsers";
                }
                else
                {
                    ec1.GetUsersSqlCommand = "select UserAccountId,EmailAddress from helpdb.dbo.NeighborhoodUsersEmails where EmailCode = " + cp["EmailCode"].ToString();

                }
                int dealIndex = 1;
                int initialColumns = 7;
                int ColumnsForDeal = 5;

                int DealNameColumnOrder = 0;
                int DealOriginalValueColumnOrder = 1;
                int DealValueColumnOrder = 2;
                int DealUrlColumnOrder = 3;
                int DealImageUrlColumnOrder = 4;

                ec1.emailBodyTemplate.Subject = cp["EmailSubject"].ToString();
                ec1.emailBodyTemplate.utm_campaign = cp["utm_campaign"].ToString();
                ec1.emailBodyTemplate.MiddleDealTemplate = new CommonLightDealTemplate();
                ec1.emailBodyTemplate.TopDealTemplate = new OneTopDealTemplate();
                int i = 0;
                while (initialColumns + (dealIndex * ColumnsForDeal) <= cp.ItemArray.Length)
                {
                    i++;
                    int DealNameColumnIndex = initialColumns + ((dealIndex - 1) * ColumnsForDeal) + DealNameColumnOrder;
                    int DealOriginalValueColumnIndex = initialColumns + ((dealIndex - 1) * ColumnsForDeal) + DealOriginalValueColumnOrder;
                    int DealValueColumnIndex = initialColumns + ((dealIndex - 1) * ColumnsForDeal) + DealValueColumnOrder;
                    int DealUrlColumnIndex = initialColumns + ((dealIndex - 1) * ColumnsForDeal) + DealUrlColumnOrder;
                    int DealImageUrlColumnIndex = initialColumns + ((dealIndex - 1) * ColumnsForDeal) + DealImageUrlColumnOrder;


                    if (!DBNull.Value.Equals(cp[DealNameColumnIndex]))
                    {
                        HtmlDeal deal = new HtmlDeal();
                        deal.Name = (string)cp[DealNameColumnIndex];
                        deal.OriginalValue = (string)cp[DealOriginalValueColumnIndex];
                        deal.Value = (string)cp[DealValueColumnIndex];
                        deal.DealUrl = (string)cp[DealUrlColumnIndex];
                        deal.ImageUrl = (string)cp[DealImageUrlColumnIndex];
                        deal.utm_medium = "email";
                        deal.utm_source = "mailfish";
                        deal.utm_campaign = ec1.emailBodyTemplate.utm_campaign;
                        deal.utm_term = "";
                        /*
                        if (ec1.emailBodyTemplate.FeatureLayoutType == FeatureLayoutType.Feature1 && i == 1)
                        {
                            deal.DealTemplate = new OneTopDealTemplate();
                        }
                        else if (ec1.emailBodyTemplate.FeatureLayoutType == FeatureLayoutType.Features2 && i <= 2)
                        {
                            deal.DealTemplate = new TwoTopDealTemplate();
                        }
                        else
                        {
                 //           deal.DealTemplate = new CommonDealTemplate();
                            deal.DealTemplate = new CommonLightDealTemplate();
                        }
                         */
                        ec1.emailBodyTemplate.Deals.Add(deal);
                        dealIndex++;
                    }
                    else
                    {
                        break;
                    }

                }
                EmailCampaignList.Add(ec1);

            }



        }
        public string fileContent(string path)
        {
            string retorno;
            using (System.IO.StreamReader sr = new System.IO.StreamReader(path.Replace("file:\\", "")))
            {
                retorno = sr.ReadToEnd();
                sr.Close();
            }

            return retorno;
        }
    }
}
