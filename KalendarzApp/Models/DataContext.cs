using System;
using System.Collections.Generic;
using System.IO;
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
        /// Zbiór kategorii wpisów w kalendarzu.
        /// </summary>
        public DbSet<EntryCategory> EntryCategories { get; set; }

        /// <summary>
        /// Konfiguruje opcje dla kontekstu bazy danych.
        /// </summary>
        /// <param name="options">Opcje konfiguracyjne dla kontekstu bazy danych.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "calendar.db");
            options.UseSqlite($"Data Source={dbPath}");
        }
    }
}
