using Application.Maps;
using Domain.DTO;
using Domain.Entities;
using Domain.IAdapters;
using Domain.IServices;

namespace Application.Services
{
    public class SearchHistoryService : ISearchHistoryService
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;

        public SearchHistoryService(ISearchHistoryRepository searchHistoryRepository)
        {
            _searchHistoryRepository = searchHistoryRepository;
        }

        public List<SearchHistoryDto> GetHistory()
        {
            var searchHistoryList = _searchHistoryRepository.GetHistory();

            return HistoryServiceMap.MapToDto(searchHistoryList);
        }

        public async Task CreateHistoryAsync(string cityName)
        {
            var entity = new SearchHistoryEntity()
            {
                city_name = cityName,
                timestamp = DateTimeOffset.Now.ToString()
            };

            _searchHistoryRepository.CreateHistoryAsync(entity);
        }
    }
}
