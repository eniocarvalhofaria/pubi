using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Marketing
{
    public class AgeRange : StoredObject
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public static StoredObject[] Search(string text)
        {
       //     var filter = FilterExpressions.
            return null;
        }
    }
}
