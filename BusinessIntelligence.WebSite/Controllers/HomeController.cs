using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessIntelligence.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = System.Threading.Thread.CurrentThread.CurrentCulture.Name + " (" + BusinessIntelligence.I18n.Messages.Language + ").";

            return View();
        }

        public ActionResult About()
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
