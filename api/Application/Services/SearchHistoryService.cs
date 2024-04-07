using Domain.DTO;
using Domain.Entities;
using Domain.IAdapters;
using Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var historyDto = new List<SearchHistoryDto>();
            foreach (var searchHistory in searchHistoryList)
            {
                historyDto.Add(
                    new SearchHistoryDto()
                    {
                        city_name = searchHistory.city_name,
                        timestamp = searchHistory.timestamp
                    });
            }

            return historyDto;
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
