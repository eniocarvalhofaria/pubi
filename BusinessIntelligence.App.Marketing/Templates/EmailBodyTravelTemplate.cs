using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
namespace BusinessIntelligence.App.Marketing
{
    public  class EmailBodyTravelTemplate : EmailBodyTemplate
    {
        private string _OriginalTemplate;
        public override string OriginalTemplate
        {
            get
            {
                if (string.IsNullOrEmpty(_OriginalTemplate))
                {
                    string arquivo = null;
                    arquivo = @"\Resources\Html\Body\EmailBodyTravel.html";
                    String strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                    _OriginalTemplate = BusinessIntelligence.Util.EmbeddedResource.TextResource(Assembly.GetExecutingAssembly(), arquivo.Substring(1).Replace("\\", "."));

 
                }
                return _OriginalTemplate;
            }
            set { _OriginalTemplate = value; }

        }
        public override string Description
        {
            get
            {
                return "Categorias - Viagens e Turismo";
            }
        }
        private DealTemplate _TopDealTemplate = new TwoTopLightDealTemplate();
        public override DealTemplate TopDealTemplate
        {
            get { return _TopDealTemplate; }
            set
            {
                _TopDealTemplate = value;
                reDraw();
            }
        }
        private DealTemplate _MiddleDealTemplate = new TwoTopLightDealTemplate();
        public override DealTemplate MiddleDealTemplate
        {
            get { return _MiddleDealTemplate; }
            set
            {
                _MiddleDealTemplate = value;
                reDraw();
            }
        }
    }

}