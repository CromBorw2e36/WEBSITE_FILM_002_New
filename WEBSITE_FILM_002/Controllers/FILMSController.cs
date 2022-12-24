using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Controllers
{
    public class FILMSController : Controller
    {
        WEBSITE_FILM _conntext = new WEBSITE_FILM();
        // GET: FILMS
        public ActionResult Index()
        {
            var Films = _conntext.FILMS.ToList();
            return View(Films);
        }
    }
}