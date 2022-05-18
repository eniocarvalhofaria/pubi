using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Data;
namespace BusinessIntelligence.ETL.LTV_AB
{
    public class LTVTest
    {
        public LTVTest(string name)
        {
            _Name = name;
        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
        }
        private List<LTVTestGroup> _LTVTestGroupList = new List<LTVTestGroup>();

        public List<LTVTestGroup> LTVTestGroupList
        {
            get { return _LTVTestGroupList; }
            set { _LTVTestGroupList = value; }
        }
        private static QueryExecutor _QueryExecutor;

        public static QueryExecutor QueryExecutor
        {
            get { return _QueryExecutor; }
            set { _QueryExecutor = value; }
        }
        public static void RefreshPurchaseOrder()
        {
            QueryExecutor.Execute(".RefreshPurchaseOrder.txt");
        }
        public void Run()
        {
            QueryExecutor.Execute(".CreateUsersAnalysis.txt");
            QueryExecutor.AddTextParameter("testname", this.Name);
            bool isFirst = true;
            foreach (var gr in this.LTVTestGroupList)
            {
                QueryExecutor.AddTextParameter("usertypename", gr.Name);
                QueryExecutor.AddTextParameter("criterialtext", gr.CriterialText);
                if (isFirst)
                {
                    QueryExecutor.AddTextParameter("exclusioncriterial", "where");
                }
                else {
                    QueryExecutor.AddTextParameter("exclusioncriterial",Util.EmbeddedResource.TextResource(System.Reflection.Assembly.GetExecutingAssembly(), ".ExclusionCriterial.txt"));
                }
                QueryExecutor.Execute(".SelectGroup.txt");
                isFirst = false;
            }
            QueryExecutor.Execute(".InsertLTVComparisonDetail.txt");
            QueryExecutor.Execute(".InsertLTVComparison.txt");
        }  
    }

}
