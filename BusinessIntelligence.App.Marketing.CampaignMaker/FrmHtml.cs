using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusinessIntelligence.App.Marketing.CampaignMaker
{
    public partial class FrmHtml : Form
    {
        public FrmHtml()
        {
            InitializeComponent();
        }

        public string Html
        {
            get { return htmlMaker1.Html; }
            set { htmlMaker1.Html = value; }
        }
    }
}
