using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ETradingSystem.Controllers.E_Trading.CustomerFun;
using ETradingSystem.Models;

namespace ETradingSystem.Controllers.E_Trading.VendorFun
{
    public class Products1Controller : Controller
    {
        private E_TradingDBEntities db = new E_TradingDBEntities();

        public ActionResult Index()
        {
            try
            {
                var products = db.Products.Include(p => p.Vendor);
                return View(products.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving products: " + ex.Message;
                return View("Error");
            }
        }

        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            ViewBag.Vendor_Id = new SelectList(db.Vendors, "Vendor_Id", "Vendor_Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_Id,Vendor_Id,Product_Name,Brand,Color,Price,Available_Stock,Status,isdeleted,ImageURL")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Vendor_Id = new SelectList(db.Vendors, "Vendor_Id", "Vendor_Name", product.Vendor_Id);
                return View(product);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while creating the product: " + ex.Message;
                return View("Error");
            }
        }

        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            try
            {
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while deleting the product: " + ex.Message;
                return View("Error");
            }
        }

        public ActionResult PlaceOrderConfirmed(decimal id)
        {
            try
            {
                if (CustomerValidationController.cust_id == 0)
                {
                    return RedirectToAction("Login", "CustomerValidation");
                }
                else
                {
                    Product p = new Product();
                    Random r = new Random();
                    int purchaseId = r.Next(1111, 9999);
                    int productId = ProductsController.productId;
                    int customerId = (int)CustomerValidationController.cust_id;
                    db.orderPlaced(purchaseId, productId, customerId);
                    int orderId = r.Next(100, 999);
                    db.orderDetail(orderId, purchaseId, productId);
                    db.updateStatus(productId);
                    db.updateStatus(productId);
                    return RedirectToAction("Index", "Products1");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while placing the order: " + ex.Message;
                return View("Error");
            }
        }
    }
}
