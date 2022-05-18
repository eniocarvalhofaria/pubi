using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading;
namespace BusinessIntelligence
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
              AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
     //     System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies()
            var pe = new BusinessIntelligence.Persistence.SqlServerPersistenceEngine(new System.Data.SqlClient.SqlConnection(string.Format("Server={0};Database={1};Trusted_Connection=true", "172.26.1.143,1443", "REPORTS")), "apptest");

       //     BusinessIntelligence.Environment.Initialize();
        }
    }
}