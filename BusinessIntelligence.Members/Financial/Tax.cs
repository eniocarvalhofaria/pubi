using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Financial
{
    public class Tax : PersistentObject
    {
        private int _Cod;

        public int Cod
        {
            get { return _Cod; }
            set { _Cod = value; }
        }
        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private decimal _Rate;
        [Decimal(TotalDigits = 5, DecimalPlaces = 4)]
        public decimal Rate
        {
            get { return _Rate; }
            set { _Rate = value; }
        }
        private List<Account> _AccountsAffected = new List<Account>();
        public List<Account> AccountsAffected
        {
            get { return _AccountsAffected; }
            set { _AccountsAffected = value; }
        }

        public static Tax GetTaxByDescription(string description)
        {
            foreach (Tax t in PersistenceSettings.PersistenceEngine.GetObjects<Tax>())
            {
                if (t.Description.Equals(description))
                {
                    return t;
                }
            }

            return null;
        }
    }
}
