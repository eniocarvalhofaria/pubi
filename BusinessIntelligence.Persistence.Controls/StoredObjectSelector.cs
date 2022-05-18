using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Persistence.Controls
{
    public partial class StoredObjectSelector : UserControl
    {
        public StoredObjectSelector()
        {
            InitializeComponent();
        }
        List<StoredObject> _AllObjects = new List<StoredObject>();
        public List<StoredObject> AllObjects
        {
            get { return _AllObjects; }
        }
        List<StoredObject> _SelectedObjects = new List<StoredObject>();
        public List<StoredObject> SelectedObjects
        {
            get { return _SelectedObjects; }
        }
        Dictionary<string, Persistence.StoredObject> ListByName = new Dictionary<string, StoredObject>();
        public void Populate<T>(string displayMember) where T : Persistence.StoredObject
        {

            var objs = BusinessIntelligence.Persistence.PersistenceSettings.PersistenceEngine.GetObjects<T>(_FilterExpression, _SortExpression);
            //     combo.DataSource = objs;
            foreach (var obj in objs)
            {
                combo.Items.Add(obj);
                fsl.AddItem(obj.GetValue(displayMember).ToString());
                ListByName.Add(obj.GetValue(displayMember).ToString(), obj);
            }
            AllObjects.AddRange(objs);
            combo.DisplayMember = displayMember;
            combo.ValueMember = "Id";


        }
        public bool IsContained
        {
            get { return rdIn.Checked; }
            set
            {

                rdIn.Checked = value;
                rdNotIn.Checked = !value;
            }
        }
        IFilterExpression _FilterExpression;

        public IFilterExpression FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        SortExpression _SortExpression;

        public SortExpression SortExpression
        {
            get { return _SortExpression; }
            set { _SortExpression = value; }
        }
        public void ClearSelection()
        {
            SelectedObjects.Clear();
            lbLists.Text = "";

        }
        DateTime _Date = DateTime.MinValue;

        private void btClearList_Click(object sender, EventArgs e)
        {
            ClearSelection();
            fsl.SelectedItems.Clear();
            btAddItem.Enabled = true;
        }
        public void SelectItems(StoredObject[] items)
        {
            foreach (var item in items)
            {
                SelectItem(item);
            }
        }
        public void SelectItem(StoredObject item)
        {
            if (!SelectedObjects.Contains(item))
            {
                SelectedObjects.Add(item);
                if (!fsl.SelectedItems.Contains(item.GetValue(combo.DisplayMember).ToString()))
                {
                    fsl.SelectItem(item.GetValue(combo.DisplayMember).ToString());
                }
                lbLists.Text += (item).GetValue(combo.DisplayMember).ToString() + "; ";
                if (SelectedObjects.Count % 2 == 0)
                {
                    lbLists.Text += "\r\n";
                }
            }
            btAddItem.Enabled = MultipleSelection;
            tt = new ToolTip();
            tt.ToolTipIcon = ToolTipIcon.Info;
            tt.IsBalloon = true;
            tt.ShowAlways = true;
            tt.SetToolTip(lbLists, lbLists.Text);
        }
        private void btAddList_Click(object sender, EventArgs e)
        {
            SelectItem((StoredObject)combo.SelectedItem);
        }
        int _SelectedId;

        public int SelectedId
        {
            get { return _SelectedId; }
            set { _SelectedId = value; }
        }

        private bool _MultipleSelection = true;
        [System.ComponentModel.Category("Custom")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool MultipleSelection
        {
            get { return _MultipleSelection; }
            set { _MultipleSelection = value; }
        }
        ToolTip tt = new ToolTip();
        private void StoredObjectSelector_Load(object sender, EventArgs e)
        {


        }
        public bool Checked { get { return ckEnable.Checked; } set { ckEnable.Checked = value; } }
        FormSelectedList fsl = new FormSelectedList();
        private void btManageItens_Click(object sender, EventArgs e)
        {
            fsl.ShowDialog();
            SelectedObjects.Clear();
            foreach (var s in fsl.SelectedItems)
            {
                SelectItem(ListByName[s]);
            }
        }
        bool loaded = false;
        private void ckEnable_CheckedChanged(object sender, EventArgs e)
        {
            grSelector.Enabled = ckEnable.Checked;
            if (!loaded && LoadAction != null)
            {

                LoadAction.Invoke();
                loaded = true;
            }
        }
        [System.ComponentModel.Category("Custom")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return ckEnable.Text;
            }
            set
            {
                ckEnable.Text = value;
            }
        }
        public Action LoadAction { get; set; }
    }

}
