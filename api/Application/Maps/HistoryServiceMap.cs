using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Maps
{
    public static class HistoryServiceMap
    {
        public static List<SearchHistoryDto> MapToDto(List<SearchHistoryEntity> searchHistoryList)
        {
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
    }
}
