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
    public class DeliveryTeamsController : Controller
    {
        private DB22Entities3 db = new DB22Entities3();

        // GET: DeliveryTeams
        public ActionResult Index()
        {
            return View(db.DeliveryTeams.ToList());
        }

        public ActionResult Welcome(string id)
        {

            DeliveryTeam sp = new DeliveryTeam();
            int y = db.DeliveryTeams.FirstOrDefault(x => x.Id.Equals(id)).DelivererId;
            sp.DelivererId = y;
            return View(sp);
        }

        public ActionResult ViewOrders(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AssignOrder cat = db.AssignOrders.Find(id);
            if (cat == null)
            {
                ViewBag.msg = "No Order is assigned to you";
                //return HttpNotFound();
            }
            List<AssignOrder> list = new List<AssignOrder>();
            var books = db.AssignOrders;
            foreach (AssignOrder b in books)
            {
                if (b.DelivererId == id && b.Status == "Not Delivered")
                {
                    list.Add(b);
                }
            }
            return View(list);
        }

        // GET: DeliveryTeams/Details/5

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
            var books = db.AssignOrders;
            foreach (AssignOrder b in books.ToList())
            {
                if (b.OrderId == id )
                {
                    b.Status ="Delivered";
                    //db.Entry(b).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var book = db.OrderDetails;
            foreach (OrderDetail b in book.ToList())
            {
                if (b.OrderId == id)
                {
                    b.State = "Done";
                    //db.Entry(b).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Welcome","DeliveryTeams",new { id = idd});
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
 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryTeam deliveryTeam = db.DeliveryTeams.Find(id);
            if (deliveryTeam == null)
            {
                return HttpNotFound();
            }
            return View(deliveryTeam);
        }

        // GET: DeliveryTeams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryTeams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DelivererId,Id,Name")] DeliveryTeam deliveryTeam)
        {
            if (ModelState.IsValid)
            {
                db.DeliveryTeams.Add(deliveryTeam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deliveryTeam);
        }

        // GET: DeliveryTeams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryTeam deliveryTeam = db.DeliveryTeams.Find(id);
            if (deliveryTeam == null)
            {
                return HttpNotFound();
            }
            return View(deliveryTeam);
        }

        // POST: DeliveryTeams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DelivererId,Id,Name")] DeliveryTeam deliveryTeam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deliveryTeam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deliveryTeam);
        }

        // GET: DeliveryTeams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryTeam deliveryTeam = db.DeliveryTeams.Find(id);
            if (deliveryTeam == null)
            {
                return HttpNotFound();
            }
            return View(deliveryTeam);
        }

        // POST: DeliveryTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeliveryTeam deliveryTeam = db.DeliveryTeams.Find(id);
            db.DeliveryTeams.Remove(deliveryTeam);
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
