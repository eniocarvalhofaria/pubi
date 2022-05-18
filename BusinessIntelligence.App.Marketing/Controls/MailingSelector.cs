using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessIntelligence.Members.Marketing;
using BusinessIntelligence.Persistence;
using BusinessIntelligence.Persistence.Controls;
using System.Reflection;
using BusinessIntelligence.Controls;
namespace BusinessIntelligence.App.Marketing
{
    public partial class MailingSelector : UserControl
    {
        public MailingSelector()
        {
            InitializeComponent();
        }



        private void rdNeverBuy_CheckedChanged(object sender, EventArgs e)
        {
            pnPurchaseProfile.Enabled = rdBuyer.Checked;
            pnPurchaseAny.Enabled = rdBuyer.Checked || rdAny.Checked;
        }

        private void ckPurchaseProfile_CheckedChanged(object sender, EventArgs e)
        {
            rdNeverBuy.Enabled = ckPurchaseProfile.Checked;
            rdBuyer.Enabled = ckPurchaseProfile.Checked;
            rdAny.Enabled = ckPurchaseProfile.Checked;
            pnPurchaseProfile.Enabled = (ckPurchaseProfile.Checked && rdBuyer.Checked);
            pnPurchaseAny.Enabled = ckPurchaseProfile.Checked && (rdBuyer.Checked || rdAny.Checked);
        }




        public MailingSelectorData d;
        private bool _IsConnected;

        public bool IsConnected
        {
            get { return _IsConnected; }

        }
        private Site _CampaignSite;
    
        private void MailingSelector_Load(object sender, EventArgs e)
        {

            d = MailingSelectorData.GetInstance(mailingSelectorDatabase);
            if (!d.TryConnect())
            {
                MessageBox.Show("Você não conseguiu se conectar ao banco " + d.MailingDatabase.ToString());
                _IsConnected = false;
                ((Form)this.Parent).Close();
                return;
            }
            _IsConnected = true;
            MessageBox.Show("Aguarde algumas consultas ao banco de dados.");
            Gender.AddItem("Masculino");
            Gender.AddItem("Feminino");
            randomNumber = (DateTime.Now.Second + (DateTime.Now.Minute * 60)) % 100;


            BuyOffers.IdField = "UnifiedDiscountId";
            BuyOffers.NameField = "DiscountName";
            BuyOffers.QueryEecutor = d.MailingSelectorQueryExecutor;

            AgeRange.LoadAction = new Action(() =>
            {
                if (AgeRange.AllObjects.Count == 0)
                {
                    AgeRange.Populate<AgeRange>("Name");
                }
            });


            MailingList.LoadAction = new Action(() =>
            {
                if (MailingList.AllObjects.Count == 0)
                {
                    var ms = new SortExpression();
                    ms.Add("Name", Option.Ascending);
                    MailingList.SortExpression = ms;
                    MailingList.Populate<MailingList>("Name");
                }
            });
            CampaignExclusion.LoadAction = new Action(() =>
            {

                //CampaignExclusion.FilterExpression = BusinessIntelligence.Persistence.FilterExpressions.Custom("SentDate between dateadd(day,-30,'" + Campaigns[0].SentDate.ToString("yyyy-MM-dd") + "') and dateadd(day,-3,'" + Campaigns[0].SentDate.ToString("yyyy-MM-dd") + "')");
                CampaignExclusion.FilterExpression = BusinessIntelligence.Persistence.FilterExpressions.Custom("SentDate between dateadd(day,-30,'" + Campaigns[0].SentDate.ToString("yyyy-MM-dd") + "') and dateadd(day,0,'" + Campaigns[0].SentDate.ToString("yyyy-MM-dd") + "')");
                var s = new SortExpression();
                s.Add("SentDate", Option.Descending);
                s.Add("Name", Option.Descending);
                CampaignExclusion.SortExpression = s;
                CampaignExclusion.Populate<CampaignBasic>("Name");
                var campaignsofTheDay = Campaign.GetBySentDate(Campaigns[0].SentDate);

                if (campaignsofTheDay.Length > 0)
                {
                    CampaignExclusion.SelectItems(campaignsofTheDay);
                }


            });
            CampaignExclusion.Checked = true;
            CampaignExclusion.IsContained = false;
            customizedLists.LoadAction = new Action(() =>
            {
                if (this.customizedLists.AllObjects.Count == 0)
                {
                    var ms = new SortExpression();
                    ms.Add("Name", Option.Ascending);
                    this.customizedLists.SortExpression = ms;
                    customizedLists.Populate<CustomizedList>("Name");
                }
            });
            if (mailingSelectorDatabase == "REDSHIFT")
            {

                BuyOffers.QualifiedObjectName = "(select UnifiedDiscountId,Max(DiscountName) DiscountName from ods.discount where DiscountName not like 'FALSO%' group by UnifiedDiscountId) x";
            }
            else
            {
                BuyOffers.QualifiedObjectName = "(select UnifiedDiscountId,Max(DiscountName) DiscountName from pudw.ods.discount where DiscountName not like 'FALSO%' group by UnifiedDiscountId ) x";
            }

        }
        private int _QtySlices;

        public int QtySlices
        {
            get { return _QtySlices; }
            set { _QtySlices = value; }
        }


        private List<Campaign> _Campaigns = new List<Campaign>();

        public List<Campaign> Campaigns
        {
            get { return _Campaigns; }
            set { _Campaigns = value; }
        }
        int randomNumber;
        string semeados;
        private void GenerateSql()
        {



            string createCampaignExclusion = null;

            if (mailingSelectorDatabase == "REDSHIFT")
            {
                createCampaignExclusion = BusinessIntelligence.Util.EmbeddedResource.TextResource(Assembly.GetExecutingAssembly(), @"Resources.Sql.MailingCreationRedshift.txt");

            }
            else
            {
                createCampaignExclusion = BusinessIntelligence.Util.EmbeddedResource.TextResource(Assembly.GetExecutingAssembly(), @"Resources.Sql.MailingCreation.txt");

            }
            var sb = new StringBuilder();
            if (mailingSelectorDatabase == "REDSHIFT")
            {
                sb.Append("select EmailAddress,UserAccountId, Name into #MailingSelection from ods.UserAccount\r\nwhere (");

            }
            else
            {
                sb.Append("select EmailAddress,UserAccountId, Name into #MailingSelection from pudw.ods.UserAccount\r\nwhere (");
            }

            if (birthDate.Checked)
            {
                //      string fieldExpression = BusinessIntelligence.Util.EmbeddedResource.TextResource(Assembly.GetExecutingAssembly(), @"Resources.Sql.Aniversary.txt").Replace("<@aniversary@>", "BirthDay");
                sb.Append(" and " + birthDate.GetExpression("Anniversary", Campaigns[0].SentDate));


            }
            if (facebookConnect.Checked)
            {
                sb.Append(" and " + facebookConnect.GetExpression("usesFacebookLogin", " = 1", " = 0") + "\r\n");
            }
            if (Gender.Checked && !string.IsNullOrEmpty(Gender.SelectedItem))
            {
                if (mailingSelectorDatabase == "REDSHIFT")
                {
                    if (Gender.SelectedItem.Equals("Masculino"))
                    {
                        sb.Append(" and Female = 0\r\n");
                    }
                    else
                    {
                        sb.Append(" and Female = 1\r\n");
                    }

                }
                else
                {
                    if (Gender.SelectedItem.Equals("Masculino"))
                    {
                        sb.Append(" and Genre = 'M'\r\n");
                    }
                    else
                    {
                        sb.Append(" and Genre = 'F'\r\n");
                    }
                }
            }
            if (dtsUserRegistered.Checked)
            {
                var exp = dtsUserRegistered.GetExpression("DateRegistered", Campaigns[0].SentDate);
                if (!string.IsNullOrEmpty(exp))
                {
                    sb.Append(" and " + exp);
                }
            }
            if (AgeRange.Checked)
            {
                var exp = GetSelectionExpression("AgeRangeId", AgeRange);
                if (!string.IsNullOrEmpty(exp))
                {
                    sb.Append(" and " + exp);
                }
            }
            if (customizedLists.Checked)
            {
                var exp = GetCustomizedListSelectionExpression(customizedLists);
                if (!string.IsNullOrEmpty(exp))
                {
                    sb.Append(" and " + exp);
                }
            }
            if (formCategories != null)
            {
                var exp = formCategories.GetExpression();
                if (!string.IsNullOrEmpty(exp))
                {
                    sb.Append(" and UserAccountId in \r\n" + exp);
                }
            }
            if (mailingSelectorDatabase == "REDSHIFT")
            {
                if (MailingList.Checked)
                {
                    sb.Append(" and UserAccountId in (select UserAccountId from ods.MailingList_UserAccount where  " + GetSelectionExpression("MailingListID", MailingList).Replace("\r\n", "") + ") \r\n");
                }
                else
                {
                    sb.Append(" and UserAccountId in (select UserAccountId from ods.MailingList_UserAccount)\r\n");
                }
            }
            else
            {
                if (MailingList.Checked)
                {
                    sb.Append(" and UserAccountId in (select UserAccountId from pudw.stage.MailingList_UserAccount where  " + GetSelectionExpression("MailingListID", MailingList).Replace("\r\n", "") + ") \r\n");
                }
                else
                {
                    sb.Append(" and UserAccountId in (select UserAccountId from pudw.stage.MailingList_UserAccount)\r\n");
                }
                sb.Append(" and CountryId = 1\r\n");
            }
            if (CampaignSite == Marketing.Site.PeixeUrbano)
            {
                sb.Append(" and ChannelId = 1\r\n");

            }
            else if (CampaignSite == Marketing.Site.Groupon)
            {
                sb.Append(" and ChannelId = 6\r\n");
            }
           

            if (CampaignExclusion.Checked && CampaignExclusion.SelectedObjects.Count > 0)
            {
                createCampaignExclusion = createCampaignExclusion.Replace("<@CampaignExclusion@>", GetSelectionExpression("CampaignID", CampaignExclusion).Replace("\r\n", ""));
                sb.Append(" and UserAccountId not in (select UserAccountId from #MailingExclusion)\r\n");// where  " + GetSelectioExpression("CampaignID", CampaignExclusion, true).Replace("\r\n", "") + ") \r\n");

            }
            else
            {
                createCampaignExclusion = createCampaignExclusion.Replace("<@CampaignExclusion@>", "1 = 0");
            }

            if (this.Engagement.Checked)
            {
                sb.Append(" and " + Engagement.GetExpression("Engagement"));
            }
            if (this.Status.Checked)
            {
                sb.Append(" and " + Status.GetExpression("Status"));
            }

            if (ckPurchaseProfile.Checked)
            {
                if (rdNeverBuy.Checked)
                {

                    sb.Append(" and DateFirstPurchase is null \r\n");
                }
                else if (rdBuyer.Checked || rdAny.Checked)
                {
                    if (this.HasAppPurchase.Checked)
                    {
                        string trueExpression;
                        string falseExpression;
                        trueExpression = " in (select UserAccountId from pudw.ods.sales where SourceID = 2)";
                        falseExpression = " not " + trueExpression;
                        if (mailingSelectorDatabase == "REDSHIFT")
                        {
                            trueExpression = trueExpression.Replace("pudw.ods", "ods");
                            falseExpression = falseExpression.Replace("pudw.ods", "ods");
                        }
                        sb.Append(" and " + HasAppPurchase.GetExpression("UserAccountId", trueExpression, falseExpression));
                    }
                    if (this.LastPurchaseUsePromocode.Checked)
                    {
                        string trueExpression;
                        string falseExpression;
                        trueExpression = " in (select\r\n" +
                                         "UserAccountId\r\n" +
                                         "from\r\n" +
                                         "(\r\n" +
                                        "\tselect\r\n" +
                                        "UserAccountId\r\n" +
                                         ",	Promocodes\r\n" +
                                        ",	rank() over(partition by UserAccountId order by PurchaseID desc) ordem\r\n" +
                                         "from pudw.ods.Sales\r\n" +
                                        ") x\r\n" +
                                        "where ordem = 1\r\n" +
                                        "and Promocodes > 0\r\n" +
                                        ")\r\n";
                        falseExpression = " not " + trueExpression;
                        if (mailingSelectorDatabase == "REDSHIFT")
                        {
                            trueExpression = trueExpression.Replace("pudw.ods", "ods");
                            falseExpression = falseExpression.Replace("pudw.ods", "ods");
                        }
                        sb.Append(" and " + LastPurchaseUsePromocode.GetExpression("UserAccountId", trueExpression, falseExpression));
                    }
                    if (this.BuyOffers.Checked)
                    {
                        string templateExpression = null;
                        if (mailingSelectorDatabase == "REDSHIFT")
                        {
                            templateExpression = "  UserAccountId {0} (select UserAccountId from ods.sales where UnifiedDiscountId in ({1}) and type = 1)";
                        }
                        else
                        {
                            templateExpression = "  UserAccountId {0} (select UserAccountId from pudw.ods.sales where UnifiedDiscountId in ({1}) and type = 1)";

                        }
                        BuyOffers.TemplateExpression = templateExpression;
                        sb.Append(" and " + BuyOffers.GetExpression());
                    }
                    if (rdBuyer.Checked)
                    {
                        sb.Append(" and DateFirstPurchase is not null \r\n");
                        if (dateLastPurchase.Checked)
                        {
                            sb.Append(" and " + dateLastPurchase.GetExpression("DateLastPurchase", Campaigns[0].SentDate));
                        }
                        if (this.PromocodeName.Checked)
                        {
                            string promocodeNameExpression = " and UserAccountId in \r\n" +
        "(\r\n" +
        "select distinct UserAccountId\r\n" +
        "from	stage.PromotionalCode B\r\n" +
        "inner join stage.LogPromotionalCodeUse A\r\n" +
        "on A.PromotionalCodeID=B.PromotionalCodeID\r\n" +
        "inner join ods.sales C\r\n" +
        "on A.PurchaseID = C.PurchaseID\r\n" +
        "where " + PromocodeName.GetExpression("lower(b.Code)").ToLower() + "\r\n" +
        ")\r\n";
                            sb.Append(promocodeNameExpression);
                        }
                        if (this.NetRevenue.Checked)
                        {
                            sb.Append(" and " + NetRevenue.GetExpression("NetRevenue"));
                        }
                        if (this.QTYPurchases.Checked)
                        {
                            sb.Append(" and " + QTYPurchases.GetExpression("TotalPurchases"));
                        }
                        if (this.maxPurchaseValue.Checked)
                        {
                            sb.Append(" and " + maxPurchaseValue.GetExpression("MaximumPurchaseValue"));
                        }
                        if (this.Promocodes.Checked)
                        {
                            sb.Append(" and " + Promocodes.GetExpression("CountOfValePresente"));
                        }

                    }
                }
            }
            sb.Replace("where ( and", "where (");
            semeados = null;
            if (string.IsNullOrEmpty(txtSemeados.Text))
            {
                sb.Append(")");
                semeados = "1 <> 1";
            }
            else
            {
                sb.Append(") or UserAccountId in (");
                sb.Append(txtSemeados.Text);
                sb.Append(")");
                semeados = " UserAccount in (" + txtSemeados.Text + ")";
            }
            string campaignInsert = null;
            string campaignIds = null;
            string templateSplit = BusinessIntelligence.Util.EmbeddedResource.TextResource(Assembly.GetExecutingAssembly(), @"Resources.Sql.MailingSplit.txt");
            if (mailingSelectorDatabase == "REDSHIFT")
            {
                templateSplit = templateSplit.Replace("dbo", "reports");
            }

            if (Campaigns.Count == 1)
            {
                campaignInsert = ";" + templateSplit.Replace("<@CampaignId@>", Campaigns[0].Id.ToString()).Replace("<@finalMod@>", (percControl.Value - 1).ToString()).Replace("<@CampaignWhere@>", "(UserAccountId + <@RandomNumber@>) % 100 between " + (percControl.Value - 1).ToString() + " and 100");
            }
            else
            {
                int step = Convert.ToInt32(((100 - percControl.Value) / Campaigns.Count));
                int i = 0;
                foreach (Campaign c in Campaigns)
                {
                    campaignInsert += "\r\n;" + templateSplit.Replace("<@CampaignId@>", c.Id.ToString()).Replace("<@finalMod@>", (percControl.Value - 1).ToString()).Replace("<@CampaignWhere@>", "(UserAccountId + <@RandomNumber@>) % " + Campaigns.Count + " = " + i.ToString());
                    i++;
                }

            }
            sb.Append(campaignInsert);


            foreach (Campaign c in Campaigns)
            {
                campaignIds += "," + c.Id.ToString();
            }
            campaignIds = campaignIds.Substring(1);
            txtSql.Text = createCampaignExclusion.Replace("<@QuerySelection@>", sb.ToString()).Replace("<@RandomNumber@>", randomNumber.ToString()).Replace("<@finalMod@>", (percControl.Value - 1).ToString()).Replace("<@CampaignIds@>", campaignIds).Replace("<@Semeados@>", semeados).Replace("<@NotSemeados@>", semeados.Replace("in", "not in").Replace("<>", "=")).Replace("<@ReferenceDate@>", Campaigns[0].SentDate.ToString("yyyy-MM-dd"));

            countsSql = BusinessIntelligence.Util.EmbeddedResource.TextResource(Assembly.GetExecutingAssembly(), @"Resources.Sql.CountMailing.txt");
            countsSql = countsSql.Replace("<@CampaignIds@>", campaignIds);
            if (mailingSelectorDatabase == "REDSHIFT")
            {
                countsSql = countsSql.Replace("dbo", "reports");
            }
        }
        string countsSql = null;
        private string GetCustomizedListSelectionExpression(StoredObjectSelector so)
        {
            bool isFirst = true;
            string ret = null;
            foreach (CustomizedList item in so.SelectedObjects)
            {
                if (isFirst)
                {
                    ret = item.SqlText;
                    isFirst = false;
                }
                else
                {
                    ret += "or " + item.SqlText;
                }
            }
            if (!string.IsNullOrEmpty(ret))
            {
                if (so.IsContained)
                {
                    return " (" + ret + ")\r\n";
                }
                else
                {
                    return " not (" + ret + ")\r\n";
                }
            }
            else
            { return null; }
        }
        private string GetSelectionExpression(string fieldName, StoredObjectSelector so)
        {
            bool isFirst = true;
            string ret = null;

            foreach (var item in so.SelectedObjects)
            {
                if (isFirst)
                {
                    ret = item.Id.ToString();
                    isFirst = false;
                }
                else
                {
                    ret += ", " + item.Id.ToString();
                }
            }
            if (!string.IsNullOrEmpty(ret))
            {
                if (so.IsContained)
                {
                    return fieldName + " in (" + ret + ")\r\n";
                }
                else
                {
                    return fieldName + " not in (" + ret + ")\r\n";
                }
            }
            else
            { return null; }

        }

        private void ckAgeRange_CheckedChanged(object sender, EventArgs e)
        {

        }
        List<string> seededs = new List<string>();
        private void btGenerateMailing_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSql.Text))
            {
                GenerateSql();
            }
            MessageBox.Show(d.GenerateMailing(txtSql.Text));
            d.RefreshCounts(countsSql, Campaigns);
            foreach (var c in Campaigns)
            {
                MessageBox.Show("Na campanha " + c.Name + " foram gerados " + c.MailingUsersCount.ToString("#,###.") + " usuários de mailing, e " + c.ControlUsersCount.ToString("#,###.") + " foram separados como controle.");
                c.MailingSelectorSql = txtSql.Text.Split(';')[5];
                c.Update();

            }
            btExportMailing.Enabled = true;
            btGenerateMailing.Enabled = false;
            _MailingWasGenerated = true;
        }

        private void btGenerateSql_Click(object sender, EventArgs e)
        {

            GenerateSql();
            btGenerateMailing.Enabled = true;
            btDeleteMailing.Enabled = true;
        }

        private void exportMailing_FileOk(object sender, CancelEventArgs e)
        {

            Application.DoEvents();

        }
        string lastCampaignName = "";

        private void btClear_Click(object sender, EventArgs e)
        {
            AgeRange.Checked = false;
            CampaignExclusion.Checked = false;



            birthDate.Checked = false;
            facebookConnect.Checked = false;
            MailingList.ClearSelection();
            AgeRange.ClearSelection();
            CampaignExclusion.ClearSelection();
            dtsUserRegistered.ClearSelection();
            Engagement.ClearSelection();
            dateLastPurchase.ClearSelection();
            QTYPurchases.ClearSelection();
            dateSelector2.ClearSelection();
            Promocodes.ClearSelection();
            NetRevenue.ClearSelection();
            birthDate.ClearSelection();
            txtSql.Text = "";
            if (formCategories != null)
            {
                formCategories.ClearSelection();
            }
        }

        private void btExportMailing_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            //MessageBox.Show(fbd.SelectedPath);
            seededs = new List<string>();
            seededs.AddRange(txtSeededs.Text.Replace("\n", ",").Replace("\r", ",").Replace(",,", ",").Split(','));
            string templateExport = BusinessIntelligence.Util.EmbeddedResource.TextResource(Assembly.GetExecutingAssembly(), @"Resources.Sql.ExportMailing.txt");
            int i = 0;
            foreach (Campaign c in Campaigns)
            {
                d.ExportMailing(templateExport.Replace("<@CampaignId@>", c.Id.ToString()).Replace("<@Semeados@>", semeados).Replace("<@finalMod@>", (percControl.Value - 1).ToString()).Replace("<@RandomNumber@>", randomNumber.ToString()).Replace("<@CampaignWhere@>", "(UserAccountId + " + randomNumber + ") % " + Campaigns.Count + " = " + i.ToString()), fbd.SelectedPath + "\\" + c.Name + ".csv", seededs);
                i++;
            }
            MessageBox.Show("Mailings gerados com sucesso!");
        }

        private void btClose_Click(object sender, EventArgs e)
        {

            ((Form)this.Parent).Close();
        }

        private void btModify_Click(object sender, EventArgs e)
        {
            btGenerateSql.Enabled = true;
        }
        private bool _MailingWasGenerated = false;

        public bool MailingWasGenerated
        {
            get { return _MailingWasGenerated; }

        }

        public Site CampaignSite
        {
            get
            {
                return _CampaignSite;
            }

            set
            {
                _CampaignSite = value;
                if (_CampaignSite == Marketing.Site.PeixeUrbano && !rdPU.Checked)
                {
                    rdPU.Checked = true;
                }
                else {
                    rdGroupon.Checked = true;
                }
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            _MailingWasGenerated = false;
            btDeleteMailing.Enabled = false;
            btExportMailing.Enabled = false;
            btGenerateMailing.Enabled = false;
            MessageBox.Show(d.DeleteMailing(Campaigns));
        }
        string mailingSelectorDatabase = "REDSHIFT";



        FormCategories formCategories;
        private void btCategories_Click(object sender, EventArgs e)
        {
            if (formCategories == null)
            {
                formCategories = new FormCategories();
            }

            formCategories.ShowDialog();

        }

        private void rdPU_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPU.Checked)
            {
                _CampaignSite = Marketing.Site.PeixeUrbano;
            }
            else
            {
                _CampaignSite = Marketing.Site.Groupon;
            }
        }
    }
}
