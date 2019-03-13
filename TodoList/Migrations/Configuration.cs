namespace TodoList.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TodoList.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TodoList.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TodoList.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            AddUsers(context);
        }

        void AddUsers(TodoList.Models.ApplicationDbContext context)
        {
            var user = new ApplicationUser { UserName = "u1@gmail.com" };
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            um.Create(user, "password");

            var user2= new ApplicationUser { UserName = "u2@gmail.com" };
            var um2 = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            um2.Create(user2, "password2");

        }
               
    }
}
