using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace BusinessIntelligence.Configurations
{
    public class ReportConfigurationInfo
    {
        public ReportConfigurationInfo(string configFileName)
        {
            _ReportName = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/reportName");
            _ReportFileName = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/reportFile");
            _EmailBodySheet = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/emailBodySheet");
            _ReportMacro = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/reportMacro");
            _subjectSheetCell = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/subjectSheetCell");
            string daysReference = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/daysReference");
            string[] filtConfigTexts = ApplicationConfigurationInfo.TryGetParameters(configFileName, "/root/filterConfig");
            _AdminEmailAddress = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/adminEmailAddress");


            try
            {
                var a = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/fileAvailabilityType");
                _FileAvailabilityType = (FileAvailabilityType)Enum.Parse(FileAvailabilityType.GetType(), a);
            }
            catch (Exception ex)
            {

            }
            try
            {
                var b = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/securytyMethod");
                _SecurityMethod = (SecurityMethod)Enum.Parse(SecurityMethod.GetType(), b);
            }
            catch (Exception ex)
            {

            }
      
            foreach (var item in filtConfigTexts)
            {
                ExcelFilteredConfigurationInfoList.Add(new ExcelFilteredConfigurationInfo(item));
            }
            if (!string.IsNullOrEmpty(daysReference))
            {
                _DaysReference = Convert.ToInt32(daysReference);
            }
            else
            {
                _DaysReference = -1;
            }

            try
            {
                var conn = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/recipientsSQLConnection");
                if (!string.IsNullOrEmpty(conn))
                {
                    var sqlRecipients = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/recipientsSQL");
                    if (!string.IsNullOrEmpty(sqlRecipients))
                    {
                        var cn = BusinessIntelligence.Data.Connections.GetNewConnection(conn);
                        var qex = new BusinessIntelligence.Data.QueryExecutor(cn);
                        var dt = qex.ReturnData(sqlRecipients);
                        foreach (DataRow row in dt.Rows)
                        {
                            Recipients.Add(row["emailaddress"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

          
            if (this.Recipients.Count == 0 && System.IO.File.Exists("recipients.txt"))
                  {
                using (var sr = new System.IO.StreamReader("recipients.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        var l = sr.ReadLine();
                        if (!string.IsNullOrEmpty(l) && l.IndexOf("@") > 1)
                        {
                            Recipients.Add(l);
                        }
                    }
                    sr.Close();
                    sr.Dispose();
                }
            }
        }
        private List<ExcelFilteredConfigurationInfo> _ExcelFilteredConfigurationInfoList = new List<ExcelFilteredConfigurationInfo>();

        private string _AdminEmailAddress;

        public string AdminEmailAddress
        {
            get { return _AdminEmailAddress; }
            set { _AdminEmailAddress = value; }
        }

        private int _DaysReference;


        public int DaysReference
        {
            get { return _DaysReference; }
            set { _DaysReference = value; }
        }

        private string _ReportMacro;
        public string ReportMacro
        {
            get { return _ReportMacro; }
            set { _ReportMacro = value; }
        }

        private string _ReportName;

        public string ReportName
        {
            get { return _ReportName; }
            set { _ReportName = value; }
        }
        private string _ReportFileName;

        public string ReportFileName
        {
            get { return _ReportFileName; }
            set { _ReportFileName = value; }
        }
        private string _EmailBodySheet;

        public string EmailBodySheet
        {
            get { return _EmailBodySheet; }
            set { _EmailBodySheet = value; }
        }
        private string _subjectSheetCell;

        public string SubjectSheetCell
        {
            get { return _subjectSheetCell; }
            set { _subjectSheetCell = value; }
        }
       
        public List<ExcelFilteredConfigurationInfo> ExcelFilteredConfigurationInfoList
        {
            get
            {
                return _ExcelFilteredConfigurationInfoList;
            }

            set
            {
                _ExcelFilteredConfigurationInfoList = value;
            }
        }
        private List<string> _Recipients = new List<string>();
        public List<string> Recipients
        {
            get
            {
                return _Recipients;
            }

            set
            {
                _Recipients = value;
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



    }


}
