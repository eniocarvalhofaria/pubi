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
    public partial class FormBigListSelector : FormSelector
    {
        public FormBigListSelector()
        {
            InitializeComponent();
        }
     

        public List<string> SelectedItems
        {
            get {

                List<string> _SelectedItems = new List<string>();
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (item.Cells[0].Value != null && (bool)(item.Cells[0].Value))
                    {
                        _SelectedItems.Add(item.Cells[1].Value.ToString());
                    }
                }
                return _SelectedItems; 
            
            }

        }
        private BigListSelector _BigListSelector;

        public BigListSelector BigListSelector
        {
            get { return _BigListSelector; }
            set { _BigListSelector = value; }
        }


        private void btSearch_Click(object sender, EventArgs e)
        {

            BigListSelector bls = BigListSelector;

            string query = "select " + bls.IdField + " , " + bls.NameField + " from " + bls.QualifiedObjectName + " where lower(" + bls.NameField + ") like '%" + txtSearch.Text.ToLower().Replace("'","''") + "%'";
            var dt = bls.QueryEecutor.ReturnData(query);

            dataGridView1.DataSource = null;
            DataTable ds = new DataTable();
            ds.Columns.Add("Selecionado", typeof(bool));
            ds.Columns.Add("Id", typeof(string));
            ds.Columns.Add("Descrição", typeof(string));

            foreach (DataRow r in dt.Rows)
            {
                var nr = ds.NewRow();
                nr[0] = true;
                nr[1] = r[0];
                nr[2] = r[1];
                ds.Rows.Add(nr);
            }
            dataGridView1.DataSource = ds;

           dataGridView1.Columns[0].Width = 80;
           dataGridView1.Columns[1].Width = 80;
           dataGridView1.Columns[2].Width = 800;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
