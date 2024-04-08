using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface ICurrentWeatherService
    {
        Task<CurrentWeatherDto> GetCurrentWeatherAsync(string cityName, string apiKey);
    }
}
