using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Persistence
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IndexedAttribute : Attribute
    {
        public IndexedAttribute()
        {
        }
    }
}
