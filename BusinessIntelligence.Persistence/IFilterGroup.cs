using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Persistence
{
    public interface IFilterGroup : IExpression
    {
        List<IExpression> ExpressionList {get;set;}
    }
}
