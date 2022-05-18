using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Integration.Salesforce
{
    public enum ExecutionStep
    {
        GenerateMetadata,
        Extract,
        ConvertToCsv,
        Load,
        Merge,
        Upsert
    }
}
