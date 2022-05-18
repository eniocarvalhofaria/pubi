using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop;
using System.Xml;
using System.IO;
using BusinessIntelligence.Configurations;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.Reports
{
    class Program
    {
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
        static void Main(string[] args)
        {
            try
            {
                if (Environment.GetCommandLineArgs().Length > 1)
                {
                    string currentDirectory = GetFullPath(Environment.CurrentDirectory, Environment.GetCommandLineArgs()[1]);
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Directory selected: " + Environment.GetCommandLineArgs()[1]);
                    var exc = new BusinessIntelligence.Report.ExcelReport(currentDirectory);
                    //        exc.IsTest = true;
                    if (exc.Refresh())
                    {
                        exc.Publish();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.GetInstance().Write(ex);
                Environment.Exit(1);
            }
        }
    }
}
