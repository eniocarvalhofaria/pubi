using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Marketing
{
    public class CampaignBasic : StoredObject
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private DateTime _SentDate;

        public DateTime SentDate
        {
            get { return _SentDate; }
            set { _SentDate = value; }
        }

    }
}
