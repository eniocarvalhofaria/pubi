using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.ETL.LTV_AB
{
    public class LTVTestGroup
    {
        public LTVTestGroup(string name)
        {
            _Name = name;
        }
        public LTVTestGroup(string name, string criterialText)
        {
            _Name = name;
            CriterialText = criterialText;
        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }

        }
        private string _CriterialText;

        public string CriterialText
        {
            get
            {
                return _CriterialText;
            }
            set {

                if (value.Substring(0, 1) == ".")
                {
                    this._CriterialText = Util.EmbeddedResource.TextResource(System.Reflection.Assembly.GetExecutingAssembly(), value);
                }
                else
                {
                    _CriterialText = value;
                }
            }
        }
    }
}
