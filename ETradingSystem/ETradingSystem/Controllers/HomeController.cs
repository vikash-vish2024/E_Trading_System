using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETradingSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Help()
        {
            ViewBag.Messenge = "Help page";
            return View();
        }
        public ActionResult Welcome()
        {
            ViewBag.Messenge = "Welcome page";
            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Messenge = "Login page";
            return View();
        }
        public ActionResult AboutUs()
        {
            ViewBag.Messenge = "AboutUS page";
            return View();
        }

    }
}
