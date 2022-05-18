using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Persistence
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoLoadableAttribute : Attribute
    {
        public AutoLoadableAttribute()
        {
        }
    }
}
