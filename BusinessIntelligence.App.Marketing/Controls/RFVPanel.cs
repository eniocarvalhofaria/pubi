using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusinessIntelligence.App.Marketing.Controls
{
    public partial class RFVPanel : UserControl
    {
        public RFVPanel()
        {
            InitializeComponent();
        }

        public string GetExpression(Dictionary<string, List<int>> filters, string basicFilterTable, string advancedFilterTable)
        {
            var sb = new StringBuilder();
            sb.Append("(\r\n");
            sb.Append("\tselect distinct\r\n");
            sb.Append("\t\tUserAccountId\r\n");
            sb.Append("\tfrom\r\n");
            if (rdBasic.Checked)
            {
                sb.Append("\t" + basicFilterTable + "\r\n");
                sb.Append("\twhere\r\n");
                sb.Append("\t(\r\n");
                string interactionTypeCode = null;

                if (rdPurchase.Checked)
                {
                    interactionTypeCode = "S";
                }
                else if (rdOrder.Checked)
                {
                    interactionTypeCode = "O";
                }
                string interactionType = null;

                if (rdPurchase.Checked)
                {
                    interactionType = "Sale";
                }
                else if (rdOrder.Checked)
                {
                    interactionType = "Order";
                }

                bool isFirstItem = true;
                foreach (var item in filters)
                {
                    if (!isFirstItem)
                    {
                        sb.Append("\tor ");
                    }
                    isFirstItem = false;
                    if (ckMoreValueEvent.Checked)
                    {
                        string fieldName = "Main" + item.Key + "GS" + interactionTypeCode;
                        if (rdVAllTime.Checked)
                        {
                            fieldName += "Total";
                        }
                        else if (rdVLast12Months.Checked)
                        {
                            fieldName += "12";
                        }
                        else if (rdVLast6Months.Checked)
                        {
                            fieldName += "6";
                        }
                        sb.Append("\t\t" + fieldName + " in (");
                        bool isFirstId = true;
                        foreach (var id in item.Value)
                        {
                            if (!isFirstId)
                            {
                                sb.Append(",");
                            }
                            sb.Append(id.ToString());
                            isFirstId = false;
                        }
                        sb.Append(")\r\n");
                    }
                    if (ckMoreQtyEvent.Checked)
                    {
                        string fieldName = "Main" + item.Key + "Q" + interactionTypeCode;
                        if (rdFAllTime.Checked)
                        {
                            fieldName += "Total";
                        }
                        else if (rdFLast12Months.Checked)
                        {
                            fieldName += "12";
                        }
                        else if (rdFLast6Months.Checked)
                        {
                            fieldName += "6";
                        }
                        sb.Append("\tor " + "\t" + fieldName + " in (");
                        bool isFirstId = true;
                        foreach (var id in item.Value)
                        {
                            if (!isFirstId)
                            {
                                sb.Append(",");
                            }
                            sb.Append(id.ToString());
                            isFirstId = false;
                        }
                        sb.Append(")\r\n");
                    }
                    if (ckLastEvent.Checked)
                    {
                        string fieldName = item.Key + "Last" + interactionType;

                        sb.Append("\tor " + "\t" + fieldName + " in (");
                        bool isFirstId = true;
                        foreach (var id in item.Value)
                        {
                            if (!isFirstId)
                            {
                                sb.Append(",");
                            }
                            sb.Append(id.ToString());
                            isFirstId = false;
                        }
                        sb.Append(")\r\n");
                    }
                }
                sb.Append("\t)\r\n");
            }
            else
            {
                sb.Append("\t" + advancedFilterTable + "\r\n");
                sb.Append("\twhere\r\n");
                sb.Append("\t(");
                bool isFirstItem = true;
                foreach (var item in filters)
                {
                    if (!isFirstItem)
                    {
                        sb.Append("\tor");
                    }
                    sb.Append("\t" + item.Key);
                    sb.Append(" in (");
                    bool isFirstId = true;
                    foreach (var id in item.Value)
                    {
                        if (!isFirstId)
                        {
                            sb.Append(",");
                        }
                        sb.Append(id.ToString());
                        isFirstId = false;
                    }
                    sb.Append(")\r\n");

                    isFirstItem = false;
                }
                sb.Append("\t)\r\n");
                sb.Append("\tand\r\n");
                sb.Append("\t(\r\n");
                if (this.ValueEvents.Checked)
                {
                    string fieldName = null;
                    if (rdVAllTime.Checked)
                    {
                        fieldName = "GrossSalesOrderTotal";
                    }
                    else if (rdVLast12Months.Checked)
                    {
                        fieldName = "GrossSalesOrder12Months";
                    }
                    else if (rdVLast6Months.Checked)
                    {
                        fieldName = "GrossSalesOrder6Months";
                    }
                    sb.Append("\tor \t" + ValueEvents.GetExpression(fieldName));
                }
                if (this.QtyOfEvent.Checked)
                {
                    string fieldName = null;
                    if (rdFAllTime.Checked)
                    {
                        fieldName = "QuantityOrderTotal";
                    }
                    else if (rdFLast12Months.Checked)
                    {
                        fieldName = "QuantityOrder12Months";
                    }
                    else if (rdFLast6Months.Checked)
                    {
                        fieldName = "QuantityOrder6Months";
                    }
                    sb.Append("\tor \t" + QtyOfEvent.GetExpression(fieldName));
                }
                if (rdPurchase.Checked)
                {
                    sb.Replace("Order", "Sales");
                }
                if (this.TimeOfEvent.Checked)
                {
                    string fieldName = null;
                    if (rdPurchase.Checked)
                    {
                        fieldName = "LastDateSale";
                    }
                    else
                    {

                        fieldName = "LastDateOrder";
                    }

                    sb.Append("\tor \t" + TimeOfEvent.GetExpression(fieldName));
                }

                sb.Append("\t)\r\n");
            }
            sb.Append(")");
            sb.Replace(",)", ")");
            sb.Replace("( or", "(");
            sb.Replace("( \tor", "(");
            sb.Replace("or \t\t", "or \t");
            sb.Replace("(\r\n\tor", "(\r\n\t");
            sb.Replace("or \tor \t", "or \t");
            if (sb.ToString().Replace("\t", "").Replace(" ", "").Replace("\r\n", "").Contains("()"))
            {
                return null;
            }
            else
            {
                return sb.ToString();
            }
        }
        private void rdCheckedChanged(object sender, EventArgs e)
        {
            if (rdOrder.Checked)
            {
                ValueEvents.Text = "Valor total dos pedidos";
                QtyOfEvent.Text = "Qtde de pedidos";
                TimeOfEvent.Text = "Período do último pedido";
                ckLastEvent.Text = "É o último pedido do usuário";
                ckMoreQtyEvent.Text = "Tem maior qtde de pedidos";
                ckMoreValueEvent.Text = "Tem maior valor de pedidos";
            }
            else if (rdPurchase.Checked)
            {
                ValueEvents.Text = "Valor total das compras";
                QtyOfEvent.Text = "Qtde da compras";
                TimeOfEvent.Text = "Período da última compra";
                ckLastEvent.Text = "É a última compra aprovada do usuário";
                ckMoreQtyEvent.Text = "Tem maior qtde de compras aprovadas";
                ckMoreValueEvent.Text = "Tem maior valor de compras aprovadas";
            }
        }

        private void ValueEvents_CheckedChanged(object sender, EventArgs e)
        {
            rdVAllTime.Enabled = ValueEvents.Checked;
            rdVLast12Months.Enabled = ValueEvents.Checked;
            rdVLast6Months.Enabled = ValueEvents.Checked;
            ckMoreValueEvent.Enabled = ValueEvents.Checked;
        }

        private void QtyOfEvent_CheckedChange(object sender, EventArgs e)
        {
            rdFAllTime.Enabled = QtyOfEvent.Checked;
            rdFLast12Months.Enabled = QtyOfEvent.Checked;
            rdFLast6Months.Enabled = QtyOfEvent.Checked;
            ckMoreQtyEvent.Enabled = QtyOfEvent.Checked;
        }

        private void TimeOfEvent_CheckedChange(object sender, EventArgs e)
        {
            ckLastEvent.Enabled = TimeOfEvent.Checked;
        }

        private void TimeOfEvent_Load(object sender, EventArgs e)
        {

        }

        private void rdBasic_CheckedChanged(object sender, EventArgs e)
        {
            ckLastEvent.Visible = rdBasic.Checked;
            ckMoreQtyEvent.Visible = rdBasic.Checked;
            ckMoreValueEvent.Visible = rdBasic.Checked;

            ckLastEvent.Enabled = rdBasic.Checked;
            ckMoreQtyEvent.Enabled = rdBasic.Checked;
            ckMoreValueEvent.Enabled = rdBasic.Checked;

            ValueEvents.Visible = rdAdvanced.Checked;
            QtyOfEvent.Visible = rdAdvanced.Checked;
            TimeOfEvent.Visible = rdAdvanced.Checked;

            ValueEvents.Checked = false;
            QtyOfEvent.Checked = false;
            TimeOfEvent.Checked = false;

            rdFAllTime.Enabled = false;
            rdFLast12Months.Enabled = false;
            rdFLast6Months.Enabled = false;

            rdVAllTime.Enabled = false;
            rdVLast12Months.Enabled = false;
            rdVLast6Months.Enabled = false;

            ckLastEvent.Checked = false;
            ckMoreQtyEvent.Checked = false;
            ckMoreValueEvent.Checked = false;
        }

        private void ckMoreValueEvent_CheckedChanged(object sender, EventArgs e)
        {
            rdVAllTime.Enabled = ckMoreValueEvent.Checked;
            rdVLast12Months.Enabled = ckMoreValueEvent.Checked;
            rdVLast6Months.Enabled = ckMoreValueEvent.Checked;
        }

        private void ckMoreQtyEvent_CheckedChanged(object sender, EventArgs e)
        {
            rdFAllTime.Enabled = ckMoreQtyEvent.Checked;
            rdFLast12Months.Enabled = ckMoreQtyEvent.Checked;
            rdFLast6Months.Enabled = ckMoreQtyEvent.Checked;
        }

        private void RFVPanel_Load(object sender, EventArgs e)
        {
            rdBasic.Checked = false;
            rdBasic.Checked = true;
            
        }

        public void ClearSelection()
        {
            ckLastEvent.Checked = false;
            ckMoreQtyEvent.Checked = false;
            ckMoreValueEvent.Checked = false;
            QtyOfEvent.Checked = false;
            ValueEvents.Checked = false;
            TimeOfEvent.Checked = false;
            rdOrder.Checked = true;
            rdBasic.Checked = true;
            rdVAllTime.Checked = true;
            rdFAllTime.Checked = true;

        }
    }
}
