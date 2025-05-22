using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalendarzApp.Models
{
    /// <summary>
    /// Reprezentuje kategorię wpisów w kalendarzu.
    /// </summary>
    public class EntryCategory
    {
        /// <summary>
        /// Identyfikator kategorii.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nazwa kategorii.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Kolekcja wpisów przypisanych do tej kategorii.
        /// </summary>
        public ICollection<Entry>? Entries { get; set; }
    }
}
