using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusinessIntelligence.App.Marketing
{
    public partial class DealsSelector : UserControl
    {
        public DealsSelector()
        {
            InitializeComponent();
        }
        BusinessIntelligence.App.Marketing.Data d;
        List<Page> pages = new List<Page>();
        FormDeals formDeals;
        Dictionary<DateTime, Dictionary<string, List<Deal>>> pagesAllDeals = new Dictionary<DateTime, Dictionary<string, List<Deal>>>();
        List<Page> _SelectedPages = new List<Page>();
        public List<Page> SelectedPages
        {
          get{return _SelectedPages;}
        }
        public List<Deal> SelectedDeals
        {

            get
            {
                if (formDeals != null)
                {
                    return formDeals.SelectedDeals;
                }
                return new List<Deal>();
            }

        }


        private void populatePages()
        {
            pages = d.GetPages();
            cbPages.DataSource = pages;
 
            cbPages.DisplayMember = "title";
            cbPages.ValueMember = "page";


        }

        private void OnBtClearDealsClick(object sender, EventArgs e)
        {
            ClearDeals();
        }
        public void ClearDeals()
        {

            if (formDeals != null)
            {
                formDeals.DisposeAll();
                formDeals.Dispose();
                GC.Collect();
            }
            formDeals = null;
            fsd = null;
       
            lbOfertas.Text = "0 oferta\r\nselecionada";
            btClearDeals.Enabled = false;
            btSortDeals.Enabled = false;
            grPage.Enabled = true;
            ClearPages();
     
        }
        public void ClearPages()
        {
            _SelectedPages.Clear();
            lbPages.Text = "";
        }
        DateTime _Date = DateTime.MinValue;

        public DateTime Date
        {
            get { return _Date; }
            set
            {
                _Date = value;
                formDeals = null;
            }
        }
        string pagesTitle = null;
        private void OnBtOfertasClick(object sender, EventArgs e)
        {

            if (Date == DateTime.MinValue)
            {
                MessageBox.Show("Você precisa escolher uma data válida.");
                return;
            }
            else if (SelectedPages.Count == 0)
            {
                MessageBox.Show("Você precisa escolher alguma página.");
                return;
            }
            else
            {

                Cursor = Cursors.WaitCursor;
                try
                {
                    if (formDeals == null)
                    {
                        formDeals = new FormDeals();
                        pagesTitle = null;
                        foreach (Page p in SelectedPages)
                        {
                            pagesTitle = pagesTitle + ", " + p.title;
                            List<Deal> l;
                            Dictionary<string, List<Deal>> dic;
                            if (pagesAllDeals.TryGetValue(Date, out dic) && dic.TryGetValue(p.page, out l))
                            {
                                formDeals.DealList.AddRange(l);
                            }
                            else
                            {
                                l = d.GetDiscounts(p.page, Date);
                                if (dic != null)
                                {
                                    dic.Add(p.page, l);
                                }
                                else
                                {
                                    dic = new Dictionary<string, List<Deal>>();
                                    dic.Add(p.page, l);
                                    pagesAllDeals.Add(Date, dic);
                                }
                                formDeals.DealList.AddRange(l);
                            }

                        }

                    }

                    Cursor = Cursors.Default;
                    btClearDeals.Enabled = true;
                    grPage.Enabled = false;

                    formDeals.Text = pagesTitle.Substring(1);
                    formDeals.ShowDialog();

                    int o = formDeals.SelectedDeals.Count;
                    if (o <= 1)
                    {
                        lbOfertas.Text = o.ToString() + " oferta\r\nselecionada";
                    }
                    else
                    {
                        lbOfertas.Text = o.ToString() + " ofertas\r\nselecionadas";

                    }

                    btSortDeals.Enabled = true;
                }catch(Exception ex){
                MessageBox.Show(ex.Message);
                }
            }
        }

        private void OLoad(object sender, EventArgs e)
        {
            try
            {
                d = Data.GetInstance();
                populatePages();
            }
            catch (Exception ex)
            {

            }
        }
        FormSortDeals fsd;
        private void btSortDeals_Click(object sender, EventArgs e)
        {
            if (fsd == null)
            {
                fsd = new FormSortDeals();
                fsd.DealSelector = this;
            }
            fsd.ShowDialog();
        }

        public List<Deal> SortedSelectedDeals
        {
            get
            {
                if (fsd != null)
                {
                    var l = new List<Deal>();
                    foreach (var item in fsd.Collection)
                    {
                        l.Add(item.Value.Deal);
                    }
                    return l;
                }
                return SelectedDeals;
            }
        }

        private void btClearPage_Click(object sender, EventArgs e)
        {
            ClearPages();
        }

        private void btAddPage_Click(object sender, EventArgs e)
        {
            if (!SelectedPages.Contains((Page)cbPages.SelectedItem))
            {
                SelectedPages.Add((Page)cbPages.SelectedItem);
                lbPages.Text += ((Page)cbPages.SelectedItem).title + "; ";
                if (SelectedPages.Count % 2 == 0)
                {
                    lbPages.Text += "\r\n";
                }
            }
        }

    }

}
