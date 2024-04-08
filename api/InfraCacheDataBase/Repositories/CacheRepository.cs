using Domain.Entities;
using Domain.IAdapters;
using Microsoft.Extensions.Logging;

namespace InfraCacheDataBase.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly ILogger<CacheRepository> _logger;
        private CacheDbContext _dbContext;

        public CacheRepository(ILogger<CacheRepository> logger)
        {
            _logger = logger;
            _dbContext = new CacheDbContext();
        }

        public async Task<string>? ReadCacheAsync(string? cityName, string endpoint)
        {
            switch (endpoint)
            {
                case "current":
                    var currentCache = await ReadFromCurrentAsync(cityName);
                    return currentCache?.CacheData;
                case "forecast":
                    var forecastCache = await ReadFromForecastAsync(cityName);
                    return forecastCache?.CacheData;
                default:
                    return null;
                    break;
            }
        }

        public async Task WriteCacheAsync(string? cityName, string cacheData, string endpoint)
        {
            switch (endpoint)
            {
                case "current":
                    WriteToCurrentAsync(cityName, cacheData);
                    break;
                case "forecast":
                    WriteToForecastAsync(cityName, cacheData);
                    break;
            }
        }

        public async Task CleanCacheDb()
        {
            try
            {
                var currentTable = _dbContext.Set<CurrentCacheEntity>();
                var forecastTable = _dbContext.Set<ForecastCacheEntity>();

                currentTable.RemoveRange(currentTable);
                forecastTable.RemoveRange(forecastTable);

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private async Task WriteToCurrentAsync(string cityName, string cacheData)
        {
            try
            {
                _dbContext.current.AddAsync(new CurrentCacheEntity()
                {
                    CityName = cityName,
                    CacheData = cacheData
                });

                _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private async Task WriteToForecastAsync(string cityName, string cacheData)
        {
            try
            {
                _dbContext.forecast.AddAsync(new ForecastCacheEntity()
                {
                    CityName = cityName,
                    CacheData = cacheData
                });

                _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private async Task<CacheEntity> ReadFromCurrentAsync(string cityName)
        {
            try
            {
                var cacheEntity = await _dbContext.current.FindAsync(cityName);
                return cacheEntity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private async Task<CacheEntity> ReadFromForecastAsync(string cityName)
        {
            try
            {
                var cacheEntity = await _dbContext.forecast.FindAsync(cityName);
                return cacheEntity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
