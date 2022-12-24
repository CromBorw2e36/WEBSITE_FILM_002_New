using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        WEBSITE_FILM _conntext = new WEBSITE_FILM();
        // GET: Admin/Users
        public ActionResult Users()
        {
            ViewBag.PagePositon = "USERS";
            var Users = _conntext.USERS.ToList();
            return View(Users);
        }

        [HttpGet]
        public ActionResult EditUser (int id)
        {
            ViewBag.PagePositon = "USERS";
            var user  = _conntext.USERS.Where( x => x.USERID == id ).FirstOrDefault(); 
            if(user != null)
            {
                return View(user);
            }
            return RedirectToAction("Error", "MainDashboarh");
        }

        [HttpPost]
        public ActionResult EditUser(USERS request)
        {
            var User = _conntext.USERS.Where( x => x.USERID == request.USERID).FirstOrDefault();
            if(User != null)
            {
                User.LASTNAME = request.LASTNAME;
                User.FIRSTNAME = request.FIRSTNAME;
                User.BORN = (DateTime)request.BORN;
                User.GENDER = request.GENDER;
                User.IMAGENAME = (string)(request.IMAGENAME != "" ? request.IMAGENAME : "unknown.png");
                _conntext.Entry(User).State = EntityState.Modified;
                _conntext.SaveChanges();

                return View(User);
            }
            return RedirectToAction("Error", "MainDashboard");
        }

        [HttpGet]
        public ActionResult DetailUser(int id)
        {
            ViewBag.PagePositon = "USERS";
            var User = _conntext.USERS.Where( x => x.USERID == id ).FirstOrDefault();
            if(User != null )
            {
                return View(User);
            }
            return View();
        }
    }
}