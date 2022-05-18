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
    public partial class Selector : UserControl
    {
        public Selector()
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
        DataTypeCriterial _DataTypeCriterial;


        [System.ComponentModel.Category("Custom")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataTypeCriterial DataTypeCriterial
        {
            get { return _DataTypeCriterial; }
            set { _DataTypeCriterial = value; }
        }
        private void ckEnable_CheckedChanged(object sender, EventArgs e)
        {
            imSelector.Enabled = ckEnable.Checked;
            if (imSelector.Enabled)
            {
                this.imSelector.Image = global::BusinessIntelligence.Controls.Properties.Resources.filter32;
               
            }
            else
            {
                this.imSelector.Image = global::BusinessIntelligence.Controls.Properties.Resources.filtergrey32;
            }
            CheckedChange?.Invoke(this, e);
        }

        FormSelector _FormSelector;

        public FormSelector FormSelector
        {
            get { return _FormSelector; }
            set { _FormSelector = value; }
        }
        object _Criterial;

        public object Criterial
        {
            get { return _Criterial; }
            set { _Criterial = value; }
        }
        public FormSelector CreateNewFormSelector()
        {
            switch (DataTypeCriterial)
            {
                case BusinessIntelligence.DataTypeCriterial.Number:
                    {
                        return new FormValueSelector();
                    }
                case BusinessIntelligence.DataTypeCriterial.Date:
                    {
                        return new FormDateSelector();
                    }
                case BusinessIntelligence.DataTypeCriterial.BigList:
                           {
                               return new FormBigListSelector();         
                           }
                case BusinessIntelligence.DataTypeCriterial.Text:
                    {
                        return new FormTextSelector();
                    }
            }
            return null;
        }
        private string _SelectionInfo;

        public string SelectionInfo
        {
            get
            {
                return _SelectionInfo;
            }
            set
            {
                _SelectionInfo = value;
                if (!string.IsNullOrEmpty(value))
                {
                    lbValueSelected.Text = value.Replace("\r\n", "");
                }
            }
        }
        [System.ComponentModel.Category("Behavior")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public event EventHandler CheckedChange ;
        private void btSelector_Click(object sender, EventArgs e)
        {
            if (FormSelector == null)
            {
                FormSelector = CreateNewFormSelector();
                FormSelector.Selector = this;
            }
            FormSelector.ShowDialog();
        }



        public string GetExpression(string fieldName)
        {
            return GetExpression(fieldName, DateTime.Now);
        }
        public string GetExpression(string fieldName, DateTime dateReference)
        {
            switch (DataTypeCriterial)
            {
                case BusinessIntelligence.DataTypeCriterial.Number:
                    {
                        return ((FormValueSelector)FormSelector).GetExpression(fieldName);
                    }
                case BusinessIntelligence.DataTypeCriterial.Date:
                    {
                        return ((FormDateSelector)FormSelector).GetExpression(fieldName,dateReference);
                    }
                case BusinessIntelligence.DataTypeCriterial.Text:
                    {
                        return ((FormTextSelector)FormSelector).GetExpression(fieldName);
                    }
                case BusinessIntelligence.DataTypeCriterial.BigList:
                    {
                        return null;
                    }
                default:
                    {
                        return null;
                    }
            }
        }
        public bool Checked
        {
            get { return ckEnable.Checked; }
            set { ckEnable.Checked = value; }
        }
        public void ClearSelection()
        {
            SelectionInfo = "";
            FormSelector = null;
            ckEnable.Checked = false;
        }

        private void imSelector_Click(object sender, EventArgs e)
        {
            if (FormSelector == null)
            {
                FormSelector = CreateNewFormSelector();
                FormSelector.Selector = this;
            }
            FormSelector.ShowDialog();
        }
    }
}
