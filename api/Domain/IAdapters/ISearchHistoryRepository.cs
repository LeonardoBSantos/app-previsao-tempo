using Domain.Entities;

namespace Domain.IAdapters
{
    public interface ISearchHistoryRepository
    {
        Task CreateHistoryAsync(SearchHistoryEntity entity);
        List<SearchHistoryEntity> GetHistory();
    }
}
