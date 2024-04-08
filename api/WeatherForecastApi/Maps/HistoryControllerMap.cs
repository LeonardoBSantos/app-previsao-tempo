using Application.Model;
using Domain.DTO;

namespace WeatherForecastApi.Maps
{
    public static class HistoryControllerMap
    {
        public static SearchHistoryModel MapToViewModel(List<SearchHistoryDto> response)
        {
            var hitoryList = new List<HistoryModel>();
            foreach (var item in response)
            {
                hitoryList.Add(
                    new HistoryModel()
                    {
                        cidade = item.city_name,
                        data = item.timestamp
                    });
            }

            var historyViewModel = new SearchHistoryModel
            {
                lista_historico = hitoryList
            };

            return historyViewModel;
        }
    }
}
