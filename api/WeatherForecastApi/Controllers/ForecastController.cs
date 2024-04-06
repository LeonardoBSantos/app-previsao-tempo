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
    public class ForecastController : ControllerBase
    {
        private const string Endpoint_NAME = "forecast";
        private readonly ILogger<CurrentWeatherController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly ICacheService<WeatherForecastModel> _cacheService;

        public ForecastController(
            ILogger<CurrentWeatherController> logger, 
            ICurrentWeatherService currentWeatherService, 
            IWeatherForecastService weatherForecastService, 
            ISearchHistoryService searchHistoryService,
            ICacheService<WeatherForecastModel> cacheService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
            _cacheService = cacheService;
        }

        [HttpGet("/5DaysWeatherForecast")]
        public IActionResult Get5DaysWeatherForecast([FromQuery] string cityName, [FromQuery] string apiKey)
        {
            try
            {
                var city = Regex.Replace(cityName, @"[^\w\s]", "");
                var forecastWeatherCache = _cacheService.ReadCache(city, Endpoint_NAME);
                if (forecastWeatherCache is null)
                {
                    var response = _weatherForecastService.Get5DaysForecast(cityName, apiKey);
                    WeatherForecastModel forecastWeather = MapToWeatherForecastViewModel(response);
                    _cacheService.WriteCache(city, JsonConvert.SerializeObject(forecastWeather), Endpoint_NAME);

                    return Ok(forecastWeather);
                }
                else
                {
                    return Ok(forecastWeatherCache);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private WeatherForecastModel MapToWeatherForecastViewModel(WeatherForecastDto response)
        {
            var forecastViewModel = new WeatherForecastModel();
            forecastViewModel.listaDePrevisoes = new List<Previsao>();

            foreach (var item in response.list)
            {
                forecastViewModel.listaDePrevisoes.Add(
                    new Previsao()
                    {
                        data = item.dt_txt,
                        descricao = item.description,
                        temperatura = item.temp,
                        umidade = item.humidity,
                        velocidade_do_vento = item.speed
                    });
            }

            return forecastViewModel;
        }
    }
}