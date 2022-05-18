using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Persistence
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DecimalAttribute : Attribute
    {
        private int _TotalDigits = 18;

        public int TotalDigits
        {
            get { return _TotalDigits; }
            set { _TotalDigits = value; }
        }
       
        private int _DecimalPlaces = 2;
        public int DecimalPlaces
        {
            get { return _DecimalPlaces; }
            set { _DecimalPlaces = value; }
        }
      
    }
}
