using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class CurrentWeatherModel
    {
        public string cidade { get; set; }

        public double temperatura { get; set; }

        public int umidade { get; set; }

        public string descricao { get; set; }

        public double velocidade_do_vento { get; set; }

        public string unidades_de_medida { get; set; }
    }
}
