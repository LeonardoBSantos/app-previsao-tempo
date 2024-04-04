using Domain.Entities;
using Domain.IAdapters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InfraExternalApi.Repositories
{
    public class ExternalApiRepository: IExternalApiRepository
    {
        static HttpClient client = new HttpClient();

        public ExternalApiRepository()
        {
            HttpClientInstanceInit();
        }

        public async Task<List<GeocodingEntity>> GetGeocoding(string cityName, string apiKey)
        {
            try
            {
                //HttpClientInstanceInit();

                List<GeocodingEntity> geocodingList = null;
                HttpResponseMessage response = await client.GetAsync($"geo/1.0/direct?q={cityName}&limit=1&appid={apiKey}");
                if (response.IsSuccessStatusCode)
                {
                    geocodingList = await response.Content.ReadAsAsync<List<GeocodingEntity>>();
                }
                return geocodingList;
            }
            catch(Exception ex)
            {
                throw;
            }
           
        }

        public async Task<CurrentWeather> GetCurrentWeather(string lat, string lon, string apiKey)
        {
            CurrentWeather currentWeather = null;
            HttpResponseMessage response = await client.GetAsync($"data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric&lang=pt_br");
            if (response.IsSuccessStatusCode)
            {
                currentWeather = await response.Content.ReadAsAsync<CurrentWeather>();
            }
            return currentWeather;
        }

        public void Get5DaysWeatherForecast()
        {
            throw new NotImplementedException();
        }

        private async static Task HttpClientInstanceInit()
        {
            client.BaseAddress = new Uri("http://api.openweathermap.org/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
