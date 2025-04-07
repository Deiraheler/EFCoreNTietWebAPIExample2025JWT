using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week7WebAPIS00243021;

namespace Week7SupplierDataServiceS00243021
{
    public interface IWeatherService
    {
        Task<List<WeatherForecast>> getWeatherForecast();
    }
}
