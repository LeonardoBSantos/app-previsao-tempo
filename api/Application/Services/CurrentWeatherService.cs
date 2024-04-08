using Domain.DTO;
using Domain.IAdapters;
using Domain.IServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CurrentWeatherService: ICurrentWeatherService
    {
        private readonly IExternalApiRepository _externalApiRepository;
        private readonly ILogger<CurrentWeatherService> _logger;

        public CurrentWeatherService(IExternalApiRepository externalApiRepository, ILogger<CurrentWeatherService> logger)
        {
            _externalApiRepository = externalApiRepository;
            _logger = logger;
        }

        public async Task<CurrentWeatherDto> GetCurrentWeatherAsync(string cityName, string apiKey)
        {
            var geocodingApiResponse = await _externalApiRepository.GetGeocodingAsync(cityName, apiKey);
            if (geocodingApiResponse is null)
            {
                _logger.LogError($"Erro ao obter geolocalização usando cidade:{cityName}, apiKey:{apiKey}");
                throw new ApplicationException("Erro ao obter geolocalização");
            }

            var lat = geocodingApiResponse.ElementAt(0).lat.ToString();
            var lon = geocodingApiResponse.ElementAt(0).lon.ToString();
            var currentWeatherApiResponse = await _externalApiRepository.GetCurrentWeatherAsync(lat, lon, apiKey);
            if (currentWeatherApiResponse is null)
            {
                _logger.LogError($"Erro ao obter clima atual usando lat:{lat}, lon {lon}, apiKey:{apiKey}");
                throw new ApplicationException("Erro ao obter clima atual");
            }

            return new CurrentWeatherDto
            {
                cityName = geocodingApiResponse.ElementAt(0).name,
                description = currentWeatherApiResponse.weather.ElementAt(0).description,
                humidity = currentWeatherApiResponse.main.humidity,
                temp = currentWeatherApiResponse.main.temp,
                speed = currentWeatherApiResponse.wind.speed
            };
        }
    }
}
