namespace Domain.DTO
{
    public class WeatherForecastDto
    {
        public string city_name { get; set; }

        public List<ListData> list { get; set; }
    }

    public class ListData
    {
        public double temp { get; set; }

        public int humidity { get; set; }

        public string description { get; set; }

        public double speed { get; set; }

        public string dt_txt { get; set; }

    }
}