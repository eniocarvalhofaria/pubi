using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessIntelligence.Members.Financial;
namespace BusinessIntelligence.App.Financial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Tax PIS = null;
        Tax COFINS = null;
        Tax ISS = null;
        Data d = new Data();
        private void OnLoad(object sender, EventArgs e)
        {
            if (!d.TryConnect())
            {
                MessageBox.Show("Você não está conectado ao banco de dados");
                Environment.Exit(1);
            }
            populateTv<AccountingGroup>(tvAccounting.Nodes, AccountingGroup.GetHierarchy<AccountingGroup>());
            populateTv<ManagementGroup>(tvManagement.Nodes, ManagementGroup.GetHierarchy<ManagementGroup>());
            populateWithAccount(cbAnotherAccount,     Persistence.PersistenceSettings.PersistenceEngine.GetObjects<Account>());
            populateWithAccount(cbAccount, Persistence.PersistenceSettings.PersistenceEngine.GetObjects<Account>());

            PIS = Tax.GetTaxByDescription("PIS");
            COFINS = Tax.GetTaxByDescription("COFINS");
            ISS = Tax.GetTaxByDescription("ISS");
            label10.Text = "";
            cbAdjustmentType.DataSource = Enum.GetValues(typeof(AdjustmentType));
        }
        Dictionary<int, TreeNode> accountingGroups = new Dictionary<int, TreeNode>();
        Dictionary<int, TreeNode> managementGroups = new Dictionary<int, TreeNode>();
        private void populateTv<T>(TreeNodeCollection tn, Group[] children) where T : Group
        {
            foreach (var a in children)
            {
                TreeNode childrenNode = new TreeNode();
                childrenNode.Text = a.Description;
                childrenNode.Tag = a;
                if (a.Level == 4)
                {
                    childrenNode.ImageIndex = 1;
                    childrenNode.SelectedImageIndex = 1;
                    if (a.ObjectTypeName.Equals("ManagementGroup"))
                    {
                        managementGroups.Add(a.Id, childrenNode);
                    }
                    else
                    {
                        accountingGroups.Add(a.Id, childrenNode);
                    }
                }

                populateTv<T>(childrenNode.Nodes, a.GetChildren<T>());
                tn.Add(childrenNode);
            }
        }
        private void populateWithAccount(ComboBox cb,Account[] accounts)
        {

            cb.DataSource = accounts;
            cb.DisplayMember = "Description";
            cb.ValueMember = "Id";

        }



        private void OnAnotherAccountChange(object sender, EventArgs e)
        {
            grAnotherAccount.Enabled = ckAnotherAccount.Checked;
        }

        private void OnckTaxChange(object sender, EventArgs e)
        {
            grTax.Enabled = ckTax.Checked;
        }
        private void SelectNode(TreeNode node)
        {
            if (((Group)node.Tag).Level == 4)
            {
                if (node.TreeView.Name.Contains("Accounting"))
                {
                    txtAccountingGroup.Text = node.Text;
                    txtAccountingGroup.Tag = node.Tag;
                    accountingTreeNodeSelected = node;
                }
                else
                {
                    txtManagementGroup.Text = node.Text;
                    txtManagementGroup.Tag = node.Tag;
                    managementTreeNodeSelected = node;
                }

            }
            ExpandHierarchy(node);
        }
        private void OnTvDbClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SelectNode(e.Node);
        }


        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        TreeNode accountingTreeNodeSelected;
        TreeNode managementTreeNodeSelected;
        private void OnSelect(object sender, EventArgs e)
        {
            if (cbAnotherAccount.Enabled)
            {
                try
                {
                    accountingTreeNodeSelected = accountingGroups[((Account)cbAnotherAccount.SelectedItem).AccountingGroupAggregator.Id];
                    SelectNode(accountingTreeNodeSelected);
                }
                catch (Exception ex)
                {

                }
                managementTreeNodeSelected = managementGroups[((Account)cbAnotherAccount.SelectedItem).ManagementGroupAggregator.Id];
                SelectNode(managementTreeNodeSelected);
            }
        }
        private void ExpandHierarchy(TreeNode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Expand();
                ExpandHierarchy(node.Parent);
            }
        }



        private void OnCodKeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void OnCreate(object sender, EventArgs e)
        {
            bool isUpdate = true;
            if (txtCod.Text.Length != 6)
            {
                MessageBox.Show("Campo de código com tamanho inválido!", "Erro de cadastro");
                return;
            }
            Account a;
            a = Account.GetAccountByCod(Convert.ToInt32(txtCod.Text));
            if (a == null)
            {
                a = Persistence.PersistenceSettings.PersistenceEngine.GetNewEmptyObject<Account>();
                isUpdate = false;
            }
            if (txtManagementGroup.Tag == null)
            {
                MessageBox.Show("O grupo gerencial tem de ser preenchido!", "Erro de cadastro");
                return;
            }
            if (txtAccountingGroup.Tag == null)
            {
                MessageBox.Show("O grupo contábil tem de ser preenchido!", "Erro de cadastro");
                return;
            }
            a.Cod = Convert.ToInt32(txtCod.Text);
            a.Description = txtCod.Text + " - " + txtDescription.Text;
            a.ManagementGroupAggregator = (ManagementGroup)txtManagementGroup.Tag;
            a.AccountingGroupAggregator = (AccountingGroup)txtAccountingGroup.Tag;
            int accountingCod = 1;
            foreach (Account account in Persistence.PersistenceSettings.PersistenceEngine.GetObjects<Account>())
            {

                if (account.AccountingGroupAggregator != null && account.AccountingGroupAggregator.Id == a.AccountingGroupAggregator.Id)
                {
                    accountingCod++;
                }
            }
            a.AccountingCod = a.AccountingGroupAggregator.Description.Split('-')[0].Trim() + "." + accountingCod.ToString("D2");
            if (isUpdate)
            {
                a.Update();
            }
            else
            {
                a.Create();
            }
            if (ckTax.Checked)
            {
                if (ckISS.Checked)
                {
                    ISS.AccountsAffected.Add(a);
                    ISS.Update();
                }
                if (ckPIS.Checked)
                {
                    PIS.AccountsAffected.Add(a);
                    PIS.Update();
                }
                if (ckCOFINS.Checked)
                {
                    COFINS.AccountsAffected.Add(a);
                    COFINS.Update();
                }

            }

            populateWithAccount(cbAnotherAccount,    Persistence.PersistenceSettings.PersistenceEngine.GetObjects<Account>());
            populateWithAccount(cbAccount, Persistence.PersistenceSettings.PersistenceEngine.GetObjects<Account>());
            MessageBox.Show("Conta cadastrada com sucesso!");
        }

        private void btRefreshAccountNotRegistered_Click(object sender, EventArgs e)
        {
            dgAccountNotRegistered.DataSource = d.GetAccountNotRegistered();
   
        }

        private void SelectAccount(Account a)
        {

            if (a.Description.Contains(a.Cod.ToString()))
            {
                txtDescription.Text = a.Description.Substring(9);
            }
            else
            {

                txtDescription.Text = a.Description;
            }
            try
            {
                accountingTreeNodeSelected = accountingGroups[a.AccountingGroupAggregator.Id];
                SelectNode(accountingTreeNodeSelected);
            }
            catch (Exception ex)
            {

            }
            managementTreeNodeSelected = managementGroups[a.ManagementGroupAggregator.Id];
            SelectNode(managementTreeNodeSelected);
            ckISS.Checked = ISS.AccountsAffected.Contains(a);
            ckPIS.Checked = PIS.AccountsAffected.Contains(a);
            ckCOFINS.Checked = COFINS.AccountsAffected.Contains(a);

            if (ckISS.Checked || ckPIS.Checked || ckCOFINS.Checked)
            {
                ckTax.Checked = true;
            }
            else
            {
                ckTax.Checked = false;
            }
        }

        private void txtCod_TextChanged(object sender, EventArgs e)
        {
            if (txtCod.Text.Length == 6)
            {
                int i;
                if (Int32.TryParse(txtCod.Text, out i))
                {
                    var a = Account.GetAccountByCod(i);
                    if (a != null)
                    {
                        SelectAccount(a);
                    }
                }
            }
        }

        private void OnSelectIndexChange(object sender, EventArgs e)
        {
            switch (((TabControl)sender).SelectedIndex)
            {
                case 0:
                    {
                        this.Size = new Size(1166, this.Size.Height);
                        break;
                    }
                case 1:
                    {
                        this.Size = new Size(600, this.Size.Height);
                        break;
                    }
                case 2:
                    {
                        this.Size = new Size(547, this.Size.Height);
                        break;
                    }
            }
        }
        private List<ManagementAdjustment> adjustments = new List<ManagementAdjustment>();
        private void OnAddAdjustment(object sender, EventArgs e)
        {
            if (txtValueAdjustment.Text == null)
            {
                return;
            }
            var adj = Persistence.PersistenceSettings.PersistenceEngine.GetNewEmptyObject<ManagementAdjustment>();
            adj.AdjustmentDate = dtAdjustment.Value;
            adj.AdjustmentValue = Convert.ToDecimal(txtValueAdjustment.Text.Replace(",", "."));
            adjustments.Add(adj);
            label10.Text += "\r\nData: " + dtAdjustment.Value.ToString("yyyy-MM-dd") + "      Valor: R$" + txtValueAdjustment.Text;

        }

        private void OnAdjustmentClear(object sender, EventArgs e)
        {
            adjustments.Clear();
            label10.Text = "";
        }

        private void OnDecimalKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
           
            
        }

        private void OnCreateAdjustment(object sender, EventArgs e)
        {
            var inv = Persistence.PersistenceSettings.PersistenceEngine.GetNewEmptyObject<Invoice>();
            inv.Account = (Account)cbAccount.SelectedItem;
            inv.DocumentNumber = Convert.ToInt32(txtDocument.Text);
            inv.InvoiceNumber = Convert.ToInt32(txtInvoiceNumber.Text);
            inv.InvoiceDate = dtInvoice.Value;
            inv.Supplier = txtSupplier.Text;
            AdjustmentType a;
            Enum.TryParse<AdjustmentType>(cbAdjustmentType.SelectedItem.ToString(), out a);
            inv.AdjustmentType = a;
            inv.Create();

            foreach (var adj in adjustments)
            {
                adj.Invoice = inv;
                adj.Create();
            }
            MessageBox.Show("Ajuste cadastrado com sucesso!");
        }

    }

}