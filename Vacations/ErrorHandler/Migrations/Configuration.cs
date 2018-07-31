using System.Collections.Generic;
using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Vacations.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //Add-Migration InitialMigrations -IgnoreChanges
            //This should generate a blank "InitialMigration" file.Now, add any desired 
            //changes to the class you want.Once changes are added, run the update command again:
            //update-database -verbose
            //Now the automatic migration will be applied and the table will be altered with your changes.

            // Initialize default identity roles
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            // RoleTypes is a class containing constant string values for different roles

            var identityRoles = new List<IdentityRole>
            {
                new IdentityRole() {Name = "Administrator"},
                new IdentityRole() {Name = "TeamLeader"},
                new IdentityRole() {Name = "Employee"}
            };

            foreach (var role in identityRoles)
            {
                manager.Create(role);
            }

            // Initialize default user
            var admin = new ApplicationUser
            {
                Email = "admin@admin.com",
                UserName = "admin@admin.com"
            };

            context.Users.AddOrUpdate(u => u.UserName, admin);

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            roleManager.Create(new IdentityRole("Administrator"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            userManager.Create(admin, "admin321");
            userManager.AddToRole(admin.Id, "Administrator");
        }
    }
}
