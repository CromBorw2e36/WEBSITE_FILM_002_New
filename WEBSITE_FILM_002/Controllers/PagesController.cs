﻿using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Controllers
{
    public class PagesController : Controller
    {
        WEBSITE_FILM _context = new WEBSITE_FILM();

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

        //trang chủ 

        public ActionResult Index()
        {
            // danh sách phim mới
            var _NewFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0).OrderByDescending(x => x.FILMID).Take(12).ToList();
            ViewBag.NewFilm = _NewFilm;

            // danh sách phim theo chủ đề
            var _ShowingFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0 && x.STATUS == "Đang chiếu").OrderByDescending(x => x.FILMID).Take(12).ToList();
            ViewBag.ShowingFilm = _ShowingFilm;

            // danh sách phim theo ngôn ngữ
            var _LanguageTQFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0 && x.LANGUAGE == "Trung Quốc").OrderByDescending(x => x.FILMID).Take(12).ToList();
            var _LanguageTVFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0 && x.LANGUAGE == "Phụ đề Việt").OrderByDescending(x => x.FILMID).Take(12).ToList();
            var _LanguageHQFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0 && x.LANGUAGE == "Hàn Quốc").OrderByDescending(x => x.FILMID).Take(12).ToList();
            
            ViewBag.TQFilm = _LanguageTQFilm;
            ViewBag.TVFilm = _LanguageTVFilm;
            ViewBag.HQFilm = _LanguageHQFilm;
            
            // Xếp hạng phim

            var _RankFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0).OrderByDescending(x => x.VIEWS).ThenByDescending(x => x.FILMID).Take(3);
            ViewBag.RankFilm = _RankFilm;


            return View();
        }

        // Trang xem phim
        public ActionResult PlayVideo(int id)
        {
            // Lấy Film ra chiếu
            var _Film = _context.FILMS.Where(x=>x.FILMID == id && x.FILM_STATUS == 0).SingleOrDefault();

            // Lấy danh sách phim mới

            var _NewFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0 && x.STATUS != null).OrderByDescending(x => (DateTime)x.PRODUCTIONYEAR).Take(8);

            ViewBag.NewFilm = _NewFilm;

            // Xếp hạng danh sách phim

            var _RankFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0).OrderByDescending(x => x.VIEWS).ThenByDescending(x => x.FILMID).Take(5);

            ViewBag.RankFilm = _RankFilm;

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

            return View(_Film);
        }

        // Trang đăng nhập

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login (FormCollection formCollection)
        {
            string username = formCollection["ACCOUNTNAME"];
            string userpass = formCollection["ACCOUNTPASS"];
            var _account = _context.ACCOUNTS.Where(x=> x.ACCOUNTNAME == username).FirstOrDefault();
            if (_account != null)
            {
                bool checkpass = _account.ACCOUNTPASS.Equals(userpass);
                if(checkpass)
                {
                    var _user = _context.USERS.Where(x => x.ACCOUNTID == _account.ACCOUNTID).FirstOrDefault();
                    if (_user != null)
                    {

                        Session["userid"] = _user.USERID;
                        Session["avatar"] = _user.IMAGENAME;
                        Session["lastname"] = _user.LASTNAME;
                        Session["firstname"] = _user.FIRSTNAME;
                        Session["isLogin"] = true;
                        return RedirectToAction("UserPage", "_Users");
                    }
                }
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection formCollection)
        {
            return View();
        }



        //test
        public JsonResult GET_FILM()
        {
            var _Film = (from f in _context.FILMS
                         where f.FILM_STATUS == 0 && f.LANGUAGE == "Phụ đề Việt"
                         select new
                         {
                             FILMID = f.FILMID,
                         }).OrderByDescending(x=>x.FILMID).Take(24).ToList();
            return Json(JsonConvert.SerializeObject(_Film), JsonRequestBehavior.AllowGet);
        }



    }
}