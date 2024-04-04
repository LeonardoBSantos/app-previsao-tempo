using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class CurrentWeatherModel
    {
        public double temperatura { get; set; }

        public int umidade { get; set; }

        public string descricao { get; set; }

        public double velocidade_do_vento { get; set; }
    }
}
