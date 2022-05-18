using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.ETL
{
    class SetLog: IScriptCommand
    {
        public SetLog(bool showLog)
        {
            _ShowLog = showLog;
        }
        bool _ShowLog;

        public bool ShowLog
        {
            get { return _ShowLog; }
            set { _ShowLog = value; }
        }
        private bool _HasExecutionError = false;
        public  bool HasExecutionError
        {
            get { return _HasExecutionError; }
            set { _HasExecutionError = value; }
        }
        private bool _IsInTry = false;
        public  bool IsInTry
        {
            get { return _IsInTry; }
            set { _IsInTry = value; }
        }
        public void Execute()
        {
            Script.LogEnabled = ShowLog;
        }
    }
}
