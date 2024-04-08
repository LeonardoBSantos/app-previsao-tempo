using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
