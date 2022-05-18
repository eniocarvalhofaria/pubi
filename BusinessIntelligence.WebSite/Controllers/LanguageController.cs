using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
namespace BusinessIntelligence.Controllers
{
    public class LanguageController : BaseController
    {
        //
        // GET: /Language/

        public string SelectPortuguese()
        {
            return SelectLanguage("pt-br");
        }
        public string SelectEnglish()
        {
            return SelectLanguage("es-US");
        }

        public string SelectLanguage(string language)
        {
            var cookie = new HttpCookie("_language");
            cookie.Value = language;
            cookie.Expires = DateTime.Now.AddDays(365);
            Response.Cookies.Add(cookie);
            string cultureName = null;
            cultureName = BusinessIntelligence.Helpers.CultureHelper.GetImplementedCulture(cultureName); // This is safe


            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            return null; // View();
        }

        
    }
}
