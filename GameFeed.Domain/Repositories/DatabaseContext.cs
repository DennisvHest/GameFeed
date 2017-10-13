using System.Data.Entity;
using GameFeed.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GameFeed.Domain.Repositories {

    public class DatabaseContext : IdentityDbContext<User> {

        public DatabaseContext() : base("DatabaseContext") { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<GameCompany> GameCompanies { get; set; }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            //Change default ASPNET Identity table names
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}
