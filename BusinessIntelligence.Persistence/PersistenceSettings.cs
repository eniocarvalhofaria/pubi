using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Persistence
{
    static public class PersistenceSettings
    {
        private static PersistenceEngine _PersistenceEngine;

        public static PersistenceEngine PersistenceEngine
        {
            get { return _PersistenceEngine; }
            set { _PersistenceEngine = value; }
        }
    }
}
