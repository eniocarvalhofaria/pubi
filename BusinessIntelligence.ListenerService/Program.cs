using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace BusinessIntelligence.Listening
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
//C:\Windows\Microsoft.NET\Framework\v4.0.30319>
            if (true)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
			{ 
				new ListenerService() 
			};
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                var service = new ListenerService();
                service.StartService();
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            }
        }
    }
}
