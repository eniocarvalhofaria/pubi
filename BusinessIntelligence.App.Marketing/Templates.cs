using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.App.Marketing
{
    public  class Templates
    {
        private static List<DealTemplate> _DealTemplateList = new List<DealTemplate>();

        public static List<DealTemplate> DealTemplateList
        {
            get
            {
                if (_DealTemplateList.Count == 0)
                {
                    _DealTemplateList.Add(new MiddleDealTemplate());
                    _DealTemplateList.Add(new CommonLightDealTemplate());
                    _DealTemplateList.Add(new HorizontalDealTemplate());
                    _DealTemplateList.Add(new OneTopDealTemplate());
                    _DealTemplateList.Add(new TwoTopLightDealTemplate());
                    _DealTemplateList.Add(new TwoTopDealTemplate());
                    _DealTemplateList.Add(new TwoTopDealBlackFridayTemplate());
                    _DealTemplateList.Add(new VPDealTemplate());
                }
                return _DealTemplateList;
            }
            set { _DealTemplateList = value; }
        }

        private static List<EmailBodyTemplate> _EmailBodyTemplateList = new List<EmailBodyTemplate>();

        public static List< EmailBodyTemplate> EmailBodyTemplateList
        {
            get
            {
                if (_EmailBodyTemplateList.Count == 0)
                {
                    _EmailBodyTemplateList.Add(new CommonEmailBodyTemplate());
                    _EmailBodyTemplateList.Add(new NewsEmailBodyTemplate());
                    _EmailBodyTemplateList.Add(new EmailBodyTravelTemplate());
                    _EmailBodyTemplateList.Add(new TopTravelEmailBodyTemplate());
                    _EmailBodyTemplateList.Add(new ECommerceEmailBodyTemplate());
                    _EmailBodyTemplateList.Add(new BlackFridayEmailBodyTemplate());
                    _EmailBodyTemplateList.Add(new VPEmailBodyTemplate());
                    var rjba = new RegionEmailBodyTemplate();
                    rjba.SetDescription("Rio de Janeiro - Barra & Região");
                    rjba.RegionImageUrl = "http://s3.amazonaws.com/pu_mkt_BR/2014/email/zonas/barra-e-regiao.jpg";
                    _EmailBodyTemplateList.Add(rjba);
                    var rjce = new RegionEmailBodyTemplate();
                    rjce.SetDescription("Rio de Janeiro - Centro & Tijuca");
                    rjce.RegionImageUrl = "http://s3.amazonaws.com/pu_mkt_BR/2014/email/zonas/centro-tijuca.jpg";
                    _EmailBodyTemplateList.Add(rjce);
                    var rjzn = new RegionEmailBodyTemplate();
                    rjzn.SetDescription( "Rio de Janeiro - Zona Norte");
                    rjzn.RegionImageUrl = "http://s3.amazonaws.com/pu_mkt_BR/2014/email/zonas/zona-norte.jpg";
                    _EmailBodyTemplateList.Add(rjzn);
                    var rjzo = new RegionEmailBodyTemplate();
                    rjzo.SetDescription("Rio de Janeiro - Zona Oeste");
                    rjzo.RegionImageUrl = "http://s3.amazonaws.com/pu_mkt_BR/2014/email/zonas/zona-oeste.jpg";
                    _EmailBodyTemplateList.Add(rjzo);
                    var rjzs = new RegionEmailBodyTemplate();
                    rjzs.SetDescription("Rio de Janeiro - Zona Sul");
                    rjzs.RegionImageUrl = "http://s3.amazonaws.com/pu_mkt_BR/2014/email/zonas/zona-sul.jpg";
                    _EmailBodyTemplateList.Add(rjzs);
                   
                   
                 


                }
                return _EmailBodyTemplateList;
            }
            set { _EmailBodyTemplateList = value; }
        }

    }
}
