using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfraCacheDataBase
{
    public class CacheDbContext : DbContext
    {
        public DbSet<CurrentCacheEntity> current { get; set; }
        public DbSet<ForecastCacheEntity> forecast { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Cache");
        }

    }
}
