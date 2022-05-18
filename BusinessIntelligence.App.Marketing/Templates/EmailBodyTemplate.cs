using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
namespace BusinessIntelligence.App.Marketing
{
    public abstract class EmailBodyTemplate : HtmlContent, ITrackable, ITemplate
    {
        public abstract DealTemplate TopDealTemplate { get; set; }
        public abstract DealTemplate MiddleDealTemplate { get; set; }
        public abstract string Description { get; }
        public string utm_source { get; set; }
        public string utm_medium { get; set; }
        public string utm_campaign { get; set; }
        public string utm_term { get; set; }
        public string Subject { get; set; }
        public abstract string OriginalTemplate { get; set; }
        public List<HtmlDeal> Deals = new List<HtmlDeal>();
        protected void reDraw()
        {
            if (Deals.Count > 0)
            {
                int i = 0;
                foreach (HtmlDeal deal in Deals)
                {
                    if (TopDealTemplate != null && i < TopDealTemplate.DealsPerRow)
                    {
                        deal.DealTemplate = TopDealTemplate;
                    }
                    else
                    {
                        deal.DealTemplate = MiddleDealTemplate;
                    }
                    i++;
                }
            }
        }


 
        private string GetRowHtml(DealTemplate dealTemplate)
        {
            string arquivo = null;
            switch (dealTemplate.DealsPerRow)
            {
                case 1:
                    {
                        arquivo = @"\Resources\Html\Row\RowOneDeal.html";
                        break;
                    }
                case 2:
                    {
                        arquivo = @"\Resources\Html\Row\RowTwoDeals.html";
                        break;
                    }
                case 3:
                    {
                        arquivo = @"\Resources\Html\Row\RowThreeDeals.html";
                        break;
                    }
            }

               
            return BusinessIntelligence.Util.EmbeddedResource.TextResource(Assembly.GetExecutingAssembly(), arquivo.Substring(1).Replace("\\", "."));
      

        }
        public override string GetHtml()
        {
            String strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string RowCommonDeal = this.GetRowHtml(MiddleDealTemplate);

            string RowTopDeal = null;
            if (TopDealTemplate != null)
            {
                 RowTopDeal = this.GetRowHtml(TopDealTemplate);
            }
            string CurrentRow = null;

            int currenteDealIndex = 0;
            int currentPosition = 0;
           
           
     //       string Body = OriginalTemplate.Replace("utm_medium=" ,"utm_medium=" + utm_medium).Replace("utm_campaign=" , "utm_campaign=" + utm_campaign).Replace("utm_source=" , "utm_source=" + utm_source).Replace("utm_term=" , "utm_term=" + utm_term).Replace("my_campaign=email&my_ad=", "my_campaign=email&my_ad=" + utm_campaign);
            string Body = OriginalTemplate.Replace("my_campaign=email&my_ad=", "my_campaign=email&my_ad=" + utm_campaign);

            string TopContent = null;
            string content = null;

            foreach (HtmlDeal deal in Deals)
            {
                deal.utm_campaign = utm_campaign;
                deal.utm_source = utm_source;
                deal.utm_medium = utm_medium;
                currentPosition++;
                if (TopDealTemplate != null && currenteDealIndex == 0)
                {
                    TopContent = RowTopDeal;
                }
                else if ((TopDealTemplate == null || currenteDealIndex >= TopDealTemplate.DealsPerRow) && (currenteDealIndex - (TopDealTemplate == null ? 0 : TopDealTemplate.DealsPerRow)) % MiddleDealTemplate.DealsPerRow == 0)
                {
                    if (!string.IsNullOrEmpty(CurrentRow))
                    {
                        content = content + CurrentRow;
                    }
                    CurrentRow = RowCommonDeal;
                }

                if (TopDealTemplate != null && currenteDealIndex < TopDealTemplate.DealsPerRow)
                {
               
                    deal.DealTemplate = TopDealTemplate;
                    TopContent = TopContent.Replace("@Deal" + (currenteDealIndex + 1).ToString(), deal.GetHtml());
                }
                else
                {
                    deal.DealTemplate = MiddleDealTemplate;
                    CurrentRow = CurrentRow.Replace("@Deal" + (((currenteDealIndex - (TopDealTemplate == null ? 0 : TopDealTemplate.DealsPerRow)) % MiddleDealTemplate.DealsPerRow) + 1).ToString(), deal.GetHtml());
                }

                currenteDealIndex++;
            }
            if (!string.IsNullOrEmpty(CurrentRow))
            {
                content = content + CurrentRow.Replace("@Deal3","").Replace("@Deal2","");
            }
            Body = Body.Replace("@TopContent", TopContent);
            Body = Body.Replace("@MiddleContent", content);

            return Body;


        }
        public void SaveHtmlContent(string fileName)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, false);
            sw.WriteLine(this.GetHtml());
            sw.Close();
            sw.Dispose();
        }




    }

}