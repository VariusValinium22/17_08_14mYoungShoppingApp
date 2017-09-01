using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _17_08_14mYoungShoppingApp.Models
{
	public class Item
	{
		public int Id { get; set; }							//1. we created fields(properties)
		public DateTime CreationDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string MediaURL { get; set; }                //basically code from all other classes...don't need to be connected
		[AllowHtml]
		public string Description { get; set; }
	}
}