using System.Configuration;
using System.Data;
using System.Windows;
using KalendarzApp.Data;

namespace KalendarzApp
{
    /// <summary>
    /// Główna klasa aplikacji WPF KalendarzApp.
    /// Odpowiada za inicjalizację aplikacji oraz uruchomienie logiki startowej.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Metoda wywoływana podczas uruchamiania aplikacji.
        /// Inicjuje bazę danych poprzez wywołanie DbInitializer.EnsureDatabase().
        /// </summary>
        /// <param name="e">Argumenty uruchomienia aplikacji.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DbInitializer.EnsureDatabase();
        }
    }

}
