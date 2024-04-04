using Domain.IServices;
using Microsoft.AspNetCore.Mvc;

namespace WeatherForecastApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICurrentWeatherService _currentWeatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICurrentWeatherService currentWeatherService)
        {
            _logger = logger;
            _currentWeatherService = currentWeatherService;
        }

        [HttpGet("/current")]
        public IActionResult GetCurrentWeather([FromQuery] string cityName, [FromQuery] string apiKey)
        {
            try
            {
                var response = _currentWeatherService.GetCurrentWeather(cityName, apiKey);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}