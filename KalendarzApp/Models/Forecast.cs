using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KalendarzApp.Models
{
    /// <summary>
    /// Reprezentuje prognozę pogody. Dane pochodzą ze strony weatherapi.com.
    /// </summary>
    public class Forecast
    {
        /// <summary>
        /// Lista dni prognozy pogody.
        /// </summary>
        [JsonProperty("forecastday")]
        public List<WeatherForecastDay>? ForecastDays { get; set; }
    }
}
