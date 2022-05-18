using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusinessIntelligence.Controls
{
    public partial class BoolSelector : UserControl
    {
        public BoolSelector()
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
         private void ckEnable_CheckedChanged(object sender, EventArgs e)
        {
            rdYes.Enabled = ckEnable.Checked;
            rdNo.Enabled = ckEnable.Checked;
        }

        object _Criterial;

        public object Criterial
        {
            get { return _Criterial; }
            set { _Criterial = value; }
        }

        public string GetExpression(string fieldName, string trueExpression, string falseExpression)
        {
            if (rdYes.Checked)
            {
                return fieldName  + trueExpression;
            }
            else
            {
                return fieldName  + falseExpression;
            }
        }
        public bool Checked
        {
            get { return ckEnable.Checked; }
            set { ckEnable.Checked = value; }
        }



    }
}
