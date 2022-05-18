using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Data;
namespace BusinessIntelligence.ETL
{
    public class SqlCommand : IDatabaseCommand
    {
        public SqlCommand(Script owner, string commandText, BusinessIntelligence.Data.QueryExecutor queryExecutor)
        {
            _CommandText = commandText;
            this.owner = owner;
            QueryExecutor = queryExecutor;
        }
        public QueryExecutor QueryExecutor
        {
            get; set;
        }
        private Script owner;
        private string _CommandText;
        public string CommandText
        {
            get { return _CommandText; }
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
            QueryExecutor.Execute(CommandText, Script.LogEnabled);
            owner.ReturnCode = QueryExecutor.ReturnCode;
            if (QueryExecutor.ReturnCode != 0)
            {
                if (this.IsInTry)
                {
                    this.HasExecutionError = true;
                    
                    Script.AddParameter("errorcode", owner.QueryExecutor.ReturnCode.ToString());
                    Script.AddParameter("errormessage", "'" + owner.QueryExecutor.DatabaseMessage.ToString().Replace("'", "''") + "'");
                }
                else
                {
                    Environment.Exit(QueryExecutor.ReturnCode);
                }
            }
            else {
                Script.AddParameter("recordsaffected", owner.QueryExecutor.RecordsAffected.ToString());
            }
 
        }
  
    }
}
