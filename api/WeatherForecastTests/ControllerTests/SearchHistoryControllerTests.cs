using Application.Model;
using Domain.DTO;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherForecastApi.Controllers;

namespace WeatherForecastTests.ControllerTests
{
    public class SearchHistoryControllerTests
    {
        private readonly SearchHistoryController _searchHistoryController;
        private readonly Mock<ILogger<SearchHistoryController>> _loggerMock;
        private readonly Mock<ISearchHistoryService> _searchHistoryServiceMock;

        public SearchHistoryControllerTests()
        {
            _searchHistoryServiceMock = new Mock<ISearchHistoryService>();
            _loggerMock = new Mock<ILogger<SearchHistoryController>>();
            _searchHistoryController = new SearchHistoryController(_loggerMock.Object, _searchHistoryServiceMock.Object);
        }

        [Fact]
        public void GetHistory_ReturnsOk()
        {
            // Arrange
            var expectedJsonResponse = new List<SearchHistoryDto>()
            {
                new SearchHistoryDto()
                {
                    city_name = "London",
                    timestamp = DateTimeOffset.Now.ToString()
                }
            };

            var expectedViewModel = new SearchHistoryModel()
            {
                lista_historico = new List<HistoryModel>()
                {
                    new HistoryModel
                    {
                        cidade = "London",
                        data = DateTimeOffset.Now.ToString()
                    }
                    
                }
            };

            _searchHistoryServiceMock.Setup(x => x.GetHistory()).Returns(expectedJsonResponse);

            // Act
            var result = _searchHistoryController.GetSearchHistory();

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var view = Assert.IsType<SearchHistoryModel>(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedViewModel.lista_historico.ElementAt(0).cidade, view.lista_historico.ElementAt(0).cidade);
            Assert.Equal(expectedViewModel.lista_historico.ElementAt(0).data, view.lista_historico.ElementAt(0).data);

        }

        [Fact]
        public void GetHistory_ReturnsError_When_ExceptionOccurs()
        {
            // Arrange
            var expectedException = new Exception("Erro ao obter histórico");
            var expectedResponse = new ObjectResult(new ErrorModel()
            {
                message = expectedException.Message
            });

            _searchHistoryServiceMock.Setup(x => x.GetHistory()).Throws(expectedException);

            // Act
            var result = _searchHistoryController.GetSearchHistory();

            // Assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
