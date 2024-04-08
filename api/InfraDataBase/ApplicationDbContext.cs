using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
