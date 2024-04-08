using Domain.Entities;
using Domain.IAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraDataBase.Repositories
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private ApplicationDbContext _context;

        public SearchHistoryRepository()
        {
            this._context = new ApplicationDbContext();
        }

        public async Task CreateHistoryAsync(SearchHistoryEntity entity)
        {
            try
            {
                _context.searchHistoryEntity.AddAsync(entity);

                _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<SearchHistoryEntity> GetHistory()
        {
            var searchHistoryList = _context.searchHistoryEntity.ToList();
            return searchHistoryList;
        }
    }
}
