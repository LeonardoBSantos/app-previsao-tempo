using Application.Model;
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
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly ISearchHistoryService _searchHistoryService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICurrentWeatherService currentWeatherService, IWeatherForecastService weatherForecastService, ISearchHistoryService searchHistoryService)
        {
            _logger = logger;
            _currentWeatherService = currentWeatherService;
            _weatherForecastService = weatherForecastService;
            _searchHistoryService = searchHistoryService;
        }

        [HttpGet("/current")]
        public IActionResult GetCurrentWeather([FromQuery] string cityName, [FromQuery] string apiKey)
        {
            try
            {
                var response = _currentWeatherService.GetCurrentWeather(cityName, apiKey);
                //return Ok(response);
                return Ok(new CurrentWeatherModel
                {
                    descricao = response.description,
                    temperatura = response.temp,
                    umidade = response.humidity,
                    velocidade_do_vento = response.speed
                });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("/5DaysWeatherForecast")]
        public IActionResult Get5DaysWeatherForecast([FromQuery] string cityName, [FromQuery] string apiKey)
        {
            try
            {
                var response = _weatherForecastService.Get5DaysForecast(cityName, apiKey);
                
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
                return Ok(forecastViewModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("/search_history")]
        public IActionResult GetSearchHistory()
        {
            try
            {
                var response = _searchHistoryService.GetHistory();

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

                return Ok(historyViewModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}