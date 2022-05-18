using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Reports
{
   public class ExcelReport: IReport
    {

        private string _ReportFile;

        public string ReportFile
        {
            get { return _ReportFile; }
            set { _ReportFile = value; }
        }

        public void Refresh()
        { }

        public void Publish()
        { }

    }
}
