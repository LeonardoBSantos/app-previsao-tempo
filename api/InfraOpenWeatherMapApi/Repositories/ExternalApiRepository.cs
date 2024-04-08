using Domain.Entities;
using Domain.IAdapters;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace InfraExternalApi.Repositories
{
    public class ExternalApiRepository: IExternalApiRepository
    {
        private readonly ILogger<ExternalApiRepository> _logger;
        static HttpClient client = new HttpClient();

        public ExternalApiRepository(ILogger<ExternalApiRepository> logger)
        {
            _logger = logger;
            HttpClientInstanceInit();
        }

        public async Task<List<GeocodingEntity>>? GetGeocodingAsync(string cityName, string apiKey)
        {
            try
            {
                List<GeocodingEntity> geocodingList = null;
                HttpResponseMessage response = await client.GetAsync($"geo/1.0/direct?q={cityName}&limit=1&appid={apiKey}");
                if (response.IsSuccessStatusCode)
                {
                    geocodingList = await response.Content.ReadAsAsync<List<GeocodingEntity>>();
                }
                return geocodingList;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
           
        }

        public async Task<CurrentWeatherEntity>? GetCurrentWeatherAsync(string lat, string lon, string apiKey)
        {
            try
            {
                CurrentWeatherEntity currentWeather = null;
                HttpResponseMessage response = await client.GetAsync($"data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric&lang=pt_br");
                if (response.IsSuccessStatusCode)
                {
                    currentWeather = await response.Content.ReadAsAsync<CurrentWeatherEntity>();
                }
                return currentWeather;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<WeatherForecastEntity>? Get5DaysWeatherForecastAsync(string lat, string lon, string apiKey)
        {
            try
            {
                WeatherForecastEntity weatherForecast = null;
                HttpResponseMessage response = await client.GetAsync($"data/2.5/forecast?lat={lat}&lon={lon}&appid={apiKey}&units=metric&lang=pt_br");
                if (response.IsSuccessStatusCode)
                {
                    weatherForecast = await response.Content.ReadAsAsync<WeatherForecastEntity>();
                }
                return weatherForecast;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        private async static Task HttpClientInstanceInit()
        {
            client.BaseAddress = new Uri("http://api.openweathermap.org/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
