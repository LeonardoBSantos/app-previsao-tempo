using Domain.Entities;
using Domain.IAdapters;
using Microsoft.Extensions.Logging;

namespace InfraDataBase.Repositories
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private ApplicationDbContext _context;
        private ILogger<SearchHistoryRepository> _logger;

        public SearchHistoryRepository(ILogger<SearchHistoryRepository> logger)
        {
            _logger = logger;
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
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public List<SearchHistoryEntity> GetHistory()
        {
            try
            {
                var searchHistoryList = _context.searchHistoryEntity.ToList();
                return searchHistoryList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
