using Domain.IAdapters;
using InfraCacheDataBase.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InfraCacheDataBase
{
    public static class CacheDbModuleDependency
    {
        public static void addCacheDbModule(this IServiceCollection services)
        {
            services.AddScoped<ICacheRepository, CacheRepository>();
            //services.AddScoped<ICacheRepository, ForecastWeatherCacheRepository>();
        }
    }
}