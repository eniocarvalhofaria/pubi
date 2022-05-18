using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Controls
{
    public class NumberCriterial
    {

        private decimal _MaxValue = decimal.MaxValue;
        public decimal MaxValue
        {
            get { return _MaxValue; }
            set { _MaxValue = value; }
        }
        private decimal _MinValue = decimal.MinValue;
        public decimal MinValue
        {
            get { return _MinValue; }
            set { _MinValue = value; }
        }

        decimal _StartValue;
        public decimal StartValue
        {
            get { return _StartValue; }
            set { _StartValue = value; }
        }
        decimal _EndValue;
        public decimal EndValue
        {
            get { return _EndValue; }
            set { _EndValue = value; }
        }
        decimal _Value;

        public decimal Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        NumberCriterialType _ValueTypeSelected;

        public NumberCriterialType ValueTypeSelected
        {
            get { return _ValueTypeSelected; }
            set { _ValueTypeSelected = value; }
        }
    }
}
