using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.App.Marketing
{
    public class LayoutSchema : HtmlContent
    {
        private List<DealTemplate> _DealTemplateList = new List<DealTemplate>();

        public List<DealTemplate> DealTemplateList
        {
            get { return _DealTemplateList; }
            set { _DealTemplateList = value; }
        }
        public  int DealsCount
        {
            get
            {

                return 0;
            }
        }
        public override string GetHtml()
        {
            return null;
        }
    }
}
