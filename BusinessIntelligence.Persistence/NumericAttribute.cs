using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Persistence
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NumericAttribute : Attribute
    {
        public NumericAttribute(Int64 minValue, Int64 maxValue)
        {
            _MinValue = minValue;
            _MaxValue = maxValue;
        }

        private Int64 _MinValue = Int64.MinValue;
        public Int64 MinValue
        {
            get { return _MinValue; }

        }      
        private Int64 _MaxValue = Int64.MaxValue;
        public Int64 MaxValue
        {
            get { return _MaxValue; }

        }
      
    }
}
