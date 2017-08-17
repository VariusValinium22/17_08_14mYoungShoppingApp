using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using _17_08_14mYoungShoppingApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace _17_08_14mYoungShoppingApp

{
	internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(ApplicationDbContext context)
		{
			var roleManager = new RoleManager<IdentityRole>(
				new RoleStore<IdentityRole>(context));
			if (!context.Roles.Any(r => r.Name == "Admin"))
			{
				roleManager.Create(new IdentityRole { Name = "Admin" });
			}
			var userManager = new UserManager<ApplicationUser>(
				new UserStore<ApplicationUser>(context));
			if (!context.Users.Any(u => u.Email == "martingyoung22@gmail.com"))		//All this code(seed method) is to configuare the program we are writing
			{																		//to pre-existing external frameworks that will take our input DATA
				userManager.Create(new ApplicationUser								//and set it to pre-defined web structures like tables.
				{               
						UserName = "martingyoung22@gmail.com",
						Email = "martingyoung22@gmail.com",
						FirstName = "Martin",
						LastName = "Young",
				}, "VaginaDentata22");
			}
			var userId = userManager.FindByEmail("martingyoung22@gmail.com").Id;
			userManager.AddToRole(userId, "Admin");
		}
	}
}