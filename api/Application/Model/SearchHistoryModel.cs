using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class SearchHistoryModel
    {
        public List<HistoryModel> lista_historico { get; set; }
    }

    public class HistoryModel
    {
        public string cidade { get; set; }
        public string data { get; set; }
    }
}
