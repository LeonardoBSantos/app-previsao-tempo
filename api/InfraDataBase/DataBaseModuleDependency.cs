using Domain.IAdapters;
using InfraDataBase.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InfraDataBase
{
    public static class DataBaseModuleDependency
    {
        public static void addDataBaseModule(this IServiceCollection services)
        {
            services.AddScoped<ISearchHistoryRepository, SearchHistoryRepository>();
        }
    }
}