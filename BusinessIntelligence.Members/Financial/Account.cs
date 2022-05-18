using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;

namespace BusinessIntelligence.Members.Financial
{
    public class Account : PersistentObject
    {
        private int _Cod;
        [IsUnique]
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
        private string _AccountingCod;
        public string AccountingCod
        {
            get { return _AccountingCod; }
            set { _AccountingCod = value; }
        }
        ManagementGroup _ManagementGroupAggregator;
        public ManagementGroup ManagementGroupAggregator
        {
            get { return _ManagementGroupAggregator; }
            set { _ManagementGroupAggregator = value; }
        }
        AccountingGroup _AccountingGroupAggregator;
        public AccountingGroup AccountingGroupAggregator
        {
            get { return _AccountingGroupAggregator; }
            set { _AccountingGroupAggregator = value; }
        }
        public static Account GetAccountByCod(int cod)
        {
            foreach (Account t in PersistenceSettings.PersistenceEngine.GetObjects<Account>())
            {
                if (t.Cod.Equals(cod))
                {
                    return t;
                }
            }

            return null;
        }
    }
}
