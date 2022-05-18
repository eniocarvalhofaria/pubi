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
    public partial class DealItem : UserControl
    {
        public DealItem(Deal deal)
        {
            InitializeComponent();
            Deal = deal;
            lbName.ForeColor = Color.FromArgb(50,50,50);
            lbName.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private HtmlDeal hd;
        Deal _Deal;

        public Deal Deal
        {
            get { return _Deal; }
            set
            {
                _Deal = value;
                hd = new HtmlDeal(value);
               
                lbName.Text = hd.Name;
                pictureBox1.ImageLocation = value.images[0].image.Split('&')[0] + "&w=128&h=82";
                if (string.IsNullOrEmpty(hd.Neighborhood))
                {
                    imMarker.Visible = false;
                }else{
                 imMarker.Visible = true;
                 if (hd.Neighborhood.Split(',').Length <= 5)
                 {
                     lbNeighborhood.Text = hd.Neighborhood;
                 }
                 else
                 {
                     bool isFirst = true;
                     lbNeighborhood.Text = null;
                     for(int i =0 ; i< 5;i++)
                     {
                         if (isFirst)
                         {
                             lbNeighborhood.Text += hd.Neighborhood.Split(',')[i];
                             isFirst = false;
                         }
                         else
                         {
                             lbNeighborhood.Text += "," + hd.Neighborhood.Split(',')[i];
                         }
                      
                     }
                     lbNeighborhood.Text += " + " + (hd.Neighborhood.Split(',').Length - 5).ToString();
                 }
              
                }
            }
        }
        public bool IsSelected
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }

        public bool Selectable
        {
            get { return checkBox1.Visible; }
            set { checkBox1.Visible = value; }
        }
        public bool Sortable
        {
            get
            {

                return imDown.Visible || imUp.Visible;
            }
            set
            {
                imDown.Visible = value;
                imUp.Visible = value;
                imTop.Visible = value;
            }
        }
        private void imUp_Click(object sender, EventArgs e)
        {
            ToUp();
        }

        private void imDown_Click(object sender, EventArgs e)
        {
            ToDown();
        }
        void ToTop()
        {
            if (SortIndex > 0)
            {
                int oldIndex;
                oldIndex = this.SortIndex;
                this.SortIndex = 0;

                for (int i = oldIndex - 1; i >= 0; i--)
                {
                    Collection[i + 1] = Collection[i];
                    Collection[i + 1].SortIndex = i + 1;
                }

                Collection[0] = this;

            }
        }
        void ToUp()
        {
            if (SortIndex > 0)
            {

                Collection[this.SortIndex] = Collection[this.SortIndex - 1];
                Collection[this.SortIndex - 1] = this;
                Collection[this.SortIndex].SortIndex = this.SortIndex;
                this.SortIndex -= 1;

            }
        }
        void ToDown()
        {
            if (SortIndex < Collection.Count - 1)
            {
                Collection[this.SortIndex] = Collection[this.SortIndex + 1];
                Collection[this.SortIndex + 1] = this;
                Collection[this.SortIndex].SortIndex = this.SortIndex;
                this.SortIndex += 1;

            }
        }
        int minLocation()
        {
            if (Collection == null)
            {
                return 10;
            }
            else if (Collection[0].SortIndex == 0)
            {

                return Collection[0].Location.Y;

            }
            else if (Collection[1].SortIndex == 0)
            {
                return (Collection == null ? 0 : Collection[1].Location.Y);
            }
            else
            {
                return 10;
            }

        }
        int _SortIndex;

        public int SortIndex
        {
            get { return _SortIndex; }
            set
            {
                this.Location = new Point(0, ((this.Height + 10) * value) + minLocation() );
                _SortIndex = value;
            }
        }
        SortedDictionary<int, DealItem> _Collection;

        public SortedDictionary<int, DealItem> Collection
        {
            get { return _Collection; }
            set { _Collection = value; }
        }

        private void imTop_Click(object sender, EventArgs e)
        {
            ToTop();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            EventHandler handler = SelectedChange;
            handler(this, new EventArgs());
        }

        public event EventHandler SelectedChange;
    
 

    }
}
