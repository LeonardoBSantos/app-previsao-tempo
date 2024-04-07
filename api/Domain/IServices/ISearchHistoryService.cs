using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface ISearchHistoryService
    {
        List<SearchHistoryDto> GetHistory();

        Task CreateHistoryAsync(string cityName);
    }
}
