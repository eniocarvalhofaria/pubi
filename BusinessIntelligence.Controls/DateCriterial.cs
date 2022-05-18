using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence
{
   public class DateCriterial
    {
        public DateTime StartDate
        {
            get;
            set;
        }
        DateTime _EndDate;
        public DateTime EndDate
        {
            get;
            set;
        }

        public DateCriterialType DateTypeSelected
        {
            get;
            set;
        }
    }
}
