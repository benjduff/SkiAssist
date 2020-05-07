using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkiAssistWebsite.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult NewClass()
        {
            return View();
        }
    }
}