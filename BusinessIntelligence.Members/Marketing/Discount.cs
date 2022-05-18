using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Marketing
{
  public  class Discount:StoredObject
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _BriefName;

        public string BriefName
        {
            get { return _BriefName; }
            set { _BriefName = value; }
        }
        private string _UrlFormattedName;

        public string UrlFormattedName
        {
            get { return _UrlFormattedName; }
            set { _UrlFormattedName = value; }
        }
        private string _TinyImageUrl;

        public string TinyImageUrl
        {
            get { return _TinyImageUrl; }
            set { _TinyImageUrl = value; }
        }
        private string _SmallImageUrl;

        public string SmallImageUrl
        {
            get { return _SmallImageUrl; }
            set { _SmallImageUrl = value; }
        }
        private string _MediumImageUrl;

        public string MediumImageUrl
        {
            get { return _MediumImageUrl; }
            set { _MediumImageUrl = value; }
        }
        private string _BigImageUrl;

        public string BigImageUrl
        {
            get { return _BigImageUrl; }
            set { _BigImageUrl = value; }
        }
        private decimal _OriginalValue;

        public decimal OriginalValue
        {
            get { return _OriginalValue; }
            set { _OriginalValue = value; }
        }
        private decimal _Value;

        public decimal Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

    }
}
