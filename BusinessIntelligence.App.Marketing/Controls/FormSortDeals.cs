using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusinessIntelligence.App.Marketing
{
    public partial class FormSortDeals : Form
    {
        public FormSortDeals()
        {
            InitializeComponent();
        }

        DealsSelector _DealSelector;
        public void Clear()
        {
            Collection.Clear();
        }
        public DealsSelector DealSelector
        {
            get { return _DealSelector; }
            set { _DealSelector = value; }
        }
        List<DealItem> dealsItens = new List<DealItem>();
        private void OnLoad(object sender, EventArgs e)
        {
           
            int i = 0;
            Collection.Clear();
                foreach (Deal d in DealSelector.SelectedDeals)
                {

                    DealItem ds = new DealItem(d);
                    this.splitContainer1.Panel1.Controls.Add(ds);
                    ds.Selectable = false;
                    ds.Sortable = true;
                    ds.SortIndex = i;
                    ds.Collection = Collection;
                    Collection.Add(i, ds);
               
                    dealsItens.Add(ds);
                    i++;
                }
            
        }
     public   SortedDictionary<int, DealItem> Collection = new SortedDictionary<int,DealItem>();

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
