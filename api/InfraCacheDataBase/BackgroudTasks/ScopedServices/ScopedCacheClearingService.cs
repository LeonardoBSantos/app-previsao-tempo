using Domain.IAdapters;
using Microsoft.Extensions.Logging;

namespace InfraCacheDataBase.BackgroudTasks.ScopedServices
{
    public class ScopedCacheClearingService: IScopedCacheClearingService
    {
        private readonly ILogger _logger;
        private readonly ICacheRepository _cacheRepository;

        public ScopedCacheClearingService(ILogger<ScopedCacheClearingService> logger, ICacheRepository cacheRepository)
        {
            _logger = logger;
            _cacheRepository = cacheRepository;
        }

        public async Task CacheClearing()
        {
            _cacheRepository.CleanCacheDb();
            _logger.LogCritical("Limpeza do cache feita com sucesso!");
        }
    }
}
