using Application.Model;
using Application.Services;
using Domain.DTO;
using Domain.Entities;
using Domain.IAdapters;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastApi.Controllers;

namespace WeatherForecastTests.ServiceTests
{
    public class WeatherForecastServiceTests
    {
        private readonly Mock<ILogger<WeatherForecastService>> _loggerMock;
        private readonly Mock<IExternalApiRepository> _externalApiRepositoryMock;
        private readonly WeatherForecastService _weatherForecastService;

        public WeatherForecastServiceTests()
        {
            _loggerMock = new Mock<ILogger<WeatherForecastService>>();
            _externalApiRepositoryMock = new Mock<IExternalApiRepository>();
            _weatherForecastService = new WeatherForecastService(_externalApiRepositoryMock.Object);
        }

        [Fact]
        public async Task GetForecastWeatherAsync_Returns_ForecastWeatherDto()
        {
            // Arrange
            var geocodingApiResponse = new List<GeocodingEntity>
            {
                new GeocodingEntity { lat = 51.5074, lon = 0.1278, name = "London" }
            };

            var forecastWeatherApiResponse = new WeatherForecastEntity
            {
                cnt = 1,
                cod = "",
                message = 200,
                list = new List<Domain.Entities.ListData>
                {
                    new Domain.Entities.ListData
                    {
                        dt = 1,
                        main = new MainData
                        {
                            temp = 21.35,
                            feels_like = 21.71,
                            temp_min = 20.59,
                            temp_max = 21.35,
                            pressure = 1021,
                            sea_level = 1021,
                            grnd_level = 927,
                            humidity = 83,
                            temp_kf = 0.76
                        },
                        weather = new List<WeatherData>
                        {
                            new WeatherData
                            {
                                description = "broken clouds"
                            }
                        },
                        wind = new WindData
                        {
                            speed = 2.25
                        },
                        dt_txt = ""
                    }
                }
            };

            _externalApiRepositoryMock.Setup(repo => repo.GetGeocodingAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(geocodingApiResponse);

            _externalApiRepositoryMock.Setup(repo => repo.Get5DaysWeatherForecastAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(forecastWeatherApiResponse);

            // Act
            var result = await _weatherForecastService.Get5DaysForecastAsync(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetForecastWeatherAsync_Throws_Exception_If_Geocoding_Api_Fails()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => _weatherForecastService.Get5DaysForecastAsync(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async Task GetForecastWeatherAsync_Throws_Exception_If_ForecastWeather_Api_Fails()
        {
            // Arrange

            var geocodingApiResponse = new List<GeocodingEntity>
            {
                new GeocodingEntity { lat = 51.5074, lon = 0.1278, name = "London" }
            };

            _externalApiRepositoryMock.Setup(repo => repo.GetGeocodingAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(geocodingApiResponse);

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => _weatherForecastService.Get5DaysForecastAsync(It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
