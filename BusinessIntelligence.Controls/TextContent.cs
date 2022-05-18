using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusinessIntelligence
{
    public partial class TextContent : UserControl
    {
        public TextContent()
        {
            InitializeComponent();
        }
        [System.ComponentModel.Category("Custom")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return txtSeeded.Text; }
            set
            {
                txtSeeded.Text = value;
            }
        }
        [System.ComponentModel.Category("Custom")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public  string Title
        {
            get { return grSeeded.Text; }
            set
            {
                grSeeded.Text = value;
            }
        }
        private void btSeededFile_Click(object sender, EventArgs e)
        {
            openSeededList.ShowDialog();
        }

        private void openSeededList_FileOk(object sender, CancelEventArgs e)
        {
            using (var sr = new System.IO.StreamReader(openSeededList.FileName))
            {
                txtSeeded.Text = sr.ReadToEnd();
                sr.Close();
            }
        }
    }
}
