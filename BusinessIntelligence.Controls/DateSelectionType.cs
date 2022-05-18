using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence
{
    public enum DateCriterialType
    {
        Today = 1,
        Yesterday = 2,
        ThisWeek = 3,
        PreviousWeek = 4,
        ThisMonth = 5,
        PreviousMonth = 6,
        ThisQuarter = 7,
        PreviousQuarter = 8,
        ThisYear = 9,
        PreviousYear = 10,
        Last30Days = 11,
        Last60Days = 12,
        Last90Days = 13,
        Last120Days = 14,
        Last180Days = 15,
        Last365Days = 16,
        Between = 17,
        NotBetween = 18,
        Between31And60 = 19,
        Between61And90 = 20,
        Before90 = 21

    }
}
