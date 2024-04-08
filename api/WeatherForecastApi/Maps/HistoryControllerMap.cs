using Application.Model;
using Domain.DTO;

namespace WeatherForecastApi.Maps
{
    public static class HistoryControllerMap
    {
        public static List<SearchHistoryModel> MapToViewModel(List<SearchHistoryDto> response)
        {
            var historyViewModel = new List<SearchHistoryModel>();

            foreach (var item in response)
            {
                historyViewModel.Add(
                    new SearchHistoryModel()
                    {
                        cidade = item.city_name,
                        data = item.timestamp
                    });
            }

            return historyViewModel;
        }
    }
}
