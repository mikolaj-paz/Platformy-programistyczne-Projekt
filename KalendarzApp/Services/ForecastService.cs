using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KalendarzApp.Models;
using Newtonsoft.Json;

namespace KalendarzApp.Services
{
    /// <summary>
    /// Usługa odpowiedzialna za pobieranie prognozy pogody.
    /// </summary>
    public static class ForecastService
    {
        /// <summary>
        /// Asynchronicznie pobiera trzydniową prognozę pogody dla podanej lokalizacji.
        /// </summary>
        /// <param name="location">Nazwa lokalizacji, dla której ma zostać pobrana prognoza pogody. Miasto lub IP.</param>
        /// <returns>Obiekt <see cref="WeatherForecast"/> zawierający prognozę pogody.</returns>
        /// <exception cref="HttpRequestException">Występuje, gdy nie uda się pobrać prognozy pogody.</exception>
        /// <exception cref="JsonException">Występuje, gdy nie uda się zdeserializować odpowiedzi JSON.</exception>
        /// <example>
        /// Przykład użycia:
        /// <code>
        /// new ForecastService().GetForecastAsync("Berlin").ContinueWith((task) =>
        /// {
        ///     var index = task.Result;
        /// });
        /// </code>
        /// </example>
        /// <note>
        /// Pobiera prognozę pogody z API weatherapi.com o strukturze JSON:
        /// <code>
        /// {
        ///     "forecast": {
        ///         "forecastday": [
        ///             {
        ///                 "day": {
        ///                     "avgtemp_c": ...,
        ///                     "condition": {...}
        ///                 }
        ///             },
        ///             {...},
        ///             {...}
        ///         ]
        ///     }
        /// }
        /// </code>
        /// </note>
        public static async Task<WeatherForecast> GetForecastAsync(string location)
        {
            using (HttpClient client = new())
            {
                var uri = new Uri("https://api.weatherapi.com/v1/");
                string apiKey = "97ecbd04a5364353b89113231250604";

                HttpResponseMessage response = await client.GetAsync($"{uri}forecast.json?key={apiKey}&q={location}&days=3");
                if (response.IsSuccessStatusCode)
                {
                    string forecastData = await response.Content.ReadAsStringAsync();

                    WeatherForecast? forecast = JsonConvert.DeserializeObject<WeatherForecast>(forecastData);
                    return forecast ?? throw new JsonException("Nie można odczytać prognozy pogody.");
                }
                else
                {
                    throw new HttpRequestException("Wystąpił błąd przy pobieraniu prognozy pogody.");
                }
            }
        }
    }
}
