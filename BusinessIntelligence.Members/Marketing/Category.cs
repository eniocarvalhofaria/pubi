using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Marketing
{
    public class Category : HierarchyItem
    {
        public new List<SubCategory> Children
        {
            get { return base.Children.Cast<SubCategory>().ToList(); }
            set { base.Children = value.Cast<HierarchyItem>().ToList(); }

        }
        public new CategoryGroup Owner
        {
            get { return (CategoryGroup)base.Owner; }
            set { base.Owner = value; }
        }
    }
}
