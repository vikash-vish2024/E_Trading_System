using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETradingSystem.Models;

namespace ETradingSystem.Controllers.E_Trading.VendorFun
{
    public class VendorValidationController : Controller
    {
        public decimal Vendor_ID;
        Vendor v = new Vendor();
        private readonly E_TradingDBEntities db;

        public VendorValidationController()
        {
            db = new E_TradingDBEntities();
        }

        // GET: Vendor
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
                if (IsValidVendor(email, password) && v.Status == "Active")
                {
                    Vendor_ID = db.Vendors.Where(p => p.Vendor_Email == email).Select(p => p.Vendor_Id).FirstOrDefault();
                    return RedirectToAction("Index/" + Vendor_ID, "Products");
                }
                else if (IsValidVendor(email, password) && v.Status == "Inactive")
                {
                    ViewBag.ErrorMessage = "Your Account is inactive. Please contact the admin.";
                    return View();
                }
                else
                {
                    ViewBag.InvalidLogin = "Invalid Admin Email or Password.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while processing the login request: " + ex.Message;
                return View("Error");
            }
        }

        private bool IsValidVendor(string email, string password)
        {
            try
            {
                var vendor = db.Vendors.FirstOrDefault(x => x.Vendor_Email == email && x.Passowrd == password);
                if (vendor != null && vendor.Status == "Active")
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while validating the vendor: " + ex.Message;
                return false;
            }
        }
    }
}
