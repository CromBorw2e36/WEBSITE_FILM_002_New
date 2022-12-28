using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Controllers
{
    public class _FILMSController : Controller
    {
        WEBSITE_FILM _context = new WEBSITE_FILM();


        //GET all films 
        [HttpPost]
        public JsonResult GET_FILMS()
        {
            var GET_Films = _context.FILMS.Select(x => new
            {
                filmid = x.FILMID,
                filmname = x.FILMNAME,
                status = x.STATUS,
                filmdirector = x.FILMDIRECTOR,
                productionyear = x.PRODUCTIONYEAR,
                time = x.TIME,
                quality = x.QUALITY,
                resolution = x.RESOLUTION,
                language = x.LANGUAGE,
                category = x.CATEGORY,
                views = x.VIEWS,
                manufacturing_conpany = x.MANUFACTURING_COMPANY,
                movie_review = x.MOVIEREVIEW,
                content_film = x.CONTENT_FILM,
                film_image = x.IMAGEID,
            });
            return Json(JsonConvert.SerializeObject(GET_Films), JsonRequestBehavior.DenyGet);
        }


        //GET Single FILMS
        [HttpGet]
        public JsonResult GET_FILM(int id)
        {
            var Film = (from x in _context.FILMS
                        where x.FILMID == (decimal)id
                        select new
                        {
                            film_id = id,
                            film_name = x.FILMNAME,
                            status = x.STATUS,
                            film_director = x.FILMDIRECTOR,
                            production_year = x.PRODUCTIONYEAR,
                            time = x.TIME,
                            quality = x.QUALITY,
                            resolution = x.RESOLUTION,
                            language = x.LANGUAGE,
                            category = x.CATEGORY,
                            views = x.VIEWS,
                            manufacturing_conpany = x.MANUFACTURING_COMPANY,
                            movie_review = x.MOVIEREVIEW,
                            content_film = x.CONTENT_FILM,
                            film_image = x.IMAGEID,
                        }).SingleOrDefault();
            var ConverJson = JsonConvert.SerializeObject(Film);
            return Json(ConverJson, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GET_Video_Film (int id)
        {
            var Video_Link = _context.FILMS.Where(x=>x.FILMID.Equals(id)).Select(x=> new { address = x.VIDEOID}).SingleOrDefault();
            return Json(JsonConvert.SerializeObject(Video_Link), JsonRequestBehavior.DenyGet);
        }
        public ActionResult GetFilm()
        {
            var _Flim = _context.FILMS.ToList();
            ViewBag.message = _Flim.Count();
            return View(_Flim);
        }
        public ActionResult DetailFilm(decimal id)
        {
            var res = _context.FILMS.Where(x => x.FILMID == id).FirstOrDefault();

            return View(res);
        }

    }
}