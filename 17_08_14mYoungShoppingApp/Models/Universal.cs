using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace _17_08_14mYoungShoppingApp.Models
{
	public class Universal : Controller
	{
		public ApplicationDbContext db = new ApplicationDbContext();

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (User.Identity.IsAuthenticated)  //Check to see: is the user logged in??
			{
				var user = db.Users.Find(User.Identity.GetUserId());
				ViewBag.FirstName = user.FirstName;
				ViewBag.LastName = user.LastName;
				ViewBag.FullName = user.FullName;
				ViewBag.CartItems = db.CartItems.Where(c => c.CustomerId == user.Id).ToList();//when checking the db to compare Customer to user id you'll leave no tracking

				decimal Total = 0;
				foreach (var item in db.CartItems.Include("Item").Where(c => c.CustomerId == user.Id).ToList())
				{
					Total += item.Count * item.Item.Price;//two items because it's an item that's an object id that has a price.
				}
				ViewBag.CartTotal = Total;

				base.OnActionExecuted(filterContext);
			}

		}
	}
}