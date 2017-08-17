using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace _17_08_14mYoungShoppingApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser  //This is a user table

    {
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public string FullName
		{
			get {
				return FirstName + " " + LastName;
			}
		}

		public ApplicationUser()
		{
			this.Orders = new HashSet<Order>();
				this.CartItem = new HashSet<CartItem>();
		}

		public virtual ICollection<Order> Orders { get; set; }
		public virtual ICollection<CartItem> CartItem { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>  //We put the methods here that will build tables.When we migrate
    {																		//we want them to build tables.
																			//THis class will be instantiated on the top of controllers


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();  //build your tables below this method NOT inside of it !!!!
        }
		public DbSet<Item> Items { get; set; }		//this will allow the program create, read, update delete the language of the program
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		}
}