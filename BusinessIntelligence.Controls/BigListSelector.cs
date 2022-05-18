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
    public partial class BigListSelector : UserControl
    {
        public BigListSelector()
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
            grSelector.Enabled = ckEnable.Checked;
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
            var a = new FormBigListSelector();
            a.BigListSelector = this;
            return a;
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
                lbValueSelected.Text = value;
            }
        }
        private void btSelector_Click(object sender, EventArgs e)
        {
            if (FormSelector == null)
            {
                FormSelector = CreateNewFormSelector();
                ((FormBigListSelector)FormSelector).BigListSelector = this;
            }
            FormSelector.ShowDialog();
        }

        private Data.QueryExecutor _QueryEecutor;

        public Data.QueryExecutor QueryEecutor
        {
            get { return _QueryEecutor; }
            set { _QueryEecutor = value; }
        }


        private string _IdField;

        public string IdField
        {
            get { return _IdField; }
            set { _IdField = value; }
        }
        private string _NameField;

        public string NameField
        {
            get { return _NameField; }
            set { _NameField = value; }
        }
        private string _QualifiedObjectName;

        public string QualifiedObjectName
        {
            get { return _QualifiedObjectName; }
            set { _QualifiedObjectName = value; }
        }
        private string _TemplateExpression;
        [System.ComponentModel.Category("Custom")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string TemplateExpression
        {
            get { return _TemplateExpression; }
            set { _TemplateExpression = value; }
        }
        public string GetExpression()
        {
            if (FormSelector != null && TemplateExpression != null)
            {
                string list = null;
                foreach (var item in ((FormBigListSelector)FormSelector).SelectedItems)
                {
                    list += "," + item;
                }
                if (list != null)
                {
                    list = list.Substring(1);
                    return string.Format(TemplateExpression, GetInExpression(), list);
                }
            }

            return null;

        }
        private string GetInExpression()
        {
            if (rdYes.Checked)
            {
                return " in ";
            }
            else
            {
                return " not in ";
            }

        }
        public  bool Checked
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
    }
}
