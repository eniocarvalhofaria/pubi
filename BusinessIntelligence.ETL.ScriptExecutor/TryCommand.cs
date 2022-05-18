using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.ETL
{
    public class TryCommand : BusinessIntelligence.ETL.IBlockCommand
    {
        public TryCommand(Script owner, string innerScript)
        {
             this.owner = owner;
            _InnerScript = new Script(owner.QueryExecutor,innerScript);
        }
        Script _InnerScript;

        public Script InnerScript
        {
            get { return _InnerScript; }
        }
        Script _CatchScript;

        public Script CatchScript
        {
            get { return _CatchScript; }
            set { _CatchScript = value; }
        }


        private Script owner;
        System.Data.DataTable dt = null;
        bool _Returned;

        public bool Returned
        {
            get { return _Returned; }

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
        public void Execute()
        {
            InnerScript.IsInTry = true;
            InnerScript.Execute();

            if (InnerScript.Returned)
            {
                _Returned = true;
                return;
            }
            if (InnerScript.HasExecutionError)
            {

                if (CatchScript != null)
                {
                    CatchScript.Execute();
                    if (InnerScript.Returned)
                    {
                        _Returned = true;
                        return;
                    }
                }

            }
        }
    }
}
