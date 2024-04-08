using Newtonsoft.Json;

namespace Domain.Entities
{
    public class WeatherForecastEntity
    {
        [JsonProperty("cod")]
        public string cod { get; set; }

        [JsonProperty("message")]
        public int message { get; set; }

        [JsonProperty("cnt")]
        public int cnt { get; set; }

        [JsonProperty("list")]
        public List<ListData> list { get; set; }

        [JsonProperty("city")]
        public City city { get; set; }
    }

    public class ListData
    {
        [JsonProperty("dt")]
        public int dt { get; set; }

        [JsonProperty("main")]
        public MainData main { get; set; }

        [JsonProperty("weather")]
        public List<WeatherData> weather { get; set; }

        [JsonProperty("clouds")]
        public Cloud clouds { get; set; }

        [JsonProperty("wind")]
        public WindData wind { get; set; }

        [JsonProperty("visibility")]
        public int visibility { get; set; }

        [JsonProperty("pop")]
        public double pop { get; set; }

        [JsonProperty("sys")]
        public SysData sys { get; set; }

        [JsonProperty("dt_txt")]
        public string dt_txt { get; set; }

        [JsonProperty("rain")]
        public Rain rain { get; set; }
    }

    public class City
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("coord")]
        public Coordinates coord { get; set; }

        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("population")]
        public int population { get; set; }

        [JsonProperty("timezone")]
        public int timezone { get; set; }

        [JsonProperty("sunrise")]
        public int sunrise { get; set; }

        [JsonProperty("sunset")]
        public int sunset { get; set; }
    }

    public class MainData
    {
        [JsonProperty("temp")]
        public double temp { get; set; }

        [JsonProperty("feels_like")]
        public double feels_like { get; set; }

        [JsonProperty("temp_min")]
        public double temp_min { get; set; }

        [JsonProperty("temp_max")]
        public double temp_max { get; set; }

        [JsonProperty("pressure")]
        public int pressure { get; set; }

        [JsonProperty("sea_level")]
        public int sea_level { get; set; }

        [JsonProperty("grnd_level")]
        public int grnd_level { get; set; }

        [JsonProperty("humidity")]
        public int humidity { get; set; }

        [JsonProperty("temp_kf")]
        public double temp_kf { get; set; }
    }

    public class WeatherData
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("main")]
        public string main { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("icon")]
        public string icon { get; set; }
    }

    public class Cloud
    {
        [JsonProperty("all")]
        public int all { get; set; }
    }

    public class WindData
    {
        [JsonProperty("speed")]
        public double speed { get; set; }

        [JsonProperty("deg")]
        public int deg { get; set; }

        [JsonProperty("gust")]
        public double gust { get; set; }
    }

    public class SysData
    {
        [JsonProperty("pod")]
        public string pod { get; set; }
    }

    public class Rain
    {
        [JsonProperty("3h")]
        public double _3h { get; set; }
    }

    public class Coordinates
    {
        [JsonProperty("lat")]
        public double lat { get; set; }

        [JsonProperty("lon")]
        public double lon { get; set; }
    }
}
