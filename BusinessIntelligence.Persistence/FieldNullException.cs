using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Persistence
{
    public class FieldNullException : System.ArgumentNullException
    {
        public FieldNullException(string fieldName)
            : base(fieldName)
        { }
        public FieldNullException(string[] fieldNames)
            : base()
        {
            foreach (string item in fieldNames)
            {
                this.Data.Add(item, item);

            }
        }
    }
}
