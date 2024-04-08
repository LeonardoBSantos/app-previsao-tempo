using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfraDataBase
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<SearchHistoryEntity> searchHistoryEntity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "History");
        }

    }
}
