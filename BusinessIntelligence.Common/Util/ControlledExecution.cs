using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Data;
namespace BusinessIntelligence.Util
{
    public class ControlledExecution
    {
        public ControlledExecution(QueryExecutor queryExecutor, string processName)
        {
            _ProcessName = processName;
            QueryExecutor = queryExecutor;
        }
        public ControlledExecution(QueryExecutor queryExecutor, string processName,int idExecution)
        {
            _ProcessName = processName;
            IdExecution = idExecution;
            QueryExecutor = queryExecutor;
        }
        public void UpdateLog(string text)
        {
            string query = "update reports.dbo.ExecutionControl set ExecutionLog = '" + text.Replace("'","''") + "' \r\n where id  = "+ IdExecution.ToString() ;
            this.QueryExecutor.Execute(query, false, false,10);
        }
        public static ControlledExecution GetInstance(string processName)
        {
            if (instances.ContainsKey(processName))
            {
                return instances[processName];
            }
            else
            {
                QueryExecutor qex = new Data.QueryExecutor(Connections.GetNewConnection("PROCESSCONTROL"));
                qex.CommandTimeout = 30;
                string query = "select * from  reports.dbo.ExecutionControl where id in (select max(id) from reports.dbo.ExecutionControl where EndDate is null and (ExecutionLog like '%" + processName + "%' or Processname = '" + processName + "'))";


              

                var dt = qex.ReturnData(query,false,false);
                if (qex.ReturnCode == 0)
                {
                    if (!dt.HasErrors && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        var ce = new ControlledExecution(qex, dt.Rows[0][1].ToString(), Convert.ToInt32(dt.Rows[0][0].ToString()));
                        instances.Add(processName, ce);
                        return ce;
                    }
                    else
                    {
                        return null;
                    }
                } else { return null; }
            }

        }
        private static Dictionary<string, ControlledExecution> instances = new Dictionary<string, ControlledExecution>();
        QueryExecutor QueryExecutor;
        private string _ProcessName;
        public string ProcessName
        {
            get { return _ProcessName; }
        }
        private int IdExecution = 0;
        public void Start()
        {
            //    QueryExecutor.Execute("insert into dbo.ExecutionControl(ProcessName,StartDate,LastDateTimeAvailable) select '" + ProcessName + "',	getdate(),	Info from dbo.LastDateTimeAvailable",false);

            IdExecution = Convert.ToInt32(QueryExecutor.ReturnData("exec dbo.ControlledExecutionStart '" + ProcessName + "'", false).Rows[0][0]);
        }
        public static bool CheckExecutedOk(string processName)
        {
            QueryExecutor qex = new Data.QueryExecutor(Connections.GetNewConnection("REPORTS"));
            string query = "select count(distinct processName) qtyProcess, LastDateTimeAvailable from dbo.ExecutionControlReport\r\n where ReturnCode = 0\r\n and processname like '" + processName + "'";
            var dt = qex.ReturnData(query);
            if (!dt.HasErrors && Convert.ToInt32(dt.Rows[0][0]) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void End()
        {
            QueryExecutor.Execute("exec dbo.ControlledExecutionEnd " + IdExecution.ToString(), false);
        }
        public void EndWithError(string message, int returnCode = 1)
        {
            QueryExecutor.Execute("exec dbo.ControlledExecutionEndWithError " + IdExecution.ToString() + ",'" + message + "'," + returnCode.ToString(), false);

        }
    }
}
