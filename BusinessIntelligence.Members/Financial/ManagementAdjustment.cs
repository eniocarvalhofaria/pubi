using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Financial
{
    public class ManagementAdjustment : PersistentObject
    {
        private DateTime _AdjustmentDate;
        [DateTime(true, "2010-01-01", "2050-12-31")]
        public DateTime AdjustmentDate
        {
            get { return _AdjustmentDate; }
            set { _AdjustmentDate = value; }
        }
        private decimal _AdjustmentValue;
        [Decimal(TotalDigits = 18, DecimalPlaces = 2)]
        public decimal AdjustmentValue
        {
            get { return _AdjustmentValue; }
            set { _AdjustmentValue = value; }
        }

        private Invoice _Invoice;
        public Invoice Invoice
        {
            get { return _Invoice; }
            set { _Invoice = value; }
        }
    }
}
