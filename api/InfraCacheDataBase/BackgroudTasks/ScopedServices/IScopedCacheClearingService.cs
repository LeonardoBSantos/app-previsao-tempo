using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraCacheDataBase.BackgroudTasks.ScopedServices
{
    public interface IScopedCacheClearingService
    {
        Task CacheClearing();
    }
}
