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
    public class SupplierCategoriesController : Controller
    {
        private DB22Entities3 db = new DB22Entities3();

        // GET: SupplierCategories
        public ActionResult Index(int id)
        {
            List<SupplierCategory> list = new List<SupplierCategory>();
            var books = db.SupplierCategories;
            foreach (SupplierCategory b in books)
            {
                b.SupplierId = id;
                
                    list.Add(b);
            }
            return View(list);
        }

        // GET: SupplierCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierCategory supplierCategory = db.SupplierCategories.Find(id);
            if (supplierCategory == null)
            {
                return HttpNotFound();
            }
            return View(supplierCategory);
        }

        // GET: SupplierCategories/Create
        public ActionResult Create(string id)
        {
            Supplier sp = new Supplier();
            sp.SupplierId = db.Suppliers.FirstOrDefault(x => x.SupplierId.Equals(id)).SupplierId; 
            return View(sp);
        }

        // POST: SupplierCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CatId,Name,SupplierId")] SupplierCategory supplierCategory)
        {
            if (ModelState.IsValid)
            {
                db.SupplierCategories.Add(supplierCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplierCategory);
        }

        // GET: SupplierCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierCategory supplierCategory = db.SupplierCategories.Find(id);
            if (supplierCategory == null)
            {
                return HttpNotFound();
            }
            return View(supplierCategory);
        }

        // POST: SupplierCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CatId,Name,SupplierId")] SupplierCategory supplierCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplierCategory);
        }

        // GET: SupplierCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierCategory supplierCategory = db.SupplierCategories.Find(id);
            if (supplierCategory == null)
            {
                return HttpNotFound();
            }
            return View(supplierCategory);
        }

        // POST: SupplierCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupplierCategory supplierCategory = db.SupplierCategories.Find(id);
            db.SupplierCategories.Remove(supplierCategory);
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
