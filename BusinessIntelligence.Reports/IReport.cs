using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Reports
{
    public interface IReport
    {
        void Refresh();
        void Publish();
    }
}
