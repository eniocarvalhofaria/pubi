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
    public partial class FormValueSelector : FormSelector
    {
        public FormValueSelector()
            : base()
        {
            InitializeComponent();
        }
        NumberCriterialType _ValueTypeSelected;
        public NumberCriterialType ValueTypeSelected
        {
            get
            {
                return _ValueTypeSelected;

            }
        }
        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        string _TemplateExpression;
        public string TemplateExpression
        {
            get { return _TemplateExpression; }
            set { _TemplateExpression = value; }
        }
        private void rdBetween_CheckedChanged(object sender, EventArgs e)
        {
            txtEndValue.Visible = rdBetween.Checked || rdNotBetween.Checked;
            lbAnd.Visible = txtEndValue.Visible;
        }
        public string GetExpression(string fieldName)
        {
            NumberCriterial c = new NumberCriterial();

            c.StartValue = Convert.ToDecimal(txtValue.Text);

            if (!string.IsNullOrEmpty(txtEndValue.Text))
            {
                c.EndValue = Convert.ToDecimal(txtEndValue.Text);
            }
            c.Value = Convert.ToDecimal(txtValue.Text);
            switch (ValueTypeSelected)
            {
                case NumberCriterialType.Equal:
                    {
                       TemplateExpression = "{0} = " + c.Value.ToString();
                        Selector.SelectionInfo = "Igual a " + c.Value.ToString();
                        break;
                    }
                case NumberCriterialType.NotEqual:
                    {
                       TemplateExpression = "{0} <> " + c.Value.ToString();
                        Selector.SelectionInfo = "Diferente de " + c.Value.ToString();
                        break;
                    }
                case NumberCriterialType.LessThan:
                    {
                       TemplateExpression = "{0} < " + c.Value.ToString();
                        Selector.SelectionInfo = "Menor que " + c.Value.ToString();
                        break;
                    }
                case NumberCriterialType.LessOrEqualThan:
                    {
                       TemplateExpression = "{0} <= " + c.Value.ToString();
                        Selector.SelectionInfo = "Menor ou igual a " + c.Value.ToString();
                        break;
                    }
                case NumberCriterialType.GreaterThan:
                    {
                       TemplateExpression = "{0} > " + c.Value.ToString();
                        Selector.SelectionInfo = "Maior que " + c.Value.ToString();
                        break;
                    }
                case NumberCriterialType.GreaterOrEqualThan:
                    {
                       TemplateExpression = "{0} >= " + c.Value.ToString();
                        Selector.SelectionInfo = "Maior ou igual a " + c.Value.ToString();
                        break;
                    }
                case NumberCriterialType.Between:
                    {
                       TemplateExpression = "{0} between " + c.StartValue.ToString() + " and " + c.EndValue.ToString();
                        Selector.SelectionInfo = "Entre " + c.StartValue.ToString() + " e " + c.EndValue.ToString();
                        break;
                    }
                case NumberCriterialType.NotBetween:
                    {
                       TemplateExpression = "{0} not between " + c.StartValue.ToString() + " and " + c.EndValue.ToString();
                        Selector.SelectionInfo = "Não está entre " + c.StartValue.ToString() + " e " + c.EndValue.ToString();
                        break;
                    }
            }
            Selector.Criterial = c;
            return string.Format(TemplateExpression + "\r\n", fieldName);
        }
        private void btOk_Click(object sender, EventArgs e)
        {

            if (rdEqual.Checked)
            {
                _ValueTypeSelected = NumberCriterialType.Equal;
            }
            if (rdNotEqual.Checked)
            {
                _ValueTypeSelected = NumberCriterialType.NotEqual;
            }
            if (rdLess.Checked)
            {
                _ValueTypeSelected = NumberCriterialType.LessThan;
            }
            if (rdLessOrEqual.Checked)
            {
                _ValueTypeSelected = NumberCriterialType.LessOrEqualThan;
            }
            if (rdGreater.Checked)
            {
                _ValueTypeSelected = NumberCriterialType.GreaterThan;
            }
            if (rdGreaterOrEqual.Checked)
            {
                _ValueTypeSelected = NumberCriterialType.GreaterOrEqualThan;
            }
            if (rdBetween.Checked)
            {
                _ValueTypeSelected = NumberCriterialType.Between;
            }
            if (rdNotBetween.Checked)
            {
                _ValueTypeSelected = NumberCriterialType.NotBetween;
            }
            GetExpression("");
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
