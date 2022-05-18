using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Controls
{
    public  class FormSelector : System.Windows.Forms.Form
    {
        private Selector _Selector;
        public Selector Selector
        {
            get { return _Selector; }
            set { _Selector = value; }
        }
    }

}
