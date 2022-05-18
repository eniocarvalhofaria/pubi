using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.ETL
{
    public interface IDatabaseCommand:IScriptCommand
    {
        BusinessIntelligence.Data.QueryExecutor QueryExecutor { get; set; }
    }
}
