using Application.Services;
using Domain.IServices;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationModuleDependency
    {
        public static void AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<ICurrentWeatherService, CurrentWeatherService>();
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddScoped<ISearchHistoryService, SearchHistoryService>();
            services.AddScoped(typeof(ICacheService<>), typeof(CacheService<>));
        }

    }
}
