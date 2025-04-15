using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KalendarzApp.Models
{
    /// <summary>
    /// Reprezentuje prognozę pogody na jeden dzień pobraną z weatherapi.com.
    /// </summary>
    public class WeatherForecastDay
    {
        /// <summary>
        /// Prognoza pogody na dany dzień.
        /// </summary>
        [JsonProperty("day")]
        public ForecastDay? Day { get; set; }
    }
}
