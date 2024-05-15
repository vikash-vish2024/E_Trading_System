using ETradingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETradingSystem.Controllers.E_Trading.VendorFun;

namespace ETradingSystem.Controllers.E_Trading.CustomerFun
{
    public class CustomersController : Controller
    {
        private readonly E_TradingDBEntities db;

        public CustomersController()
        {
            db = new E_TradingDBEntities();
        }

        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving customers: " + ex.Message;
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            try
            {
                ViewBag.Hint_Id = new SelectList(db.Hints, "Hint_Id", "Questions");
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading data: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customer_Id,Customer_Name,Customer_Email,Date_Of_Birth,Address,Balance,Mobile_Number,Password,Hint_Id,Hint_Answer,Status")] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    customer.Status = "Active";
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Hint_Id = new SelectList(db.Hints, "Hint_Id", "Questions", customer.Hint_Id);
                return View(customer);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while creating the customer: " + ex.Message;
                return View("Error");
            }
        }

        public ActionResult GetOrderDetailsOfCustomerId(int id)
        {
            try
            {
                Order order = db.Orders.Find(id);
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving order details: " + ex.Message;
                return View("Error");
            }
        }
    }
}
