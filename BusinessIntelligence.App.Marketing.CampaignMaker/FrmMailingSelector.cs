using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessIntelligence.Members.Marketing;
namespace BusinessIntelligence.App.Marketing.CampaignMaker
{
    public partial class FrmMailingSelector : Form
    {
        public FrmMailingSelector()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          

        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Campaign> Campaigns
        {
            get { return mailingSelector1.Campaigns; }
            set
            {
                mailingSelector1.Campaigns = value;
            }
        }
        private Site _CampaignSite;
        public bool MailingWasGenerated
        {
            get { return mailingSelector1.MailingWasGenerated; }

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
                mailingSelector1.CampaignSite = value;
            }
        }
    }
}
