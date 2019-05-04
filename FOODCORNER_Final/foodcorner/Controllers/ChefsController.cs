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
    public class ChefsController : Controller
    {
        private DB22Entities3 db = new DB22Entities3();
        int f;

        // GET: Chefs
        public ActionResult Index()
        {
            return View(db.Chefs.ToList());
        }
        public ActionResult Welcome(string id)
        {

            Chef sp = new Chef();
            int y = db.Chefs.FirstOrDefault(x => x.Id.Equals(id)).ChefId;
            sp.ChefId = y;
            return View(sp);
            //return RedirectToAction("ViewOrders","Chefs",new { idd = sp.ChefId});
        }

        public ActionResult ViewOrders(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PassOrder cat = db.PassOrders.Find(id);
            if (cat == null)
            {
                ViewBag.msg = "No Order is assigned to you";
                //return HttpNotFound();
            }
            List<PassOrder> list = new List<PassOrder>();
            var books = db.PassOrders;
            foreach (PassOrder b in books.ToList())
            {
                if (b.OrderId == id)
                {
                    list.Add(b);
                }
            }
            return View(list);
        }

        public ActionResult Done(int? id,int? idd)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (idd == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var books = db.PassOrders;
            foreach (PassOrder b in books.ToList())
            {
                if (b.OrderId == id)
                {
                    b.Status= "Done";
                    //db.Entry(b).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var book = db.Chefs;
            foreach (Chef b in book.ToList())
            {
                if (b.ChefId == idd)
                {
                    b.State = "Free";
                    //db.Entry(b).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Welcome", "Chefs", new { id = idd });
        }

        public ActionResult OrderDet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            /*PlaceOrder cat = db.PlaceOrders.Find(id);
            if (cat == null)
            {
                ViewBag.msg = "Could Not Found";
                //return HttpNotFound();
            }*/
            List<PlaceOrder> list = new List<PlaceOrder>();
            var books = db.PlaceOrders;
            foreach (PlaceOrder b in books)
            {
                if (b.OrderId == id)
                {
                    list.Add(b);
                }
            }
            return View(list);
        }
        // GET: Chefs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chef chef = db.Chefs.Find(id);
            if (chef == null)
            {
                return HttpNotFound();
            }
            return View(chef);
        }

        // GET: Chefs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chefs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChefId,Id,Name,State")] Chef chef)
        {
            if (ModelState.IsValid)
            {
                db.Chefs.Add(chef);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chef);
        }

        // GET: Chefs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chef chef = db.Chefs.Find(id);
            if (chef == null)
            {
                return HttpNotFound();
            }
            return View(chef);
        }

        // POST: Chefs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChefId,Id,Name,State")] Chef chef)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chef).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chef);
        }

        // GET: Chefs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chef chef = db.Chefs.Find(id);
            if (chef == null)
            {
                return HttpNotFound();
            }
            return View(chef);
        }

        // POST: Chefs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chef chef = db.Chefs.Find(id);
            db.Chefs.Remove(chef);
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
