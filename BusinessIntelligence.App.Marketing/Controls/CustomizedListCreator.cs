using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessIntelligence.Members.Marketing;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.App.Marketing
{
    public partial class CustomizedListCreator : UserControl
    {
        public CustomizedListCreator()
        {
            InitializeComponent();
        }

        private void btNext1_Click(object sender, EventArgs e)
        {

            CustomizedList a = CustomizedList.GetByCustomizedListName(txtName.Text);

            if (a != null)
            {
                MessageBox.Show("Já existe uma lista customizada com esse nome!\r\nEscolha outro nome para a lista que quer criar");
                return;
            }
            customizedList = Persistence.PersistenceSettings.PersistenceEngine.GetNewEmptyObject<CustomizedList>();
            customizedList.Name = txtName.Text;

            if (rdSql.Checked)
            {
       
                showTab(tbSql);
                btReturn2.Visible = true;
            }
            else if (rdFile.Checked)
            {
                showTab(tbFile);
            }
        }
        CustomizedList customizedList;
        private void btCreate_Click(object sender, EventArgs e)
        {
            if (btCreate.Text == "Voltar")
            {
                showTab(tbMain);
                btCreate.Text = "Criar";
            }
            else
            {
                if (txtSql.Text == "")
                {
                    MessageBox.Show("Você deve preencher a caixa com um sql válido para o Redshift", "Erro no Sql", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var a = MailingSelectorData.GetInstance("REDSHIFT").TestSqlList(txtSql.Text);
                    if (a.Sucess)
                    {
                        customizedList.SqlText = txtSql.Text;
                        customizedList.Create();
                        MessageBox.Show(a.Message);
                        showTab(tbMain);
                    }
                    else
                    {
                        MessageBox.Show(a.Message, "Erro no Sql", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btInsert_Click(object sender, EventArgs e)
        {
            txtSql.Text = "";
            txtName.Text = "";
            showTab(tbListType);
        }

        private void showTab(TabPage tab)
        {
            foreach (TabPage t in tabControl1.TabPages)
            {
                tabControl1.TabPages.Remove(t);
            }
            tabControl1.TabPages.Add(tab);
        }
        private void btSelect_Click(object sender, EventArgs e)
        {
            LoadList();
            showTab(tbSelect);
            btNext2.Text = "Ok";
            btReturn2.Visible = false;
        }
        private void LoadList()
        {
            CustomizedList[] all = Persistence.PersistenceSettings.PersistenceEngine.GetObjects<CustomizedList>();

            comboBox1.DataSource = all;
            comboBox1.DisplayMember = "Name";

        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            LoadList();
            showTab(tbSelect);
            btNext2.Text = "Apagar";
        }

        private void CustomizedListCreator_Load(object sender, EventArgs e)
        {
            MailingSelectorData.GetInstance("REDSHIFT");
            showTab(tbMain);
        }

        private void btNext2_Click(object sender, EventArgs e)
        {
            customizedList = (CustomizedList)comboBox1.SelectedItem;
            if (btNext2.Text == "Apagar")
            {
                DialogResult d = MessageBox.Show("Tem certeza que deseja excluir a lista \"" + customizedList.Name + "\"?", "Exclusão de lista", MessageBoxButtons.YesNo);
                if (d == DialogResult.Yes)
                {
                    customizedList.Delete();
                }
                showTab(tbMain);
            }
            else if (btNext2.Text == "Ok")
            {
              
                txtSql.Text = customizedList.SqlText;
                //TODO Rotina de select
                btCreate.Text = "Voltar";
                showTab(tbSql);
            }
        }
        private void btSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            lbFileName.Text = openFileDialog1.FileName;
        }

        private void btReturrn_Click(object sender, EventArgs e)
        {
            gotoMain();
        }
        private void gotoMain()
        {
            showTab(tbMain);
         
        }

        private void btReturn2_Click(object sender, EventArgs e)
        {
            gotoMain();
        }

        private void btLoadFile_Click(object sender, EventArgs e)
        {
           var a =  MailingSelectorData.GetInstance("REDSHIFT").CreateListFromFile(lbFileName.Text, customizedList);
           MessageBox.Show(a.Message);
            showTab(tbMain);
        }
    }
}
