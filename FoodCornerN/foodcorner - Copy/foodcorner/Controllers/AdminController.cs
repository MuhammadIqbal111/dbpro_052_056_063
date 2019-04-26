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
    public class AdminController : Controller
    {
        private DB22Entities3 db = new DB22Entities3();

        public ActionResult Index()
        {
            
            return View(db.Categories.ToList());
            
            
        }
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult ViewSupplierMenu()
        {
            return View();
        }
        public ActionResult ViewCustomers()
        {
            return RedirectToAction("Index", "Customers");

        }
        public ActionResult ViewChefs()
        {
            return RedirectToAction("Index", "Chefs");

        }
        public ActionResult ViewOrders()
        {
            return View();
        }
        public ActionResult ViewSuppliers()
        {
            return RedirectToAction("Index", "Suppliers");
        }
        public ActionResult ViewDelivery()
        {
            return RedirectToAction("Index", "DeliveryTeams");
        }
        public ActionResult Menu()
        {
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult ViewItems(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category cat = db.Categories.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
        }

        public ActionResult Create1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category cat = db.Categories.Find(id);
            ItemsDetail it = new ItemsDetail();
            it.CategoryId = cat.CategoryId;
            if (cat == null)
            {
                return HttpNotFound();
            }
            

            return View(it);

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1([Bind(Include = "CategoryId,Name,Description,Price,Quantity,ImageFile")] ItemsDetail itemsDetail)
        {

            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(itemsDetail.ImageFile.FileName);
                string extension = Path.GetExtension(itemsDetail.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                itemsDetail.Image = "~/Images/" + fileName;
                Console.WriteLine(extension);
                //var supportedType = new[] { "jpg", "jpeg", "png" };
                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    itemsDetail.ImageFile.SaveAs(fileName);

                    db.ItemsDetails.Add(itemsDetail);
                    db.SaveChanges();
                    return RedirectToAction("ViewItems", new { id = itemsDetail.CategoryId });
                

            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", itemsDetail.CategoryId);
            return View(itemsDetail);
        }
        [HttpGet]
        public ActionResult Edit1(int? id, int? idd)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category cat = db.Categories.Find(idd);
            ItemsDetail itemsDetail = db.ItemsDetails.Find(id);
            Session["imagepath"] = itemsDetail.Image;
            itemsDetail.CategoryId = cat.CategoryId;
            if (itemsDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", itemsDetail.CategoryId);
            return View(itemsDetail);
        }
        // POST: ItemsDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "ItemId, CategoryId,Name,Description,Price,Quantity,ImageFile")] ItemsDetail itemsDetail)
        {
            if (ModelState.IsValid)
            {
                if (itemsDetail.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(itemsDetail.ImageFile.FileName);
                    string extension = Path.GetExtension(itemsDetail.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    itemsDetail.Image = "~/Images/" + fileName;
                    //var supportedType = new[] { "jpg", "jpeg", "png" };
                    
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        db.Entry(itemsDetail).State = EntityState.Modified;
                        if (db.SaveChanges() > 0)
                        {
                            itemsDetail.ImageFile.SaveAs(fileName);
                            return RedirectToAction("ViewItems", new { id = itemsDetail.CategoryId });
                        }
                    

                }

                else
                {
                    itemsDetail.Image = Session["imagepath"].ToString();
                    db.Entry(itemsDetail).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        return RedirectToAction("ViewItems", new { id = itemsDetail.CategoryId });
                    }
                }


            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", itemsDetail.CategoryId);
            return View(itemsDetail);
        }
        public ActionResult Delete1(int? id)
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

        // POST: ItemsDetails/Delete/5
        [HttpPost, ActionName("Delete1")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed1(int id)
        {
            int cati;
            ItemsDetail itemsDetail = db.ItemsDetails.Find(id);
            cati = itemsDetail.CategoryId ;
            db.ItemsDetails.Remove(itemsDetail);
            db.SaveChanges();
            return RedirectToAction("ViewItems", new { id = cati});
        }
        // items
        public ActionResult AdminBuyProduct()
        {
            return View();
        }
        public ActionResult DeliveredOrders()
        {
            return View();
        }

        public ActionResult ListOfAdminFoodItems()
        {
            return View();
        }
        public ActionResult ListOfChefs()
        {
            return View();
        }
        public ActionResult ListOfOrders()
        {
            return View();
        }
      

            // GET: Admin/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
