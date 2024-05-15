using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ETradingSystem.Models;

namespace ETradingSystem.Controllers.E_Trading.CustomerFun
{
    public class OrdersController : Controller
    {
        private E_TradingDBEntities db = new E_TradingDBEntities();

        public ActionResult Index()
        {
            try
            {
                var orders = db.Orders.Include(o => o.Customer);
                return View(orders.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving orders: " + ex.Message;
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
                Order order = db.Orders.Find(id);
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving order details: " + ex.Message;
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            try
            {
                ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_Id", "Customer_Name");
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading the create order page: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Purchase_Id,Customer_Id,Delivery_Date,Order_Amount,Payment_Mode,Address,Status")] Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_Id", "Customer_Name", order.Customer_Id);
                return View(order);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while creating the order: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Purchase_Id,Customer_Id,Delivery_Date,Order_Amount,Payment_Mode,Address,Status")] Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_Id", "Customer_Name", order.Customer_Id);
                return View(order);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while editing the order: " + ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Delete(decimal id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Order order = db.Orders.Find(id);
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving order for deletion: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            try
            {
                Order order = db.Orders.Find(id);
                if (order != null)
                {
                    order.Status = "Cancelled";
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while deleting the order: " + ex.Message;
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
