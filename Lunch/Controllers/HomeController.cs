using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Lunch.Models;

namespace Lunch.Controllers
{
    public class HomeController : Controller
    {
        //ApplicationDbContext db = new ApplicationDbContext();
        //ApplicationUser UserInSession;
        public ActionResult Index()
        {
            //var userIdInSession = User.Identity.GetUserId();
            //if (userIdInSession != null)
            //    UserInSession = db.Users.Where(user => user.Id == userIdInSession).First();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}