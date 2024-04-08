using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class CacheEntity
    {
        [Key]
        public string CityName { get; set; }
        public string CacheData { get; set; }
    }
}
