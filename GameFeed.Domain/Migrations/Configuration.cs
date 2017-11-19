using GameFeed.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GameFeed.Domain.Migrations {

    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<GameFeed.Domain.Repositories.DatabaseContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GameFeed.Domain.Repositories.DatabaseContext context) {
            //Identity users roles
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("admin")) {
                roleManager.Create(new IdentityRole("admin"));
            }

            User admin = new User {UserName = "DennisvHest"};

            IdentityResult result = userManager.Create(admin, "Dennieboy-1137");

            if (result.Succeeded) {
                userManager.AddToRole(admin.Id, "admin");
            }
        }
    }
}
