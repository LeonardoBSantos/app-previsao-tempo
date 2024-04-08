using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Maps
{
    public static class CurrentServiceMap
    {
        public static CurrentWeatherDto MapToDto(List<GeocodingEntity>? geocodingApiResponse, CurrentWeatherEntity? currentWeatherApiResponse)
        {
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
