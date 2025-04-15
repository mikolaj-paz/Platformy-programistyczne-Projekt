using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KalendarzApp.Models;

namespace KalendarzApp.Data
{
    /// <summary>
    /// Klasa EntriesData zawiera metody do zarządzania wpisami w bazie danych.
    /// </summary>
    public static class EntriesData
    {
        /// <summary>
        /// Dodaje nowy wpis do bazy danych.
        /// </summary>
        /// <param name="entry">Obiekt Entry reprezentujący wpis do dodania.</param>
        /// <exception cref="ValidationException">Rzucane, gdy tytuł jest pusty, data rozpoczęcia jest późniejsza niż data zakończenia lub data rozpoczęcia jest w przeszłości.</exception>
        public static void AddEntryToDb(Entry entry)
        {
            using (var db = new DataContext())
            {
                if (string.IsNullOrWhiteSpace(entry.Title))
                {
                    throw new ValidationException("Tytuł nie może być pusty.");
                }

                if (entry.StartDate > entry.EndDate)
                {
                    throw new ValidationException("Data rozpoczęcia nie może być późniejsza niż data zakończenia.");
                }

                if (entry.StartDate < DateTime.Now)
                {
                    throw new ValidationException("Data rozpoczęcia nie może być w przeszłości.");
                }

                db.Add(entry);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Pobiera wszystkie wpisy z bazy danych.
        /// </summary>
        /// <returns>Lista obiektów Entry reprezentujących wszystkie wpisy.</returns>
        public static List<Entry> GetAllEntries()
        {
            using (var db = new DataContext())
            {
                return db.Entries.OrderBy(e => e.StartDate).ToList();
            }
        }

        /// <summary>
        /// Pobiera wpis z bazy danych na podstawie identyfikatora.
        /// </summary>
        /// <param name="entryId">Identyfikator wpisu.</param>
        /// <returns>Obiekt Entry reprezentujący znaleziony wpis.</returns>
        /// <exception cref="KeyNotFoundException">Rzucane, gdy wpis o podanym identyfikatorze nie zostanie znaleziony.</exception>
        public static Entry GetEntryById(int entryId)
        {
            using (var db = new DataContext())
            {
                var entry = db.Entries.Find(entryId);
                if (entry != null)
                {
                    return entry;
                }
                else
                {
                    throw new KeyNotFoundException($"Nie znaleziono wpisu o ID {entryId}.");
                }
            }
        }

        /// <summary>
        /// Pobiera wpisy z bazy danych na podstawie miesiąca.
        /// </summary>
        /// <param name="month">Data reprezentująca miesiąc, dla którego mają zostać pobrane wpisy.</param>
        /// <returns>Lista obiektów Entry reprezentujących wpisy z danego miesiąca.</returns>
        public static List<Entry> GetEntriesByMonth(DateTime month)
        {
            using (var db = new DataContext())
            {
                return db.Entries
                    .Where(e => e.StartDate.Month == month.Month && e.StartDate.Year == month.Year)
                    .OrderBy(e => e.StartDate)
                    .ToList();
            }
        }

        /// <summary>
        /// Pobiera określoną liczbę wpisów z bazy danych.
        /// </summary>
        /// <param name="num">Liczba wpisów do pobrania.</param>
        /// <returns>Lista obiektów Entry reprezentujących pobrane wpisy.</returns>
        public static List<Entry> GetNumOfEntries(int num)
        {
            using (var db = new DataContext())
            {
                return db.Entries
                    .OrderBy(e => e.StartDate)
                    .Take(num)
                    .ToList();
            }
        }

        /// <summary>
        /// Pobiera wpisy z bazy danych na podstawie daty.
        /// </summary>
        /// <param name="date">Data, dla której mają zostać pobrane wpisy.</param>
        /// <returns>Lista obiektów Entry reprezentujących wpisy z danej daty.</returns>
        public static List<Entry> GetEntriesByDate(DateTime date)
        {
            using (var db = new DataContext())
            {
                return db.Entries
                    .Where(e => e.StartDate.Date == date.Date)
                    .OrderBy(e => e.StartDate)
                    .ToList();
            }
        }

        /// <summary>
        /// Aktualizuje istniejący wpis w bazie danych.
        /// </summary>
        /// <param name="entry">Obiekt Entry reprezentujący wpis do zaktualizowania.</param>
        public static void UpdateEntryInDb(Entry entry)
        {
            using (var db = new DataContext())
            {
                db.Update(entry);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Usuwa wpis z bazy danych na podstawie identyfikatora.
        /// </summary>
        /// <param name="entryId">Identyfikator wpisu do usunięcia.</param>
        /// <exception cref="KeyNotFoundException">Rzucane, gdy wpis o podanym identyfikatorze nie zostanie znaleziony.</exception>
        public static void DeleteEntryFromDb(int entryId)
        {
            using (var db = new DataContext())
            {
                var entry = db.Entries.Find(entryId);
                if (entry != null)
                {
                    db.Remove(entry);
                    db.SaveChanges();
                }
                else
                {
                    throw new KeyNotFoundException($"Nie znaleziono wpisu o ID {entryId}.");
                }
            }
        }
    }
}
