using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using CommandLine;
using CommandLine.Parsing;
using CommandLine.Text;
namespace BusinessIntelligence.ETL.Financial.AccountingBalance
{
    public class Program:BusinessIntelligence.ETL.CommonETL
    {
        public static void Main(string[] args)
        {
            BusinessIntelligence.ETL.CommonETL.Initialize(args);

            var dt = new DataTable();
            dt.Columns.Add("ReferenceDate", typeof(string));
            dt.Columns.Add("Cod", typeof(int));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Accumulated", typeof(double));
            dt.Columns.Add("Real", typeof(double));

  

            foreach (string file in Directory.GetFiles(options.FilesDirectory, "*.txt"))
            {
                using (StreamReader sr = new StreamReader(file, Encoding.Default))
                {
                    int lineIndex = 0;
                    DateTime ReferenceDate = DateTime.MinValue;
                    string[] fields;
                    bool isJan = false;
                    while (!sr.EndOfStream)
                    {
                        lineIndex++;
                        string line = sr.ReadLine();
                        fields = line.Split('|');
                        if (lineIndex == 7)
                        {
                            string period = fields[5].Split('-')[1];
                            string month = period.Substring(0, 2);
                            if (month.Equals("01"))
                            { isJan = true; }

                            string year = period.Substring(3, 4);
                            ReferenceDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);
                        }
                        else
                        {

                            if (fields.Length > 7 && lineIndex > 8)
                            {
                                if (fields[2].Equals("BR01"))
                                {
                                    var r = dt.NewRow();

                                    r["ReferenceDate"] = ReferenceDate.ToString("yyyy-MM-dd");
                                    string cod = fields[4].Substring(0, 11).Trim();
                                    r["Cod"] = cod;
                                    string description = fields[4].Substring(11).Trim();
                                    r["Description"] = description;
                                    double accum = Convert.ToDouble(fields[5].Trim().Replace(".", "").Replace("-", ""));
                                    if (fields[5].IndexOf('-') > -1)
                                    {
                                        accum = accum * -1;
                                    }
                                    r["Accumulated"] = accum;
                                    double real;
                                    int realField;
                                    if (isJan && (Convert.ToInt32(cod.Substring(0, 1)) > 2 || ReferenceDate.Year == 2013))
                                    //    if (isJan)
                                    {
                                        realField = 5;

                                    }
                                    else
                                    {
                                        realField = 7;
                                    }
                                    real = Convert.ToDouble(fields[realField].Trim().Replace(".", "").Replace("-", ""));
                                    if (fields[realField].IndexOf('-') > -1)
                                    {
                                        real = real * -1;
                                    }
                                    r["Real"] = real;
                                    dt.Rows.Add(r);
                                }

                            }
                        }
                    }
                    sr.Close();
                    sr.Dispose();
                }
            }
            var cn = BusinessIntelligence.Data.Connections.GetNewConnection("APPPROD");
            var ex = new BusinessIntelligence.Data.QueryExecutor(cn);
            ex.Execute("delete from fin.AccountingBalance_stage");
            using (SqlBulkCopy bc = new SqlBulkCopy((SqlConnection)cn))
            {

                bc.DestinationTableName = "fin.AccountingBalance_stage";
                bc.BatchSize = 10000;
                bc.BulkCopyTimeout = 0;
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + string.Format(" Loading {0} rows in {1}.", dt.Rows.Count.ToString(), bc.DestinationTableName));
                bc.WriteToServer(dt);
            }
            ex.Execute("exec fin.RefreshAccountingBalance");
        }
    }
}
