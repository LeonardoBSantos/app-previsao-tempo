using Domain.IServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastApi.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Application.Model;
using Microsoft.Extensions.Logging;
using Domain.DTO;

namespace WeatherForecastTests.ControllerTests
{
    public class CurrentWeatherControllerTets
    {
        private readonly CurrentWeatherController _currentWeatherController;
        private readonly Mock<ICurrentWeatherService> _currentWeatherServiceMock;
        private readonly Mock<ISearchHistoryService> _searchHistoryServiceMock;
        private readonly Mock<ICacheService<CurrentWeatherModel>> _cacheServiceMock;
        private readonly Mock<ILogger<CurrentWeatherController>> _loggerMock;

        public CurrentWeatherControllerTets()
        {
            _currentWeatherServiceMock = new Mock<ICurrentWeatherService>();
            _searchHistoryServiceMock = new Mock<ISearchHistoryService>();
            _cacheServiceMock = new Mock<ICacheService<CurrentWeatherModel>>();
            _loggerMock = new Mock<ILogger<CurrentWeatherController>>();
            _currentWeatherController = new CurrentWeatherController(_loggerMock.Object, _currentWeatherServiceMock.Object, _cacheServiceMock.Object, _searchHistoryServiceMock.Object);
        }

        [Fact]
        public void GetCurrentWeather_ReturnsOk_When_CacheIsEmpty()
        {
            // Arrange
            string cityName = "London";
            string apiKey = "key";
            var expectedJsonResponse = new CurrentWeatherDto()
            {
                description = "nublado",
                humidity = 14,
                speed = 5.0,
                temp = 21.5
            };
            var expectedViewModel = new CurrentWeatherModel()
            {
                descricao = "nublado",
                umidade = 14,
                velocidade_do_vento = 5.0,
                temperatura = 21.5
            } ;

            _currentWeatherServiceMock.Setup(x => x.GetCurrentWeatherAsync(cityName, apiKey)).ReturnsAsync(expectedJsonResponse);

            // Act
            var result = _currentWeatherController.GetCurrentWeather(cityName, apiKey);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var view = Assert.IsType<CurrentWeatherModel>(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedViewModel.descricao, view.descricao);
            Assert.Equal(expectedViewModel.umidade, view.umidade);
            Assert.Equal(expectedViewModel.velocidade_do_vento, view.velocidade_do_vento);
            Assert.Equal(expectedViewModel.temperatura, view.temperatura);
            
        }

        [Fact]
        public void GetCurrentWeather_ReturnsOk_When_CacheIsNotEmpty()
        {
            // Arrange
            string cityName = "London";
            string apiKey = "key";
            var cachedJsonResponse = new CurrentWeatherModel()
            {
                descricao = "nublado",
                umidade = 14,
                velocidade_do_vento = 5.0,
                temperatura = 21.5
            };

            _cacheServiceMock.Setup(x => x.ReadCacheAsync(cityName, It.IsAny<string>())).ReturnsAsync(cachedJsonResponse);

            // Act
            var result = _currentWeatherController.GetCurrentWeather(cityName, apiKey);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        //[Fact]
        //public void GetCurrentWeather_ReturnsBadRequest_When_ExceptionOccurs()
        //{
        //    // Arrange
        //    string cityName = "InvalidCityName";
        //    string apiKey = "your-api-key";
        //    var expectedException = new Exception("Invalid city name provided.");

        //    _cacheServiceMock.Setup(x => x.ReadCache(cityName, It.IsAny<string>())).Throws(expectedException);

        //    // Act
        //    var result = _currentWeatherController.GetCurrentWeather(cityName, apiKey);

        //    // Assert
        //    Assert.IsType<BadRequestObjectResult>(result);
        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        //    Assert.Equal(400, badRequestResult.StatusCode);
        //    Assert.Equal(expectedException.Message, badRequestResult.Value);
        //}
    }
}





