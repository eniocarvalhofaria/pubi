using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Util;

namespace BusinessIntelligence.ETL
{
    public class PrintCommand : IScriptCommand
    {
        public PrintCommand(Script owner, string text)
        {
            this.owner = owner;
            _Text = text;
        }
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
        private Script owner;
        private string _Text = null;
        public void Execute()
        {
            Log.GetInstance().WriteLine("print(" + _Text + ")");
            Console.WriteLine(owner.GetExpression(_Text));
        }
    }

}
