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
    public partial class ParameterList : UserControl
    {
        public ParameterList()
        {
            InitializeComponent();
        }


        [System.ComponentModel.Category("Custom")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return groupBox1.Text; }
            set
            {
                groupBox1.Text = value;
            }
        }
    }
}
