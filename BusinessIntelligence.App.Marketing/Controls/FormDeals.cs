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
    public partial class FormDeals : Form
    {
        public FormDeals()
        {
            InitializeComponent();
        }

        private List<Deal> _DealList = new List<Deal>();
        public void DsCheckChanged(object sender, EventArgs e)
        {
            if (((DealItem)sender).IsSelected)
            {
                if (!_SelectedDeals.Contains(((DealItem)sender).Deal))
                {
                    _SelectedDeals.Add(((DealItem)sender).Deal);
                }

            }
            else
            {
                _SelectedDeals.Remove(((DealItem)sender).Deal);
            }
        }
        public List<Deal> DealList
        {
            get { return _DealList; }
            set { _DealList = value; }
        }
        private List<Deal> CurrentDealList = new List<Deal>();
        List<DealItem> dealSelectors = new List<DealItem>();
        List<int> dealAdded = new List<int>();
        int dealSelectorsLimit = 20;
        int totalPages = 0;
        private void recaculatePages()
        {
            if (CurrentDealList.Count % dealSelectorsLimit > 0)
            {
                totalPages = Convert.ToInt32(CurrentDealList.Count / dealSelectorsLimit) + 1;
            }
            else
            {
                totalPages = Convert.ToInt32(CurrentDealList.Count / dealSelectorsLimit);
            }


        }
        private void OnLoad(object sender, EventArgs e)
        {
            menuStrip1.SendToBack();
            int i = 0;
            CurrentDealList.Clear();
            CurrentDealList.AddRange(DealList);
            foreach (Deal d in CurrentDealList)
            {
                if (!dealAdded.Contains(d.unified_discount_id))
                {
                    if (i < dealSelectorsLimit)
                    {
                        DealItem ds = new DealItem(d);
                        ds.SelectedChange += new System.EventHandler(this.DsCheckChanged);
                        ds.Location = new Point(0, ((ds.Height + 10) * i) + 10);
                        ds.Sortable = false;
                        this.panel1.Controls.Add(ds);
                        dealSelectors.Add(ds);

                    }
                    dealAdded.Add(d.unified_discount_id);
                    i++;
                }
                else
                {
                    DealList.Remove(d);
                }
            }
           setPage(1);
        }
        private List<Deal> _SelectedDeals = new List<Deal>();
        public List<Deal> SelectedDeals
        {

            get
            {
                return _SelectedDeals;
            }
        }

        private void OnBtCloseClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnResize(object sender, EventArgs e)
        {
            //       MessageBox.Show("Width: " + this.Size.Width.ToString() + " Height: " + this.Size.Height.ToString());
        }

        private void mostrarTodasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentDealList.Clear();
            CurrentDealList.AddRange(DealList);
            setPage(1);
        }

        private void marcarTodasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SelectedDeals.Clear();
            _SelectedDeals.AddRange(CurrentDealList);

            refreshPage();
        }
        private void refreshPage()
        {
            setPage(currentpage);
        }
        private void desmarcarTodasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SelectedDeals.Clear();
            refreshPage();
        }
        public void DisposeAll()
        {
            foreach (var ds in dealSelectors)
            {
                ds.Dispose();
            }
            dealSelectors = null;
            GC.Collect();
        }
        private void mostrarSelecionadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentDealList.Clear();
            CurrentDealList.AddRange(SelectedDeals);
            setPage(1);
        }
        private void pesquisarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            CurrentDealList.Clear();
            foreach (var d in DealList)
            {
                if (d.title.ToLower().Contains(txtFind.Text.ToLower()) || d.deal_category.ToLower().Contains(txtFind.Text.ToLower()) || d.partner.name.ToLower().Contains(txtFind.Text.ToLower()) || d.partner.neighborhood.ToLower().Contains(txtFind.Text.ToLower()))
                {
                    CurrentDealList.Add(d);
                    i++;
                }
                else
                {
                    if (d.locations != null)
                    {
                        foreach (var l in d.locations)
                        {
                            if (l.neighborhood.ToLower().Contains(txtFind.Text.ToLower()))
                            {
                                CurrentDealList.Add(d);
                            }
                        }
                    }

                }

            }
            setPage(1);
        }



        private void btFirstPage_Click(object sender, EventArgs e)
        {
            setPage(1);
        }

        private void btLastPage_Click(object sender, EventArgs e)
        {
            setPage(totalPages);
        }

        private void btPreviousPage_Click(object sender, EventArgs e)
        {
            setPage(currentpage - 1);
        }

        private void btNextPage_Click(object sender, EventArgs e)
        {
            setPage(currentpage + 1);
        }
        private int currentpage = 0;
        private void setPage(int pageSelected)
        {
            if (pageSelected == 1)
            {
                recaculatePages();
            }
            this.btFirstPage.Enabled = pageSelected > 1;
            this.btPreviousPage.Enabled = pageSelected > 1;
            this.btNextPage.Enabled = pageSelected < totalPages;
            this.btLastPage.Enabled = pageSelected < totalPages;
            int firstDealIndex = ((pageSelected - 1) * dealSelectorsLimit) + 1;
            int lastDealIndex = pageSelected * dealSelectorsLimit;
            if (lastDealIndex > CurrentDealList.Count)
            {
                lastDealIndex = CurrentDealList.Count;
            }
            int dealSelectorIndex = 0;
            for (int d = firstDealIndex; d <= lastDealIndex; d++)
            {
                var ds = dealSelectors[dealSelectorIndex];

                ds.Deal = CurrentDealList[d - 1];
                ds.Visible = true;
                ds.IsSelected = _SelectedDeals.Contains(ds.Deal);
                dealSelectorIndex++;
            }

            if (dealSelectorIndex < dealSelectorsLimit - 1)
            {
                for (int d = dealSelectorIndex; d < dealSelectorsLimit; d++)
                {
                    dealSelectors[d].Visible = false;
                }

            }

            lbCurrentPage.Text = firstDealIndex.ToString() + " a " + lastDealIndex.ToString() + " de " + CurrentDealList.Count;

            currentpage = pageSelected;
        }
    }
}
