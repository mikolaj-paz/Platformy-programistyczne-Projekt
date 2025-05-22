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
    /// Zapewnia operacje na kategoriach wpisów w kalendarzu, takie jak dodawanie nowych kategorii oraz pobieranie istniejących.
    /// </summary>
    public static class EntryCategoriesData
    {
        /// <summary>
        /// Dodaje nową kategorię do bazy danych.
        /// </summary>
        /// <param name="category">Obiekt kategorii do dodania.</param>
        /// <exception cref="ValidationException">
        /// Rzucany, gdy nazwa kategorii jest pusta lub kategoria o tej nazwie już istnieje.
        /// </exception>
        public static void AddCategoryToDb(EntryCategory category)
        {
            using (var db = new DataContext())
            {
                if (string.IsNullOrWhiteSpace(category.Name))
                {
                    throw new ValidationException("Nazwa kategorii nie może być pusta.");
                }

                if (db.EntryCategories.Any(c => c.Name == category.Name))
                {
                    throw new ValidationException("Kategoria o tej nazwie już istnieje.");
                }

                db.EntryCategories.Add(category);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Pobiera wszystkie kategorie wpisów z bazy danych.
        /// </summary>
        /// <returns>Lista wszystkich kategorii wpisów.</returns>
        public static List<EntryCategory> GetAllCategories()
        {
            using (var db = new DataContext())
            {
                return db.EntryCategories.ToList();
            }
        }

        /// <summary>
        /// Pobiera określoną liczbę kategorii wpisów, posortowanych alfabetycznie po nazwie.
        /// </summary>
        /// <param name="num">Liczba kategorii do pobrania.</param>
        /// <returns>Lista kategorii wpisów.</returns>
        public static List<EntryCategory> GetNumOfCategories(int num)
        {
            using (var db = new DataContext())
            {
                return db.EntryCategories
                    .OrderBy(c => c.Name)
                    .Take(num)
                    .ToList();
            }
        }
    }
}
