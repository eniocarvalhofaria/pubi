using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Configurations
{
    public class EmailListenerConfigurationInfo
    {
        public EmailListenerConfigurationInfo(string configFileName)
        {
            this.configFileName = configFileName;
            var searchExtension = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/searchExtension");
            if (!string.IsNullOrEmpty(searchExtension))
            {
                _SearchExtensions = searchExtension.Split(',');
            }
            _SearchPattern = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/searchPattern");
            _AttachmentDirectory = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/attachmentDirectory"); 
        }
        private string configFileName;
        private string[] _SearchExtensions;
        public string[] SearchExtensions
        {
            get { return _SearchExtensions; }
        }
        private string _SearchPattern;
        public string SearchPattern
        {
            get { return _SearchPattern; }
        }
        private string _AttachmentDirectory;

        public string AttachmentDirectory
        {
            get { return _AttachmentDirectory; }
        }
    }


}
