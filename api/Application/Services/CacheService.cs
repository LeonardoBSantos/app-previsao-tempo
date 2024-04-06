using Application.Model;
using Domain.IAdapters;
using Domain.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CacheService<T> : ICacheService<T>
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly ISearchHistoryService _searchHistoryService;

        public CacheService(ICacheRepository cacheRepository, ISearchHistoryService searchHistoryService)
        {
            _cacheRepository = cacheRepository;
            _searchHistoryService = searchHistoryService;
        }

        public T? ReadCache(string? cityName, string endpoint)
        {
            var cacheDataString = _cacheRepository.ReadCache(cityName, endpoint);
            if (!String.IsNullOrEmpty(cacheDataString))
            {
                _searchHistoryService.CreateHistory(cityName);
                return JsonConvert.DeserializeObject<T>(cacheDataString);
            }
            return default(T);
        }

        public async Task WriteCache(string? cityName, string cacheData, string endpoint)
        {
            _cacheRepository.WriteCache(cityName.ToLower(), cacheData, endpoint);
        }
    }
}
