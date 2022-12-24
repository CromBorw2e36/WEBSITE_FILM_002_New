using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBSITE_FILM_002.Areas.Admin.Controllers
{
    public class MainDashboardController : Controller
    {
        // GET: Admin/MainDashboard
        public ActionResult Index()
        {
            ViewBag.PagePositon = "MAINDASHBOARD";
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}