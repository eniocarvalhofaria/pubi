using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Data;
namespace BusinessIntelligence.ETL
{
    public struct CommandStructure
    {
        public string Keyword;
        public string Name;
        public string SqlCommand;
        public string InnerScript;
        public string Expression;
        public QueryExecutor QueryExecutor;
    }
}
