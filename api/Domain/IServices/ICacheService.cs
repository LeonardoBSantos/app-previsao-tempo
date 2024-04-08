namespace Domain.IServices
{
    public interface ICacheService<T>
    {
        Task<T> ReadCacheAsync(string? cityName, string endpoint);
        Task WriteCacheAsync(string? cityName, string cacheData, string endpoint);
    }
}
