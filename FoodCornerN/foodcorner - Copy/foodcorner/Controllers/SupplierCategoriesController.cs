using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using foodcorner.Models;
using System.IO;

namespace foodcorner.Controllers
{
    public class SupplierCategoriesController : Controller
    {
        private DB22Entities3 db = new DB22Entities3();

        // GET: SupplierCategories
        public ActionResult Index()
        {
            return View(db.SupplierCategories.ToList());
        }

        public ActionResult ViewItems(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SupplierCategory cat = db.SupplierCategories.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CatId,Name")] SupplierCategory supplierCategory)
        {
            if (ModelState.IsValid)
            {
                db.SupplierCategories.Add(supplierCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplierCategory);
        }

        public ActionResult Create1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SupplierCategory cat = db.SupplierCategories.Find(id);
            SupplierItem it = new SupplierItem();
            it.CatId = cat.CatId;
            if (cat == null)
            {
                return HttpNotFound();
            }


            return View(it);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1([Bind(Include = "CateId,Name,Description,Price,ImageFile")] SupplierItem supplierItem)
        {

            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(supplierItem.ImageFile.FileName);
                string extension = Path.GetExtension(supplierItem.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                supplierItem.Image = "~/Images/" + fileName;
                Console.WriteLine(extension);
                //var supportedType = new[] { "jpg", "jpeg", "png" };
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                supplierItem.ImageFile.SaveAs(fileName);

                db.SupplierItems.Add(supplierItem);
                db.SaveChanges();
                return RedirectToAction("ViewItems", new { id = supplierItem.CatId });


            }

            ViewBag.CategoryId = new SelectList(db.SupplierCategories, "CatId", "Name", supplierItem.CatId);
            return View(supplierItem);
        }

        [HttpGet]
        public ActionResult Edit1(int? id, int? idd)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierCategory cat = db.SupplierCategories.Find(idd);
            SupplierItem itemsDetail = db.SupplierItems.Find(id);
            Session["imagepath"] = itemsDetail.Image;
            itemsDetail.CatId = cat.CatId;
            if (itemsDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatId = new SelectList(db.SupplierCategories, "CatId", "Name", itemsDetail.CatId);
            return View(itemsDetail);
        }
        // POST: ItemsDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "ItemId, CatId,Name,Description,Price,ImageFile")] SupplierItem supplierItem)
        {
            if (ModelState.IsValid)
            {
                if (supplierItem.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(supplierItem.ImageFile.FileName);
                    string extension = Path.GetExtension(supplierItem.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    supplierItem.Image = "~/Images/" + fileName;
                    //var supportedType = new[] { "jpg", "jpeg", "png" };

                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    db.Entry(supplierItem).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        supplierItem.ImageFile.SaveAs(fileName);
                        return RedirectToAction("ViewItems", new { id = supplierItem.CatId });
                    }


                }

                else
                {
                    supplierItem.Image = Session["imagepath"].ToString();
                    db.Entry(supplierItem).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        return RedirectToAction("ViewItems", new { id = supplierItem.CatId });
                    }
                }


            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CatId", "Name", supplierItem.CatId);
            return View(supplierItem);
        }
        public ActionResult Delete1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierItem supplierItem = db.SupplierItems.Find(id);
            if (supplierItem == null)
            {
                return HttpNotFound();
            }
            return View(supplierItem);
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
        public ActionResult Edit([Bind(Include = "CatId,Name")] SupplierCategory supplierCategory)
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
