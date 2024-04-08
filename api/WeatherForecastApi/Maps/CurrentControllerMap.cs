using Application.Model;
using Domain.DTO;

namespace WeatherForecastApi.Maps
{
    public static class CurrentControllerMap
    {
        public static CurrentWeatherModel mapToViewModel(CurrentWeatherDto response)
        {
            return new CurrentWeatherModel
            {
                cidade = response.cityName,
                descricao = response.description,
                temperatura = response.temp,
                umidade = response.humidity,
                velocidade_do_vento = response.speed,
                unidades_de_medida = "Sistema Métrico"
            };
        }
    }
}
