using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalendarzApp.UI
{
    /// <summary>
    /// Przechowuje ustawienia aplikacji oraz różne zmienne pełniące rolę zmiennych globalnych.
    /// </summary>
    static class Settings
    {
        public static bool needToRefreshMainWindow = false;
        public static string location = "Wrocław";
    }
}
