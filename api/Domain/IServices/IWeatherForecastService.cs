using Domain.DTO;

namespace Domain.IServices
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecastDto> Get5DaysForecastAsync(string cityName, string apiKey);
    }
}
