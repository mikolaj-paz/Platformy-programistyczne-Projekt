using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KalendarzApp.Models
{
    /// <summary>
    /// Reprezentuje prognozę pogody na jeden dzień pobraną z weatherapi.com.
    /// </summary>
    public class ForecastDay
    {
        /// <summary>
        /// Średnia temperatura w stopniach Celsjusza.
        /// </summary>
        [JsonProperty("avgtemp_c")]
        public double AvgTempC { get; set; }

        /// <summary>
        /// Warunki pogodowe.
        /// </summary>
        [JsonProperty("condition")]
        public Condition? Condition { get; set; }
    }
}
