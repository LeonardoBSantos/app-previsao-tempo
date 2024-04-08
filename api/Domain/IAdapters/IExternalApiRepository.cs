using Domain.Entities;

namespace Domain.IAdapters
{
    public interface IExternalApiRepository
    {
        Task<List<GeocodingEntity>> GetGeocodingAsync(string cityName, string apiKey);

        Task<CurrentWeatherEntity> GetCurrentWeatherAsync(string lat, string lon, string apikey);

        Task<WeatherForecastEntity> Get5DaysWeatherForecastAsync(string lat, string lon, string apikey);
    }
}
