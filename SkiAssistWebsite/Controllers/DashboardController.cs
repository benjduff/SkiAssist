using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkiAssistWebsite.ViewModels;

namespace SkiAssistWebsite.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            var dashboardViewModel = new DashboardViewModel();
            return View(dashboardViewModel);
        }

        public ActionResult AdminDashboard()
        {
            var staffListVm = new StaffListViewModel();
            return View(staffListVm);
        }
    }
}