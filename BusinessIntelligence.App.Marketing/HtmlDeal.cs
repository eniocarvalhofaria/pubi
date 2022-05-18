using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
namespace BusinessIntelligence.App.Marketing
{
    public class HtmlDeal : HtmlContent, ITrackable
    {
        public HtmlDeal() { }
        public HtmlDeal(Deal deal)
        {
            var th = System.Threading.Thread.CurrentThread;
            th.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            this.Name = deal.title;
            this.ShortName = deal.short_title;
            this.ImageUrl = deal.images[0].image;
            this.DealUrl = deal.canonical_url;
            this.utm_source = "mailfish";
            this.utm_medium = "email";
            this.Category = deal.deal_category;
            this.PartnerName = deal.partner.name;
            double minOriginalValue = 9999999999;
            double maxOriginalValue = -1;
            double minValue = 9999999999;
            double maxValue = -1;

            if (deal.deal_type != "Viagens" && deal.deal_type != "Produtos")
            {
                if (deal.locations == null)
                {
                    this.Neighborhood = deal.partner.neighborhood;
                }
                else
                {
                    string nh = null;
                    bool isFirst = true;
                    foreach (var n in deal.locations)
                    {
                        if (isFirst)
                        {
                            nh += n.neighborhood;
                        }
                        else
                        {
                            nh += ", " + n.neighborhood;
                     
                        }
                        isFirst = false;
                    }
                    this.Neighborhood = nh;
                }
            }

            foreach (var a in deal.buying_options)
            {
                if (a.full_price < minOriginalValue)
                {
                    minOriginalValue = a.full_price;
                }
                if (a.full_price > maxOriginalValue)
                {
                    maxOriginalValue = a.full_price;
                }
                if (a.sale_price < minValue)
                {
                    minValue = a.sale_price;
                }
                if (a.sale_price > maxValue)
                {
                    maxValue = a.sale_price;
                }
            }
            if (minValue == maxValue)
            {
                this.Value = "R$ " + formatValue(minValue);
            }
            else
            {
                this.Value = "R$ " + formatValue(minValue) + " a R$ " +  formatValue(maxValue);
            }
            if (minOriginalValue == maxOriginalValue)
            {
                this.OriginalValue = "R$ " + formatValue(minOriginalValue);
            }
            else
            {
                this.OriginalValue = "R$ " + formatValue(minOriginalValue) + " a R$ " + formatValue(maxOriginalValue);
            }


        }
        private string formatValue(double value)
        { 
            string initialFormat =  value.ToString("N2");
            string inteiro = initialFormat.Substring(0, initialFormat.Length - 3);
            string dec = initialFormat.Substring(initialFormat.Length - 2);
            if (dec == "00")
            {
                return inteiro.Replace(",", ".");
            }else
            {
            return inteiro.Replace(",", ".") + "," + dec;
            }
       
        }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public string DealUrl { get; set; }
        public string utm_source { get; set; }
        public string utm_medium { get; set; }
        public string utm_campaign { get; set; }
        public string utm_term { get; set; }
        public string OriginalValue { get; set; }
        public string Value { get; set; }
        public string Neighborhood { get; set; }
        public string PartnerName { get; set; }
        public bool IsFractionated
        {
            get
            {
                if (Value.ToLower().IndexOf("a") > -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public string InitialValue
        {
            get
            {
                if (IsFractionated)
                {
                    return Value.Split('a')[0].Trim().Substring(3);
                }
                else
                {
                    return Value;
                }
            }
        }
        public string FinalValue
        {
            get
            {
                if (IsFractionated)
                {
                    return this.Value.ToLower().Split('a')[1].Trim();
                }
                else
                {
                    return this.Value;
                }
            }
        }
        private DealTemplate _DealTemplate = new MiddleDealTemplate();
        public DealTemplate DealTemplate
        {
            get
            {
                return _DealTemplate;
            }
            set
            {

                _DealTemplate = value;
                _DealTemplate.Deal = this;
            }
        }
        public override string GetHtml()
        {
            return DealTemplate.GetHtml(this);
        }

    }
}
