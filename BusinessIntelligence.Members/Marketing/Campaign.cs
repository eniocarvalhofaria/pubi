using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Marketing
{
    public class Campaign : PersistentObject
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _PartnerDescription;

        public string PartnerDescription
        {
            get { return _PartnerDescription; }
            set { _PartnerDescription = value; }
        }
        private string _PublicDescription;

        public string PublicDescription
        {
            get { return _PublicDescription; }
            set { _PublicDescription = value; }
        }
        private string _Subject;
        [CharAttribute(false, 4000)]
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        private string _EmailAddressToReceiveTest;
        [CharAttribute(false, 4000)]
        public string EmailAddressToReceiveTest
        {
            get { return _EmailAddressToReceiveTest; }
            set { _EmailAddressToReceiveTest = value; }
        }
        private string _EmailAddressToReceiveConfirmation;
        [CharAttribute(false, 4000)]
        public string EmailAddressToReceiveConfirmation
        {
            get { return _EmailAddressToReceiveConfirmation; }
            set { _EmailAddressToReceiveConfirmation = value; }
        }
        private MailfishSender _MailfishSender;

        public MailfishSender MailfishSender
        {
            get { return _MailfishSender; }
            set { _MailfishSender = value; }
        }

        private string _MailingSelectorSql;
        [CharAttribute(false, 50000)]
        public string MailingSelectorSql
        {
            get { return _MailingSelectorSql; }
            set { _MailingSelectorSql = value; }
        }
        private string _HtmlEmail;
        [CharAttribute(false, 50000)]
        public string HtmlEmail
        {
            get { return _HtmlEmail; }
            set { _HtmlEmail = value; }
        }
        private string _CategoryDescription;
        public string CategoryDescription
        {
            get { return _CategoryDescription; }
            set { _CategoryDescription = value; }
        }
        private string _Promocode;
        public string Promocode
        {
            get { return _Promocode; }
            set { _Promocode = value; }
        }
        int _MailingId;
        public int MailingId
        {
            get { return _MailingId; }
            set { _MailingId = value; }
        }
        int _MailingUsersCount = 0;

        public int MailingUsersCount
        {
            get { return _MailingUsersCount; }
            set { _MailingUsersCount = value; }
        }
        int _ControlUsersCount = 0;

        public int ControlUsersCount
        {
            get { return _ControlUsersCount; }
            set { _ControlUsersCount = value; }
        }
        private DateTime _SentDate;

        public DateTime SentDate
        {
            get { return _SentDate; }
            set { _SentDate = value; }
        }

        private List<int> _UnifiedDiscounts = new List<int>();

        public List<int> UnifiedDiscounts
        {
            get { return _UnifiedDiscounts; }
            set { _UnifiedDiscounts = value; }
        }
        public static Campaign GetByCampaignName(string name)
        {
            Campaign[] u = PersistenceSettings.PersistenceEngine.GetObjects<Campaign>(FilterExpressions.Equal("Name", name));
            if (u.Length > 0)
            {
                return u[0];
            }
            else
            { return null; }

        }
        public static Campaign[] GetBySentDate(DateTime date)
        {
            return  PersistenceSettings.PersistenceEngine.GetObjects<Campaign>(FilterExpressions.Equal("SentDate", date.ToString("yyyy-MM-dd")));
  
        }
        private CampaignTestType _CampaignTestType;

        public CampaignTestType CampaignTestType
        {
            get { return _CampaignTestType; }
            set { _CampaignTestType = value; }
        }
    }
}
