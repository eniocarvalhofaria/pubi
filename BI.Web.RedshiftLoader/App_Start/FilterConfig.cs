using System.Web;
using System.Web.Mvc;

namespace BI.Web.RedshiftLoader
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
