using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Marketing
{
    public class SubCategory : HierarchyItem
    {
        public new Category Owner
        {
            get { return (Category)base.Owner; }
            set { base.Owner = value; }
        }
    }
}
