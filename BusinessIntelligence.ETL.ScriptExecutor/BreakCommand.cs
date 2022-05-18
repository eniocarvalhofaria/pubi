using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.ETL
{
    class BreakCommand: IScriptCommand
    {
        private bool _HasExecutionError = false;
        public bool HasExecutionError
        {
            get { return _HasExecutionError; }
            set { _HasExecutionError = value; }
        }
        private bool _IsInTry = false;
        public bool IsInTry
        {
            get { return _IsInTry; }
            set { _IsInTry = value; }
        }
        public void Execute()
        { 
        
        }
    }
}
