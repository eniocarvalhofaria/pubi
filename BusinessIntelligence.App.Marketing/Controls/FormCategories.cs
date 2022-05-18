using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessIntelligence.Members.Marketing;
namespace BusinessIntelligence.App.Marketing
{
    public partial class FormCategories : Form
    {
        public FormCategories()
        {
            InitializeComponent();
        }
        bool loaded = false;
        private void FormCategories_Load(object sender, EventArgs e)
        {
            if (!loaded)
            {
                Persistence.PersistenceSettings.PersistenceEngine.GetObjects<SubCategory>();
                Persistence.PersistenceSettings.PersistenceEngine.GetObjects<Category>();
                Persistence.PersistenceSettings.PersistenceEngine.GetObjects<CategoryGroup>();
                var categoryTypes = Persistence.PersistenceSettings.PersistenceEngine.GetObjects<CategoryType>();

                foreach (var categoryType in categoryTypes)
                {
                    if (categoryType.IsValid)
                    {
                        var tn0 = new TreeNode(categoryType.Name);
                        tn0.Tag = categoryType;
                        foreach (var categoryGroup in categoryType.Children)
                        {
                            if (categoryGroup.IsValid)
                            {
                                var tn1 = new TreeNode(categoryGroup.Name);
                                tn1.Tag = categoryGroup;
                                foreach (var category in categoryGroup.Children)
                                {
                                    if (category.IsValid)
                                    {
                                        var tn2 = new TreeNode(category.Name);
                                        tn2.Tag = category;
                                        foreach (var subcategory in category.Children)
                                        {
                                            if (subcategory.IsValid)
                                            {
                                                var tn3 = new TreeNode(subcategory.Name);
                                                tn3.Tag = subcategory;
                                                tn2.Nodes.Add(tn3);
                                            }
                                        }
                                        tn1.Nodes.Add(tn2);
                                    }
                                }
                                tn0.Nodes.Add(tn1);
                            }
                        }
                        treeView1.Nodes.Add(tn0);
                    }
                }
                loaded = true;
            }
        }
        TreeNode treeNodeAction = null;
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                treeNodeAction = e.Node;
            }

            if (e.Action == TreeViewAction.ByMouse)
            {
                foreach (TreeNode n in e.Node.Nodes)
                {
                    n.Checked = e.Node.Checked;
                }
            }
            if (e.Node.Parent != null && e.Action == TreeViewAction.ByMouse)
            {
                if (e.Node.Parent.Checked && !e.Node.Checked)
                {
                    e.Node.Parent.Checked = false;
                }
                else if (e.Node.Checked)
                {
                    bool allChecked = true;
                    foreach (TreeNode n in e.Node.Parent.Nodes)
                    {
                        if (!n.Checked)
                        {
                            allChecked = false;
                        }
                    }
                    if (allChecked)
                    {
                        e.Node.Parent.Checked = true;
                    }
                }
            }
        }
        public string GetExpression()
        {
            selectedCategories.Clear();
            GetSelectedCategories(treeView1.Nodes);
            if (selectedCategories.Count > 0)
            {
                return rfvPanel1.GetExpression(selectedCategories, "reports.UserAccountMainCategories", "reports.UserAccountCategories");
            }
            else
            {
                MessageBox.Show("Você não selecionou nenhuma categoria.");
                return null;
            }
        }
        private void btOk_Click(object sender, EventArgs e)
        {
            selectedCategories.Clear();
            var exp = GetExpression();
            if (string.IsNullOrEmpty(exp))
            {
                MessageBox.Show("Você não selecionou nenhum filtro.");
            }

            this.Close();
        }
        public void ClearSelection()
        {
            rfvPanel1.ClearSelection();
            foreach (TreeNode n in treeView1.Nodes)
            {
                ClearNode(n);
            }
        }
        Dictionary<string, List<int>> selectedCategories = new Dictionary<string, List<int>>();
        private Dictionary<string, List<int>> GetSelectedCategories(TreeNodeCollection tnc)
        {

            foreach (TreeNode tn in tnc)
            {
                if (tn.Checked)
                {
                    if (tn.Tag is CategoryType)
                    {
                        if (!selectedCategories.ContainsKey("CategoryTypeId"))
                        {
                            selectedCategories.Add("CategoryTypeId", new List<int>());
                        }
                        selectedCategories["CategoryTypeId"].Add(((CategoryType)tn.Tag).Id);
                    }
                    if (tn.Tag is CategoryGroup)
                    {
                        if (!selectedCategories.ContainsKey("CategoryGroupId"))
                        {
                            selectedCategories.Add("CategoryGroupId", new List<int>());
                        }
                        selectedCategories["CategoryGroupId"].Add(((CategoryGroup)tn.Tag).Id);
                    }
                    if (tn.Tag is Category)
                    {
                        if (!selectedCategories.ContainsKey("CategoryId"))
                        {
                            selectedCategories.Add("CategoryId", new List<int>());
                        }
                        selectedCategories["CategoryId"].Add(((Category)tn.Tag).Id);
                    }
                    if (tn.Tag is SubCategory)
                    {
                        if (!selectedCategories.ContainsKey("SubCategoryId"))
                        {
                            selectedCategories.Add("SubCategoryId", new List<int>());
                        }
                        selectedCategories["SubCategoryId"].Add(((SubCategory)tn.Tag).Id);
                    }
                }
                else
                {
                    GetSelectedCategories(tn.Nodes);
                }
            }
            return selectedCategories;
        }
        bool inMovement = false;
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!inMovement)
            {
                inMovement = true;
                this.Width = splitContainer1.SplitterDistance + 907;
                splitContainer1.Dock = DockStyle.Fill;
            }
            else
            {

                inMovement = false;
            }
        }

        private void btClearCategories_Click(object sender, EventArgs e)
        {
            foreach (TreeNode n in treeView1.Nodes)
            {
                ClearNode(n);
            }
        }
        private void ClearNode(TreeNode node)
        {
            node.Checked = false;
            foreach (TreeNode n in node.Nodes)
            {
                ClearNode(n);
            }
        }
    }
}
