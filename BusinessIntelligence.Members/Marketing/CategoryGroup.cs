using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Marketing
{
    public class CategoryGroup : HierarchyItem
    {
        public new List<Category> Children
        {
            get { return base.Children.Cast<Category>().ToList(); }
            set { base.Children = value.Cast<HierarchyItem>().ToList(); }
        }
        public new CategoryType Owner
        {
            get { return (CategoryType)base.Owner; }
            set { base.Owner = value; }
        }
    }
}