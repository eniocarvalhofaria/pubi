using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
namespace BusinessIntelligence.App.Marketing
{
    public class OneTopDealTemplate : DealTemplate
    {
        public override int DealsPerRow
        {
            get
            {
                return 1;
            }
        }

        private string _OriginalTemplate;
        public override string OriginalTemplate
        {
            get
            {
                if (string.IsNullOrEmpty(_OriginalTemplate))
                {
                    string arquivo = null;
                    arquivo = @"\Resources\Html\Top\OneTopDeal.html";
                    String strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                    _OriginalTemplate = BusinessIntelligence.Util.EmbeddedResource.TextResource(Assembly.GetExecutingAssembly(), arquivo.Substring(1).Replace("\\", "."));

 
                }
                return _OriginalTemplate;
            }
            set { _OriginalTemplate = value; }

        }
        public override string Description
        {
            get
            {
                return "1 oferta grande";
            }
        }
    }
}
