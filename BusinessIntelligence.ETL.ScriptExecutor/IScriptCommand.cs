using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.ETL
{
    public interface IScriptCommand
    {
        void Execute();

        bool HasExecutionError { get; set; }
        bool IsInTry { get; set; }
      
    }
}
