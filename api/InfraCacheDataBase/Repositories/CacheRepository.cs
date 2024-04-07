using Domain.Entities;
using Domain.IAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraCacheDataBase.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private CacheDbContext _dbContext;

        public CacheRepository()
        {
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

        private void WriteToCurrentAsync(string cityName, string cacheData)
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
                throw;
            }
        }

        private async Task<CacheEntity> ReadFromCurrentAsync(string cityName)
        {
            var cacheEntity = await _dbContext.current.FindAsync(cityName);
            return cacheEntity;
        }

        private async Task<CacheEntity> ReadFromForecastAsync(string cityName)
        {
            var cacheEntity = await _dbContext.forecast.FindAsync(cityName);
            return cacheEntity;
        }
    }
}
