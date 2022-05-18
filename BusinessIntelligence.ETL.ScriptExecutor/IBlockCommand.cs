using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.ETL
{
    public interface IBlockCommand : IScriptCommand
    {
        Script InnerScript { get; }
        bool Returned { get; }
    }
}
