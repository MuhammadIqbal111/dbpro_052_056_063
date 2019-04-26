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
            sp.DelivererId = db.DeliveryTeams.FirstOrDefault(x => x.Id.Equals(id)).DelivererId;
            return View(sp);
        }
        // GET: DeliveryTeams/Details/5
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
