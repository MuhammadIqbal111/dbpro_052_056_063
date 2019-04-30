using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using foodcorner.Models;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace foodcorner.Controllers
{
    public class ReportsController : Controller
    {
        private DB22Entities3 db = new DB22Entities3();

        // GET: AdminOrders
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }
        public ActionResult AdminBuyProduct()
        {
            ReportDocument Report = new ReportDocument();
            Report.Load(Server.MapPath("~/adminbuyproduct.rpt"));
            Report.SetDatabaseLogon("USER1\\SQLEXPRESS", "DB22");

            Stream s = Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        
        public ActionResult DeliveredOrders()
        {
            ReportDocument Report = new ReportDocument();

            Report.Load(Server.MapPath("~/Delivered_orders.rpt"));
            Report.SetDatabaseLogon("USER1\\SQLEXPRESS", "DB22");

            Stream s = Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult ListOfAdminFoodItems()
        {
            ReportDocument Report = new ReportDocument();
            Report.Load(Server.MapPath("~/ListOfAdminFoodItems.rpt"));
            Report.SetDatabaseLogon("USER1\\SQLEXPRESS", "DB22");

            Stream s = Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult ListOfChefs()
        {
            ReportDocument Report = new ReportDocument();
            Report.Load(Server.MapPath("~/ListOfChefs.rpt"));
            Report.SetDatabaseLogon("USER1\\SQLEXPRESS", "DB22");

            Stream s = Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult ListOfOrders()
        {
            ReportDocument Report = new ReportDocument();
            Report.Load(Server.MapPath("~/ListOfOrders.rpt"));
            Report.SetDatabaseLogon("USER1\\SQLEXPRESS", "DB22");

            Stream s = Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult MostDemandingFoodItem()
        {
            ReportDocument Report = new ReportDocument();
            Report.Load(Server.MapPath("~/MostDemandingFoodItem.rpt"));
            Report.SetDatabaseLogon("USER1\\SQLEXPRESS", "DB22");

            Stream s = Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult NotDoneOrders()
        {
            ReportDocument Report = new ReportDocument();
            Report.Load(Server.MapPath("~/NotDoneOrders.rpt"));
            Report.SetDatabaseLogon("USER1\\SQLEXPRESS", "DB22");

            Stream s = Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult CustomersWithMaximumOrders()
        {
            ReportDocument Report = new ReportDocument();
            Report.Load(Server.MapPath("~/CustMaxOrder.rpt"));
            Report.SetDatabaseLogon("USER1\\SQLEXPRESS", "DB22");

            Stream s = Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult OrdersInADay()
        {
            ReportDocument Report = new ReportDocument();
            Report.Load(Server.MapPath("~/OrdersInday.rpt"));
            Report.SetDatabaseLogon("USER1\\SQLEXPRESS", "DB22");

            Stream s = Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult ListOfSuppliers()
        {
            ReportDocument Report = new ReportDocument();
            Report.Load(Server.MapPath("~/ListOfsuppliers.rpt"));
            Report.SetDatabaseLogon("USER1\\SQLEXPRESS", "DB22");

            Stream s = Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult UndeliveredOrders()
        {
            ReportDocument Report = new ReportDocument();
            Report.Load(Server.MapPath("~/undeliOrders.rpt"));
            Report.SetDatabaseLogon("USER1\\SQLEXPRESS", "DB22");

            Stream s = Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }

        // GET: AdminOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminOrder adminOrder = db.AdminOrders.Find(id);
            if (adminOrder == null)
            {
                return HttpNotFound();
            }
            return View(adminOrder);
        }

        // GET: AdminOrders/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.AdminOrderDetails, "OrderId", "OrderId");
            return View();
        }

        // POST: AdminOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId")] AdminOrder adminOrder)
        {
            if (ModelState.IsValid)
            {
                db.AdminOrders.Add(adminOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.AdminOrderDetails, "OrderId", "OrderId", adminOrder.OrderId);
            return View(adminOrder);
        }

        // GET: AdminOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminOrder adminOrder = db.AdminOrders.Find(id);
            if (adminOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.AdminOrderDetails, "OrderId", "OrderId", adminOrder.OrderId);
            return View(adminOrder);
        }

        // POST: AdminOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId")] AdminOrder adminOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.AdminOrderDetails, "OrderId", "OrderId", adminOrder.OrderId);
            return View(adminOrder);
        }

        // GET: AdminOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminOrder adminOrder = db.AdminOrders.Find(id);
            if (adminOrder == null)
            {
                return HttpNotFound();
            }
            return View(adminOrder);
        }

        // POST: AdminOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminOrder adminOrder = db.AdminOrders.Find(id);
            db.AdminOrders.Remove(adminOrder);
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
