using Application.Services;
using Domain.IServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationModuleDependency
    {
        public static void AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<ICurrentWeatherService, CurrentWeatherService>();
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddScoped<ISearchHistoryService, SearchHistoryService>();
        }

    }
}
