using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Financial
{
    public class Invoice : PersistentObject
    {
        private long _DocumentNumber;

        public long DocumentNumber
        {
            get { return _DocumentNumber; }
            set { _DocumentNumber = value; }
        }
        private Account _Account;

        public Account Account
        {
            get { return _Account; }
            set { _Account = value; }
        }
        private long _InvoiceNumber;

        public long InvoiceNumber
        {
            get { return _InvoiceNumber; }
            set { _InvoiceNumber = value; }
        }
        private DateTime _InvoiceDate;
        [DateTime(true, "2010-01-01", "2050-12-31")]
        public DateTime InvoiceDate
        {
            get { return _InvoiceDate; }
            set { _InvoiceDate = value; }
        }
        private string _Supplier;

        public string Supplier
        {
            get { return _Supplier; }
            set { _Supplier = value; }
        }
        private AdjustmentType _AdjustmentType;

        public AdjustmentType AdjustmentType
        {
            get { return _AdjustmentType; }
            set { _AdjustmentType = value; }
        }
    }
}
