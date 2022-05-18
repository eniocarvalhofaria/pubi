using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace SalesForceCopyLoader
{
    public class DirectoryListener
    {
        public DirectoryListener(string path)
        {
            _Path = path;
        }
        public DirectoryListener(string path, string searchPattern)
        {
            if(String.IsNullOrEmpty(path))
            {
                _Path = Environment.CurrentDirectory;
            }else if(path.Substring(path.Length - 1,  1).Equals(@"\"))
            {
                _Path = path.Substring(0, path.Length - 1);
            }
            else{
            _Path = path;
            }
            _SearchPattern = searchPattern;
        }
        private string _Path;

        public string Path
        {
            get { return _Path; }
        }
        string _SearchPattern;

        public string SearchPattern
        {
            get { return _SearchPattern; }
        }

        private Dictionary<string, FileInfo> filesProvided = new Dictionary<string, FileInfo>();
        public FileInfo GetNextFile()
        {
            string[] files;
            if (string.IsNullOrEmpty(SearchPattern))
            {
                files = Directory.GetFiles(Path);
            }
            else
            { 
               files = Directory.GetFiles(Path, SearchPattern); 
            }
            foreach (string f in files)
            {
                if (!filesProvided.ContainsKey(f))
                {
                    filesProvided.Add(f, new FileInfo(f));
                    return filesProvided[f];
                }
                else 
                {
                    if (!filesProvided[f].CreationTime.Equals(new FileInfo(f).CreationTime))
                    {
                        filesProvided.Remove(f);
                        filesProvided.Add(f, new FileInfo(f));
                        return filesProvided[f];
                    }
                }
            }
            return null;
        }

    }
}
