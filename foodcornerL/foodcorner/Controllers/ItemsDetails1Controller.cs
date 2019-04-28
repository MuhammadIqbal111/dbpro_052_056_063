using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using foodcorner.Models;

namespace foodcorner.Controllers
{
    public class ItemsDetails1Controller : Controller
    {
        private DB22Entities3 db = new DB22Entities3();

        // GET: ItemsDetails1
        public ActionResult Index()
        {
            var itemsDetails = db.ItemsDetails.Include(i => i.Category);
            return View(itemsDetails.ToList());
        }

        // GET: ItemsDetails1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemsDetail itemsDetail = db.ItemsDetails.Find(id);
            if (itemsDetail == null)
            {
                return HttpNotFound();
            }
            return View(itemsDetail);
        }

        // GET: ItemsDetails1/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: ItemsDetails1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,CategoryId,Name,Description,Price,Quantity,Image")] ItemsDetail itemsDetail)
        {
            if (ModelState.IsValid)
            {
                db.ItemsDetails.Add(itemsDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", itemsDetail.CategoryId);
            return View(itemsDetail);
        }

        // GET: ItemsDetails1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemsDetail itemsDetail = db.ItemsDetails.Find(id);
            if (itemsDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", itemsDetail.CategoryId);
            return View(itemsDetail);
        }

        // POST: ItemsDetails1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,CategoryId,Name,Description,Price,Quantity,Image")] ItemsDetail itemsDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemsDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", itemsDetail.CategoryId);
            return View(itemsDetail);
        }

        // GET: ItemsDetails1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemsDetail itemsDetail = db.ItemsDetails.Find(id);
            if (itemsDetail == null)
            {
                return HttpNotFound();
            }
            return View(itemsDetail);
        }

        // POST: ItemsDetails1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemsDetail itemsDetail = db.ItemsDetails.Find(id);
            db.ItemsDetails.Remove(itemsDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
