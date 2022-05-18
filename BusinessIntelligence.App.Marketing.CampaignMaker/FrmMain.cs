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
namespace BusinessIntelligence.App.Marketing.CampaignMaker
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        BusinessIntelligence.App.Marketing.Data d;
        private void OnLoad(object sender, EventArgs e)
        {
            d = Data.GetInstance();
            Cursor.Current = Cursors.Default;
            foreach (TabPage item in tabControl1.TabPages)
            {
                tabs.Add(item);
            }
            selectTab(0);
            checkTest();
            MailfishSender.Populate<MailfishSender>("Name");
            if (campaigns.Count > 0)
            {

                SelectCampaign(campaigns[0]);
                qtySlices.Value = campaigns.Count;
                if (campaigns.Count > 1)
                {
                    switch (campaigns[0].CampaignTestType)
                    {
                        case CampaignTestType.Subject:
                            {
                                SelectItemByText(this.sliceType, "Assunto do email");
                                break;
                            }
                        case CampaignTestType.TimeToSend:
                            {
                                SelectItemByText(this.sliceType, "Hora do disparo");
                                break;
                            }
                        default:
                            {
                                SelectItemByText(this.sliceType, "Conteúdo");
                                break;
                            }
                    }


                    txtDescription2.Text = campaigns[1].Description;
                }
                if (campaigns.Count > 2)
                {
                    txtDescription3.Text = campaigns[2].Description;
                }
                if (campaigns.Count > 3)
                {
                    txtDescription4.Text = campaigns[3].Description;
                }
            }
        }

        public static void SelectItemByText(ComboBox cbo, string value)
        {
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                if (cbo.Items[i].ToString().Equals(value))
                {
                    cbo.SelectedIndex = i;
                    break;
                }
            }
        }
        private void SelectCampaign(Campaign c)
        {
            txtCategory.Text = c.CategoryDescription;
            txtPartnerDescription.Text = c.PartnerDescription;
            txtPublic.Text = c.PublicDescription;
            txtPromocode.Text = c.Promocode;
            txtDescription1.Text = c.Description;
          
            btMailingSelector.Enabled = true;
        }
        public void defineCampaigns(List<Campaign> c)
        {
            campaigns = c;
        }
        private void btSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "csv files (*.csv)|*.csv";
            openFileDialog1.ShowDialog();
        }


        private List<Campaign> campaigns = new List<Campaign>();



        FrmMailingSelector frmMailingSelector = new FrmMailingSelector();
        FrmEmailMaker frmEmailMaker = new FrmEmailMaker();
        Form frmEmailMaker2 = new FrmEmailMaker();
        Form frmEmailMaker3 = new FrmEmailMaker();
        Form frmEmailMaker4 = new FrmEmailMaker();

        private void btMailingSelector_Click(object sender, EventArgs e)
        {
            frmMailingSelector.Campaigns = campaigns;
            if(rdPU.Checked)
            {
                frmMailingSelector.CampaignSite = Marketing.Site.PeixeUrbano;

            }
            else
            {
                frmMailingSelector.CampaignSite = Marketing.Site.Groupon;

            }

            frmMailingSelector.ShowDialog();

            if (!frmMailingSelector.MailingWasGenerated)
            {
                MessageBox.Show("Você não gerou o mailing. Faça agora pra facilitar seu trabalho...");
            }
        }
        private void qtySlices_ValueChanged(object sender, EventArgs e)
        {
            sliceType.Enabled = qtySlices.Value > 1;
            checkTest();
        }
        private string selectedSliceType()
        {
            if (sliceType.SelectedItem != null)
            {
                return sliceType.SelectedItem.ToString();
            }
            return "";
        }
        private void checkTest()
        {
            txtDescription4.Visible = qtySlices.Value >= 4;
            txtDescription3.Visible = qtySlices.Value >= 3;
            txtDescription2.Visible = qtySlices.Value >= 2;
            lbDescription4.Visible = qtySlices.Value >= 4;
            lbDescription3.Visible = qtySlices.Value >= 3;
            lbDescription2.Visible = qtySlices.Value >= 2;



            lbDescription2.Text = "Descrição " + this.Text + "B";
            lbDescription3.Text = "Descrição " + this.Text + "C";
            lbDescription4.Text = "Descrição " + this.Text + "D";

            lbSubject2.Text = "Assunto " + this.Text + "B";
            lbSubject3.Text = "Assunto " + this.Text + "C";
            lbSubject4.Text = "Assunto " + this.Text + "D";

            btEmailMaker2.Text = "HTML " + this.Text + "B";
            btEmailMaker3.Text = "HTML " + this.Text + "C";
            btEmailMaker4.Text = "HTML " + this.Text + "D";



            lbDtToSend2.Text = "Hora do disparo " + this.Text + "B";
            lbDtToSend3.Text = "Hora do disparo " + this.Text + "C";
            lbDtToSend4.Text = "Hora do disparo " + this.Text + "D";

            lbDescription1.Text = "Descrição " + this.Text;
            lbSubject.Text = "Assunto " + this.Text;
            btEmailMaker.Text = "HTML " + this.Text;
            lbDtToSend1.Text = "Hora do disparo " + this.Text;

            if (qtySlices.Value > 1)
            {
                lbDescription1.Text = "Descrição " + this.Text + "A";
                if (sliceType.SelectedItem != null)
                {
                    switch (sliceType.SelectedItem.ToString())
                    {
                        case ("Assunto do email"):
                            {

                                lbSubject.Text = "Assunto " + this.Text + "A";
                                break;
                            }
                        case ("Conteúdo"):
                            {

                                btEmailMaker.Text = "HTML " + this.Text + "A";
                                break;

                            }
                        case ("Hora do disparo"):
                            {
                                lbDtToSend1.Text = "Hora do disparo " + this.Text + "A";
                                break;
                            }
                    }
                }

            }


            txtSubject4.Visible = qtySlices.Value >= 4 && selectedSliceType().Equals("Assunto do email");
            txtSubject3.Visible = qtySlices.Value >= 3 && selectedSliceType().Equals("Assunto do email");
            txtSubject2.Visible = qtySlices.Value >= 2 && selectedSliceType().Equals("Assunto do email");
            lbSubject4.Visible = qtySlices.Value >= 4 && selectedSliceType().Equals("Assunto do email");
            lbSubject3.Visible = qtySlices.Value >= 3 && selectedSliceType().Equals("Assunto do email");
            lbSubject2.Visible = qtySlices.Value >= 2 && selectedSliceType().Equals("Assunto do email");
            btEmailMaker4.Visible = qtySlices.Value >= 4;
            btEmailMaker3.Visible = qtySlices.Value >= 3;
            btEmailMaker2.Visible = qtySlices.Value >= 2;
            frmEmailMaker = new FrmEmailMaker();
            if (selectedSliceType().Equals("Conteúdo"))
            {
                frmEmailMaker2 = new FrmEmailMaker();
                frmEmailMaker3 = new FrmEmailMaker();
                frmEmailMaker4 = new FrmEmailMaker();
            }
            else
            {
                frmEmailMaker2 = new FrmHtml();
                frmEmailMaker3 = new FrmHtml();
                frmEmailMaker4 = new FrmHtml();
            }

            lbDtToSend4.Visible = qtySlices.Value >= 4 && selectedSliceType().Equals("Hora do disparo");
            lbDtToSend3.Visible = qtySlices.Value >= 3 && selectedSliceType().Equals("Hora do disparo");
            lbDtToSend2.Visible = qtySlices.Value >= 2 && selectedSliceType().Equals("Hora do disparo");
            dtToSend4.Visible = qtySlices.Value >= 4 && selectedSliceType().Equals("Hora do disparo");
            dtToSend3.Visible = qtySlices.Value >= 3 && selectedSliceType().Equals("Hora do disparo");
            dtToSend2.Visible = qtySlices.Value >= 2 && selectedSliceType().Equals("Hora do disparo");


        }
        private void sliceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkTest();
        }

        private void btEmailMaker_Click(object sender, EventArgs e)
        {
            frmEmailMaker.Campaign = campaigns[0];
            frmEmailMaker.ShowDialog();
        }
        private void btEmailMaker2_Click(object sender, EventArgs e)
        {
            if (selectedSliceType().Equals("Conteúdo"))
            {
                ((FrmEmailMaker)frmEmailMaker2).Campaign = campaigns[1];
            }
            else
            {
                ((FrmHtml)frmEmailMaker2).Html = ((FrmEmailMaker)frmEmailMaker).Html.Replace(campaigns[0].Name,campaigns[1].Name);
            }

            frmEmailMaker2.ShowDialog();

        }

        private void btEmailMaker3_Click(object sender, EventArgs e)
        {
            if (selectedSliceType().Equals("Conteúdo"))
            {
                ((FrmEmailMaker)frmEmailMaker3).Campaign = campaigns[2];
            }
            else
            {
                ((FrmHtml)frmEmailMaker3).Html = ((FrmEmailMaker)frmEmailMaker).Html.Replace(campaigns[0].Name, campaigns[2].Name);
            }

            frmEmailMaker3.ShowDialog();
        }

        private void btEmailMaker4_Click(object sender, EventArgs e)
        {
            if (selectedSliceType().Equals("Conteúdo"))
            {
                ((FrmEmailMaker)frmEmailMaker4).Campaign = campaigns[3];
            }
            else
            {
                ((FrmHtml)frmEmailMaker4).Html = ((FrmEmailMaker)frmEmailMaker).Html.Replace(campaigns[0].Name, campaigns[3].Name);
            }

            frmEmailMaker4.ShowDialog();
        }
        List<TabPage> tabs = new List<TabPage>();
        private void selectTab(int index)
        {
            foreach (TabPage item in tabControl1.TabPages)
            {
                tabControl1.TabPages.Remove(item);
            }

            btPrevious.Visible = index != 0;
            //   btNext.Visible = index != tabs.Count - 1;

            tabControl1.TabPages.Add(tabs[index]);
            currentTabIndex = index;
        }
        int currentTabIndex = 0;
        private void btNext_Click(object sender, EventArgs e)
        {
            btNext.Text = "Próxima";
            if (currentTabIndex == 0)
            {
                if (sliceType.SelectedItem == null && qtySlices.Value > 1)
                {
                    MessageBox.Show("Você tem que especificar o tipo de fatiamento desejado.");
                    return;
                }
                if (campaigns.Count == 0)
                {
                    if (qtySlices.Enabled && qtySlices.Value > 1)
                    {
                        campaigns.Add(createCampaign(this.Text + "A"));
                        campaigns.Add(createCampaign(this.Text + "B"));
                        if (qtySlices.Value >= 3)
                        {
                            campaigns.Add(createCampaign(this.Text + "C"));
                        }
                        if (qtySlices.Value >= 4)
                        {
                            campaigns.Add(createCampaign(this.Text + "D"));
                        }
                        foreach (var c in campaigns)
                        {
                            if (selectedSliceType().Equals("Conteúdo"))
                            {
                                c.CampaignTestType = CampaignTestType.Content;
                            }
                            if (selectedSliceType().Equals("Assunto do email"))
                            {
                                c.CampaignTestType = CampaignTestType.Subject;
                            }
                            if (selectedSliceType().Equals("Hora do disparo"))
                            {
                                c.CampaignTestType = CampaignTestType.TimeToSend;
                            }
                        }
                    }
                    else
                    {
                        campaigns.Add(createCampaign(this.Text));
                    }
                    List<string> Descriptions = new List<string>();
                    Descriptions.Add(txtDescription1.Text);
                    Descriptions.Add(txtDescription2.Text);
                    Descriptions.Add(txtDescription3.Text);
                    Descriptions.Add(txtDescription4.Text);

                    for (int i = 0; i < qtySlices.Value; i++)
                    {

                        switch (selectedSliceType())
                        {
                            case "Conteúdo":
                                {
                                    campaigns[i].CampaignTestType = CampaignTestType.Content;
                                    break;
                                }
                            case "Assunto do email":
                                {
                                    campaigns[i].CampaignTestType = CampaignTestType.Subject;
                                    break;
                                }
                            case "Hora do disparo":
                                {
                                    campaigns[i].CampaignTestType = CampaignTestType.TimeToSend;
                                    break;
                                }
                        }
                        campaigns[i].CategoryDescription = txtCategory.Text;
                        campaigns[i].PartnerDescription = txtPartnerDescription.Text;
                        campaigns[i].PublicDescription = txtPublic.Text;
                        campaigns[i].Promocode = txtPromocode.Text;
                        campaigns[i].Description = Descriptions[i];
                    }
                    grCampaign.Enabled = false;
                    dtToSend1.MinDate = campaigns[0].SentDate;
                    dtToSend2.MinDate = campaigns[0].SentDate;
                    dtToSend3.MinDate = campaigns[0].SentDate;
                    dtToSend4.MinDate = campaigns[0].SentDate;
                    dtToSend1.MaxDate = campaigns[0].SentDate.AddDays(1).AddSeconds(-1);
                    dtToSend2.MaxDate = campaigns[0].SentDate.AddDays(1).AddSeconds(-1);
                    dtToSend3.MaxDate = campaigns[0].SentDate.AddDays(1).AddSeconds(-1);
                    dtToSend4.MaxDate = campaigns[0].SentDate.AddDays(1).AddSeconds(-1);
                }
                else {
                    List<string> Descriptions = new List<string>();
                    Descriptions.Add(txtDescription1.Text);
                    Descriptions.Add(txtDescription2.Text);
                    Descriptions.Add(txtDescription3.Text);
                    Descriptions.Add(txtDescription4.Text);
                    for (int i = 0; i < campaigns.Count; i++)
                    {
                        campaigns[i].CategoryDescription = txtCategory.Text;
                        campaigns[i].PartnerDescription = txtPartnerDescription.Text;
                        campaigns[i].PublicDescription = txtPublic.Text;
                        campaigns[i].Promocode = txtPromocode.Text;
                        campaigns[i].Description = Descriptions[i];
                        campaigns[i].Update();
                    }
                }


            }
            else if (currentTabIndex == 1)
            {
                if (!frmMailingSelector.MailingWasGenerated)
                {
                    MessageBox.Show("Campanha sem mailing não rola. Faça agora pra facilitar seu trabalho...");
                }
            }
            else if (currentTabIndex == 2)
            {

                if (qtySlices.Value >= 1 && selectedSliceType().Equals("Assunto do email"))
                {
                    List<string> Subjects = new List<string>();
                    Subjects.Add(txtSubject1.Text);
                    Subjects.Add(txtSubject2.Text);
                    Subjects.Add(txtSubject3.Text);
                    Subjects.Add(txtSubject4.Text);
                    for (int i = 0; i < qtySlices.Value; i++)
                    {
                        campaigns[i].Refresh();
                        campaigns[i].Subject = Subjects[i];
                        campaigns[i].Update();
                    }
                }
                else
                {
                    for (int i = 0; i < qtySlices.Value; i++)
                    {
                        campaigns[i].Refresh();
                        campaigns[i].Subject = txtSubject1.Text;
                        campaigns[i].Update();
                    }
                }
            }
            else if (currentTabIndex == 3)
            {

                if (qtySlices.Value >= 1 && selectedSliceType().Equals("Conteúdo"))
                {

                    List<string> Htmls = new List<string>();
                    Htmls.Add(frmEmailMaker.Html);
                    Htmls.Add(((FrmEmailMaker)frmEmailMaker2).Html);
                    Htmls.Add(((FrmEmailMaker)frmEmailMaker3).Html);
                    Htmls.Add(((FrmEmailMaker)frmEmailMaker4).Html);

                    List<List<int>> UnifiedDiscounts = new List<List<int>>();
                    UnifiedDiscounts.Add(frmEmailMaker.SelectedUnifiedDiscounts);
                    UnifiedDiscounts.Add(((FrmEmailMaker)frmEmailMaker2).SelectedUnifiedDiscounts);
                    UnifiedDiscounts.Add(((FrmEmailMaker)frmEmailMaker3).SelectedUnifiedDiscounts);
                    UnifiedDiscounts.Add(((FrmEmailMaker)frmEmailMaker4).SelectedUnifiedDiscounts);
                    for (int i = 0; i < qtySlices.Value; i++)
                    {
                        campaigns[i].Refresh();
                        campaigns[i].HtmlEmail = Htmls[i];
                        campaigns[i].UnifiedDiscounts = UnifiedDiscounts[i];
                        campaigns[i].Update();
                    }
                }
                else
                {
                    if (qtySlices.Value > 1)
                    {
                        for (int i = 0; i < qtySlices.Value; i++)
                        {
                            campaigns[i].Refresh();
                            campaigns[i].HtmlEmail = frmEmailMaker.Html.Replace(campaigns[0].Name, campaigns[i].Name);
                            campaigns[i].UnifiedDiscounts = frmEmailMaker.SelectedUnifiedDiscounts;
                            campaigns[i].Update();
                        }
                    }
                    else
                    {
                        campaigns[0].Refresh();
                        campaigns[0].HtmlEmail = frmEmailMaker.Html;
                        campaigns[0].UnifiedDiscounts = frmEmailMaker.SelectedUnifiedDiscounts;
                        campaigns[0].Update();
                    }
                }
            }
            else if (currentTabIndex == 4)
            {

                if (qtySlices.Value >= 1 && selectedSliceType().Equals("Hora do disparo"))
                {
                    List<DateTime> DtsToSend = new List<DateTime>();
                    DtsToSend.Add(dtToSend1.Value);
                    DtsToSend.Add(dtToSend2.Value);
                    DtsToSend.Add(dtToSend3.Value);
                    DtsToSend.Add(dtToSend4.Value);

                    for (int i = 0; i < qtySlices.Value; i++)
                    {
                        campaigns[i].SentDate = DtsToSend[i];
                        campaigns[i].Update();
                    }
                }
                else
                {
                    for (int i = 0; i < qtySlices.Value; i++)
                    {
                        campaigns[i].Refresh();
                        campaigns[i].SentDate = dtToSend1.Value;
                        campaigns[i].Update();
                    }
                }
                btNext.Text = "Concluir";
                MailfishSender.Populate<MailfishSender>("Name");

            }
            else if (currentTabIndex == 5)
            {
                if (this.MailfishSender.SelectedObjects.Count == 0)
                {
                    MessageBox.Show("Você precisa selecionar um sender!");
                    return;
                }
                if (txtEmailAddressTest.Text.IndexOf("@") < 1)
                {
                    MessageBox.Show("Você precisa definir pelo menos um endereço para o recebimento do teste!");
                    return;
                }
                if (txtEmailAddressConfirmation.Text.IndexOf("@") < 1)
                {
                    MessageBox.Show("Você precisa definir um endereço para o recebimento da confirmação do envio!");
                    return;
                }
                for (int i = 0; i < qtySlices.Value; i++)
                {
                    campaigns[i].Refresh();
                    campaigns[i].MailfishSender = (MailfishSender)MailfishSender.SelectedObjects[0];
                    campaigns[i].EmailAddressToReceiveTest = txtEmailAddressTest.Text;
                    campaigns[i].EmailAddressToReceiveConfirmation = txtEmailAddressConfirmation.Text;
                    campaigns[i].Update();
                }
                for (int i = 0; i < qtySlices.Value; i++)
                {
                    d.CreateInMailfish(campaigns[i]);
                }
                MessageBox.Show("Você completou o processo.\r\nVerifique no Mailfish se está tudo certinho ;)");
                this.Close();
                this.Dispose();
                GC.Collect();
                return;
            }
            selectTab(currentTabIndex + 1);
        }

        private void btPrevious_Click(object sender, EventArgs e)
        {
            btNext.Text = "Próxima";
            selectTab(currentTabIndex - 1);
        }



        public Campaign createCampaign(string campaignName)
        {

            var campaign = BusinessIntelligence.Persistence.PersistenceSettings.PersistenceEngine.GetNewEmptyObject<Campaign>();
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

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }






    }
}
