using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class WeatherForecastDto
    {
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