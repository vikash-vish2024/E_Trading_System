using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ETradingSystem.Models;

namespace ETradingSystem.Controllers.E_Trading.VendorFun
{
    public class ProductsController : Controller
    {
        private E_TradingDBEntities db = new E_TradingDBEntities();
        public static int productId;

        public ActionResult Index(decimal? id)
        {
            try
            {
                var products = db.Products.Where(v => v.Vendor_Id == id);
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
            try
            {
                productId = (int)id;
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
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving product details: " + ex.Message;
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            try
            {
                ViewBag.Vendor_Id = new SelectList(db.Vendors, "Vendor_Id", "Vendor_Name");
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading the create product page: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_Id,Vendor_Id,Product_Name,Brand,Color,Price,Available_Stock,Status,ImageFileName,isdeleted")] Product product)
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

        public ActionResult Edit(decimal id)
        {
            try
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
                ViewBag.Vendor_Id = new SelectList(db.Vendors, "Vendor_Id", "Vendor_Name", product.Vendor_Id);
                return View(product);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading the edit product page: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_Id,Vendor_Id,Product_Name,Brand,Color,Price,Available_Stock,Status,ImageFileName,isdeleted")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Vendor_Id = new SelectList(db.Vendors, "Vendor_Id", "Vendor_Name", product.Vendor_Id);
                return View(product);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while editing the product: " + ex.Message;
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
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading the delete product page: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            try
            {
                Product product = db.Products.Find(id);
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
