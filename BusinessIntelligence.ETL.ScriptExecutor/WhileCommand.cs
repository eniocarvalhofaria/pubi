using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.ETL
{
    public class WhileCommand : BusinessIntelligence.ETL.IBlockCommand
    {
        public WhileCommand(Script owner, string logicalExpression,string innerScript, BusinessIntelligence.Data.QueryExecutor queryExecutor)
        {
            _LogicalExpression = logicalExpression;
            this.owner = owner;
            _InnerScript = new Script(queryExecutor,innerScript);
          
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
        Script _InnerScript;

        public Script InnerScript
        {
            get { return _InnerScript; }
        }
        private Script owner;
        private string _LogicalExpression;
        public string LogicalExpression
        {
            get { return _LogicalExpression; }
        }
        System.Data.DataTable dt = null;
        bool _Returned;

        public bool Returned
        {
            get { return _Returned; }

        }
         public void Execute()
        {
            Log.GetInstance().WriteLine("while(" + LogicalExpression + ")");
            while (owner.TestLogicalExpression(LogicalExpression))
            {
                InnerScript.Execute();
                if (InnerScript.Returned)
                {
                    _Returned = true;
                    return;
                }
                if (InnerScript.Breaked)
                {
                    return;
                }
            }
          
        }
    }
}
