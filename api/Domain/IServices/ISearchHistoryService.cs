using Domain.DTO;

namespace Domain.IServices
{
    public interface ISearchHistoryService
    {
        List<SearchHistoryDto> GetHistory();

        Task CreateHistoryAsync(string cityName);
    }
}
