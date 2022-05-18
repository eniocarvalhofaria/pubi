using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Persistence
{
    public abstract class HierarchyItem : PersistentObject
    {
        public bool IsValid { get; set; }
        public string Name { get; set; }
        public HierarchyItem Owner { get; set; }
        public List<HierarchyItem> Children
        {
            get
            {
                return _Children;
            }

            set
            {
                _Children = value;
            }
        }

        private List<HierarchyItem> _Children = new List<HierarchyItem>();
    }
}
