using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Controllers
{
    public class PagesController : Controller
    {
        WEBSITE_FILM _context = new WEBSITE_FILM();

        //Page index 
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Detail_Film(int id)
        {
            var _film = _context.FILMS.Where(x=>x.FILMID== id).FirstOrDefault();

            //Lấy danh sách bình luận của phim

            var _comment = (from cmt in _context.COMMENTS
                            join user in _context.USERS on cmt.USERID equals user.USERID
                            where cmt.COMMENT_STATUS == 0 && cmt.FILMID == id
                            select new _ListComments
                            {
                                comment_content = cmt.COMMENT_CONTENT.ToString(),
                                date = cmt.DATECREATE,
                                userFirstName = user.FIRSTNAME,
                                userLastName = user.LASTNAME,
                                avatar = user.IMAGENAME,
                            }).ToList();

            ViewBag.Comment = _comment;


            // Lấy danh sách phim mới

            var _NewFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0 && x.STATUS != null).OrderByDescending(x => (DateTime)x.PRODUCTIONYEAR).Take(8);

            ViewBag.NewFilm = _NewFilm;

            // Xếp hạng danh sách phim

            var _RankFilm = _context.FILMS.Where(x=>x.FILM_STATUS == 0).OrderByDescending(x=>x.VIEWS).ThenByDescending(x=>x.FILMID).Take(5);

            ViewBag.RankFilm = _RankFilm;

            return View(_film) ;
        }

    }
}