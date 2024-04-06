using Application.Model;
using Domain.DTO;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace WeatherForecastApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchHistoryController : ControllerBase
    {
        private const string Endpoint_NAME = "history";
        private readonly ILogger<CurrentWeatherController> _logger;
        private readonly ISearchHistoryService _searchHistoryService;
        private readonly ICacheService<CurrentWeatherModel> _cacheService;

        public SearchHistoryController(
            ILogger<CurrentWeatherController> logger, 
            ISearchHistoryService searchHistoryService,
            ICacheService<CurrentWeatherModel> cacheService)
        {
            _logger = logger;
            _searchHistoryService = searchHistoryService;
            _cacheService = cacheService;
        }

        [HttpGet("/search_history")]
        public IActionResult GetSearchHistory()
        {
            try
            {
                var response = _searchHistoryService.GetHistory();
                List<SearchHistoryModel> history = MapToSearchHistoryViewModel(response);

                return Ok(history);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private List<SearchHistoryModel> MapToSearchHistoryViewModel(List<SearchHistoryDto> response)
        {
            var historyViewModel = new List<SearchHistoryModel>();

            foreach (var item in response)
            {
                historyViewModel.Add(
                    new SearchHistoryModel()
                    {
                        Cidade = item.city_name,
                        Data = item.timestamp
                    });
            }

            return historyViewModel;
        }
    }
}