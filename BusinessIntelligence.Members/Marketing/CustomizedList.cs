using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Marketing
{
    public class CustomizedList : PersistentObject
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _SqlText;
        [Char(false, 8000)]
        public string SqlText
        {
            get { return _SqlText; }
            set { _SqlText = value; }
        }
        public static CustomizedList GetByCustomizedListName(string name)
        {
            CustomizedList[] u = PersistenceSettings.PersistenceEngine.GetObjects<CustomizedList>(FilterExpressions.Equal("Name", name));
            if (u.Length > 0)
            {
                return u[0];
            }
            else
            { return null; }

        }
    }
}
