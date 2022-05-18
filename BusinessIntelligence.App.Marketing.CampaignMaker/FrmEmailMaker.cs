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
    public partial class FrmEmailMaker : Form
    {
        public FrmEmailMaker()
        {
            InitializeComponent();
        }

        private void FrmEmailMaker_Load(object sender, EventArgs e)
        {
            emailMaker1.CanChangeCampaign = false;
        }
        public Campaign Campaign
        {
            get { return emailMaker1.Campaign; }
            set
            {
                emailMaker1.Campaign = value;
            }
        }
        public string Html
        {
            get { return emailMaker1.Html; }
        }
        public List<Deal> SelectedDeals
        {
            get { return emailMaker1.SelectedDeals; }
        }
        public List<int> SelectedUnifiedDiscounts
        {
            get {
                var ret = new List<int>();
                foreach (var item in SelectedDeals)
                {
                    ret.Add(item.unified_discount_id);
                }
                return ret;
            }
        }
        public  void Dispose()
        {
            emailMaker1.Dispose(); 
            base.Dispose();
         
        }
    }
}
