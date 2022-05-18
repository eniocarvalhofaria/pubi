using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Financial
{
    public class ManagementGroup : Group
    {
        ManagementGroup _ManagementGroupAggregator;
        public ManagementGroup ManagementGroupAggregator
        {
            get { return _ManagementGroupAggregator; }
            set
            {
                _ManagementGroupAggregator = value;
                GroupAggregator = value;
            }
        }
    }
}
