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

        public WeatherForecastService(IExternalApiRepository externalApiRepository)
        {
            _externalApiRepository = externalApiRepository;
        }

        public async Task<WeatherForecastDto> Get5DaysForecastAsync(string cityName, string apiKey)
        {
            var geocodingApiResponse = await _externalApiRepository.GetGeocodingAsync(cityName, apiKey);
            if (geocodingApiResponse is null)
            {
                throw new ApplicationException("Erro ao obter geolocalização");
            }

            var lat = geocodingApiResponse.ElementAt(0).lat.ToString();
            var lon = geocodingApiResponse.ElementAt(0).lon.ToString();

            var weatherForecastResponse = await _externalApiRepository.Get5DaysWeatherForecastAsync(lat, lon, apiKey);
            if (weatherForecastResponse is null)
            {
                throw new ApplicationException("Erro ao obter Previsão do clima");
            }

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
