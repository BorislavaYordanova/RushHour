using RushHour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace RushHour.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.LoginUser = "Anonymous User";
            if (Utility.LoginUserSession.Current != null)
            {
                ViewBag.LoginUser = Utility.LoginUserSession.Current.Name;
            }
            return View();
        }
    }
}


