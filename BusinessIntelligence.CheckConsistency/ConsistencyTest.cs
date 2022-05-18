using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessIntelligence.CheckConsistency
{
    public class ConsistencyTest
    {
        public ConsistencyTest()
        {
            ErrorMinValue = double.MinValue;
            ErrorMaxValue = double.MaxValue;
            ErrorTolerance = 0;
            WarningMinValue = double.MinValue;
            WarningMaxValue = double.MaxValue;
            WarningTolerance = 0;

        }
        public string Name { get; set; }
        public string Connection1Name { get; set; }
        public string Sql1 { get; set; }
        public string Connection2Name { get; set; }
        public string Sql2 { get; set; }
        public int ErrorTolerance { get; set; }
        public int WarningTolerance { get; set; }
        public TestType Type { get; set; }
        public double ErrorMinValue { get; set; }
        public double ErrorMaxValue { get; set; }
        public double WarningMinValue { get; set; }
        public double WarningMaxValue { get; set; }
        public string Xml { get; set; }
    }
}
