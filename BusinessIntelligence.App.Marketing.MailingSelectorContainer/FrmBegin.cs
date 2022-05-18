using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessIntelligence.Members.Marketing;
namespace BusinessIntelligence.App.Marketing.MailingSelectorContainer
{
    public partial class FrmBegin : Form
    {
        public FrmBegin()
        {
            InitializeComponent();
        }

        private void btBeginCreation_Click(object sender, EventArgs e)
        {
            campaigns.Clear();
            if (qtySlices.Enabled && qtySlices.Value > 1)
            {

                campaigns.Add(createCampaign(txtCampaign.Text + "A"));

                campaigns.Add(createCampaign(txtCampaign.Text + "B"));

                if (qtySlices.Value >= 3)
                {
                    campaigns.Add(createCampaign(txtCampaign.Text + "C"));
                }
                if (qtySlices.Value >= 4)
                {
                    campaigns.Add(createCampaign(txtCampaign.Text + "D"));
                }
            }
            else
            {
                campaigns.Add(createCampaign(txtCampaign.Text));
            }
            if (campaigns.Count == 0 || campaigns[0] == null)
            {
                return;
            }
            btBeginCreation.Enabled = false;
            qtySlices.Enabled = false;
            FrmMailingSelector frmMailingSelector = new FrmMailingSelector();
            frmMailingSelector.mailingSelector1.Campaigns = campaigns;
            frmMailingSelector.ShowDialog();
        }

        List<Campaign> campaigns = new List<Campaign>();
        public Campaign createCampaign(string campaignName)
        {
            Campaign campaign;
            campaign = Campaign.GetByCampaignName(campaignName);
            if (campaign != null)
            {
                return campaign;
            }
            campaign = BusinessIntelligence.Persistence.PersistenceSettings.PersistenceEngine.GetNewEmptyObject<Campaign>();
            campaign.Name = campaignName;
            DateTime sentDate = DateTime.MinValue;
            DateTime.TryParse("20" + campaignName.Substring(5, 2) + "-" + campaignName.Substring(7, 2) + "-" + campaignName.Substring(9, 2), out sentDate);
            if (sentDate.Equals(DateTime.MinValue))
            {
                MessageBox.Show("O formato de data está inválido. Após as 5 primeiras letras, deve-se colocar o ano com 2 posições, o mês com 2 posições, e o dia com 2 posições. Após isso pode se colocar qualquer coisa.");
                return null;
            }
            campaign.SentDate = sentDate;
            campaign.Create();
            return campaign;
        }

        private void btVerify_Click(object sender, EventArgs e)
        {
            lbSlices.Enabled = Campaign.GetByCampaignName(txtCampaign.Text) == null && Campaign.GetByCampaignName(txtCampaign.Text + "A") == null;
            qtySlices.Enabled = lbSlices.Enabled;
            btBeginCreation.Enabled = true;
        }

        private void FrmBegin_Load(object sender, EventArgs e)
        {

            MessageBox.Show("Aguarde o teste de conexão");
            Cursor.Current = Cursors.WaitCursor;
            var d = MailingSelectorData.GetInstance(BusinessIntelligence.Data.Database.REDSHIFT);
            if (!d.TryConnect())
            {
                MessageBox.Show("Você não está conectado ao banco de dados");
                Environment.Exit(1);
            }
        }
    }
}
