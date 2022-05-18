using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusinessIntelligence.Persistence.Controls
{
    public partial class FormSelectedList : Form
    {
        public FormSelectedList()
        {
            InitializeComponent();
        }
        StoredObjectSelector _SelectorOwner;

        public StoredObjectSelector SelectorOwner
        {
            get { return _SelectorOwner; }
            set { _SelectorOwner = value; }
        }
        public void AddItem(string name)
        {
            cklItens.Items.Add(name);
        }
        public void SelectItem(string name)
        {
            SelectedItems.Add(name);
            for (int i =0; i<cklItens.Items.Count; i++)
            {
                if (cklItens.Items[i].ToString().Equals(name))
                {
                    cklItens.SetItemChecked(i, true);
                }
            }
        }
        public void ClearSelecteds()
        {
            SelectedItems.Clear();
        }
        private void FormSelectedList_Load(object sender, EventArgs e)
        {

        }
        private List<String> _SelectedItems = new List<string>();

        public List<String> SelectedItems
        {
            get { return _SelectedItems; }
            set { _SelectedItems = value; }
        }
        private void btOk_Click(object sender, EventArgs e)
        {
            SelectedItems.Clear();
            for (int i = 0; i < cklItens.CheckedItems.Count; i++)
            {

                SelectedItems.Add(cklItens.CheckedItems[i].ToString());
            }

            this.Close();
        }

        private void cklItens_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cklItens_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (e.NewValue == CheckState.Checked)
            {
           //     SelectedItems.Add();
            }
            else {

            }
         
        }
    }
}
