using System.Data.Entity;
using GameFeed.Domain.Entities;

namespace GameFeed.Domain.Concrete {

    public class DatabaseContext : DbContext {

        public DbSet<Game> Games { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
