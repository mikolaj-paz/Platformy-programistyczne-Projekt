using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KalendarzApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KalendarzApp.Data
{
    /// <summary>
    /// Klasa pomocnicza odpowiedzialna za inicjalizację bazy danych aplikacji.
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Zapewnia istnienie bazy danych oraz stosuje wszelkie oczekujące migracje.
        /// </summary>
        public static void EnsureDatabase()
        {
            using var context = new DataContext();
            context.Database.Migrate();
        }
    }
}
