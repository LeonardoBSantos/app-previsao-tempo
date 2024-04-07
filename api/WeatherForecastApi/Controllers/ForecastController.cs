using Application.Model;
using Domain.DTO;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;
using WeatherForecastApi.Utils;

namespace WeatherForecastApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForecastController : ControllerBase
    {
        private const string Endpoint_NAME = "forecast";
        private readonly ILogger<ForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly ICacheService<WeatherForecastModel> _cacheService;
        private readonly ISearchHistoryService _searchHistoryService;


        public ForecastController(
            ILogger<ForecastController> logger,  
            IWeatherForecastService weatherForecastService, 
            ISearchHistoryService searchHistoryService,
            ICacheService<WeatherForecastModel> cacheService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
            _cacheService = cacheService;
            _searchHistoryService = searchHistoryService;
        }

        [HttpGet("/5DaysWeatherForecast")]
        public async Task<IActionResult> Get5DaysWeatherForecast([FromQuery] string cityName, [FromQuery] string apiKey)
        {
            try
            {
                EntryPointValidations.ValidateCityName(cityName);
                _searchHistoryService.CreateHistoryAsync(cityName);
                
                var forecastWeatherCache = await _cacheService.ReadCacheAsync(cityName, Endpoint_NAME);
                if (forecastWeatherCache is null)
                {
                    _logger.LogInformation("Clima Previsto obtido por consumo de API");
                    
                    var response = await _weatherForecastService.Get5DaysForecastAsync(cityName, apiKey);
                    WeatherForecastModel forecastWeather = MapToWeatherForecastViewModel(response);
                    _cacheService.WriteCacheAsync(cityName, JsonConvert.SerializeObject(forecastWeather), Endpoint_NAME);

                    return Ok(forecastWeather);
                }
                else
                {
                    _logger.LogInformation("Clima Previsto obtido do cache");
                    return Ok(forecastWeatherCache);
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

        private WeatherForecastModel MapToWeatherForecastViewModel(WeatherForecastDto response)
        {
            var forecastViewModel = new WeatherForecastModel();
            forecastViewModel.cidade = response.city_name;
            forecastViewModel.unidades_de_medida = "Sistema Métrico";
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