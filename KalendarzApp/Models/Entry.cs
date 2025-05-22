using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalendarzApp.Models
{
    /// <summary>
    /// Reprezentuje wpis w kalendarzu.
    /// </summary>
    public class Entry
    {
        /// <summary>
        /// Identyfikator wpisu.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tytuł wpisu.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Opis wpisu.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Data rozpoczęcia wpisu.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Data zakończenia wpisu.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Wartość wskazującą, czy wpis jest wydarzeniem całodniowym.
        /// </summary>
        public bool IsAllDayEvent { get; set; }

        /// <summary>
        /// Lokalizację wpisu.
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Klucz zewnętrzny do kategorii wpisu.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Kategoria wpisu.
        /// </summary>
        public EntryCategory? Category { get; set; }

        /// <summary>
        /// Wzorzec powtarzania wpisu.
        /// </summary>
        public string RecurrencePattern { get; set; } = string.Empty;

        /// <summary>
        /// Przypomnienie dla wpisu.
        /// </summary>
        public string Reminder { get; set; } = string.Empty;
    }
}
