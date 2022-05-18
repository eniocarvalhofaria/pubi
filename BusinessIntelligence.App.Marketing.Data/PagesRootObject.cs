using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.App.Marketing
{
    public class Page
    {
        public string page { get; set; }
        public string title { get; set; }
        public int legacy_id { get; set; }
        public bool popular { get; set; }
        public bool navigable { get; set; }
        public bool show_banner { get; set; }
    }

    public class PagesRootObject
    {
        public int code { get; set; }
        public List<Page> response { get; set; }
    }
}
