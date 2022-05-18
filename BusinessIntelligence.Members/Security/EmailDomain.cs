using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members
{
  public  class EmailDomain: PersistentObject
    {

        private string _DomainName;

        public string DomainName
        {
            get { return _DomainName; }
            set { _DomainName = value; }
        }

        static EmailDomain[] _Domains;

        public void RefreshDomains()
        {
            _Domains = Persistence.PersistenceSettings.PersistenceEngine.GetObjects<EmailDomain>();
        }
        public static EmailDomain[] Domains
        {
            get {
                if (_Domains == null)
                {
                    _Domains = Persistence.PersistenceSettings.PersistenceEngine.GetObjects<EmailDomain>();
                }
                
                return EmailDomain._Domains; 
            }

        }
        public bool CheckDomain(string EmailAddress)
        {
            string domain;
            if (EmailAddress.IndexOf("@") > -1)
            {
                domain = EmailAddress.Split('@')[1];
                foreach (EmailDomain ed in Domains)
                {
                    if (ed.DomainName.Equals(domain))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
