using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.ETL
{
    public class IfCommand : BusinessIntelligence.ETL.IBlockCommand
    {
        public IfCommand(Script owner, string logicalExpression, string innerScript, BusinessIntelligence.Data.QueryExecutor queryExecutor)
        {
            _LogicalExpression = logicalExpression;
            this.owner = owner;
            _InnerScript = new Script(queryExecutor, innerScript);
        }
        Script _InnerScript;

        public Script InnerScript
        {
            get { return _InnerScript; }
        }
        Script _ElseScript;

        public Script ElseScript
        {
            get { return _ElseScript; }
            set { _ElseScript = value; }
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
            Log.GetInstance().WriteLine("if(" + LogicalExpression + ")");
            if (owner.TestLogicalExpression(LogicalExpression))
            {
                InnerScript.Execute();
                if (InnerScript.Returned)
                {
                    _Returned = true;
                    return;
                }
            }
            else
            {
                if (ElseScript != null)
                {
                    ElseScript.Execute();
                    owner.ReturnCode = ElseScript.ReturnCode;
                    if (ElseScript.Returned)
                    {
                        _Returned = true;
                        return;
                    }
                }
            }
        }
    }
}
