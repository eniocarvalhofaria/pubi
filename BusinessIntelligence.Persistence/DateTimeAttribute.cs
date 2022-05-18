using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Persistence
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateTimeAttribute : Attribute
    {

        public DateTimeAttribute(bool hasTimeDetail,string maxDateTime, string minDateTime)
        {
            this._HasTimeDetail = hasTimeDetail;
            this._MaxDateTime = DateTime.Parse(maxDateTime);
            this._MinDateTime = DateTime.Parse(minDateTime);
        }
        public DateTimeAttribute(bool hasTimeDetail)
        {
            this._HasTimeDetail = hasTimeDetail;
        }
        private bool _HasTimeDetail = false;
        public bool HasTimeDetail
        {
            get { return _HasTimeDetail; }
        }


        private DateTime _MaxDateTime = DateTime.Parse("2100-12-31 23:59:59");
        public DateTime MaxDateTime
        {
            get { return _MaxDateTime; }
            set { _MaxDateTime = value; }
        }

        private DateTime _MinDateTime = DateTime.Parse("1900-01-01 00:00:00");
        public DateTime MinDateTime
        {
            get { return _MinDateTime; }
            set { _MinDateTime = value; }
        }
      
    }
}
