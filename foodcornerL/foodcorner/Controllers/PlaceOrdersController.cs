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
    public class PlaceOrdersController : Controller
    {
        private DB22Entities3 db = new DB22Entities3();

        // GET: PlaceOrders
        public ActionResult Index()
        {
            var placeOrders = db.PlaceOrders.Include(p => p.ItemsDetail).Include(p => p.OrderDetail);
            return View(placeOrders.ToList());
        }

        // GET: PlaceOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceOrder placeOrder = db.PlaceOrders.Find(id);
            if (placeOrder == null)
            {
                return HttpNotFound();
            }
            return View(placeOrder);
        }

        // GET: PlaceOrders/Create
        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.ItemsDetails, "ItemId", "Name");
            ViewBag.OrderId = new SelectList(db.OrderDetails, "OrderId", "State");
            return View();
        }

        // POST: PlaceOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,ItemId,quantity,Bill,Feedback,PlaceId")] PlaceOrder placeOrder)
        {
            if (ModelState.IsValid)
            {
                db.PlaceOrders.Add(placeOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.ItemsDetails, "ItemId", "Name", placeOrder.ItemId);
            ViewBag.OrderId = new SelectList(db.OrderDetails, "OrderId", "State", placeOrder.OrderId);
            return View(placeOrder);
        }

        // GET: PlaceOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceOrder placeOrder = db.PlaceOrders.Find(id);
            if (placeOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.ItemsDetails, "ItemId", "Name", placeOrder.ItemId);
            ViewBag.OrderId = new SelectList(db.OrderDetails, "OrderId", "State", placeOrder.OrderId);
            return View(placeOrder);
        }

        // POST: PlaceOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,ItemId,quantity,Bill,Feedback,PlaceId")] PlaceOrder placeOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(placeOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.ItemsDetails, "ItemId", "Name", placeOrder.ItemId);
            ViewBag.OrderId = new SelectList(db.OrderDetails, "OrderId", "State", placeOrder.OrderId);
            return View(placeOrder);
        }

        // GET: PlaceOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceOrder placeOrder = db.PlaceOrders.Find(id);
            if (placeOrder == null)
            {
                return HttpNotFound();
            }
            return View(placeOrder);
        }

        // POST: PlaceOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlaceOrder placeOrder = db.PlaceOrders.Find(id);
            db.PlaceOrders.Remove(placeOrder);
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
