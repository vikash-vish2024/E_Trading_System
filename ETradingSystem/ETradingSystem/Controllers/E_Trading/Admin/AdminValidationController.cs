using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETradingSystem.Models;

namespace ETradingSystem.Controllers.E_Trading.Admin
{
    public class AdminValidationController : Controller
    {
        private readonly E_TradingDBEntities db;

        public AdminValidationController()
        {
            db = new E_TradingDBEntities();
        }

        public ActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading the login page: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            try
            {
                if (IsValidAdmin(email, password))
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.InvalidLogin = "Invalid Admin Email or Password.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred during login: " + ex.Message;
                return View("Error");
            }
        }

        private bool IsValidAdmin(string email, string password)
        {
            try
            {
                string Email = db.Admins.Where(x => x.Admin_Email == email).Select(x => x.Admin_Email).FirstOrDefault();
                string Password = db.Admins.Where(x => x.Admin_Email == email).Select(x => x.Password).FirstOrDefault();
                return email == Email && password == Password;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while validating admin credentials: " + ex.Message;
                return false;
            }
        }
    }
}
