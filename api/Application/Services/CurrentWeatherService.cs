﻿using Domain.DTO;
using Domain.IAdapters;
using Domain.IServices;
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

        public CurrentWeatherService(IExternalApiRepository externalApiRepository)
        {
            _externalApiRepository = externalApiRepository;
        }

        public CurrentWeatherDto GetCurrentWeather(string cityName, string apiKey)
        {
            var geocodingApiResponse = _externalApiRepository.GetGeocoding(cityName, apiKey);
            
            var lat = geocodingApiResponse.Result.ElementAt(0).lat.ToString();
            var lon = geocodingApiResponse.Result.ElementAt(0).lon.ToString();

            var currentWeatherApiResponse = _externalApiRepository.GetCurrentWeather(lat, lon, apiKey);
            return new CurrentWeatherDto
            {
                description = currentWeatherApiResponse.Result.weather.ElementAt(0).description,
                humidity = currentWeatherApiResponse.Result.main.humidity,
                temp = currentWeatherApiResponse.Result.main.temp,
                speed = currentWeatherApiResponse.Result.wind.speed
            };
        }
    }
}