using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.ETL
{
    public class Cursor : BusinessIntelligence.ETL.IBlockCommand
    {
        public Cursor(Script owner, string name, string commandText, string innerText)
        {
            _CommandText = commandText;
            this.owner = owner;
            _Name = name;
            _InnerScript = new Script(owner.QueryExecutor,innerText);
        }
        Script _InnerScript;

        public Script InnerScript
        {
            get { return _InnerScript; }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }

        }

        private Script owner;
        private string _CommandText;
        public string CommandText
        {
            get { return _CommandText; }
        }
        System.Data.DataTable dt = null;
        bool _Returned;

        public bool Returned
        {
            get { return _Returned; }

        }
        private bool _HasExecutionError = false;
        public  bool HasExecutionError
        {
            get { return _HasExecutionError; }
            set { _HasExecutionError = value; }
        }
        private  bool _IsInTry = false;
        public  bool IsInTry
        {
            get { return _IsInTry; }
            set { _IsInTry = value; }
        }
        public void Execute()
        {
            dt = owner.QueryExecutor.ReturnData(CommandText, Script.LogEnabled);
            if (owner.QueryExecutor.ReturnCode != 0)
            {
                if (this.IsInTry)
                {
                    this.HasExecutionError = true;
                    owner.QueryExecutor.AddTextParameter("errorcode", owner.QueryExecutor.ReturnCode.ToString());
                    owner.QueryExecutor.AddTextParameter("errormessage", owner.QueryExecutor.DatabaseMessage.ToString());
                }
                else
                {
                    Environment.Exit(owner.QueryExecutor.ReturnCode);
                }
            }
            if (dt.Rows.Count == 0)
            {
                return;
            }
            else
            {
                foreach (System.Data.DataRow currentRow in dt.Rows)
                {
                    owner.SetVariables(currentRow,Name);
                    InnerScript.Execute();
                    if (InnerScript.Returned)
                    {
                        _Returned = true;
                        dt.Dispose();
                        dt = null;
                        return;
                    }
                    if (InnerScript.Breaked)
                    {
                        dt.Dispose();
                        dt = null;
                        return;
                    }
               
                }
            }
        }
    }
}
