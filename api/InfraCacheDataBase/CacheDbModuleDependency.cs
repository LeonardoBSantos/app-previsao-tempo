using Domain.IAdapters;
using InfraCacheDataBase.BackgroudTasks;
using InfraCacheDataBase.BackgroudTasks.ScopedServices;
using InfraCacheDataBase.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InfraCacheDataBase
{
    public static class CacheDbModuleDependency
    {
        public static void addCacheDbModule(this IServiceCollection services)
        {
            services.AddScoped<ICacheRepository, CacheRepository>();
            services.AddHostedService<TimedCacheClearing>();
            services.AddScoped<IScopedCacheClearingService, ScopedCacheClearingService>();
        }
    }
}