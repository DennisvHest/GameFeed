using System.Data.Entity;
using GameFeed.Domain.Entities;

namespace GameFeed.Domain.Concrete {

    public class DatabaseContext : DbContext {

        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<GameCompany> GameCompanies { get; set; }
    }
}
