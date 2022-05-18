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
    public partial class FrmMailingSelector : Form
    {
        public FrmMailingSelector()
        {
            InitializeComponent();
        }

        public List<Campaign> Campaigns
        {
            get { return mailingSelector1.Campaigns; }
            set
            {
                mailingSelector1.Campaigns = value;


            }
        }
    }
}
