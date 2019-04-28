using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using foodcorner.Models;

namespace foodcorner.Controllers
{
    
    public class CustomersController : Controller
    {
        int order;
        int cus;
        int bill;
        int q;
        int p;
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-VFGDRDF\NIDA;Initial Catalog=DB22;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");

        private DB22Entities3 db = new DB22Entities3();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }
        public ActionResult Welcome(string id)
        {

            Customer sp = new Customer();
            sp.CustomerId= db.Customers.FirstOrDefault(x => x.Id.Equals(id)).CustomerId;
            return View(sp);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Welcome(Customer model)
        {
            Customer customer = db.Customers.Find(model.CustomerId);
            customer.Address = model.Address;
            db.SaveChanges();
            return RedirectToAction("cWelcome", new { id = customer.CustomerId }); 
        }
        public ActionResult cWelcome(int id)
        {
            Customer customer = db.Customers.Find(id);
            return View(customer);
        }
        // orderdetail table created
        public ActionResult PlaceOrder(int id)
        {
            OrderDetail od = new OrderDetail();
            od.CustomerId = id;
            od.OrderDate = DateTime.Today;
            od.RequiredDate = DateTime.Today.AddMinutes(60);
            od.State = "In Progress";
            db.OrderDetails.Add(od);
            db.SaveChanges();
            cus = od.CustomerId;
            order = od.OrderId;
            TempData["id"] = order;

            return View(db.Categories.ToList());

        }
        //orderid is given
        public ActionResult GetDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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

        public ActionResult ViewOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<OrderDetail> list = new List<OrderDetail>();
            var books = db.OrderDetails;
            foreach (OrderDetail b in books)
            {
                if (b.CustomerId == id)
                {
                    list.Add(b);
                }
            }
            return View(list);
        }
        public ActionResult Addcart(int? id )
        {
            ItemsDetail cat = db.ItemsDetails.Find(id);
            PlaceOrder po = new PlaceOrder();
            po.OrderId = Convert.ToInt32(TempData["id"]); 
            po.ItemId = cat.ItemId;
            po.quantity = 1 ;
            int bb= (po.quantity * cat.Price);
            bill = bill + (po.quantity * cat.Price);
            po.Bill = bb;
            db.PlaceOrders.Add(po);
            db.SaveChanges();
            return RedirectToAction("ViewItems", new { id = cat.CategoryId });

        }
        public ActionResult ViewCart()
        {
            order = Convert.ToInt32(TempData["id"]);
            TempData["tot"] = order;

            List<PlaceOrder> list = new List<PlaceOrder>();
            var books = db.PlaceOrders;
            foreach (PlaceOrder b in books)
            {
                if (b.OrderId == order)
                {
                    list.Add(b);
                }
            }
            return View(list);

        }
        public ActionResult Checkout()
        {
            int ff;
            ff = Convert.ToInt32(TempData["tot"]);
            OrderDetail pss = db.OrderDetails.Find(ff);
            if (pss == null)
            {
                return HttpNotFound();
            }
            return View(pss);
        }
       
        public ActionResult Edit1(int? id , int price)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceOrder pss = db.PlaceOrders.Find(id);
            q =pss.quantity;
            p = price;
            bill = Convert.ToInt32(TempData["BILL"]);
            bill = bill - (q * p);
            TempData["BILL"]= bill;
            if (pss == null)
            {
                return HttpNotFound();
            }
            return View(pss);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "quantity")] PlaceOrder placeOrder , int price)
        {
            if (ModelState.IsValid)
            {
                db.Entry(placeOrder).State = EntityState.Modified;
                q = placeOrder.quantity;
                p = price;
                bill = Convert.ToInt32(TempData["BILL"]);
                bill = bill + (q * p);
                db.SaveChanges();
                return RedirectToAction("ViewCart");
            }
            
            return View(placeOrder);
        }
        // GET: ItemsDetails1/Delete/5
        public ActionResult Delete1(int? id, int price)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceOrder pss = db.PlaceOrders.Find(id);
           

            if (pss == null)
            {
                return HttpNotFound();
            }
            return View(pss);
        }

        // POST: ItemsDetails1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed1(int id ,int price)
        {
            PlaceOrder ppp = db.PlaceOrders.Find(id);
            q = ppp.quantity;
            p = price;
            bill = bill - (q * p);
            db.PlaceOrders.Remove(ppp);
            db.SaveChanges();
            return RedirectToAction("ViewCart");
        }




        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,Id,Address,Name")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,Id,Address,Name")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

        // GET: Customers/Edit/5
        public ActionResult Edit22(int? id, int? price  )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceOrder customer = db.PlaceOrders.Find(id);
           
            if (price.HasValue)
            {
                TempData["PPP"] = price;
            }
            
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.ItemsDetails, "ItemId", "Name",customer.ItemId);
            ViewBag.OrderId = new SelectList(db.OrderDetails, "OrderId", "State", customer.OrderId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit22([Bind(Include = "OrderId,ItemId,quantity,Bill,Feedback,PlaceId")] PlaceOrder placeOrder)
        {
            
            if (ModelState.IsValid)
            {
                
                placeOrder.Bill = placeOrder.quantity * placeOrder.Bill;
                db.Entry(placeOrder).State = EntityState.Modified;
                db.SaveChanges();
                TempData["id"] = placeOrder.OrderId;
                return RedirectToAction("ViewCart");
            }
            ViewBag.ItemId = new SelectList(db.ItemsDetails, "ItemId", "Name", placeOrder.ItemId);
            ViewBag.OrderId = new SelectList(db.OrderDetails, "OrderId", "State", placeOrder.OrderId);
            return RedirectToAction("ViewCart");
        }

        // GET: PlaceOrders/Delete/5
        public ActionResult Delete22(int? id)
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
        [HttpPost, ActionName("Delete22")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed22(int id)
        {
            PlaceOrder placeOrder = db.PlaceOrders.Find(id);
            db.PlaceOrders.Remove(placeOrder);
            db.SaveChanges();
            TempData["id"] = placeOrder.OrderId;
            return RedirectToAction("ViewCart");
        }

        public ActionResult Edit33(int? id)
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
        public ActionResult Edit33([Bind(Include = "OrderId,ItemId,quantity,Bill,Feedback,PlaceId")] PlaceOrder placeOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(placeOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetDetails" , new { id = placeOrder.OrderId });
            }
            ViewBag.ItemId = new SelectList(db.ItemsDetails, "ItemId", "Name", placeOrder.ItemId);
            ViewBag.OrderId = new SelectList(db.OrderDetails, "OrderId", "State", placeOrder.OrderId);
            return View(placeOrder);
        }

    }
}
