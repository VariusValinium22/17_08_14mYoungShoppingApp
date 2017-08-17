using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _17_08_14mYoungShoppingApp.Models
{
	public class CartItem
	{
		public string Id { get; set; }
		public int ItemId { get; set; }			//foreign key...Drop the "Id" part
		public string CustomerId { get; set; }	// a string because its a GUI created by the computer with letters and numbers and symbols 
		
		public int Count { get; set; }
		public DateTime CreationDate { get; set; }  //CART item will have one on one relationship..needs to only point to one item
													//use virtual. and point at foreign key "ItemId just above.
		public virtual Item Item { get; set; }      //allows us to grab the properties but doesn't get saved as a sub-object
		public virtual ApplicationUser Customer { get; set; }

		public decimal unitTotal
		{
			get
			{
				return Count * Item.Price;
			}
		}
	}
}