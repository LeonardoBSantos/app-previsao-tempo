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

        public string? ReadCache(string? cityName, string endpoint)
        {
            switch (endpoint)
            {
                case "current":
                    var currentCache = ReadFromCurrent(cityName);
                    return currentCache is null ? null: currentCache.CacheData;
                case "forecast":
                    var forecastCache = ReadFromForecast(cityName);
                    return forecastCache is null ? null : forecastCache.CacheData;
                default:
                    return null;
                    break;
            }
        }

        public async Task WriteCache(string? cityName, string cacheData, string endpoint)
        {
            switch (endpoint)
            {
                case "current":
                    WriteToCurrent(cityName, cacheData);
                    break;
                case "forecast":
                    WriteToForecast(cityName, cacheData);
                    break;
            }
        }

        private void WriteToCurrent(string cityName, string cacheData)
        {
            try
            {
                _dbContext.current.Add(new CurrentCacheEntity()
                {
                    CityName = cityName,
                    CacheData = cacheData
                });

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void WriteToForecast(string cityName, string cacheData)
        {
            try
            {
                _dbContext.forecast.Add(new ForecastCacheEntity()
                {
                    CityName = cityName,
                    CacheData = cacheData
                });

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private CacheEntity ReadFromCurrent(string cityName)
        {
            var cacheEntity = _dbContext.current.Find(cityName);
            return cacheEntity;
        }

        private CacheEntity ReadFromForecast(string cityName)
        {
            var cacheEntity = _dbContext.forecast.Find(cityName);
            return cacheEntity;
        }
    }
}
