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
    public class VendorsController : Controller
    {
        private E_TradingDBEntities db = new E_TradingDBEntities();
        Vendor v = new Vendor();

        // GET: Vendors/Details/5
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

        // GET: Vendors/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.Hint_Id = new SelectList(db.Hints, "Hint_Id", "Questions");
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading the create vendor page: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Vendor_Id,Vendor_Name,Vendor_Email,Mobile_Number,Address,Category,Vendor_Age,Passowrd,Hint_Id,Hint_Answer,Status")] Vendor vendor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    v.Status = "Active";
                    db.Vendors.Add(vendor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Hint_Id = new SelectList(db.Hints, "Hint_Id", "Questions", vendor.Hint_Id);
                return View(vendor);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while creating the vendor: " + ex.Message;
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
                Vendor vendor = db.Vendors.Find(id);
                if (vendor == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Hint_Id = new SelectList(db.Hints, "Hint_Id", "Questions", vendor.Hint_Id);
                return View(vendor);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading the edit vendor page: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Vendor_Id,Vendor_Name,Vendor_Email,Mobile_Number,Address,Category,Vendor_Age,Passowrd,Hint_Id,Hint_Answer,Status")] Vendor vendor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(vendor).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Hint_Id = new SelectList(db.Hints, "Hint_Id", "Questions", vendor.Hint_Id);
                return View(vendor);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while editing the vendor: " + ex.Message;
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
