using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBSITE_FILM_002.Controllers
{
    public class PagesController : Controller
    {
        //Page index 
        public ActionResult Index()
        {
            return View();
        }
    }
}