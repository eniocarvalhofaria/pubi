using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessIntelligence.Members.Marketing;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.App.Marketing.CampaignMaker
{
    public partial class FrmBegin : Form
    {
        public FrmBegin()
        {
            InitializeComponent();
        }
        FrmMain f;
        private void btBeginCreation_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Campaign campaign;
            List<Campaign> c = new List<Campaign>();
            campaign = Campaign.GetByCampaignName(txtCampaign.Text);
            if (campaign != null)
            {
                c.Add(campaign);
                f = new FrmMain();
                f.defineCampaigns(c);
            }
            else
            {
                campaign = Campaign.GetByCampaignName(txtCampaign.Text + "A");
                if (campaign != null)
                {

                    c.Add(campaign);

                    int ch = 65;
                    for (int i = 1; i <= 3; i++)
                    {
                        campaign = Campaign.GetByCampaignName(txtCampaign.Text + (char)(ch + i));
                        if (campaign != null)
                        {
                            c.Add(campaign);
                        }
                        else
                        {
                            break;
                        }
                    }
                    f = new FrmMain();
                    f.defineCampaigns(c);
                }
                else
                {
                    DateTime sentDate = DateTime.MinValue;
                    DateTime.TryParse("20" + txtCampaign.Text.Substring(5, 2) + "-" + txtCampaign.Text.Substring(7, 2) + "-" + txtCampaign.Text.Substring(9, 2), out sentDate);
                    if (sentDate.Equals(DateTime.MinValue))
                    {
                        MessageBox.Show("O formato de data está inválido. Após as 5 primeiras letras, deve-se colocar o ano com 2 posições, o mês com 2 posições, e o dia com 2 posições. Após isso pode se colocar qualquer coisa.");
                        return;
                    }
                    campaign = PersistenceSettings.PersistenceEngine.GetNewEmptyObject<Campaign>();
                    campaign.SentDate = sentDate;
                    btBeginCreation.Enabled = false;
                    f = new FrmMain();

                }
            }
            f.Text = txtCampaign.Text;
            f.ShowDialog();
        }

        private void FrmBegin_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Aguarde o teste de conexão");
            Cursor.Current = Cursors.WaitCursor;
            if (!Data.GetInstance().TryConnect())
            {
                MessageBox.Show("Você não está conectado ao banco de dados");
                Environment.Exit(1);
            }
            MessageBox.Show("Você está conectado ao banco de dados.");

            Cursor.Current = Cursors.Default;
        }

    }
}
