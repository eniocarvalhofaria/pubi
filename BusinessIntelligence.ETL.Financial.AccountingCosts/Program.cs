using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
namespace BusinessIntelligence.ETL.Financial.AccountingCosts
{
    class Program : BusinessIntelligence.ETL.CommonETL
    {
        public static void Main(string[] args)
        {
            BusinessIntelligence.ETL.CommonETL.Initialize(args);
            var dt = new DataTable();
            dt.Columns.Add("ReferenceDate", typeof(string));
            dt.Columns.Add("CostCenter", typeof(string));

            dt.Columns.Add("AccountCod", typeof(int));
            dt.Columns.Add("AccountDescription", typeof(string));
            dt.Columns.Add("CounterpartCod", typeof(int));
            dt.Columns.Add("CounterpartDescription", typeof(string));
            dt.Columns.Add("DocumentReference", typeof(string));
            dt.Columns.Add("Real", typeof(double));

            string dir = options.FilesDirectory;

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

                        if (fields.Length > 9 && lineIndex > 5 && !string.IsNullOrEmpty(fields[5].Trim()) && !fields[1].Trim().Equals("*") && fields[1].Trim() == "BR01")
                        {
                            double real;
                            real = Convert.ToDouble(fields[10].Trim().Replace(".", "").Replace("-", ""));

                            if (fields[10].IndexOf('-') > -1)
                            {
                                real = real * -1;
                            }

                            if (real != 0)
                            {

                                string year = fields[2].Substring(6, 4);
                                string month = fields[2].Substring(3, 2);
                                string day = fields[2].Substring(0, 2);
                                ReferenceDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));

                                var r = dt.NewRow();

                                r["ReferenceDate"] = ReferenceDate.ToString("yyyy-MM-dd");
                                string costCenter = fields[4].Substring(0, 7).Trim();
                                r["CostCenter"] = costCenter;


                                r["DocumentReference"] = fields[3].Trim();

                                r["AccountCod"] = fields[5].Trim();

                                r["AccountDescription"] = fields[6].Trim();

                                r["CounterpartCod"] = fields[7].Trim();

                                r["CounterpartDescription"] = fields[8].Trim();



                                r["Real"] = real;
                                dt.Rows.Add(r);
                            }
                        }

                    }
                    sr.Close();
                    sr.Dispose();
                }
            }
            var cn = BusinessIntelligence.Data.Connections.GetNewConnection("APPPROD");
            var ex = new BusinessIntelligence.Data.QueryExecutor(cn);
            ex.Execute("delete from fin.AccountingCosts_stage");
            using (SqlBulkCopy bc = new SqlBulkCopy((SqlConnection)cn))
            {

                bc.DestinationTableName = "fin.AccountingCosts_stage";
                bc.BatchSize = 10000;
                bc.BulkCopyTimeout = 0;
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + string.Format(" Loading {0} rows in {1}.", dt.Rows.Count.ToString(), bc.DestinationTableName));
                bc.WriteToServer(dt);
            }
            ex.Execute("exec fin.RefreshAccountingCosts");
        }
    }
}
