using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessIntelligence.Members.Marketing;
namespace BusinessIntelligence.App.Marketing
{
    public partial class EmailMaker : UserControl
    {
        public EmailMaker()
        {
            InitializeComponent();

        }

        public List<Deal> SelectedDeals
        {
            get { return dealsSelector1.SortedSelectedDeals; }
        }
        public string Html
        {
            get { return hmEmailBody.Html; }
        }

        public DealTemplate TopDealTemplate
        {
            get { return EmailBodyTemplate.TopDealTemplate; }
            set
            {
                if (EmailBodyTemplate != null)
                {
                    EmailBodyTemplate.TopDealTemplate = value;
                    hmLayoutDealTop.Template = value;
                    GenerateHtml();
                }
            }
        }
        public DealTemplate MiddleDealTemplate
        {
            get { return EmailBodyTemplate.MiddleDealTemplate; }
            set
            {
                if (EmailBodyTemplate != null)
                {
                    EmailBodyTemplate.MiddleDealTemplate = value;
                    hmLayoutDeals.Template = value;
                    GenerateHtml();
                }
            }
        }
        EmailBodyTemplate _EmailBodyTemplate;

        public EmailBodyTemplate EmailBodyTemplate
        {
            get { return _EmailBodyTemplate; }
            set
            {
                _EmailBodyTemplate = value;
                hmLayoutEmailBody.Template = value;
                if (cbTopDeals.Items.Count > 0 && !cbTopDeals.SelectedItem.Equals(value.TopDealTemplate))
                {
                    SelectItemByValue(cbTopDeals, value.TopDealTemplate.Description);
       
                }
                if (cbMiddleDeals.Items.Count > 0 && !cbMiddleDeals.SelectedItem.Equals(value.MiddleDealTemplate))
                {
                    SelectItemByValue(cbMiddleDeals, value.MiddleDealTemplate.Description);
                }
             
                GenerateHtml();
            }
        }
        public static void SelectItemByValue(ComboBox cbo, string value)
        {
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                var prop = cbo.Items[i].GetType().GetProperty(cbo.ValueMember);
                if (prop != null && prop.GetValue(cbo.Items[i], null).ToString() == value)
                {
                    cbo.SelectedIndex = i;
                    break;
                }
            }
        }
        private void OntxtCampaignLeave(object sender, EventArgs e)
        {
            DateTime sentDate = DateTime.MinValue;
            DateTime.TryParse("20" + txtCampaign.Text.Substring(5, 2) + "-" + txtCampaign.Text.Substring(7, 2) + "-" + txtCampaign.Text.Substring(9, 2), out sentDate);
            if (sentDate.Equals(DateTime.MinValue))
            {
                MessageBox.Show("O formato de data está inválido. Após as 5 primeiras letras, deve-se colocar o ano com 2 posições, o mês com 2 posições, e o dia com 2 posições. Após isso pode se colocar qualquer coisa.");
                return;
            }
            dealsSelector1.Date = sentDate;
        }
        public string HtmlBodyLayout
        {
            get { return hmLayoutEmailBody.Html; }
            set { hmLayoutEmailBody.Html = value; }
        }
        public string HtmlTopLayout
        {
            get { return hmLayoutDealTop.Html; }
            set { hmLayoutDealTop.Html = value; }
        }
        public string HtmlDealLayout
        {
            get { return hmLayoutDeals.Html; }
            set { hmLayoutDeals.Html = value; }
        }
        public string HtmlEmailBody
        {
            get { return hmEmailBody.Html; }
        }


        private void btGenerateHtml_Click(object sender, EventArgs e)
        {
            GenerateHtml();
        }
        void GenerateHtml()
        {
            Cursor.Current = Cursors.WaitCursor;
            EmailBodyTemplate.Deals.Clear();
          if (Campaign != null)
            {
                Campaign.UnifiedDiscounts.Clear();
            }
          
            foreach (Deal item in dealsSelector1.SortedSelectedDeals)
            {
                EmailBodyTemplate.Deals.Add(new HtmlDeal(item));
                if (Campaign != null)
                {
                    Campaign.UnifiedDiscounts.Add(item.unified_discount_id);
                }
             
            }
            EmailBodyTemplate.TopDealTemplate.OriginalTemplate = hmLayoutDealTop.Html;
            EmailBodyTemplate.MiddleDealTemplate.OriginalTemplate = hmLayoutDeals.Html;
            EmailBodyTemplate.OriginalTemplate = hmLayoutEmailBody.Html;
            EmailBodyTemplate.utm_campaign = txtCampaign.Text;
            EmailBodyTemplate.utm_medium = "email";
            EmailBodyTemplate.utm_source = "mailfish";
            hmEmailBody.Html = EmailBodyTemplate.GetHtml();
                
            if (Campaign != null)
            {
                Campaign.Update();
            }
            Cursor.Current = Cursors.Default;
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name.Equals("tbEmailBody"))
            {
                GenerateHtml();
            }
        }

        private void btSaveHtml_Click(object sender, EventArgs e)
        {

            saveHtmlContent.FileName = txtCampaign.Text + ".htm";
            saveHtmlContent.Filter = "Arquivos Html (*.htm)|*.htm";
            saveHtmlContent.ShowDialog();

        }

        private void saveHtmlContent_FileOk(object sender, CancelEventArgs e)
        {
            EmailBodyTemplate.SaveHtmlContent(saveHtmlContent.FileName);
        }

        [System.ComponentModel.Category("Custom")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool CanChangeCampaign
        {
            get { return txtCampaign.ReadOnly == false; }
            set { txtCampaign.ReadOnly = value == false; }
        }
        private Campaign _Campaign;
        public Campaign Campaign
        {
            get { return _Campaign; }
            set
            {
                _Campaign = value;
                txtCampaign.Text = _Campaign.Name;
                dealsSelector1.Date = value.SentDate;
            }
        }

        private void EmailMaker_Load(object sender, EventArgs e)
        {

            linkTemplateList(cbTopDeals, Templates.DealTemplateList.ToList<ITemplate>());
            linkTemplateList(cbMiddleDeals, Templates.DealTemplateList.ToList<ITemplate>());
            linkTemplateList(cbEmailBodyTemplate, Templates.EmailBodyTemplateList.ToList<ITemplate>());
            EmailBodyTemplate = new CommonEmailBodyTemplate();
        }
        private void linkTemplateList(ComboBox cb, List<ITemplate> templates)
        {
            cb.DataSource = templates;
            cb.DisplayMember = "Description";
            cb.ValueMember = "Description";
        }

        private void cbTopDeals_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TopDealTemplate = (DealTemplate)cbTopDeals.SelectedItem;
        }
        private void cbMiddleDeals_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MiddleDealTemplate = (DealTemplate)cbMiddleDeals.SelectedItem;
        }
        private void cbEmailBodyTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.EmailBodyTemplate = (EmailBodyTemplate)cbEmailBodyTemplate.SelectedItem;
        }

        private void dealsSelector1_Load(object sender, EventArgs e)
        {

        }



    }
}
