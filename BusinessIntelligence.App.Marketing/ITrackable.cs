using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.App.Marketing
{
    interface ITrackable
    {
        string utm_source { get; set; }
        string utm_medium { get; set; }
        string utm_campaign { get; set; }
        string utm_term { get; set; }
    }
}
