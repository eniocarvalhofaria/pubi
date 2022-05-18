using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Marketing
{
    public class CategoryType : HierarchyItem
    {
        public new List<CategoryGroup> Children
        {
            get { return base.Children.Cast<CategoryGroup>().ToList(); }
            set { base.Children = value.Cast<HierarchyItem>().ToList(); }
        }
    }
}
