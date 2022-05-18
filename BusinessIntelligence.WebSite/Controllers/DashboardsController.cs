using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessIntelligence.Controllers
{
    public class DashboardsController : BaseController
    {
        //
        // GET: /Dashboards/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DailySales()
        {
            ViewBag.Message = "DailySales";
            return View();
        }
    }
}
