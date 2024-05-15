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
    public class AdminCustomersController : Controller
    {
        private readonly E_TradingDBEntities db;

        public AdminCustomersController()
        {
            db = new E_TradingDBEntities();
        }

        public ActionResult Index()
        {
            try
            {
                var customers = db.Customers.Include(c => c.Hint);
                return View(customers.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving customers: " + ex.Message;
                return View("Error");
            }
        }

        public ActionResult GetCustomerByCustomerName(string customerName)
        {
            try
            {
                var customers = db.Customers.Where(c => c.Customer_Name == customerName).ToList();
                return View("Index", customers);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving customers: " + ex.Message;
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
                Customer customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving customer details: " + ex.Message;
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
                Customer customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving customer details for deletion: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            try
            {
                Customer customer = db.Customers.Find(id);
                if (customer != null)
                {
                    if (customer.Status == "Active")
                    {
                        customer.Status = "Inactive";
                    }
                    else
                    {
                        customer.Status = "Active";
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while deleting the customer: " + ex.Message;
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
