using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalendarzApp.Models
{
    public class EntryCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Entry> Entries { get; set; }
    }
}
