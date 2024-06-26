﻿namespace Domain.IAdapters
{
    public interface ICacheRepository
    {
        Task<string> ReadCacheAsync(string? cityName, string endpoint);
        Task WriteCacheAsync(string? cityName, string cacheData, string endpoint);
        Task CleanCacheDb();
    }
}
