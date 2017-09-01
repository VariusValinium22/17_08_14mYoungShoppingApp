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
    public class CartItemsController : Universal
    {
		// GET: CartItems
		[Authorize]
        public ActionResult Index()
        {
			var user = db.Users.Find(User.Identity.GetUserId());
            //var cartItems = db.CartItems.Include(c => c.Customer).Include(c => c.Item);
            return View(user.CartItems.ToList());
		}

		//private new ActionResult View(object p)
		//{
			//throw new NotImplementedException();
		//}

		// GET: CartItems/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // GET: CartItems/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name");
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? itemId)
        {
			var user = db.Users.Find(User.Identity.GetUserId());
            if (user != null || itemId != null)
            {
				if (user.CartItems.Any(c => c.ItemId == itemId))
				{
					var existingCartItem = user.CartItems.FirstOrDefault(c => c.ItemId == itemId);
					existingCartItem.Count += 1;
					db.SaveChanges();
				}
				else
				{
					CartItem cartItem = new CartItem();// this actually adds the items to the cart
					cartItem.ItemId = (int)itemId;
					cartItem.Count = 1;
					cartItem.CreationDate = DateTime.Now;
					cartItem.CustomerId = user.Id;
					db.CartItems.Add(cartItem);
					db.SaveChanges();
				}
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: CartItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "FirstName", cartItem.CustomerId);
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", cartItem.ItemId);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemId,CustomerId,Count,CreationDate")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "FirstName", cartItem.CustomerId);
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", cartItem.ItemId);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItem cartItem = db.CartItems.Find(id);
            db.CartItems.Remove(cartItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)//closes all open connections to databases, when we're done with our
														// controllers, all databases are closed.
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
