using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessIntelligence.Persistence
{
   public class PersistenceEngineSettings
    {
        public string Name { get; set; }
        public string ConnectionName { get; set; }
        public string SchemaName { get; set; }
        public string Url { get; set; }
        public string ClassName { get; set; }

    }
}
