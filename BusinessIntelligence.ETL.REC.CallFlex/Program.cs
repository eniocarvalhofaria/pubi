using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;
namespace BusinessIntelligence.ETL.REC.CallFlex
{
    class Program : BusinessIntelligence.ETL.CommonETL
    {
        static void Main(string[] args)
        {
         try
           {   
            BusinessIntelligence.ETL.CommonETL.Initialize(args);


            var dt = new DataTable();


            dt.Columns.Add("AGENTE", typeof(int));
            dt.Columns.Add("AGENTE_NOME", typeof(string));
            dt.Columns.Add("DATA_DE_LOGIN", typeof(string));
            dt.Columns.Add("LOGOFF", typeof(string));
            dt.Columns.Add("TEMPO_TOTAL_LOGADO", typeof(string));
            dt.Columns.Add("TIPO_DE_PAUSA", typeof(string));
            dt.Columns.Add("TEMPO_PAUSADO", typeof(string));
            dt.Columns.Add("INICIO_DA_PAUSA", typeof(string));
            dt.Columns.Add("FIM_DA_PAUSA", typeof(string));

            int colCount = 9;

            var oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks = oExcel.Workbooks;
            oExcel.DisplayAlerts = false;
            oExcel.Visible = false;
            object oMissing = System.Reflection.Missing.Value;
            foreach (string file in Directory.GetFiles(options.FilesDirectory, "*.xls"))
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Opening the file " + file);
                var oBook = oBooks.Open(file, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Reading content...");
                Microsoft.Office.Interop.Excel._Worksheet dataSheet = null;
                dataSheet = (Microsoft.Office.Interop.Excel._Worksheet)oBook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = dataSheet.UsedRange;
                int rowCount = xlRange.Rows.Count;
                for (int i = 2; i <= rowCount; i++)
                {
                    var row = dt.NewRow();
                    bool rowFilled = false;
                    for (int j = 1; j <= colCount; j++)
                    {
                        if ((xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).Value2 != null)
                        {
                            rowFilled = true;
                            switch (j)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                    {

                                        row[j - 1] = (xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).Value2;
                                        break;
                                    }
                                default:
                                    {
                                        row[j - 1] = null;
                                        break;
                                    }
                            }
                        }
                    }
                    if (rowFilled)
                    {
                        dt.Rows.Add(row);
                    }
                }

                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Closing the file...");
                oBook.Close(false, oMissing, oMissing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook);
                oBook = null;
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
            oBooks = null;
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Quiting from excel...");
            oExcel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
            oExcel = null;

            GC.Collect();


            var cn = BusinessIntelligence.Data.Connections.GetNewConnection(BusinessIntelligence.Data.Database.REPORTS);
            var ex = new BusinessIntelligence.Data.QueryExecutor(cn);
            ex.Execute("delete from dbo.REC_CallFlex_stage");




            using (SqlBulkCopy bc = new SqlBulkCopy((SqlConnection)cn))
            {
                bc.DestinationTableName = "dbo.REC_CallFlex_stage";
                bc.BatchSize = 10000;
                bc.BulkCopyTimeout = 0;
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + string.Format(" Loading {0} rows in {1}.", dt.Rows.Count.ToString(), bc.DestinationTableName));
                bc.WriteToServer(dt);
            }
            ex.Execute("exec  dbo.Refresh_REC_CallFlex");

            if (!Directory.Exists(options.FilesDirectory + "\\Loaded"))
            {
                Directory.CreateDirectory(options.FilesDirectory + "\\Loaded");
            }
            foreach (string file in Directory.GetFiles(options.FilesDirectory))
            {
                FileInfo fi = new FileInfo(file);
                if (File.Exists(fi.DirectoryName + "\\loaded\\" + fi.Name))
                {
                    File.Delete(fi.DirectoryName + "\\loaded\\" + fi.Name);
                }
                File.Move(file, fi.DirectoryName + "\\loaded\\" + fi.Name);
             }
           }
         catch (Exception exc)
         {
             Console.WriteLine(exc.Message);
             Console.WriteLine(exc.StackTrace);
             Environment.Exit(1);
         }
        }
    }
}
