using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Configurations
{
  public  class ProcessExecutionListenerConfigInfo
    {
      public ProcessExecutionListenerConfigInfo(string configFileName)
      {

          var processToWait = ApplicationConfigurationInfo.TryGetParameter(configFileName, "/root/processToWait");
          if (!string.IsNullOrEmpty(processToWait))
            {
                _ProcessToWaitNames = processToWait.Split(',');
            }
       }
      private string[] _ProcessToWaitNames;

      public string[] ProcessToWaitNames
      {
          get { return _ProcessToWaitNames; }
      }
    }
}
