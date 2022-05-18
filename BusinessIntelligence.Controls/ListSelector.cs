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
    public  partial class ListSelector : UserControl
    {
        public ListSelector()
        {
            InitializeComponent();
        }
        [System.ComponentModel.Category("Custom")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return ckEnable.Text; }
            set
            {
                ckEnable.Text = value;
            }
        }
        private List<String> _Items = new List<string>();
        public string SelectedItem { get { return cbSelector.SelectedItem.ToString(); } }
  
        public void AddItem(string value)
        {
            _Items.Add(value);
            cbSelector.Items.Add(value);
        }
        private void ckEnable_CheckedChanged(object sender, EventArgs e)
        {
            grSelector.Enabled = ckEnable.Checked;
        }
  
        public bool Checked
        {
            get { return ckEnable.Checked; }
            set { ckEnable.Checked = value; }
        }
    }
}
