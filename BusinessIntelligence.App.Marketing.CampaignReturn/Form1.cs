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
using System.IO;
namespace BusinessIntelligence.App.Marketing.CampaignReturn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        BusinessIntelligence.App.Marketing.Data d;
        private void OnbtGetMailingIdClick(object sender, EventArgs e)
        {
            lblMailingId.Text = "Buscando...";
            Application.DoEvents();
            int mailingId = d.GetMailingId(txtCampaign.Text);
            if (mailingId == -1)
            {
                lblMailingId.Text = "0";
                MessageBox.Show("Esta campanha não foi criada no Mailfish!");
                return;
            }
            else
            {
                lblMailingId.Text = mailingId.ToString();


            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            MessageBox.Show("Aguarde o teste de conexão");
            Cursor.Current = Cursors.WaitCursor;
            d =  Data.GetInstance();
            if (!d.TryConnect())
            {
                MessageBox.Show("Você não está conectado ao banco de dados");
                Environment.Exit(1);
            }
            MessageBox.Show("Você está conectado ao banco de dados.\r\nAguarde a listagem das páginas. ");
      
            Cursor.Current = Cursors.Default;
        }

        private void btSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "csv files (*.csv)|*.csv";
            openFileDialog1.ShowDialog();
            txtFile.Text = openFileDialog1.FileName;
        }
        Campaign campaign = null;
        string lastCampaignName = "";
  
        bool IsUpdate = false;
        private void OntxtCampaignLeave(object sender, EventArgs e)
        {
            if (!lastCampaignName.Equals(txtCampaign.Text))
            {
                IsUpdate = false;
                campaign = Campaign.GetByCampaignName(txtCampaign.Text);
                lastCampaignName = txtCampaign.Text;
                lblMailingId.Text = "";
                txtFile.Text = "";
                dealsSelector1.ClearDeals();
                dealsSelector1.ClearPages();
                btRegisterDeals.Enabled = false;
                txtCountMailing.Text = "";
                txtCountControl.Text = "";
                txtDealsCount.Text = "";
              
            }
            if (campaign != null)
            {
                IsUpdate = true;
                SelectCampaign(campaign);
            }
            else
            {
                dealsSelector1.Enabled = false;
                btCreateCampaign.Text = "Cadastrar campanha";
            }
        }
        private void SelectCampaign(Campaign c)
        {
            txtCategory.Text = c.CategoryDescription;
            txtPartnerDescription.Text = c.PartnerDescription;
            txtPublic.Text = c.PublicDescription;
            txtPromocode.Text = c.Promocode;
            txtDescription.Text = c.Description;
            txtCountMailing.Text = c.MailingUsersCount.ToString("#,###.");
            txtCountControl.Text = c.ControlUsersCount.ToString("#,###.");
            txtDealsCount.Text = c.UnifiedDiscounts.Count.ToString("#,###.");
            lblMailingId.Text = campaign.MailingId.ToString();
            dealsSelector1.Enabled = true;
            btRegisterDeals.Enabled = true;
            dealsSelector1.Date = c.SentDate;
            grMailing.Enabled = true;
            btRegisterDeals.Enabled = true;
            btCreateCampaign.Text = "Atualizar campanha";
        }

        private void OnbtnCreateCampaignClick(object sender, EventArgs e)
        {

            DateTime sentDate = DateTime.MinValue;
            DateTime.TryParse("20" + txtCampaign.Text.Substring(5, 2) + "-" + txtCampaign.Text.Substring(7, 2) + "-" + txtCampaign.Text.Substring(9, 2), out sentDate);
            if (sentDate.Equals(DateTime.MinValue))
            {
                MessageBox.Show("O formato de data está inválido. Após as 5 primeiras letras, deve-se colocar o ano com 2 posições, o mês com 2 posições, e o dia com 2 posições. Após isso pode se colocar qualquer coisa.");
                return;
            }
            if (string.IsNullOrEmpty(lblMailingId.Text))
            {
                MessageBox.Show("Esta campanha não foi criada no Mailfish!");
                return;
            }
            if (campaign != null)
            {
                IsUpdate = true;
            }
            else
            {
                campaign = PersistenceSettings.PersistenceEngine.GetNewEmptyObject<Campaign>();
            }
            campaign.CategoryDescription = txtCategory.Text;
            campaign.PartnerDescription = txtPartnerDescription.Text;
            campaign.PublicDescription = txtPublic.Text;
            campaign.Promocode = txtPromocode.Text;
            campaign.Name = txtCampaign.Text;
            campaign.Description = txtDescription.Text;
            try
            {
                campaign.MailingId = Convert.ToInt32(lblMailingId.Text);
            }
            catch (Exception ex)
            {
                campaign.MailingId = 0;
            }
            campaign.SentDate = sentDate;
            dealsSelector1.Enabled = true;
            dealsSelector1.Date = sentDate;
            grMailing.Enabled = true;
            btRegisterDeals.Enabled = true;
            if (IsUpdate)
            {
                campaign.Update();
                MessageBox.Show("Campanha atualizada com sucesso!");
            }
            else
            {
                campaign.Create();
                MessageBox.Show("Campanha cadastrada com sucesso!");
            }

        }

        private void OnUpdateOfertas(object sender, EventArgs e)
        {
            if (campaign != null)
            {
                   campaign.UnifiedDiscounts.Clear();

                if (dealsSelector1.SelectedDeals.Count > 0)
                {
                    campaign.UnifiedDiscounts.Clear();
                    foreach (Deal d in dealsSelector1.SelectedDeals)
                    {
                        campaign.UnifiedDiscounts.Add(d.unified_discount_id);
                    }
                    campaign.Update();
                    SelectCampaign(campaign);
                    MessageBox.Show("Ofertas cadastrada com sucesso!");
                }
            }
        }

        private void OnBtLoadFileClick(object sender, EventArgs e)
        {
            if (!File.Exists(txtFile.Text))
            {
                MessageBox.Show(string.Format("O arquivo {0} não existe!", txtFile.Text));
                return;
            }
            Cursor = Cursors.WaitCursor;
            var a = d.LoadFile(txtFile.Text,campaign);
            SelectCampaign(campaign);
            Cursor = Cursors.Default;
            MessageBox.Show(a);
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            txtCampaign.Text = "";
            txtCategory.Text = "";
            txtPartnerDescription.Text = "";
            txtPromocode.Text = "";
            txtPublic.Text = "";
            txtDescription.Text = "";
            lblMailingId.Text = "";
            txtCountMailing.Text = "";
            txtCountControl.Text = "";
            txtDealsCount.Text = "";
            dealsSelector1.ClearDeals();
            campaign = null;
            dealsSelector1.Enabled = false;
            txtFile.Text = "";

        }



    



    }
}
