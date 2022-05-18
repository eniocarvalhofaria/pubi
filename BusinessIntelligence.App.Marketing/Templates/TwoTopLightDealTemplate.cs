using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
namespace BusinessIntelligence.App.Marketing
{
    public class TwoTopLightDealTemplate : DealTemplate
    {
        public override int DealsPerRow
        {
            get
            {
                return 2;
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
                    arquivo = @"\Resources\Html\Top\TwoTopLightDeal.html";
                    String strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                    _OriginalTemplate = BusinessIntelligence.Util.EmbeddedResource.TextResource(Assembly.GetExecutingAssembly(), arquivo.Substring(1).Replace("\\", "."));
                }
                return _OriginalTemplate;
            }
            set { _OriginalTemplate = value; }

        }

        public override string ImageUrlReplacement(string imageUrl, string template)
        {
            return template.Replace("@ImageUrl", imageUrl.Split('&')[0] + "&w=288&h=185");
        }
        public override string Description
        {
            get
            {
                return "2 ofertas light";
            }
        }
    }
}
