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

        public async Task CreateHistory(SearchHistoryEntity entity)
        {
            try
            {
                _context.searchHistoryEntity.Add(entity);

                _context.SaveChanges();
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
