using Application.Services;
using Domain.Entities;
using Domain.IAdapters;
using Microsoft.Extensions.Logging;
using Moq;

namespace WeatherForecastTests.ServiceTests
{
    public class SearchHistoryServiceTests
    {
        private readonly Mock<ILogger<SearchHistoryService>> _loggerMock;
        private readonly Mock<ISearchHistoryRepository> _searchHistoryRepository;
        private readonly SearchHistoryService _searchHistoryService;

        public SearchHistoryServiceTests()
        {
            _loggerMock = new Mock<ILogger<SearchHistoryService>>();
            _searchHistoryRepository = new Mock<ISearchHistoryRepository>();
            _searchHistoryService = new SearchHistoryService(_searchHistoryRepository.Object);
        }

        [Fact]
        public void GetHistory_Returns_SearchHistoryDto()
        {
            // Arrange
            var searchHistoryResponse = new List<SearchHistoryEntity>
            {
                new SearchHistoryEntity { city_name = "santos", timestamp = "" }
            };

            _searchHistoryRepository.Setup(repo => repo.GetHistory()).Returns(searchHistoryResponse);

            // Act
            var result = _searchHistoryService.GetHistory();

            // Assert
            Assert.NotNull(result);
        }
    }
}
