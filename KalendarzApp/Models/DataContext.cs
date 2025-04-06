using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KalendarzApp.Models
{
    /// <summary>
    /// Reprezentuje kontekst bazy danych dla aplikacji Kalendarz.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Zbiór wpisów w kalendarzu.
        /// </summary>
        public DbSet<Entry> Entries { get; set; }

        /// <summary>
        /// Ścieżka do pliku bazy danych.
        /// </summary>
        public string DbPath = "Data/calendar.db";

        /// <summary>
        /// Konfiguruje opcje dla kontekstu bazy danych.
        /// </summary>
        /// <param name="options">Opcje konfiguracyjne dla kontekstu bazy danych.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
    }
}
