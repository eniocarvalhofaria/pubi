using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessIntelligence.App.Marketing;
using System.IO;
using System.Reflection;
namespace BusinessIntelligence.App.Marketing.SmartEmail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CampaignLoader cl = new CampaignLoader();
        EmailCampaign currentEmailCampaign;
        DealTemplate FeatureTemplate = new TwoTopDealTemplate();
        public void GenerateEmail()
        {
            currentEmailCampaign.emailBodyTemplate.TopDealTemplate = FeatureTemplate;
            String strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "");

            currentEmailCampaign.emailBodyTemplate.SaveHtmlContent(strAppDir + @"\Email.html");
            webBrowser1.Url = new Uri(strAppDir + @"\Email.html");

            txtHtml.Text = currentEmailCampaign.emailBodyTemplate.GetHtml();
            lbSubject.Text = currentEmailCampaign.emailBodyTemplate.Subject;
            txtSql.Text = currentEmailCampaign.GetUsersSqlCommand;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cl.Load();
            foreach (EmailCampaign ec in cl.EmailCampaignList)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ec.emailBodyTemplate.Subject;
                tn.Tag = ec;
                TreeNode tn2 = new TreeNode();
                tn2.Text = "Usuários afetados: " + String.Format("{0:N0}",ec.UsersAffected);
                tn2.Tag = ec;
                tn.Nodes.Add(tn2);
                treeView1.Nodes.Add(tn);
                ec.emailBodyTemplate.TopDealTemplate = FeatureTemplate;

            }
            layoutsToolStripMenuItem.Enabled = true;
            treeView1.Enabled = true;
            splitContainer2.Enabled = true;


        }
        private void destaqueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OneFeature();
        }
        private void destaquesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TwoFeatures();
        }
        private void semDestaqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoFeature();

        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ChangeCampaign((EmailCampaign)e.Node.Tag);
        }


        void ChangeCampaign(EmailCampaign ec)
        {
            currentEmailCampaign = ec;
            GenerateEmail();
        }
        void NoFeature()
        {
            FeatureTemplate = null;
            GenerateEmail();
        }
        void OneFeature()
        {
            FeatureTemplate = new OneTopDealTemplate();
        
            GenerateEmail();
        }
        void TwoFeatures()
        {
            FeatureTemplate = new TwoTopDealTemplate();
            GenerateEmail();
        }

        private void destaqueToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OneFeature();
        }

        private void destaquesToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            TwoFeatures();
        }

        private void semDestaqueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NoFeature();
        }


    }
}
