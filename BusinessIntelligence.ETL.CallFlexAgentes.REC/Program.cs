using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;
namespace BusinessIntelligence.ETL.CallFlexAgentes.REC
{
    class Program : BusinessIntelligence.ETL.CommonETL
    {
        static void Main(string[] args)
        {
         try
           {   
            BusinessIntelligence.ETL.CommonETL.Initialize(args);


            var dt = new DataTable();


            dt.Columns.Add("RAMAL", typeof(string));
            dt.Columns.Add("AGENTE", typeof(string));
            dt.Columns.Add("AGENTE_NOME", typeof(string));
            dt.Columns.Add("DATA_LOGIN", typeof(string));
            dt.Columns.Add("HORA_LOGIN", typeof(string));
            dt.Columns.Add("LOGOFF", typeof(string));
            dt.Columns.Add("TEMPO_TOTAL_LOGADO", typeof(string));
            dt.Columns.Add("TEMPO_DESLOGADO", typeof(string));
            dt.Columns.Add("PAUSAS_OFICIAIS_TEMPO", typeof(string));
            dt.Columns.Add("PAUSAS_OFICIAIS_EXCEDIDO", typeof(string));
            dt.Columns.Add("PAUSAS_OFICIAIS_EXCEDIDO_%", typeof(string));
            dt.Columns.Add("OUTRAS_PAUSAS", typeof(string));
            dt.Columns.Add("PAUSA_PRODUTIVA", typeof(string));
            dt.Columns.Add("TEMPO_TOTAL_AFTERCALL", typeof(string));
            dt.Columns.Add("TEMPO_TOTAL_DE_PAUSAS", typeof(string));
            dt.Columns.Add("ADERENCIA_POR_TURNO", typeof(string));
            dt.Columns.Add("TEMPO_EM_OPERACAO", typeof(string));
            dt.Columns.Add("FILAS_OBTIDAS", typeof(string));
            dt.Columns.Add("TEMPO_TOTAL_DISP", typeof(string));
            dt.Columns.Add("TME", typeof(string));
            dt.Columns.Add("TEMPO_TOTAL_FALADO", typeof(string));
            dt.Columns.Add("ENTRANTES_QTDE", typeof(string));
            dt.Columns.Add("ENTRANTES_TEMPO", typeof(string));
            dt.Columns.Add("ENTRANTES_TMA", typeof(string));
            dt.Columns.Add("SAINTES_QTDE", typeof(string));
            dt.Columns.Add("SAINTES_TEMPO", typeof(string));
            dt.Columns.Add("SAINTES_TMS", typeof(string));
            dt.Columns.Add("IGN", typeof(string));

            int colCount = 28;

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
                                row[j - 1] = (xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).Value2;
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
            ex.Execute("delete from dbo.REC_CallFlexAgentes_stage");




            using (SqlBulkCopy bc = new SqlBulkCopy((SqlConnection)cn))
            {
                bc.DestinationTableName = "dbo.REC_CallFlexAgentes_stage";
                bc.BatchSize = 10000;
                bc.BulkCopyTimeout = 0;
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + string.Format(" Loading {0} rows in {1}.", dt.Rows.Count.ToString(), bc.DestinationTableName));
                bc.WriteToServer(dt);
            }

            ex.Execute("exec  dbo.Refresh_REC_CallFlexAgentes");

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
