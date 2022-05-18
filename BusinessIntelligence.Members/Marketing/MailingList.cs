using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Members.Marketing
{
    public class MailingList : Persistence.StoredObject
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private int _PageRelatedId;
        public int PageRelatedId
        {
            get { return _PageRelatedId; }
            set { _PageRelatedId = value; }
        }
    }
}
