using Application.Model;
using Domain.DTO;
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

namespace WeatherForecastTests.ControllerTests
{
    public class WeatherForecastControllerTets
    {
        private readonly ForecastController _weatherForecastController;
        private readonly Mock<IWeatherForecastService> _weatherForecastServiceMock;
        private readonly Mock<ISearchHistoryService> _searchHistoryServiceMock;
        private readonly Mock<ICacheService<WeatherForecastModel>> _cacheServiceMock;
        private readonly Mock<ILogger<ForecastController>> _loggerMock;

        public WeatherForecastControllerTets()
        {
            _weatherForecastServiceMock = new Mock<IWeatherForecastService>();
            _searchHistoryServiceMock = new Mock<ISearchHistoryService>();
            _cacheServiceMock = new Mock<ICacheService<WeatherForecastModel>>();
            _loggerMock = new Mock<ILogger<ForecastController>>();
            _weatherForecastController = new ForecastController(_loggerMock.Object, _weatherForecastServiceMock.Object, _searchHistoryServiceMock.Object, _cacheServiceMock.Object);
        }

        [Fact]
        public void GetWeatherForecast_ReturnsOk_When_CacheIsEmpty()
        {
            // Arrange
            string cityName = "London";
            string apiKey = "key";
            var expectedJsonResponse = new WeatherForecastDto()
            {
                list = new List<ListData>()
                {
                    new ListData()
                    {
                        description = "nublado",
                        humidity = 14,
                        speed = 5.0,
                        temp = 21.5,
                        dt_txt = DateTimeOffset.Now.ToString()
                    }
                }
            };
            var expectedViewModel = new WeatherForecastModel()
            {
                listaDePrevisoes = new List<Previsao>()
                {
                    new Previsao()
                    {
                        descricao = "nublado",
                        umidade = 14,
                        velocidade_do_vento = 5.0,
                        temperatura = 21.5,
                        data = DateTimeOffset.Now.ToString()
                    }
                }
            };

            _weatherForecastServiceMock.Setup(x => x.Get5DaysForecastAsync(cityName, apiKey)).ReturnsAsync(expectedJsonResponse);

            // Act
            var result = _weatherForecastController.Get5DaysWeatherForecast(cityName, apiKey);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var view = Assert.IsType<WeatherForecastModel>(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedViewModel.listaDePrevisoes.ElementAt(0).descricao, view.listaDePrevisoes.ElementAt(0).descricao);
            Assert.Equal(expectedViewModel.listaDePrevisoes.ElementAt(0).umidade, view.listaDePrevisoes.ElementAt(0).umidade);
            Assert.Equal(expectedViewModel.listaDePrevisoes.ElementAt(0).velocidade_do_vento, view.listaDePrevisoes.ElementAt(0).velocidade_do_vento);
            Assert.Equal(expectedViewModel.listaDePrevisoes.ElementAt(0).temperatura, view.listaDePrevisoes.ElementAt(0).temperatura);
            Assert.Equal(expectedViewModel.listaDePrevisoes.ElementAt(0).data, view.listaDePrevisoes.ElementAt(0).data);

        }

        [Fact]
        public void GetWeatherForecast_ReturnsOk_When_CacheIsNotEmpty()
        {
            // Arrange
            string cityName = "London";
            string apiKey = "key";
            var cachedJsonResponse = new WeatherForecastModel()
            {
                listaDePrevisoes = new List<Previsao>()
                {
                    new Previsao()
                    {
                        descricao = "nublado",
                        umidade = 14,
                        velocidade_do_vento = 5.0,
                        temperatura = 21.5,
                        data = DateTimeOffset.Now.ToString()
                    }
                }
            };

            _cacheServiceMock.Setup(x => x.ReadCacheAsync(cityName, It.IsAny<string>())).ReturnsAsync(cachedJsonResponse);

            // Act
            var result = _weatherForecastController.Get5DaysWeatherForecast(cityName, apiKey);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetWeatherForecast_ReturnsBadRequest_When_ExceptionOccurs()
        {
            // Arrange
            string cityName = ".";
            string apiKey = "key";
            
            // Act
            var result = _weatherForecastController.Get5DaysWeatherForecast(cityName, apiKey);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }
    }
}
