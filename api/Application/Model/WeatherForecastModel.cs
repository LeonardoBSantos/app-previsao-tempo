using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class WeatherForecastModel
    {
        public string cidade { get; set; }
        public string unidades_de_medida { get; set; }
        public List<Previsao> listaDePrevisoes { get; set; }
    }

    public class Previsao
    {
        public string data { get; set; }

        public double temperatura { get; set; }

        public int umidade { get; set; }

        public string descricao { get; set; }

        public double velocidade_do_vento { get; set; }
    }
}
