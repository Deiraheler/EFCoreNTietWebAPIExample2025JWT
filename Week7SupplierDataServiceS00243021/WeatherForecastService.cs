using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static Week7BlazorStandaloneAppS00243021.Pages.Weather;

namespace Week7SupplierDataServiceS00243021
{
    public class WeatherForecastService : IWeatherService
    {
        private HttpClient _httpClient;

        public WeatherForecastService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<WeatherForecast>> getWeatherForecast()
        {
            return await _httpClient.GetFromJsonAsync<List<WeatherForecast>>("WeatherForecast");
        }
    }
}
