using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using BusinessIntelligence.Report;
using BusinessIntelligence.Util;
using System.Data;
namespace BusinessIntelligence.Configurations
{
    public class ExcelFilteredConfigurationInfo
    {
        public ExcelFilteredConfigurationInfo(string xmlText)
        {
            XElement xml = XElement.Parse("<root>" + xmlText + "</root>");

            Name = ApplicationConfigurationInfo.TryGetParameter(xml, "/root/name");
            EmailBodySheet = ApplicationConfigurationInfo.TryGetParameter(xml, "/root/emailBodySheet");
            SubjectSheetCell = ApplicationConfigurationInfo.TryGetParameter(xml, "/root/subjectSheetCell");
            try
            {
                var w = ApplicationConfigurationInfo.TryGetParameter(xml, "/root/daysOfWeek");
                foreach (var item in w.Split(','))
                {
                    DaysOfWeek.Add((DayOfWeek)(Convert.ToInt32(item) - 1));
                }
            }
            catch (Exception ex)
            {

            }
            if (DaysOfWeek.Count == 0)
            {
                foreach (var a in Enum.GetValues(typeof(DayOfWeek)))
                {
                    DaysOfWeek.Add((DayOfWeek)a);
                }
            }
            var sheetsToDel = ApplicationConfigurationInfo.TryGetNodes(xml, "/root/sheetsToDelete");
            if (sheetsToDel != null && sheetsToDel.Count > 0)
            {
                foreach (XmlNode a in sheetsToDel[0].ChildNodes)
                {
                    SheetsToDelete.Add( a.Attributes["sheetName"].Value);
                }

            }

            try
            {
                var a = ApplicationConfigurationInfo.TryGetParameter(xml, "/root/fileAvailabilityType");
                _FileAvailabilityType = (FileAvailabilityType)Enum.Parse(FileAvailabilityType.GetType(), a);
            }
            catch (Exception ex)
            {

            }
            try
            {
                var b = ApplicationConfigurationInfo.TryGetParameter(xml, "/root/securytyMethod");
                _SecurityMethod = (SecurityMethod)Enum.Parse(SecurityMethod.GetType(), b);
            }
            catch (Exception ex)
            {

            }
            var tablesToFilter = ApplicationConfigurationInfo.TryGetNodes(xml, "/root/tablesToFilter");
            if (tablesToFilter != null && tablesToFilter.Count > 0)
            {
                foreach (XmlNode a in tablesToFilter[0].ChildNodes)
                {
                    TablesToFilter.Add(new TableToFilter(a.Attributes["sheetName"].Value, a.Attributes["columnName"].Value));
                }

            }
            try
            {
                var conn = ApplicationConfigurationInfo.TryGetParameter(xml, "/root/recipientsSQLConnection");
                if (!string.IsNullOrEmpty(conn))
                {
                    var sqlRecipients = ApplicationConfigurationInfo.TryGetParameter(xml, "/root/recipientsSQL");
                    if (!string.IsNullOrEmpty(sqlRecipients))
                    {
                        var cn = BusinessIntelligence.Data.Connections.GetNewConnection(conn);
                        var qex = new BusinessIntelligence.Data.QueryExecutor(cn);
                        var dt = qex.ReturnData(sqlRecipients);
                        foreach (DataRow row in dt.Rows)
                        {
                            var filter = row["filtercontent"].ToString();
                            var address = row["emailaddress"].ToString();

                            if (FilterRecipients.ContainsKey(filter))
                            {
                                FilterRecipients[filter].Add(address);
                            }
                            else
                            {
                                FilterRecipients.Add(filter, new List<string>());
                                FilterRecipients[filter].Add(address);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            if (System.IO.File.Exists("recipients_" + Name + ".txt"))
            {
                using (var sr = new System.IO.StreamReader("recipients_" + Name + ".txt",Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        var l = sr.ReadLine();
                        if (!string.IsNullOrEmpty(l) && l.IndexOf("@") > 1)
                        {
                            var filter = l.Split(';')[0];
                            var address = l.Split(';')[1];
                            if (FilterRecipients.ContainsKey(filter))
                            {
                                FilterRecipients[filter].Add(address);
                            }
                            else
                            {
                                FilterRecipients.Add(filter,new List<string>());
                                FilterRecipients[filter].Add(address);
                            }
                        }
                    }
                    sr.Close();
                    sr.Dispose();
                }
            }

        }
        private List<DayOfWeek> _DaysOfWeek = new List<DayOfWeek>();
        private Dictionary<string,List<string>> _FilterRecipients = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> FilterRecipients
        {
            get
            {
                return _FilterRecipients;
            }

            set
            {
                _FilterRecipients = value;
            }
        }
        public string Name { get; set; }
        public string EmailBodySheet { get; set; }
        public string SubjectSheetCell { get; set; }
        public List<string> SheetsToDelete
        {
            get
            {
                return sheetsToDelete;
            }

            set
            {
                sheetsToDelete = value;
            }
        }

        public List<TableToFilter> TablesToFilter
        {
            get
            {
                return _TablesToFilter;
            }

            set
            {
                _TablesToFilter = value;
            }
        }
        private FileAvailabilityType _FileAvailabilityType;

        public FileAvailabilityType FileAvailabilityType
        {
            get
            {
                return _FileAvailabilityType;
            }

            set
            {
                _FileAvailabilityType = value;
            }
        }
        SecurityMethod _SecurityMethod;
        public SecurityMethod SecurityMethod
        {
            get
            {
                return _SecurityMethod;
            }

            set
            {
                _SecurityMethod = value;
            }
        }

        public List<DayOfWeek> DaysOfWeek
        {
            get
            {
                return _DaysOfWeek;
            }

            set
            {
                _DaysOfWeek = value;
            }
        }

        private List<String> sheetsToDelete = new List<string>();
        private List<BusinessIntelligence.Report.TableToFilter> _TablesToFilter = new List<BusinessIntelligence.Report.TableToFilter>();

     

    }
}
