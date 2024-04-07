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

        public async Task<WeatherForecastDto> Get5DaysForecastAsync(string cityName, string apiKey)
        {
            var geocodingApiResponse = await _externalApiRepository.GetGeocodingAsync(cityName, apiKey);

            var lat = geocodingApiResponse.ElementAt(0).lat.ToString();
            var lon = geocodingApiResponse.ElementAt(0).lon.ToString();

            var weatherForecastResponse = await _externalApiRepository.Get5DaysWeatherForecastAsync(lat, lon, apiKey);

            var listOf5DaysForecast = new WeatherForecastDto();
            listOf5DaysForecast.city_name = geocodingApiResponse.ElementAt(0).name;
            listOf5DaysForecast.list = new List<ListData>();

            foreach (var weatherForecast in weatherForecastResponse.list)
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
