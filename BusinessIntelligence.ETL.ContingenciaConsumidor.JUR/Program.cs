using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace BusinessIntelligence.ETL.ContingenciaConsumidor.JUR
{
    class Program : BusinessIntelligence.ETL.CommonETL
    {
        static void Main(string[] args)
        {
         try
           {
            BusinessIntelligence.ETL.CommonETL.Initialize(args);


            var dt = new DataTable();


            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Parte_Adversa", typeof(string));
            dt.Columns.Add("Parte_Interessada", typeof(string));
            dt.Columns.Add("Data_Distribuicao", typeof(string));
            dt.Columns.Add("Materia", typeof(string));
            dt.Columns.Add("Numero_Justica", typeof(string));
            dt.Columns.Add("N_jurisdicao", typeof(string));
            dt.Columns.Add("Foro", typeof(string));
            dt.Columns.Add("Comarca", typeof(string));
            dt.Columns.Add("Estado", typeof(string));
            dt.Columns.Add("Objeto", typeof(string));
            dt.Columns.Add("Motivo_Acao", typeof(string));
            dt.Columns.Add("Valor_Causa", typeof(string));
            dt.Columns.Add("Valor_Prognostico", typeof(string));
            dt.Columns.Add("Prognostico", typeof(string));
            dt.Columns.Add("Fase", typeof(string));
            dt.Columns.Add("Escritorio_Responsavel", typeof(string));
            dt.Columns.Add("Atividade", typeof(string));
            dt.Columns.Add("Parceiro", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Data_Cadastro", typeof(string));
            dt.Columns.Add("Data_Notificacao_Escritorio", typeof(string));
            dt.Columns.Add("Tutela_Antecipada", typeof(string));
            dt.Columns.Add("Processo_Virtual", typeof(string));
            dt.Columns.Add("Escritorio", typeof(string));
            dt.Columns.Add("advogadoPeticionante", typeof(string));
            dt.Columns.Add("Contato_REC", typeof(string));
            dt.Columns.Add("osTicket", typeof(string));


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
                            switch (j)
                            {
                                case 4:
                                case 21:
                                case 22:
                
                                    {
                                        row[j - 1] = DateTime.FromOADate((double)((xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).Value2)).ToString("yyyy-MM-dd HH:mm:ss");
                                        break;
                                    }
                                case 1:
                                case 2:
                                case 3:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                case 16:
                                case 17:
                                case 18:
                                case 19:
                                case 20:
                                case 23:
                                case 24:
                                case 25:
                                case 26:
                                case 27:
                                case 28:
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
            ex.Execute("delete from dbo.JUR_ContingenciaConsumidor_stage");




            using (SqlBulkCopy bc = new SqlBulkCopy((SqlConnection)cn))
            {
                bc.DestinationTableName = "dbo.JUR_ContingenciaConsumidor_stage";
                bc.BatchSize = 10000;
                bc.BulkCopyTimeout = 0;
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + string.Format(" Loading {0} rows in {1}.", dt.Rows.Count.ToString(), bc.DestinationTableName));
                bc.WriteToServer(dt);
            }
            ex.Execute("exec  dbo.Refresh_JUR_ContingenciaConsumidor");

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