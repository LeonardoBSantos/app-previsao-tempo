using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SearchHistoryEntity
    {
        [Key]
        public int id { get; set; }
        public string city_name { get; set; }
        public string timestamp { get; set; }
    }
}
