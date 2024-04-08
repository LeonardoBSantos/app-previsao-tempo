using Moq;
using Microsoft.Extensions.Logging;
using Domain.IAdapters;
using Domain.Entities;
using Application.Services;

namespace WeatherForecastTests.ServiceTests
{
    public class CurrentWeatherServiceTests
    {
        private readonly Mock<ILogger<CurrentWeatherService>> _loggerMock;
        private readonly Mock<IExternalApiRepository> _externalApiRepositoryMock;
        private readonly CurrentWeatherService service;

        public CurrentWeatherServiceTests()
        {
            _loggerMock = new Mock<ILogger<CurrentWeatherService>>();
            _externalApiRepositoryMock = new Mock<IExternalApiRepository>();
            service = new CurrentWeatherService(_externalApiRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetCurrentWeatherAsync_Returns_CurrentWeatherDto()
        {
            // Arrange
            var geocodingApiResponse = new List<GeocodingEntity>
            {
                new GeocodingEntity { lat = 51.5074, lon = 0.1278, name = "London" }
            };

            var currentWeatherApiResponse = new CurrentWeatherEntity
            {
                weather = new List<Weather> { new Weather { description = "Nublado" } },
                main = new Main { humidity = 70, temp = 15 },
                wind = new Wind { speed = 5 }
            };

            _externalApiRepositoryMock.Setup(repo => repo.GetGeocodingAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(geocodingApiResponse);

            _externalApiRepositoryMock.Setup(repo => repo.GetCurrentWeatherAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(currentWeatherApiResponse);

            // Act
            var result = await service.GetCurrentWeatherAsync(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.Equal("London", result.cityName);
            Assert.Equal("Nublado", result.description);
            Assert.Equal(70, result.humidity);
            Assert.Equal(15, result.temp);
            Assert.Equal(5, result.speed);
        }

        [Fact]
        public async Task GetCurrentWeatherAsync_Throws_Exception_If_Geocoding_Api_Fails()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => service.GetCurrentWeatherAsync(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async Task GetCurrentWeatherAsync_Throws_Exception_If_CurrentWeather_Api_Fails()
        {
            // Arrange

            var geocodingApiResponse = new List<GeocodingEntity>
            {
                new GeocodingEntity { lat = 51.5074, lon = 0.1278, name = "London" }
            };

            _externalApiRepositoryMock.Setup(repo => repo.GetGeocodingAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(geocodingApiResponse);

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => service.GetCurrentWeatherAsync(It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}



    





