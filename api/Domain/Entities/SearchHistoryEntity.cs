using System.ComponentModel.DataAnnotations;

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
