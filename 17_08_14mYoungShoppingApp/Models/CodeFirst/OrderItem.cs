using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _17_08_14mYoungShoppingApp.Models
{
	public class OrderItem
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int ItemId  { get; set; }
		public int Quantity  { get; set; } 
		public decimal UnitPrice  { get; set; }

		public virtual Order Order { get; set; }
		public virtual Item Item { get; set; }
	}
}

