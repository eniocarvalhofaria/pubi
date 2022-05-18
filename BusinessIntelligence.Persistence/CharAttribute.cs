using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Persistence
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CharAttribute : Attribute
    {

        public CharAttribute(bool hasFixedLength,int maxLength)
        {
            this._HasFixedLength = hasFixedLength;
            this._MaxLength = maxLength;
        }
        
        private bool _HasFixedLength = false;
        public bool HasFixedLength
        {
            get { return _HasFixedLength; }
        }

        private int _MaxLength = 255;

        public int MaxLength
        {
            get { return _MaxLength; }

        }

      
    }
}
