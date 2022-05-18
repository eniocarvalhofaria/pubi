using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Report
{
    public class TableToFilter
    {
        public TableToFilter(string sheetName, string columnName)
        {
            SheetName = sheetName;
            ColumnName = columnName;
        }
        public string SheetName { get; set; }
        public string ColumnName { get; set; }

    }
}
