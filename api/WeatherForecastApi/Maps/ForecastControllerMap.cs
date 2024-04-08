using Application.Model;
using Domain.DTO;

namespace WeatherForecastApi.Maps
{
    public static class ForecastControllerMap
    {
        public static WeatherForecastModel MapToViewModel(WeatherForecastDto response)
        {
            var forecastViewModel = new WeatherForecastModel();
            forecastViewModel.cidade = response.city_name;
            forecastViewModel.unidades_de_medida = "Sistema Métrico";
            forecastViewModel.listaDePrevisoes = new List<Previsao>();

            foreach (var item in response.list)
            {
                forecastViewModel.listaDePrevisoes.Add(
                    new Previsao()
                    {
                        data = item.dt_txt,
                        descricao = item.description,
                        temperatura = item.temp,
                        umidade = item.humidity,
                        velocidade_do_vento = item.speed
                    });
            }

            return forecastViewModel;
        }
    }
}
