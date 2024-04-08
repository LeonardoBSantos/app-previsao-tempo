using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Maps
{
    public static class ForecastServiceMap
    {
        public static WeatherForecastDto MapToDto(List<Domain.Entities.GeocodingEntity>? geocodingApiResponse, Domain.Entities.WeatherForecastEntity? weatherForecastResponse)
        {
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
