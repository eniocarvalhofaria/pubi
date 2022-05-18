using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Configurations
{
    public class ListenerConfigInfo
    {
        public ListenerConfigInfo(string configFileName)
        {

            string callReportRefreshString = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/callReportRefresh");
            if (!string.IsNullOrEmpty(callReportRefreshString))
            {
                _CallReportRefresh = callReportRefreshString.ToUpper() == "S";
            }


            _ExecutableDirectory = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/executableDirectory");
            _ExecutableFile = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/executableFile");
            _ExecutableArguments = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/executableArguments");
            _EtlName = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/etlName");
            _FilesDirectory = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/filesDirectory");

            if (string.IsNullOrEmpty(_FilesDirectory))
            {
                _FilesDirectory = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/attachmentDirectory");
            }

            _AdminEmailAddress = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/adminEmailAddress");
            try
            {
                var w = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/daysOfWeek");
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
        }
        private string _AdminEmailAddress;

        public string AdminEmailAddress
        {
            get { return _AdminEmailAddress; }
            set { _AdminEmailAddress = value; }
        }
        private string _FilesDirectory;
        public string FilesDirectory
        {
            get { return _FilesDirectory; }
            set { _FilesDirectory = value; }
        }
        private string _EtlName;
        public string EtlName
        {
            get { return _EtlName; }
            set { _EtlName = value; }
        }
        private string _ExecutableArguments;
        public string ExecutableArguments
        {
            get { return _ExecutableArguments; }
            set { _ExecutableArguments = value; }
        }

        private string _ExecutableDirectory;

        public string ExecutableDirectory
        {
            get { return _ExecutableDirectory; }
            set { _ExecutableDirectory = value; }
        }
        private string _ExecutableFile;

        public string ExecutableFile
        {
            get { return _ExecutableFile; }
            set { _ExecutableFile = value; }
        }
        private bool _CallReportRefresh;
        public bool CallReportRefresh
        {
            get { return _CallReportRefresh; }
            set { _CallReportRefresh = value; }
        }
        private List<DayOfWeek> _DaysOfWeek = new List<DayOfWeek>();
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

    }
}
