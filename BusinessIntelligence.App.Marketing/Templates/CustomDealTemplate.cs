using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.App.Marketing
{
    public class CustomDealTemplate : DealTemplate
    {
        int _DealsPerRow;
        public override int DealsPerRow
        {
            get { return _DealsPerRow; }

        }
        public void SetDealsPerRow(int value)
        {
            _DealsPerRow = value;
        }
        private string _OriginalTemplate;
        public override string OriginalTemplate
        {
            get { return _OriginalTemplate; }
            set { _OriginalTemplate = value; }
        }

        DealTemplate _ClonedDealTemplate;
        public void Clone(DealTemplate dealTemplate)
        {
            _ClonedDealTemplate = dealTemplate;
            _DealsPerRow = dealTemplate.DealsPerRow;
            _OriginalTemplate = dealTemplate.OriginalTemplate;
            dealTemplate.Deal = Deal;
            _Description = dealTemplate.Description;
        }
        private HtmlDeal _Deal;

        public override HtmlDeal Deal
        {
            get { return _Deal; }
            set { 
                
                _Deal = value;
                if (_ClonedDealTemplate != null)
                {
                    _ClonedDealTemplate.Deal = value;
                }
            }
        }
      

        public override string NameReplacement(string name, string template)
        {
            if (_ClonedDealTemplate != null)
            {
                return _ClonedDealTemplate.NameReplacement(name, template);
            }
            else
            {
                return template.Replace("@Name", name);
            }
        }
        public override string OriginalValueReplacement(string originalValue, string template)
        {
            if (_ClonedDealTemplate != null)
            {
                return _ClonedDealTemplate.OriginalValueReplacement(originalValue, template);
            }
            else
            {
                return base.OriginalValueReplacement(originalValue, template);
            }
        }
        public override string ValueReplacement(string value, string template)
        {
            if (_ClonedDealTemplate != null)
            {
                return _ClonedDealTemplate.ValueReplacement(value, template);
            }
            else
            {
                return template.Replace("@Value", value);
            }
        }
        public override string ImageUrlReplacement(string imageUrl, string template)
        {
            if (_ClonedDealTemplate != null)
            {
                return _ClonedDealTemplate.ImageUrlReplacement(imageUrl, template);
            }
            else
            {
                return template.Replace("@ImageUrl", imageUrl);
            }
        }
        public override string DealUrlReplacement(string dealUrl, string template)
        {
            if (_ClonedDealTemplate != null)
            {
                return _ClonedDealTemplate.DealUrlReplacement(dealUrl, template);
            }
            else
            {
                return template.Replace("@DealUrl", dealUrl);
            }
        }
        private string _Description = "Customizada";
        public override string Description
        {
            get
            {
                return _Description;
            }
        }
    }
}
