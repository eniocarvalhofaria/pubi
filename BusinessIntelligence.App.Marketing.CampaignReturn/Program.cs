using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.App.Marketing.CampaignReturn
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Authentication.RunAs.AuthenticateAndRecall(ApplicationInterfaceType.WindowsForm, Application.ExecutablePath);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
