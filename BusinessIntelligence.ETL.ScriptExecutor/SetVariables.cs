using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Data;

namespace BusinessIntelligence.ETL
{
    public class SetVariables : IDatabaseCommand
    {
        public SetVariables(Script owner, string commandText, BusinessIntelligence.Data.QueryExecutor queryExecutor)
        {
            _CommandText = commandText;
            this.owner = owner;
            QueryExecutor = queryExecutor;
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
        private string _CommandText;
        public string CommandText
        {
            get { return _CommandText; }
        }

        public QueryExecutor QueryExecutor
        {
            get;set;
        }

        public void Execute()
        {

            var dt = QueryExecutor.ReturnData(CommandText, Script.LogEnabled);
            owner.ReturnCode = QueryExecutor.ReturnCode;
            if (QueryExecutor.ReturnCode != 0)
            {
                throw new Exception(QueryExecutor.DatabaseMessage);
            }
            else if (dt.Rows.Count == 0)
            {
                owner.SetVariables(dt.NewRow(), null);
            }
            else if (dt.Rows.Count > 1)
            {
                throw new Exception("O comando retornou mais que 1 linha");
            }
            else
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    owner.SetVariables(dt.Rows[0],null);
                }
            }
        }

    }
}
