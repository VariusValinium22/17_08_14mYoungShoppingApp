using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _17_08_14mYoungShoppingApp.Models
{
	public class Order
	{
		public int Id { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public int ZipCode { get; set; }
		public string Country { get; set; }
		public int PhoneNumber { get; set; }
		public decimal Total { get; set; }
		public DateTime OrderDate { get; set; }
		public int CustomerId { get; set; }
		public string OrderDetails { get; set; }

		public virtual ApplicationUser Customer { get; set; }
		public virtual ICollection<OrderItem> OrderItems { get; set; }	//Here you'll be bringing in multiple items
	}
}