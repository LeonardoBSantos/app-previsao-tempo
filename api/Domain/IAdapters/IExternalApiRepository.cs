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
        Task<List<GeocodingEntity>> GetGeocoding(string cityName, string apiKey);

        Task<CurrentWeather> GetCurrentWeather(string lat, string lon, string apikey);

        void Get5DaysWeatherForecast();
    }
}
