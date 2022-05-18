using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Configuration;
using BusinessIntelligence.Configurations;
using BusinessIntelligence.Util;
using Microsoft.Office.Interop;
namespace BusinessIntelligence.Report
{
    public class ExcelReport : IReport
    {
        public ExcelReport(string applicationDirectory)
        {
            ApplicationDirectory = applicationDirectory;
            _Config = new ReportConfigurationInfo(applicationDirectory + "\\config.xml");
            ReportFile = applicationDirectory + "\\" + Config.ReportFileName;
            _DirectoryFullName = applicationDirectory;
            var con = BusinessIntelligence.Data.Connections.GetNewConnection("PROCESSCONTROL");
            queryExecutor = new Data.QueryExecutor(con);
        }
        private Data.QueryExecutor queryExecutor;
        private string _ReportFile;
        public string ApplicationDirectory { get; set; }
        public string ReportFile
        {
            get { return _ReportFile; }
            set { _ReportFile = value; }
        }
        string _EmailBody;

        public string EmailBody
        {
            get { return _EmailBody; }
            set { _EmailBody = value; }
        }

        private string _DirectoryFullName = null;

        public string DirectoryFullName
        {
            get { return _DirectoryFullName; }
            set { _DirectoryFullName = value; }
        }
        private ReportConfigurationInfo _Config;

        public ReportConfigurationInfo Config
        {
            get { return _Config; }

        }
        Microsoft.Office.Interop.Excel.Application oExcel = null;
        Microsoft.Office.Interop.Excel.Workbook oBook = null;
        Microsoft.Office.Interop.Excel.Workbooks oBooks = null;
        object oMissing = null;
        private void OpenExcelFile(string ExcelFile)
        {
            KillOldExcel();
            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Creating excel session");


            var P0 = new List<int>();
            var P1 = new List<System.Diagnostics.Process>();

            foreach (var p in System.Diagnostics.Process.GetProcessesByName("Excel"))
            {
                P0.Add(p.Id);
            }
            oExcel = new Microsoft.Office.Interop.Excel.Application();
            P1.AddRange(System.Diagnostics.Process.GetProcessesByName("Excel"));
            System.Diagnostics.Process P = null;
            if (P1.Count > 1)
            {
                for (int I = 0; I < P1.Count; I++)
                {

                    if (!P0.Contains(P1[I].Id))
                    {
                        P = P1[I];
                        using (var sw = new StreamWriter(DirectoryFullName + "\\pid.txt", false, Encoding.Default))
                        {
                            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Excel session created PID (" + P.Id.ToString() + ")");

                            sw.Write(P.Id.ToString());
                            sw.Close();
                        }
                        break;
                    }
                }
            }
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

                // Object for missing (or optional) arguments.
                oMissing = System.Reflection.Missing.Value;

                if (!System.IO.File.Exists(ExcelFile))
                {
                    Log.GetInstance().WriteLine("Arquivo " + ExcelFile + " não encontrado.");
                    throw new Exception("Arquivo " + ExcelFile + " não encontrado.");
                }
                var fi = new FileInfo(ExcelFile);
                // Create an instance of Microsoft Excel
                oExcel.DisplayAlerts = false;
                // Make it visible
                oExcel.Visible = true;
                // Define Workbooks
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Declaring Excel Workbooks");
                oBooks = oExcel.Workbooks;
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Excel Workbooks declared");
                //Open the file, using the 'path' variable
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Opening the file...");
                Log.GetInstance().WriteLine("\t" + ExcelFile);
                oBook = oBooks.Open(fi.FullName, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " File opened.");
            }
            catch (Exception ex)
            {
                Log.GetInstance().WriteLine(ex.Message);
                Log.GetInstance().WriteLine();
                Log.GetInstance().WriteLine(ex.StackTrace);
                try
                {
                    oBook.Close(false, oMissing, oMissing);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook);
                    oBook = null;
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
                    oBooks = null;
                    Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Quiting from excel...");
                    oExcel.Quit();
                    GC.Collect();

                }
                catch (Exception er)
                {

                }
                throw new Exception(ex.Message, ex);
            }



        }

        private void RefreshNotConnectedPivotCaches()
        {

            foreach (Microsoft.Office.Interop.Excel.PivotCache pc in oBook.PivotCaches())
            {
                try
                {

                    try
                    {
                        var a = pc.CommandText;


                    }
                    catch (Exception ex)
                    {
                        pc.Refresh();
                    }
                }
                catch (Exception ex1)
                {
                    Log.GetInstance().WriteLine(ex1.Message);
                    try
                    {
                        oBook.Close(false, oMissing, oMissing);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook);
                        oBook = null;
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
                        oBooks = null;
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Quiting from excel...");
                        oExcel.Quit();
                        GC.Collect();

                    }
                    catch (Exception er)
                    {

                    }

                    throw new Exception(ex1.Message, ex1);
                }
            }


        }
        public bool Refresh()
        {
            //   IsTest = true;
            var ce = new Util.ControlledExecution(queryExecutor, "Refresh " + Config.ReportName);
            ce.Start();
            try
            {

                OpenExcelFile(ReportFile);
                if (!IsTest)
                {
                    if (!string.IsNullOrEmpty(Config.ReportMacro))
                    {
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Running the macro...");
                        RunMacro(oExcel, new Object[] { string.Format("{0}!{1}", Config.ReportFileName, Config.ReportMacro) });
                    }
                    else
                    {
                        try
                        {
                            RunMacro(oExcel, new Object[] { string.Format("{0}!{1}", Config.ReportFileName, "BeforeRefresh") });
                        }
                        catch (Exception ex)
                        { }
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Refreshing all query tables...");
                        foreach (Microsoft.Office.Interop.Excel.Worksheet ws in oBook.Worksheets)
                        {
                            foreach (Microsoft.Office.Interop.Excel.ListObject qt in ws.ListObjects)
                            {
                                try
                                {
                                    string oldConnectionReplacing = qt.QueryTable.Connection.ToString();

                                    qt.QueryTable.Connection = ReplacingConnection(oldConnectionReplacing);
                                    Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Refreshing query table in: " + ws.Name);
                                    qt.QueryTable.MaintainConnection = false;
                                    qt.QueryTable.BackgroundQuery = false;
                                    qt.Refresh();

                                    qt.QueryTable.Connection = oldConnectionReplacing;
                                }
                                catch (Exception ex1)
                                {
                                    Log.GetInstance().WriteLine(ex1.Message);
                                    try
                                    {
                                        oBook.Close(false, oMissing, oMissing);
                                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook);
                                        oBook = null;
                                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
                                        oBooks = null;
                                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Quiting from excel...");
                                        oExcel.Quit();
                                        GC.Collect();
                                        ce.EndWithError(ex1.Message);
                                    }
                                    catch (Exception er)
                                    {

                                    }
                                    ce.EndWithError(ex1.Message);
                                    throw new Exception(ex1.Message, ex1);
                                }
                            }
                        }
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Refreshing all pivot caches...");
                        foreach (Microsoft.Office.Interop.Excel.PivotCache pc in oBook.PivotCaches())
                        {
                            try
                            {
                                string oldConnectionReplacing = null;
                                try
                                {
                                    Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + pc.CommandText);
                                    oldConnectionReplacing = pc.Connection.ToString();
                                    pc.Connection = ReplacingConnection(oldConnectionReplacing);

                                }
                                catch (Exception ex)
                                {

                                }
                                pc.Refresh();
                                if (oldConnectionReplacing != null)
                                {
                                    pc.Connection = oldConnectionReplacing;
                                }

                            }
                            catch (Exception ex1)
                            {
                                Log.GetInstance().WriteLine(ex1.Message);
                                try
                                {
                                    oBook.Close(false, oMissing, oMissing);
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook);
                                    oBook = null;
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
                                    oBooks = null;
                                    Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Quiting from excel...");
                                    oExcel.Quit();
                                    GC.Collect();
                                    ce.EndWithError(ex1.Message);
                                }
                                catch (Exception er)
                                {

                                }
                                ce.EndWithError(ex1.Message);
                                throw new Exception(ex1.Message, ex1);
                            }
                        }

                        try
                        {
                            RunMacro(oExcel, new Object[] { string.Format("{0}!{1}", Config.ReportFileName, "AfterRefresh") });
                        }
                        catch (Exception ex)
                        { }
                    }
                }
                if (!string.IsNullOrEmpty(Config.EmailBodySheet))
                {
                    generateEmailBody(Config.EmailBodySheet);
                }
                if (!string.IsNullOrEmpty(Config.SubjectSheetCell))
                {
                    generateSubject(Config.SubjectSheetCell);
                }



                // Quit Excel and clean up.
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Saving the file...");
                oBook.Save();
                CloseExcelFile();
                ce.End();
                return true;
            }
            catch (Exception ex)
            {
                Log.GetInstance().WriteLine(ex.Message);
                Log.GetInstance().WriteLine();
                Log.GetInstance().WriteLine(ex.StackTrace);
                try
                {
                    oBook.Close(false, oMissing, oMissing);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook);
                    oBook = null;
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
                    oBooks = null;
                    Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Quiting from excel...");
                    oExcel.Quit();
                    GC.Collect();
                    ce.EndWithError(ex.Message);
                }
                catch (Exception er)
                {

                }
                ce.EndWithError(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
        private void generateSubject(string subjectCell)
        {
            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Generating email subject...");
            Microsoft.Office.Interop.Excel._Worksheet subjectSheetCell = null;
            foreach (var xlWorksheet in oBook.Sheets)
            {
                if (((Microsoft.Office.Interop.Excel._Worksheet)xlWorksheet).Name.Equals(subjectCell.Split('!')[0]))
                {
                    subjectSheetCell = (Microsoft.Office.Interop.Excel._Worksheet)xlWorksheet;
                    break;
                }
            }
            Microsoft.Office.Interop.Excel.Range xlRange = subjectSheetCell.get_Range(subjectCell.Split('!')[1]);

            subject = (xlRange.Cells[1, 1] as Microsoft.Office.Interop.Excel.Range).Value2.ToString();
        }
        public void generateEmailBody(string emailBodySheetName)
        {

            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Generating email body...");
            Microsoft.Office.Interop.Excel._Worksheet emailBodySheet = null;
            foreach (var xlWorksheet in oBook.Sheets)
            {
                if (((Microsoft.Office.Interop.Excel._Worksheet)xlWorksheet).Name.Equals(emailBodySheetName))
                {
                    emailBodySheet = (Microsoft.Office.Interop.Excel._Worksheet)xlWorksheet;
                    break;
                }
            }

            if (emailBodySheet != null)
            {


                var content = excelRangeToHtml(emailBodySheet.UsedRange);
                SaveHtmlContent(content);
            }
        }

        private string ReplacingConnection(string connection)
        {


            string oldConnectionReplacing = connection;
            oldConnectionReplacing = oldConnectionReplacing.Replace("enio.faria", Parameter.GetParameter("redshiftuserid").Value);
            oldConnectionReplacing = oldConnectionReplacing.Replace("PostgreSQL35W", "Redshift");
            oldConnectionReplacing = oldConnectionReplacing.Replace("POSTGRESQL35W", "Redshift");
            if (oldConnectionReplacing.Contains("172.26.1"))
            {
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Replacing the ip address for hosts names");


                oldConnectionReplacing = oldConnectionReplacing.Replace("172.26.1.142", "sqlaapddp12.peixeurbano.local");
                oldConnectionReplacing = oldConnectionReplacing.Replace("172.26.1.143", "sqlaapddp13.peixeurbano.local");
                oldConnectionReplacing = oldConnectionReplacing.Replace("172.26.1.141", "sqlaapddp11.peixeurbano.local");
            }
            return oldConnectionReplacing;
        }
        private bool _IsTest;

        public bool IsTest
        {
            get { return _IsTest; }
            set { _IsTest = value; }
        }
        private void SaveHtmlContent(string content)
        {
            using (var sw = new StreamWriter(DirectoryFullName + "\\body.html", false, Encoding.Default))
            {
                sw.Write(content);
                sw.Close();
            }


        }
        private void SaveSubjectContent(string content)
        {
            using (var sw = new StreamWriter(DirectoryFullName + "\\subject.txt", false, Encoding.Default))
            {
                sw.Write(content);
                sw.Close();
            }


        }
        string subject = null;
        private string ReadHtmlContent()
        {
            string ret = null;
            if (File.Exists(DirectoryFullName + "\\body.html"))
            {
                using (var sr = new StreamReader(DirectoryFullName + "\\body.html", Encoding.Default))
                {
                    ret = sr.ReadToEnd();
                    sr.Close();
                }
            }
            return ret;
        }
        private void CloseExcelFile()
        {
            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Closing the file...");
            oBook.Close(false, oMissing, oMissing);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook);
            oBook = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
            oBooks = null;
            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Quiting from excel...");
            oExcel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
            oExcel = null;
            //Garbage collection
            GC.Collect();
        }
        private string GetValidFileName(string text)
        {
            string charactersAllowed = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var sb = new StringBuilder();
            foreach (var a in RemoveAccents(text).ToCharArray())
            {
                if (charactersAllowed.Contains(a))
                {
                    sb.Append(a);
                }
            }
            return sb.ToString();
        }
        public static string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(letter) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }
        private bool Publish(ExcelFilteredConfigurationInfo FilterConfig)
        {
            foreach (var filter in FilterConfig.FilterRecipients)
            {
                KillOldExcel();

                string filterFileName = Config.ReportFileName.Replace(".xls", "_" + GetValidFileName(filter.Key) + ".xls");
                if (File.Exists(filterFileName))
                {
                    Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Deleting file: " + filterFileName);

                    File.Delete(filterFileName);
                }
                System.IO.File.Copy(Config.ReportFileName, filterFileName);
                OpenExcelFile(filterFileName);
                foreach (var table in FilterConfig.TablesToFilter)
                {

                    Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Filtering sheet:" + table.SheetName + " column:" + table.ColumnName + " content:" + filter.Key);
                    Microsoft.Office.Interop.Excel._Worksheet tableSheet = null;
                    foreach (var xlWorksheet in oBook.Sheets)
                    {
                        if (((Microsoft.Office.Interop.Excel._Worksheet)xlWorksheet).Name.Equals(table.SheetName))
                        {


                            tableSheet = (Microsoft.Office.Interop.Excel._Worksheet)xlWorksheet;
                            Microsoft.Office.Interop.Excel.Range AllRange = tableSheet.UsedRange;

                            int cIndex = 1;
                            foreach (Microsoft.Office.Interop.Excel.Range c in AllRange.Columns)
                            {
                                string content = (AllRange.Cells[1, cIndex] as Microsoft.Office.Interop.Excel.Range).Value2;
                                if (content.ToLower() == table.ColumnName.ToLower())
                                {
                                    //    AllRange.Sort(AllRange.Columns[cIndex], Microsoft.Office.Interop.Excel.XlSortOrder.xlDescending);

                                    foreach (Microsoft.Office.Interop.Excel.ListObject qt in tableSheet.ListObjects)
                                    {
                                        qt.Range.Sort(qt.Range.Columns[cIndex, Type.Missing], Microsoft.Office.Interop.Excel.XlSortOrder.xlAscending, // the first sort key Column 1 for Range
                   qt.Range.Columns[1, Type.Missing], Type.Missing, Microsoft.Office.Interop.Excel.XlSortOrder.xlAscending,// second sort key Column 6 of the range
                  Type.Missing, Microsoft.Office.Interop.Excel.XlSortOrder.xlAscending,  // third sort key nothing, but it wants one
                  Microsoft.Office.Interop.Excel.XlYesNoGuess.xlGuess, Type.Missing, Type.Missing,
                  Microsoft.Office.Interop.Excel.XlSortOrientation.xlSortColumns,
                  Microsoft.Office.Interop.Excel.XlSortMethod.xlPinYin,
                  Microsoft.Office.Interop.Excel.XlSortDataOption.xlSortNormal,
                  Microsoft.Office.Interop.Excel.XlSortDataOption.xlSortNormal,
                  Microsoft.Office.Interop.Excel.XlSortDataOption.xlSortNormal);

                                        List<String> otherValues = new List<string>();
                                        try
                                        {

                                            var myvalues = (object[,])AllRange.Columns[cIndex].Cells.Value;

                                            for (int r = 2; r <= myvalues.Length; r++)
                                            {
                                                if (myvalues[r, 1] != null)
                                                {
                                                    string a = (myvalues[r, 1]).ToString();

                                                    if (!otherValues.Contains(a) && a != filter.Key)
                                                    {
                                                        otherValues.Add(a);
                                                    }
                                                }
                                            }
                                            if (otherValues.Count > 0)
                                            {
                                                qt.Range.AutoFilter(cIndex, otherValues.ToArray(), Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlFilterValues, Type.Missing, true);



                                                Microsoft.Office.Interop.Excel.Range filtered = AllRange.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeVisible, Type.Missing);

                                                long colstuff = filtered.Areas.Count;
                                             
                                                if (colstuff >= 1)
                                                {
                                                    tableSheet.Rows.EntireRow[1].Hidden = true;
                                                    filtered = tableSheet.UsedRange.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeVisible, Type.Missing);
                                                    foreach (Microsoft.Office.Interop.Excel.Range rng in filtered.Areas)
                                                    {
                                                        rng.EntireRow.Delete(Microsoft.Office.Interop.Excel.XlDirection.xlUp);
                                                    }
                                                    tableSheet.Rows.EntireRow[1].Hidden = false;
                                                    qt.Range.AutoFilter(cIndex, Type.Missing, Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlFilterValues, Type.Missing, true);
                                                }
                                                
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                            Log.GetInstance().WriteLine("Filtro não executado.");
                                        }
                                    }

                                    break;
                                }
                                cIndex++;
                            }
                            break;
                        }
                    }
                }
                RefreshNotConnectedPivotCaches();
                try
                {
                    RunMacro(oExcel, new Object[] { string.Format("{0}!{1}", filterFileName, "AfterFilter") });
                }
                catch (Exception ex)
                {

                }
                foreach (var xlWorksheet in oBook.Sheets)
                {
                    var name = ((Microsoft.Office.Interop.Excel._Worksheet)xlWorksheet).Name;
                    if (FilterConfig.SheetsToDelete.Contains(name))
                    {
                        ((Microsoft.Office.Interop.Excel._Worksheet)xlWorksheet).Delete();
                    }
                }
                if (!string.IsNullOrEmpty(FilterConfig.EmailBodySheet))
                {
                    generateEmailBody(FilterConfig.EmailBodySheet);
                }
                if (!string.IsNullOrEmpty(FilterConfig.SubjectSheetCell))
                {
                    generateSubject(FilterConfig.SubjectSheetCell);
                }
                oBook.Save();
                CloseExcelFile();
                PublishFile(filterFileName, filter.Value.ToArray(), FilterConfig.FileAvailabilityType, FilterConfig.SecurityMethod);
                System.IO.File.Delete(filterFileName);
            }
            return true;
        }
        public bool Publish()
        {
            var ce = new Util.ControlledExecution(queryExecutor, "Publish " + Config.ReportName);
            ce.Start();
            try
            {
                if (Config.Recipients.Count > 0)
                {
                    PublishFile(Config.ReportFileName, Config.Recipients.ToArray(), Config.FileAvailabilityType, Config.SecurityMethod);
                }
                if (Config.ExcelFilteredConfigurationInfoList.Count > 0)
                {

                    foreach (var a in Config.ExcelFilteredConfigurationInfoList)
                    {
                        if (!a.DaysOfWeek.Contains(DateTime.Now.DayOfWeek))
                        {
                            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Today is " + DateTime.Now.DayOfWeek.ToString() + ". The report will not be published");
                        }
                        else
                        {
                            if (a.FilterRecipients.Count > 0)
                            {
                                Publish(a);
                            }
                        }
                    }

                }

                ce.End();
                return true;
            }
            catch (Exception ex)
            {

                ce.EndWithError(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
        public void KillOldExcel()
        {
            if (File.Exists(DirectoryFullName + "\\pid.txt"))
            {
                int previousPID = 0;
                using (var sr = new StreamReader(DirectoryFullName + "\\pid.txt", Encoding.Default))
                {
                    var PIDcontent = sr.ReadToEnd();
                    previousPID = Convert.ToInt32(PIDcontent);
                    sr.Close();
                }
                Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Checking active excel session PID (" + previousPID.ToString() + ")");

                var pRunning = System.Diagnostics.Process.GetProcessesByName("Excel");


                foreach (var p in pRunning)
                {
                    if (p.Id == previousPID)
                    {
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Killing Excel PID (" + previousPID + ")");
                        p.Kill();
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Excel PID Killed (" + previousPID + ")");
                        File.Delete(DirectoryFullName + "\\pid.txt");
                        break;
                    }
                }

            }

        }
        private void PublishFile(string filename, string[] addressList, FileAvailabilityType fileAvailabilityType, SecurityMethod sm)
        {
            EmailPublisher p = new EmailPublisher();
            FileInfo fi = new FileInfo(filename);
            p.ReportDate = DateTime.Now.AddDays(Config.DaysReference);
            string bucket = "pu-reports/" + filename.Replace(" ", "") + "/reportdate_" + p.ReportDate.ToString("yyyyMMdd") + "/" + (filename + p.ReportDate.ToString("yyyyMMdd")).GetHashCode().ToString("X");
            //      string bucket = "pu-reports/" + sm.ToString() + "/" + filename.Replace(" ", "") + "/reportdate_" + p.ReportDate.ToString("yyyyMMdd") + "/" + (filename + p.ReportDate.ToString("yyyyMMdd")).GetHashCode().ToString("X");
            string objectName = fi.Name.Split('.')[0] + "_" + p.ReportDate.ToString("yyyyMMdd") + "." + fi.Name.Split('.')[1];
            if (fileAvailabilityType == FileAvailabilityType.S3)
            {
                var s3 = new S3Publisher(bucket, objectName, fi.FullName);
                if (s3.Publish())
                {
                    p.DownloadUrl = s3.GetUrl();
                }
            }
            p.AdminEmailAddress = Config.AdminEmailAddress;
            p.Content = this.ReadHtmlContent();
            p.ReportName = Config.ReportName;
            p.ReportFileName = fi.FullName;
            if (!string.IsNullOrEmpty(subject))
            {
                p.Subject = subject;
            }
            if (!IsTest)
            {
                p.Recipients = addressList;
            }
            else
            {
                p.Recipients = new string[] { "enio.faria@peixeurbano.com" };
            }
            //      p.Recipients = new string[] { "enio.faria@peixeurbano.com" };
            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Publishing report to " + p.Recipients.Length.ToString() + " recipients...");
            p.EmailAddress = EnvironmentParameters.EmailAddress;
            p.EmailPWD = EnvironmentParameters.EmailPwd;

            int contError = 0;
            while (true)
            {
                try
                {
                    p.Publish();
                    break;
                }
                catch (Exception pex)
                {
                    contError++;
                    if (contError > 10)
                    {
                        throw new Exception(pex.Message, pex);
                    }
                    else
                    {
                        Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Erro na " + contError.ToString() + "a tentativa de envio. ");
                        System.Threading.Thread.Sleep(5000);
                    }
                }
            }
            Log.GetInstance().WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Report published.");
        }
        private static void RunMacro(object oApp, object[] oRunArgs)
        {
            oApp.GetType().InvokeMember("Run", System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.InvokeMethod, null, oApp, oRunArgs);
        }
        private static string getBackGroundColor(Microsoft.Office.Interop.Excel.Range range)
        {
            /*
                     if (range.FormatConditions.Count > 0)
                     {
                         Log.GetInstance().Write(range.FormatConditions[0]);
                     }
                     */
            return getColorFromOffice(Convert.ToInt32(range.Interior.Color));
        }
        private static string getFontColor(Microsoft.Office.Interop.Excel.Range range)
        {
            string bgColor = (Convert.ToInt32(range.Font.Color)).ToString("X");
            return getColorFromOffice(Convert.ToInt32(range.Font.Color));
        }
        private static string getColorFromOffice(int officeColor)
        {
            string color = officeColor.ToString("X");

            //Corrigir  bug microsoft que inverte RGB pra BGR
            color = "00000" + color;
            color = color.Substring(color.Length - 6, 6);
            color = color.Substring(4, 2) + color.Substring(2, 2) + color.Substring(0, 2);
            return color;
        }
        private static string excelRangeToHtml(Microsoft.Office.Interop.Excel.Range xlRange)
        {

            var widths = new List<int>();
            foreach (Microsoft.Office.Interop.Excel.Range c in xlRange.Columns)
            {
                try
                {
                    widths.Add(Convert.ToInt32(((double)c.ColumnWidth * 7.16)));
                }
                catch (Exception wex)
                {
                    widths.Add(107);
                }
            }
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            bool[,] hideMatrix = new bool[rowCount, colCount];


            var htmlTable = new StringBuilder();
            htmlTable.Append("<table border=\"0\" width=\"561\" align=\"center\">");
            for (int i = 1; i <= rowCount; i++)
            {
                htmlTable.Append("<tr>");
                for (int j = 1; j <= colCount; j++)
                {
                    if (!hideMatrix[i - 1, j - 1])
                    {
                        if ((xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).Value2 != null)
                        {
                            htmlTable.Append("<td");
                            if (i == 1)
                            {
                                htmlTable.Append(" width=\"" + widths[j - 1].ToString() + "\" ");
                            }
                            if (((Microsoft.Office.Interop.Excel.XlHAlign)(xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).HorizontalAlignment).Equals(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter))
                            {
                                htmlTable.Append(" align=\"center\" ");
                            }
                            else if (((Microsoft.Office.Interop.Excel.XlHAlign)(xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).HorizontalAlignment).Equals(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft))
                            {
                                htmlTable.Append(" align=\"left\" ");
                            }
                            else if (((Microsoft.Office.Interop.Excel.XlHAlign)(xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).HorizontalAlignment).Equals(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight))
                            {
                                htmlTable.Append(" align=\"right\" ");
                            }
                            if (((Microsoft.Office.Interop.Excel.XlVAlign)(xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).VerticalAlignment).Equals(Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter))
                            {
                                htmlTable.Append(" valign=\"middle\" ");
                            }
                            else if (((Microsoft.Office.Interop.Excel.XlVAlign)(xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).VerticalAlignment).Equals(Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop))
                            {
                                htmlTable.Append(" valign=\"top\" ");
                            }
                            else if (((Microsoft.Office.Interop.Excel.XlVAlign)(xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).VerticalAlignment).Equals(Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignBottom))
                            {
                                htmlTable.Append(" valign=\"bottom\" ");
                            }
                            htmlTable.Append(" style=\"padding-right: 5px; padding-left: 5px;\"");
                            string bgColor = getBackGroundColor((xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range));
                            if (!bgColor.Equals("FFFFFF"))
                            {
                                htmlTable.Append(" bgcolor=\"#" + bgColor + "\" ");
                            }
                            if ((xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).MergeArea.Rows.Count > 1)
                            {
                                htmlTable.Append(" rowspan=\"" + (xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).MergeArea.Rows.Count.ToString() + "\" ");
                                for (int r = 1; r < (xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).MergeArea.Rows.Count; r++)
                                {
                                    hideMatrix[(i - 1) + r, j - 1] = true;
                                }
                            }
                            if ((xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).MergeArea.Columns.Count > 1)
                            {
                                htmlTable.Append(" colspan=\"" + (xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).MergeArea.Columns.Count.ToString() + "\" ");
                                for (int c = 1; c < (xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).MergeArea.Columns.Count; c++)
                                {
                                    hideMatrix[(i - 1), (j - 1) + c] = true;
                                }
                            }

                            htmlTable.Append(">");

                            //        Log.GetInstance().Write((xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).HorizontalAlignment);
                            //      Log.GetInstance().Write((xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).VerticalAlignment);
                            htmlTable.Append(" <a ");
                            htmlTable.Append(" style=\"font-family: ");
                            htmlTable.Append((xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).Font.Name.ToString() + ", Verdana;");

                            string fontColor = getFontColor((xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range));

                            htmlTable.Append(" color: #" + fontColor + "; ");

                            bool isBold = (bool)(xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).Font.Bold;
                            if (isBold)
                            {
                                htmlTable.Append(" font-weight: bold; ");
                            }
                            bool isItalic = (bool)(xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).Font.Italic;
                            if (isItalic)
                            {
                                htmlTable.Append(" font-style:italic; ");
                            }


                            htmlTable.Append(" font-size: " + (Convert.ToInt32((double)(xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).Font.Size) - 0).ToString() + "pt; ");
                            htmlTable.Append("\">");


                            string cellContent = (xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).Value2.ToString();
                            string format = (xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).NumberFormat.ToString();
                            string formattedContent = null;

                            format = format.Replace("_", "").Replace("*", "");
                            if (!string.IsNullOrEmpty(cellContent.Trim()))
                            {
                                double number;
                                if (format != "@" && double.TryParse(cellContent, out number))
                                {
                                    if (format.IndexOf(']') > -1)
                                    {
                                        format = format.Split(']')[1].Trim();
                                        if (format.StartsWith("-"))
                                        {
                                            format = format.Replace("-", "");
                                        }
                                    }
                                    if (format.ToLower().Equals("general") || format.ToLower().Equals("geral"))
                                    {
                                        formattedContent = number.ToString().Replace(",", ";").Replace(".", ",").Replace(";", ".");
                                    }
                                    else
                                    {
                                        formattedContent = number.ToString(format).Replace(",", ";").Replace(".", ",").Replace(";", ".");
                                    }
                                }
                                else
                                {
                                    formattedContent = cellContent;
                                }
                                htmlTable.Append(formattedContent);
                            }
                            htmlTable.Append("</a></td>");
                        }
                        else if (!(bool)(xlRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).MergeCells)
                        {
                            htmlTable.Append("<td></td>");
                        }
                    }
                }
                htmlTable.Append("</tr>");
            }
            htmlTable.Append("</table>");
            return htmlTable.ToString();
        }


        static string GetFullPath(string currentPath, string relativePathTarget)
        {
            string p = currentPath;
            if (relativePathTarget.Contains(":\\"))
            {
                return relativePathTarget;
            }

            foreach (string level in relativePathTarget.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (level == "..")
                {
                    p = Directory.GetParent(p).FullName;
                }
                else
                {
                    p += "\\" + level;
                }
            }
            return p;

        }
    }
}
