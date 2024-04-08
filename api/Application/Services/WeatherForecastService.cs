using Application.Maps;
using Domain.DTO;
using Domain.IAdapters;
using Domain.IServices;

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

            return ForecastServiceMap.MapToDto(geocodingApiResponse, weatherForecastResponse);
        }
    }
}
