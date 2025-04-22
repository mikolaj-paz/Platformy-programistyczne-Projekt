using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KalendarzApp.Data;
using KalendarzApp.Models;
using KalendarzApp.UI;

namespace KalendarzApp
{
    /// <summary>
    /// Logika interakcji dla klasy EventForm.xaml
    /// </summary>
    public partial class EventForm : Window
    {
        private DateTime _date;
        public EventForm(DateTime date)
        {
            InitializeComponent();
            _date = date;
            fillTextEntries();
        }

        public void fillTextEntries()
        {
            DateTime currentDate = DateTime.Now;

            // Pobranie aktualnej godziny i minut
            int startHourHour = currentDate.Hour + 1;
            int startHourMinute = currentDate.Minute;
            int endHourHour = startHourHour + 1;
            int endHourMinute = startHourMinute; // Zakładam, że minuta pozostaje bez zmian

            // Formatowanie daty i czasu jako "yyyy-MM-dd HH:mm"
            string startDateTimeFormatted = $"{_date:yyyy-MM-dd} {startHourHour:D2}:{startHourMinute:D2}";
            string endDateTimeFormatted = $"{_date:yyyy-MM-dd} {endHourHour:D2}:{endHourMinute:D2}";

            // Przypisanie sformatowanej daty i czasu do pól tekstowych
            StartDateTextBox.Text = startDateTimeFormatted;
            EndDateTextBox.Text = endDateTimeFormatted;
        }
        
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Sprawdzenie, czy wszystkie wymagane dane są wprowadzone
            if (string.IsNullOrWhiteSpace(EventTextBox.Text) || string.IsNullOrWhiteSpace(StartDateTextBox.Text))
            {
                MessageBox.Show("Proszę wypełnić wszystkie wymagane pola.");
                return;
            }

            try
            {
                // Tworzenie nowego wpisu
                Entry entry = new Entry
                {
                    Title = EventTextBox.Text,
                    StartDate = DateTime.Parse(StartDateTextBox.Text),
                    EndDate = DateTime.Parse(EndDateTextBox.Text),
                };

                // Dodanie wpisu do bazy danych
                EntriesData.AddEntryToDb(entry);
                //MessageBox.Show("Wpis został pomyślnie dodany!");

                EventListWindow eventListWindow = new EventListWindow(_date);
                eventListWindow.Show();

                // Zamknięcie okna
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Nieprawidłowy format daty. Proszę użyć poprawnego formatu, np. 'yyyy-MM-dd'.");
            }
            catch (Exception ex)
            {
                // Obsługa innych potencjalnych błędów
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }
    }
}
