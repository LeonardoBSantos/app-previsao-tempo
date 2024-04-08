namespace Domain.DTO
{
    public class CurrentWeatherDto
    {
        public string cityName { get; set; }
        public double temp { get; set; }
        public int humidity { get; set; }
        public string description { get; set; }
        public double speed { get; set; }
    }
}
