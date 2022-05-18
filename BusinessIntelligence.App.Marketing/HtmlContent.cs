using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace BusinessIntelligence.App.Marketing
{
    public abstract class HtmlContent
    {
        public abstract string GetHtml();
        public string fileContent(string path)
        {
            string retorno;

                using (System.IO.StreamReader sr = new System.IO.StreamReader(path.Replace("file:\\", "")))
                {
                    retorno = sr.ReadToEnd();
                    sr.Close();
                }
            
            return retorno;
        }
        
    }
}
