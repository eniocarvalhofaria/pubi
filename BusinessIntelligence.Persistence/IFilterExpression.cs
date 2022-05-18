using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Persistence
{
    public interface IFilterExpression : IExpression
    {
        string Field { get; set; }
        String Typename { get; }
    }
}
