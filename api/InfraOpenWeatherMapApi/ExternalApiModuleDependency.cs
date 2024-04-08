using Domain.IAdapters;
using InfraExternalApi.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
