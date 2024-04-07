using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IAdapters
{
    public interface IExternalApiRepository
    {
        Task<List<GeocodingEntity>> GetGeocodingAsync(string cityName, string apiKey);

        Task<CurrentWeatherEntity> GetCurrentWeatherAsync(string lat, string lon, string apikey);

        Task<WeatherForecastEntity> Get5DaysWeatherForecastAsync(string lat, string lon, string apikey);
    }
}
