using Domain.DTO;

namespace Domain.IServices
{
    public interface ICurrentWeatherService
    {
        Task<CurrentWeatherDto> GetCurrentWeatherAsync(string cityName, string apiKey);
    }
}
