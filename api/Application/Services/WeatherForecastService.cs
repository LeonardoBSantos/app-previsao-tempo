using Domain.DTO;
using Domain.IAdapters;
using Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IExternalApiRepository _externalApiRepository;
        private readonly ISearchHistoryService _searchHistoryService;

        public WeatherForecastService(IExternalApiRepository externalApiRepository, ISearchHistoryService searchHistoryService)
        {
            _externalApiRepository = externalApiRepository;
            _searchHistoryService = searchHistoryService;
        }

        public WeatherForecastDto Get5DaysForecast(string cityName, string apiKey)
        {
            _searchHistoryService.CreateHistory(cityName);
            var geocodingApiResponse = _externalApiRepository.GetGeocoding(cityName, apiKey);

            var lat = geocodingApiResponse.Result.ElementAt(0).lat.ToString();
            var lon = geocodingApiResponse.Result.ElementAt(0).lon.ToString();

            var weatherForecastResponse = _externalApiRepository.Get5DaysWeatherForecast(lat, lon, apiKey);

            var listOf5DaysForecast = new WeatherForecastDto();
            listOf5DaysForecast.list = new List<ListData>();

            foreach (var weatherForecast in weatherForecastResponse.Result.list)

            {
                listOf5DaysForecast.list.Add(
                    new ListData
                    {
                        description = weatherForecast.weather.ElementAt(0).description,
                        humidity = weatherForecast.main.humidity,
                        temp = weatherForecast.main.temp,
                        speed = weatherForecast.wind.speed,
                        dt_txt = weatherForecast.dt_txt
                    }); 
            }

            return listOf5DaysForecast;
        }
    }
}
