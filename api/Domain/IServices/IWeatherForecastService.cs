using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecastDto> Get5DaysForecastAsync(string cityName, string apiKey);
    }
}
