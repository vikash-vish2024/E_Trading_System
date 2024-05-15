using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ETradingSystem.Models;

namespace ETradingSystem.Controllers.E_Trading.Admin
{
    public class AdminVendorsController : Controller
    {
        private E_TradingDBEntities db = new E_TradingDBEntities();

        public ActionResult Index()
        {
            try
            {
                var vendors = db.Vendors.Include(v => v.Hint);
                return View(vendors.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving vendors: " + ex.Message;
                return View("Error");
            }
        }

        public ActionResult GetVendorsByVendorName(string vendorName)
        {
            try
            {
                var vendors = db.Vendors.Where(v => v.Vendor_Name == vendorName).ToList();
                return View("Index", vendors);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving vendors by name: " + ex.Message;
                return View("Error");
            }
        }

        public ActionResult Details(decimal id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vendor vendor = db.Vendors.Find(id);
                if (vendor == null)
                {
                    return HttpNotFound();
                }
                return View(vendor);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving vendor details: " + ex.Message;
                return View("Error");
            }
        }

        public ActionResult Delete(decimal id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vendor vendor = db.Vendors.Find(id);
                if (vendor == null)
                {
                    return HttpNotFound();
                }
                return View(vendor);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving vendor details for deletion: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            try
            {
                Vendor vendor = db.Vendors.Find(id);
                if (vendor != null)
                {
                    if (vendor.Status == "Active")
                    {
                        vendor.Status = "Inactive";
                    }
                    else
                    {
                        vendor.Status = "Active";
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while deleting the vendor: " + ex.Message;
                return View("Error");
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while disposing: " + ex.Message;
                View("Error");
            }
        }
    }
}
