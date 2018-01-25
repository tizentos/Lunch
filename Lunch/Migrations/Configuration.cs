namespace Lunch.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Lunch.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Lunch.Models.ApplicationDbContext";
        }

        protected override void Seed(Lunch.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));  //creating a UserManger from ASP.NET Identity system 
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));    //Create a role manager to assign roles to users
            string name = "admin";
            string password = "Boku@12345";
            if (!RoleManager.RoleExists(name))
            {
                var roleResult = RoleManager.Create(new IdentityRole(name));
            }

            var user = new ApplicationUser();
            user.UserName = name;
            user.Email = "tizentos@gmail.com";
            user.EmailConfirmed = true;
            var adminresult = UserManager.Create(user, password);

            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, name);
            }
            base.Seed(context);
        }
    }
}
