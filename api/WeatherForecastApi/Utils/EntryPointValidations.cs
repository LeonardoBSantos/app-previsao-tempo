using System.Text.RegularExpressions;

namespace WeatherForecastApi.Utils
{
    public static class EntryPointValidations
    {
        public static void ValidateCityName(string cityName)
        {
            /*[^] - Indica uma negação, ou seja, procura por caracteres que não estejam na lista definida a seguir
           * A-Z - Todas as letras maiúsculas do alfabeto
           * a-z - Todas as letras minúsculas do alfabeto
           * \s - Espaços em branco
           * \u00C0-\u017F - Faixa de caracteres Unicode para letras acentuadas latinas
           */
            string pattern = @"[^A-Za-z\s\u00C0-\u017F]";
            if (Regex.IsMatch(cityName, pattern))
            {
                throw new ApplicationException("Cidade não encontrada, tente novamente.");
            }
        }
    }
}
