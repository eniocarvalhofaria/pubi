using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusinessIntelligence.Controls
{
    public partial class FormTextSelector :  FormSelector
    {
        public FormTextSelector()
        {
            InitializeComponent();
        }
        string _TemplateExpression;
        public string TemplateExpression
        {
            get { return _TemplateExpression; }
            set { _TemplateExpression = value; }
        }
        private void btOk_Click(object sender, EventArgs e)
        {
            if (rdEqual.Checked)
            {
                _TextTypeSelected = TextCriterialType.Equal;
            }
            else if (rdNotEqual.Checked)
            {
                _TextTypeSelected = TextCriterialType.NotEqual;
            } if (rdStartsWith.Checked)
            {
                _TextTypeSelected = TextCriterialType.StartsWith;
            } if (rdNotStartsWith.Checked)
            {
                _TextTypeSelected = TextCriterialType.NotStartsWith;
            } if (rdEndsWith.Checked)
            {
                _TextTypeSelected = TextCriterialType.EndsWith;
            } if (rdEndsWith.Checked)
            {
                _TextTypeSelected = TextCriterialType.NotEndsWith;
            } if (rdContains.Checked)
            {
                _TextTypeSelected = TextCriterialType.Contains;
            } if (rdNotContains.Checked)
            {
                _TextTypeSelected = TextCriterialType.NotContains;
            }
            this.Close();
        }
        TextCriterialType _TextTypeSelected;
        public TextCriterialType TextTypeSelected
        {
            get
            {
                return _TextTypeSelected;

            }
        }
        public string GetExpression(string fieldName)
        {
            switch (TextTypeSelected)
            {
                case TextCriterialType.Equal:
                    {
                        TemplateExpression = "{0} = '" + txtText.Text + "'";
                        Selector.SelectionInfo = "Igual a " + txtText.Text;
                        break;
                    }
                case TextCriterialType.NotEqual:
                    {
                        TemplateExpression = "{0} <> '" + txtText.Text + "'";
                        Selector.SelectionInfo = "Diferente de " + txtText.Text;
                        break;
                    }
                case TextCriterialType.StartsWith:
                    {
                        TemplateExpression = "{0} like '" + txtText.Text + "%'";
                        Selector.SelectionInfo = "Começa com " + txtText.Text;
                        break;
                    }
                case TextCriterialType.NotStartsWith:
                    {
                        TemplateExpression = "{0} not like '" + txtText.Text + "%'";
                        Selector.SelectionInfo = "Não começa com " + txtText.Text;
                        break;
                    }
                case TextCriterialType.Contains:
                    {
                        TemplateExpression = "{0} like '%" + txtText.Text + "%'";
                        Selector.SelectionInfo = "Contém " + txtText.Text;
                        break;
                    }
                case TextCriterialType.NotContains:
                    {
                        TemplateExpression = "{0} not like '%" + txtText.Text + "%'";
                        Selector.SelectionInfo = "Não contém " + txtText.Text;
                        break;
                    }
                case TextCriterialType.EndsWith:
                    {
                        TemplateExpression = "{0} like '%" + txtText.Text + "'";
                        Selector.SelectionInfo = "Termina com " + txtText.Text;
                        break;
                    }
                case TextCriterialType.NotEndsWith:
                    {
                        TemplateExpression = "{0} not like '%" + txtText.Text + "'";
                        Selector.SelectionInfo = "Não termina com " + txtText.Text;
                        break;
                    }
            }
 
            return string.Format(TemplateExpression + "\r\n", fieldName);
        }
      
    }
}
