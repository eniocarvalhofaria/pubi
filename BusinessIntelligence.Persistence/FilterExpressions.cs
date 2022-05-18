using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Persistence
{
    public class FilterExpressions
    {
        protected static string GetValueExpression(object value)
        {
            switch (value.GetType().Name)
            {

                case "Int32":
                case "Int16":
                case "Int64":
                case "Long":
                case "Double":
                case "Decimal":
                    return value.ToString();

                case "Boolean":
                    if ((bool)value)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                case "DateTime":
                    return "'" + ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                default:
                    return "'" + value.ToString() + "'";
            }
        }
        public static IFilterExpression GetFilterFromProperties(Dictionary<string, object> x)
        {
            try
            {
                switch (x["TypeName"].ToString())
                {
                    


                }

            }
            catch (Exception ex)
            {

            }

            return null;

        }
        public static IFilterExpression Equal(string field, object value)
        {
            return new EqualCriterial(field, value);
        }
        public static IFilterExpression NotEqual(string field, object value)
        {
            return new EqualCriterial(field, value);
        }
        public static IFilterExpression Greater(string field, object value)
        {
            return new GreaterCriterial(field, value);
        }
        public static IFilterExpression Less(string field, object value)
        {
            return new LessCriterial(field, value);

        }
        public static IFilterExpression Between(string field, object minValue, object maxValue)
        {
            return new BetweenCriterial(field, minValue, maxValue);
        }
        public static IFilterExpression Contains(string field, string value)
        {
            return new ContainsCriterial(field,value);
        }
        public static IFilterExpression NotContains(string field, string value)
        {
            return new NotContainsCriterial(field, value);
        }
        public static IFilterExpression BeginsWith(string field, string value)
        {
            return new BeginsWithCriterial(field, value);
        }
        public static IFilterExpression NotBeginsWith(string field, string value)
        {
            return new NotBeginsWithCriterial(field, value);
        }
        public static IFilterExpression EndsWith(string field, string value)
        {
            return new EndsWithCriterial(field, value);
        }
        public static IFilterExpression NotEndsWith(string field, string value)
        {
            return new NotEndsWithCriterial(field, value);
        }
        private class EqualCriterial : IFilterExpression
        {
            public EqualCriterial(string field, object value)
            {
                this.Field = field;
                this.Value = value;
            }
            object value;
            public string GetText()
            {
                return string.Format("{0} = {1}", Field, FilterExpressions.GetValueExpression(Value));
            }
            public string Field { get; set; }

            public object Value
            {
                get
                {
                    return value;
                }

                set
                {
                    this.value = value;
                }
            }
            public string Typename
            {
                get
                {
                    return GetType().Name;
                }
            }
        }
        private class NotEqualCriterial : IFilterExpression
        {
            public NotEqualCriterial(string field, object value)
            {
                this.Field = field;
                Value = value;
            }
            public string Field { get; set; }
            public object Value { get; set; }
            public string GetText()
            {
                return string.Format("{0} <> {1}", Field, FilterExpressions.GetValueExpression(Value));
            }
            public string Typename
            {
                get
                {
                    return GetType().Name;
                }
            }
        }
        private class GreaterCriterial : IFilterExpression
        {
            public GreaterCriterial(string field, object value)
            {
                this.field = field;
                this.Value = value;
            }
            string field;

            public string GetText()
            {
                return string.Format("{0} > {1}", field, FilterExpressions.GetValueExpression(Value));
            }
            public string Field { get; set; }
            public object Value { get; set; }

            public string Typename
            {
                get
                {
                    return GetType().Name;
                }
            }
        }
        private class LessCriterial : IFilterExpression
        {
            public LessCriterial(string field, object value)
            {
                this.Field = field;
                this.Value = value;
            }
            public string GetText()
            {
                return string.Format("{0} < {1}", Field, FilterExpressions.GetValueExpression(Value));
            }
            public string Field { get; set; }
            public object Value { get; set; }
            public string Typename
            {
                get
                {
                    return GetType().Name;
                }
            }
        }
        private class BetweenCriterial : IFilterExpression
        {
            public BetweenCriterial(string field, object minValue, object maxValue)
            {
                this.Field = field;
                MinValue = minValue;
                MaxValue = maxValue;
            }
            public string Field { get; set; }

            public object MinValue { get; set; }

            public object MaxValue { get; set; }
            public string GetText()
            {
                return string.Format("{0} between {1} and {2}", Field, FilterExpressions.GetValueExpression(MinValue), FilterExpressions.GetValueExpression(MaxValue));
            }

            public string Typename
            {
                get
                {
                    return GetType().Name;
                }
            }
        }
        private class ContainsCriterial : IFilterExpression
        {
            public ContainsCriterial(string field, string value)
            {
                this.Field = field;
                this.Value = value;
            }
            public string GetText()
            {
                return string.Format("{0} like '%{1}%", Field, FilterExpressions.GetValueExpression(Value));
            }
            public string Field { get; set; }
            public string Value { get; set; }
            public string Typename
            {
                get
                {
                    return GetType().Name;
                }
            }
        }
        private class NotContainsCriterial : IFilterExpression
        {
            public NotContainsCriterial(string field, string value)
            {
                this.Field = field;
                this.Value = value;
            }
            public string GetText()
            {
                return string.Format("{0} not like '%{1}%", Field, FilterExpressions.GetValueExpression(Value));
            }
            public string Field { get; set; }
            public string Value { get; set; }
            public string Typename
            {
                get
                {
                    return GetType().Name;
                }
            }
        }
        private class BeginsWithCriterial : IFilterExpression
        {
            public BeginsWithCriterial(string field, string value)
            {
                this.Field = field;
                this.Value = value;
            }
            public string GetText()
            {
                return string.Format("{0}  like '{1}%", Field, FilterExpressions.GetValueExpression(Value));
            }
            public string Field { get; set; }
            public string Value { get; set; }
            public string Typename
            {
                get
                {
                    return GetType().Name;
                }
            }
        }
        private class NotBeginsWithCriterial : IFilterExpression
        {
            public NotBeginsWithCriterial(string field, string value)
            {
                this.Field = field;
                this.Value = value;
            }
            public string GetText()
            {
                return string.Format("{0}  not like '{1}%", Field, FilterExpressions.GetValueExpression(Value));
            }
            public string Field { get; set; }
            public string Value { get; set; }
            public string Typename
            {
                get
                {
                    return GetType().Name;
                }
            }
        }
        private class EndsWithCriterial : IFilterExpression
        {
            public EndsWithCriterial(string field, string value)
            {
                this.Field = field;
                this.Value = value;
            }
            public string GetText()
            {
                return string.Format("{0}  like '%{1}", Field, FilterExpressions.GetValueExpression(Value));
            }
            public string Field { get; set; }
            public string Value { get; set; }
            public string Typename
            {
                get
                {
                    return GetType().Name;
                }
            }
        }
        private class NotEndsWithCriterial : IFilterExpression
        {
            public NotEndsWithCriterial(string field, string value)
            {
                this.Field = field;
                this.Value = value;
            }
            public string GetText()
            {
                return string.Format("{0}  not like '%{1}", Field, FilterExpressions.GetValueExpression(Value));
            }
            public string Field { get; set; }
            public string Value { get; set; }
            public string Typename
            {
                get
                {
                    return GetType().Name;
                }
            }
        }
        public static IFilterExpression Custom(string expression)
        {
            return new CustomExpression(expression);
        }
        private class CustomExpression : IFilterExpression
        {
            public CustomExpression(string expression)
            {
                _CustomText = expression;
            }
            string _CustomText;

            public string Field { get; set; }

            public string GetText()
            {
                return _CustomText;
            }
            public string Typename
            {
                get
                {
                    return GetType().Name;
                }
            }
        }

    }
}
