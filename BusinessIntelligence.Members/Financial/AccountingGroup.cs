using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Financial
{
    public class AccountingGroup : Group
    {
        AccountingGroup _AccountingGroupAggregator;
        public AccountingGroup AccountingGroupAggregator
        {
            get { return _AccountingGroupAggregator; }
            set
            {
                _AccountingGroupAggregator = value;
                GroupAggregator = value;
            }
        }

    }
}
