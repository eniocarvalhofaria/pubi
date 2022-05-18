using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
namespace BusinessIntelligence.ETL.Financial.AccountingBudget
{
    class Program
    {
        public static void Main(string[] args)
        {
            var dt = new DataTable();
            dt.Columns.Add("ReferenceDate", typeof(string));
            dt.Columns.Add("CostCenter", typeof(string));
            dt.Columns.Add("Cod", typeof(int));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Budget", typeof(double));

            string dir = System.Environment.CurrentDirectory;

            foreach (string file in Directory.GetFiles(dir, "*.txt"))
            {
                using (StreamReader sr = new StreamReader(file, Encoding.Default))
                {
                    int lineIndex = 0;
                    DateTime ReferenceDate = DateTime.MinValue;
                    string[] fields;
                    while (!sr.EndOfStream)
                    {
                        lineIndex++;
                        string line = sr.ReadLine();
                        fields = line.Split('|');


                        if (fields.Length > 6 && lineIndex > 5 && !string.IsNullOrEmpty(fields[6].Trim()) && !fields[1].Trim().Equals("*"))
                        {
                            double budget;
                            budget = Convert.ToDouble(fields[6].Trim().Replace(".", "").Replace("-", ""));

                            if (fields[6].IndexOf('-') > -1)
                            {
                                budget = budget * -1;
                            }

                            if (budget != 0)
                            {
                       
                            string year = fields[1].ToString();
                            string month = fields[2].ToString().Trim();

                            ReferenceDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);

                            var r = dt.NewRow();

                            r["ReferenceDate"] = ReferenceDate.ToString("yyyy-MM-dd");
                            string costCenter = fields[3].Substring(0, 7).Trim();
                            r["CostCenter"] = costCenter;

                            string cod = fields[4].Substring(0, 7).Trim();
                            r["Cod"] = cod;
                            string description = fields[5].Substring(0,20).Trim();
                            r["Description"] = description;


                            r["Budget"] = budget;
                            dt.Rows.Add(r);
                            }
                        }

                    }
                    sr.Close();
                    sr.Dispose();
                }
            }
            var cn = BusinessIntelligence.Data.Connections.GetNewConnection(BusinessIntelligence.Data.Database.APPPROD);
            var ex = new BusinessIntelligence.Data.QueryExecutor(cn);
            ex.Execute("delete from fin.AccountingBudget_stage");
            using (SqlBulkCopy bc = new SqlBulkCopy((SqlConnection)cn))
            {

                bc.DestinationTableName = "fin.AccountingBudget_stage";
                bc.BatchSize = 10000;
                bc.BulkCopyTimeout = 0;
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + string.Format(" Loading {0} rows in {1}.", dt.Rows.Count.ToString(), bc.DestinationTableName));
                bc.WriteToServer(dt);
            }
            ex.Execute("exec fin.RefreshAccountingBudget");
        }
    }
}
