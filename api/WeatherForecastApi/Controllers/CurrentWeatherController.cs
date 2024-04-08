using Application.Model;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using WeatherForecastApi.Maps;
using WeatherForecastApi.Utils;

namespace WeatherForecastApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrentWeatherController : ControllerBase
    {
        private const string Endpoint_NAME = "current";
        private readonly ILogger<CurrentWeatherController> _logger;
        private readonly ICurrentWeatherService _currentWeatherService;
        private readonly ICacheService<CurrentWeatherModel> _cacheService;
        private readonly ISearchHistoryService _searchHistoryService;

        public CurrentWeatherController(
            ILogger<CurrentWeatherController> logger, 
            ICurrentWeatherService currentWeatherService,
            ICacheService<CurrentWeatherModel> cacheService,
            ISearchHistoryService searchHistoryService)
        {
            _logger = logger;
            _currentWeatherService = currentWeatherService;
            _cacheService = cacheService;
            _searchHistoryService = searchHistoryService;
        }

        [HttpGet("/current")]
        public async Task<IActionResult> GetCurrentWeather([FromQuery] string cityName, [FromQuery] string apiKey)
        {
            try
            {
                EntryPointValidations.ValidateCityName(cityName);
                _searchHistoryService.CreateHistoryAsync(cityName);

                var currentWeatherCache = await _cacheService.ReadCacheAsync(cityName, Endpoint_NAME);
                if (currentWeatherCache is null)
                {
                    _logger.LogInformation("Clima atual obtido por consumo de API");
                    var response = await _currentWeatherService.GetCurrentWeatherAsync(cityName, apiKey);
                    var currentWeather = CurrentControllerMap.mapToViewModel(response);
                    _cacheService.WriteCacheAsync(cityName, JsonConvert.SerializeObject(currentWeather), Endpoint_NAME);

                    return Ok(currentWeather);
                }
                else
                {
                    _logger.LogInformation("Clima atual obtido do Cache");
                    return Ok(currentWeatherCache);
                }

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