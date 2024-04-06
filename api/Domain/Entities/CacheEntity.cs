using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CacheEntity
    {
        [Key]
        public string CityName { get; set; }
        public string CacheData { get; set; }
    }
}
