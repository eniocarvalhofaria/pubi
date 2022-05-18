using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace BusinessIntelligence.Util
{
    public static class IOFunctions
    {
        public static string GetFullPath(string relativePathTarget)
        {

            return GetFullPath(Environment.CurrentDirectory, relativePathTarget);
        }
        public static string GetFullPath(string currentPath, string relativePathTarget)
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
        public static string GetExecutableDirectory()
        {
           return Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().CodeBase).Replace("file:\\", "");

        }

    }
}
