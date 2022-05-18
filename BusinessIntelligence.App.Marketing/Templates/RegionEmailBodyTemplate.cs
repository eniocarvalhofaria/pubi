using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
namespace BusinessIntelligence.App.Marketing
{
    public class RegionEmailBodyTemplate : EmailBodyTemplate
    {

        private string _OriginalTemplate;
        public override string OriginalTemplate
        {
            get
            {
                if (string.IsNullOrEmpty(_OriginalTemplate))
                {
                    string arquivo = null;
                    arquivo = @"\Resources\Html\Body\RegionEmailBody.html";
                    String strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

                    _OriginalTemplate = BusinessIntelligence.Util.EmbeddedResource.TextResource(Assembly.GetExecutingAssembly(), arquivo.Substring(1).Replace("\\","."));
                    _OriginalTemplate = _OriginalTemplate.Replace("@RegionImageUrl", RegionImageUrl);
                }
                return _OriginalTemplate;
            }
            set { _OriginalTemplate = value; }

        }
        private string _RegionImageUrl;

        public string RegionImageUrl
        {
            get { return _RegionImageUrl; }
            set { _RegionImageUrl = value; }
        }
        public void SetDescription(string value)
        {
            _Description = value;
        }
        private string _Description;
        public override string Description
        {
            get
            {
                return _Description;
            }
        }
        private DealTemplate _TopDealTemplate = new CommonLightDealTemplate();
        public override DealTemplate TopDealTemplate
        {
            get { return _TopDealTemplate; }
            set
            {
                _TopDealTemplate = value;
                reDraw();
            }
        }
        private DealTemplate _MiddleDealTemplate = new HorizontalDealTemplate();
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
