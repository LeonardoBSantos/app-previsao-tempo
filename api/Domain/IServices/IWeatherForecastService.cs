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
        WeatherForecastDto Get5DaysForecast(string cityName, string apiKey);
    }
}
