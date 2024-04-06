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
    public class CurrentWeatherController : ControllerBase
    {
        private const string Endpoint_NAME = "current";
        private readonly ILogger<CurrentWeatherController> _logger;
        private readonly ICurrentWeatherService _currentWeatherService;
        private readonly ICacheService<CurrentWeatherModel> _cacheService;

        public CurrentWeatherController(
            ILogger<CurrentWeatherController> logger, 
            ICurrentWeatherService currentWeatherService, 
            ICacheService<CurrentWeatherModel> cacheService)
        {
            _logger = logger;
            _currentWeatherService = currentWeatherService;
            _cacheService = cacheService;
        }

        [HttpGet("/current")]
        public IActionResult GetCurrentWeather([FromQuery] string cityName, [FromQuery] string apiKey)
        {
            try
            {
                var city = Regex.Replace(cityName, @"[^\w\s]", "");
                var currentWeatherCache = _cacheService.ReadCache(city, Endpoint_NAME);
                if (currentWeatherCache is null)
                {
                    var response = _currentWeatherService.GetCurrentWeather(cityName, apiKey);
                    var currentWeather = mapToCurrentWeatherViewModel(response);
                    _cacheService.WriteCache(city, JsonConvert.SerializeObject(currentWeather), Endpoint_NAME);

                    return Ok(currentWeather);
                }
                else
                {
                    return Ok(currentWeatherCache);
                }
                
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private CurrentWeatherModel mapToCurrentWeatherViewModel(CurrentWeatherDto response)
        {
            return new CurrentWeatherModel
            {
                descricao = response.description,
                temperatura = response.temp,
                umidade = response.humidity,
                velocidade_do_vento = response.speed
            };
        }
    }
}