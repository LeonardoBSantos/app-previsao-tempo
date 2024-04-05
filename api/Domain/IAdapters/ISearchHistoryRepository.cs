using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IAdapters
{
    public interface ISearchHistoryRepository
    {
        Task CreateHistory(SearchHistoryEntity entity);
        List<SearchHistoryEntity> GetHistory();
    }
}
