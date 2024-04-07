using Application.Model;
using Domain.DTO;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;

namespace WeatherForecastApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchHistoryController : ControllerBase
    {
        private const string Endpoint_NAME = "history";
        private readonly ILogger<SearchHistoryController> _logger;
        private readonly ISearchHistoryService _searchHistoryService;

        public SearchHistoryController(
            ILogger<SearchHistoryController> logger, 
            ISearchHistoryService searchHistoryService)
        {
            _logger = logger;
            _searchHistoryService = searchHistoryService;
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
            catch (ApplicationException apex)
            {
                _logger.LogError(apex.Message);
                return BadRequest(new ErrorModel()
                {
                    Message = apex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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