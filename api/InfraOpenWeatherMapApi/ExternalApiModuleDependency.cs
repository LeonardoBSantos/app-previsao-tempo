using Domain.IAdapters;
using InfraExternalApi.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InfraExternalApi
{
    public static class ExternalApiModuleDependency
    {
        public static void addExternalApiModule(this IServiceCollection services)
        {
            services.AddSingleton<IExternalApiRepository, ExternalApiRepository>();
        }
    }
}
