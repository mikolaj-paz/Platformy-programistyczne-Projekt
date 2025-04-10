using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KalendarzApp.Models
{
    /// <summary>
    /// Reprezentuje warunki pogodowe pobrane z weatherapi.com.
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// Ikona reprezentująca warunki pogodowe.
        /// </summary>
        [JsonProperty("icon")]
        public string? Icon { get; set; }

        /// <summary>
        /// Opis tekstowy warunków pogodowych.
        /// </summary>
        [JsonProperty("text")]
        public string? Text { get; set; }
    }
}
