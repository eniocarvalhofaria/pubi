using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Persistence
{
  public  class CriterialFilter
    {
      public static IFilterGroup Or(List<IExpression> criterials)
      {
          IFilterGroup c = new OrCriterialGroup();
          c.ExpressionList = criterials;
          return c;
      }
      public static IFilterGroup Or(IExpression[] criterials)
      {
          IFilterGroup c = new OrCriterialGroup();
          c.ExpressionList = new List<IExpression>(criterials);
          return c;
      }
      public static IFilterGroup And(List<IExpression> criterials)
      {
          IFilterGroup c = new AndCriterialGroup();
          c.ExpressionList = criterials;
          return c;
      }
      public static IFilterGroup And(IExpression[] criterials)
      {
          IFilterGroup c = new AndCriterialGroup();
          c.ExpressionList = new List<IExpression>(criterials);
          return c;
      }
      private class OrCriterialGroup : IFilterGroup
      {

          private List<IExpression> _ExpressionList = new List<IExpression>();
          public List<IExpression> ExpressionList
          {
              get
              {
                  return _ExpressionList;
              }
              set
              {
                  _ExpressionList = value;
              }
          }

          public string GetText()
          {
              bool isFirst = true;
              var sb = new StringBuilder();
              sb.Append("(\r\n");
              foreach (IExpression c in ExpressionList)
              {
                  sb.Append("\t");
                  if (!isFirst)
                  {
                      sb.Append("or ");
                  }

                  sb.Append(c.GetText().Replace("\n", "\n\t") + "\r\n");
     
                  isFirst = false;

              }
              sb.Append(")");
              return sb.ToString();
          }
      }

      private class AndCriterialGroup : IFilterGroup
      {
          private List<IExpression> _ExpressionList = new List<IExpression>();
          public List<IExpression> ExpressionList
          {
              get
              {
                  return _ExpressionList;
              }
              set
              {
                  _ExpressionList = value;
              }
          }

          public string GetText()
          {
              bool isFirst = true;
              var sb = new StringBuilder();
              sb.Append("(\r\n");
              foreach (IExpression c in ExpressionList)
              {
                  sb.Append("\t");
                  if (!isFirst)
                  {
                      sb.Append("and ");
                  }

                  sb.Append(c.GetText().Replace("\n", "\n\t") + "\r\n");
            
                  isFirst = false;

              }
              sb.Append(")");
              return sb.ToString();
          }
      }
    }
}
