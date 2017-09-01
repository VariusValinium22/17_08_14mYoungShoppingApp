using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _17_08_14mYoungShoppingApp.Models;
using Microsoft.AspNet.Identity;

namespace _17_08_14mYoungShoppingApp.Controllers
{
	[Authorize]
    public class OrdersController : Universal
    {
        // GET: Orders
        public ActionResult Index()
        {
			var user = db.Users.Find(User.Identity.GetUserId());

			return View(user.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
		public ActionResult FinalizeOrder(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Order order = db.Orders.Find(id);
			if (order == null)
			{
				return HttpNotFound();
			}
			return View(order);
		}
		// GET: Orders/Create
		public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address,City,State,ZipCode,Country,PhoneNumber,Total,OrderDate,CustomerId,OrderDetails")] Order order, decimal total)
        {
            if (ModelState.IsValid)
            {
				var user = db.Users.Find(User.Identity.GetUserId());//USER INPUT ON THE FORM SENT TO ALL OF OUR VIEWS
				order.CustomerId = user.Id;
				order.OrderDate = System.DateTime.Now;
				order.Total = total;
                db.Orders.Add(order);
                db.SaveChanges();						//created the order
				foreach (var item in user.CartItems.ToList())// closes dateabase connection
				{
					OrderItem orderitem = new OrderItem();	//create a new order item for each item that exists in the list.
					orderitem.ItemId = item.ItemId;			 
					orderitem.OrderId = order.Id;			//this line is possible because of the id being assigned by SQL in the db.Orders.Add line above.
					orderitem.Quantity = item.Count;		
					orderitem.UnitPrice = item.Item.Price;
					db.OrderItems.Add(orderitem);
					db.CartItems.Remove(item);//and also removing the item each time it is created into an order item.
					db.SaveChanges();
				}
                return RedirectToAction("Details", new { id = order.Id}); //what order do we want to look at ?? 
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,City,State,ZipCode,Country,PhoneNumber,Total,OrderDate,CustomerId,OrderDetails")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
