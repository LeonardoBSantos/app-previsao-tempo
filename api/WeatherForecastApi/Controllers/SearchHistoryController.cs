using Application.Model;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherForecastApi.Maps;

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
                var history = HistoryControllerMap.MapToViewModel(response);

                return Ok(history);
            }
            catch (ApplicationException apex)
            {
                _logger.LogError(apex.Message);
                return BadRequest(new ErrorModel()
                {
                    message = apex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}