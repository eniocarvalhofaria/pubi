using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Persistence
{
    public class SortExpression : IExpression
    {
        Dictionary<string, Option> _List = new Dictionary<string, Option>();

        public void Add(string fieldName, Option sortOption)
        {
            _List.Add(fieldName, sortOption);
        }
        public static SortExpression GetInstanceFromText(string text)
        {
          var  ret = new SortExpression();

            ret.Text = text;
            return ret;
        }
        protected string Text;
        public string GetText()
        {
            if (Text == null && _List.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (var item in _List)
                {
                    sb.Append(",\t" + item.Key);
                    if (item.Value == Option.Descending)
                    {
                        sb.Append(" desc\r\n");
                    }
                    else
                    {
                        sb.Append(" asc\r\n");
                    }
                }
                return "order by \r\n" + sb.ToString(1, sb.Length - 1);
            }
            else {
                return Text;
            }
        }
    }



    public enum Option
    {
        Ascending,
        Descending
    }
}
