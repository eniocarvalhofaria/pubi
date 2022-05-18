using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Xml;
using BusinessIntelligence.MIME;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.Listening
{
    public class ProcessExecutionListener : Listener
    {

        List<int> _MessagesReaded = new List<int>();
        string LastTimeAvailableFile = null;
        DateTime lastDateTimeAvailable = DateTime.MinValue;
        public override bool Listen(string applicationDirectory)
        {

            var conn = BusinessIntelligence.Data.Connections.GetNewConnection("PROCESSCONTROL");

            DateTime.TryParse("2015-07-23", out lastDateTimeAvailable);
            LastTimeAvailableFile = (applicationDirectory + "\\LastDateTimeAvailable.txt").Replace("\\\\", "\\");
            if (File.Exists(LastTimeAvailableFile))
            {
                using (var sr = new StreamReader(LastTimeAvailableFile))
                {
                    DateTime.TryParse(sr.ReadToEnd(), out lastDateTimeAvailable);
                    sr.Close();
                    sr.Dispose();
                }
            }

            var config = new Configurations.ProcessExecutionListenerConfigInfo(applicationDirectory + "\\config.xml");
            var ex = new Data.QueryExecutor(conn);

            var sb = new StringBuilder();
            sb.Append("select count(distinct processName) qtyProcess, LastDateTimeAvailable from dbo.ExecutionControlReport\r\n");
            sb.Append("where ReturnCode = 0\r\n");
            sb.Append("and ( ");

            bool isFirst = true;
            foreach (var pn in config.ProcessToWaitNames)
            {
                if (!isFirst)
                {
                    sb.Append(" or ");
                }

                sb.Append("   ProcessName like '%" + pn + "'");
                isFirst = false;
            }
            sb.Append(")\r\n");
            sb.Append("group by LastDateTimeAvailable\r\n");
            sb.Append("order by LastDateTimeAvailable desc\r\n");
            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Checking Execution of " + config.ProcessToWaitNames.Length.ToString() + " process...");
                      
            var dt = ex.ReturnData(sb.ToString(),false);
            if (dt.Rows.Count > 0)
            {
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + Convert.ToInt32(dt.Rows[0][0]).ToString() + " process executed ok.");

                if (Convert.ToInt32(dt.Rows[0][0]) == config.ProcessToWaitNames.Length)
                {
                    Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Last date available processed: " + lastDateTimeAvailable.ToString("yyyy-MM-dd HH:mm:ss"));

                    Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Last date available in database: " + Convert.ToDateTime(dt.Rows[0][1]).ToString("yyyy-MM-dd HH:mm:ss"));

                    if (Convert.ToDateTime(dt.Rows[0][1]).Subtract(lastDateTimeAvailable).TotalSeconds > 1)
                    {

                        lastDateTimeAvailable = Convert.ToDateTime(dt.Rows[0][1]);
                        Log.GetInstance().WriteLine("\tThere is new data!.");
                        SetListenOk();
                        return true;
                    }
                    else
                    {
                        Log.GetInstance().WriteLine("\tNo new data.");
                    }
                }
            }
            else {
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Not finded a execution for this process.");

            }
            return false;
        }
        public  void SetListenOk()
        {
            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Setting Last Time Available: " + lastDateTimeAvailable.ToString("yyyy-MM-dd HH:mm:ss"));
 
            using (var sw = new StreamWriter(LastTimeAvailableFile))
            {
                sw.Write(lastDateTimeAvailable.ToString("yyyy-MM-dd HH:mm:ss"));
                sw.Close();
                sw.Dispose();
            }
        }
    }
}