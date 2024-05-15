using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETradingSystem.Models;

namespace ETradingSystem.Controllers.E_Trading.CustomerFun
{
    public class CustomerValidationController : Controller
    {
        public static decimal cust_id = 0;
        private readonly E_TradingDBEntities db;

        public CustomerValidationController()
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
                if (IsValidCustomer(email, password))
                {
                    cust_id = db.Customers.Where(c => c.Customer_Email == email).Select(c => c.Customer_Id).FirstOrDefault();
                    return RedirectToAction("Index", "Products1");
                }
                else
                {
                    ViewBag.InvalidLogin = "Invalid Customer Email or password.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred during login: " + ex.Message;
                return View("Error");
            }
        }

        private bool IsValidCustomer(string email, string password)
        {
            try
            {
                string Email = db.Customers.Where(x => x.Customer_Email == email).Select(x => x.Customer_Email).FirstOrDefault();
                string Password = db.Customers.Where(x => x.Customer_Email == email).Select(x => x.Password).FirstOrDefault();
                return email == Email && password == Password;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while validating customer credentials: " + ex.Message;
                return false;
            }
        }
    }
}
