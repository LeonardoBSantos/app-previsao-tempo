using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface ICacheService<T>
    {
        T ReadCache(string? cityName, string endpoint);
        Task WriteCache(string? cityName, string cacheData, string endpoint);
    }
}
