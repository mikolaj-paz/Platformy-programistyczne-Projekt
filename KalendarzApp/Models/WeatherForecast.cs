using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KalendarzApp.Models
{
    /// <summary>
    /// Reprezentuje prognozę pogody.
    /// Dane pochodzą ze strony weatherapi.com.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Prognoza pogody.
        /// </summary>
        [JsonProperty("forecast")]
        public Forecast? Forecast { get; set; }
    }
}
