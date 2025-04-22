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
using Microsoft.Extensions.Logging;

namespace KalendarzApp.UI
{
    /// <summary>
    /// Logika interakcji dla klasy EventDetailWindow.xaml
    /// </summary>
    public partial class EventDetailWindow : Window
    {
        int _eventId;
        Entry _entry;
        public EventDetailWindow(int eventId)
        {
            InitializeComponent();
            _eventId = eventId;
            _entry = EntriesData.GetEntryById(_eventId);
            fillTextEntries();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EntriesData.DeleteEntryFromDb(_eventId);
            EventListWindow eventListWindow = new EventListWindow(_entry.StartDate);
            eventListWindow.Show();
            this.Close();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void fillTextEntries()
        {
            // Formatowanie daty i czasu jako "yyyy-MM-dd HH:mm"
            string startDateTimeFormatted = $"{_entry.StartDate:yyyy-MM-dd HH:mm}";
            string endDateTimeFormatted = $"{_entry.EndDate:yyyy-MM-dd HH:mm}";

            // Przypisanie sformatowanej daty i czasu do pól tekstowych
            StartDateTextBox.Text = startDateTimeFormatted;
            EndDateTextBox.Text = endDateTimeFormatted;
            EventTextBox.Text = _entry.Title.ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Validation to ensure all required fields are filled
            if (string.IsNullOrWhiteSpace(EventTextBox.Text) ||
                string.IsNullOrWhiteSpace(StartDateTextBox.Text) ||
                string.IsNullOrWhiteSpace(EndDateTextBox.Text))
            {
                MessageBox.Show("Proszę wypełnić wszystkie wymagane pola.");
                return;
            }

            try
            {
                // Parsing dates
                DateTime startDate = DateTime.Parse(StartDateTextBox.Text);
                DateTime endDate = DateTime.Parse(EndDateTextBox.Text);

                // Ensuring end date is after start date
                if (endDate <= startDate)
                {
                    MessageBox.Show("Data zakończenia musi być późniejsza niż data rozpoczęcia.");
                    return;
                }

                // Creating a new entry
                Entry entry = new Entry
                {
                    Title = EventTextBox.Text,
                    StartDate = startDate,
                    EndDate = endDate,
                };

                // Adding entry to the database
                EntriesData.AddEntryToDb(entry);

                // Removing old entries, if needed
                EntriesData.DeleteEntryFromDb(_eventId);

                // Optional success message
                //MessageBox.Show("Wpis został pomyślnie dodany!");

                // Displaying the event list window
                EventListWindow eventListWindow = new EventListWindow(entry.StartDate);
                eventListWindow.Show();

                // Closing the current window
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Nieprawidłowy format daty. Proszę użyć poprawnego formatu, np. 'yyyy-MM-dd HH:mm'.");
            }
            catch (Exception ex)
            {
                // Handling other potential errors
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }
    }
}



