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
    /// Okno wyświetlające szczegóły wydarzenia, umożliwiające edycję i usunięcie istniejącego wpisu.
    /// </summary>
    public partial class EventDetailWindow : Window
    {
        int _eventId;
        Entry _entry;

        /// <summary>
        /// Konstruktor okna szczegółów wydarzenia. Inicjalizuje komponenty i ładuje dane wydarzenia do pól tekstowych.
        /// </summary>
        /// <param name="eventId">Identyfikator wydarzenia do wyświetlenia.</param>
        public EventDetailWindow(int eventId)
        {
            InitializeComponent();
            _eventId = eventId;
            _entry = EntriesData.GetEntryById(_eventId);
            fillTextEntries();
            fillCategoryCombobox();
        }


        /// <summary>
        /// Obsługuje kliknięcie przycisku usunięcia wydarzenia. Usuwa wydarzenie z bazy danych i otwiera okno listy wydarzeń.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EntriesData.DeleteEntryFromDb(_eventId);
            EventListWindow eventListWindow = new EventListWindow(_entry.StartDate);
            eventListWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Obsługuje zmianę tekstu w jednym z pól tekstowych. (Obecnie niewykorzystane.)
        /// </summary>
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Wypełnia pola tekstowe danymi wydarzenia (daty, tytuł, lokalizacja, kategoria, opis).
        /// </summary>
        private void fillTextEntries()
        {
            // Formatowanie daty i czasu jako "yyyy-MM-dd HH:mm"
            string startDateTimeFormatted = $"{_entry.StartDate:yyyy-MM-dd HH:mm}";
            string endDateTimeFormatted = $"{_entry.EndDate:yyyy-MM-dd HH:mm}";

            // Przypisanie sformatowanej daty i czasu do pól tekstowych
            StartDateTextBox.Text = startDateTimeFormatted;
            EndDateTextBox.Text = endDateTimeFormatted;
            EventTextBox.Text = _entry.Title.ToString();
            LocationTextBox.Text = _entry.Location.ToString();
            
            List<EntryCategory> categories = EntryCategoriesData.GetAllCategories();
            string entryCategoryName = categories.FirstOrDefault(c => c.Id == _entry.CategoryId)?.Name ?? "Brak kategorii";
            CategoryCombobox.SelectedValue = $"{_entry.CategoryId}. {entryCategoryName}";

            DescriptionTextBox.Text = _entry.Description.ToString();            
        }

        /// <summary>
        /// Obsługuje zmianę tekstu w polach tekstowych. (Obecnie niewykorzystane.)
        /// </summary>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Obsługuje kliknięcie przycisku zapisu. Waliduje dane wejściowe, aktualizuje wpis w bazie
        /// i odświeża widok głównego okna.
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Validation to ensure all required fields are filled
            if (string.IsNullOrWhiteSpace(EventTextBox.Text) ||
                string.IsNullOrWhiteSpace(StartDateTextBox.Text) ||
                string.IsNullOrWhiteSpace(EndDateTextBox.Text))
            {
                MessageBox.Show("Proszę wypełnić wszystkie wymagane pola: Tytuł, data rozpoczęcia, data zakończenia");
                return;
            }

            try
            {
                DateTime startDate = DateTime.Parse(StartDateTextBox.Text);
                DateTime endDate = DateTime.Parse(EndDateTextBox.Text);

                if (endDate <= startDate)
                {
                    MessageBox.Show("Data zakończenia musi być późniejsza niż data rozpoczęcia.");
                    return;
                }

                Entry entry = new Entry
                {
                    Title = EventTextBox.Text,
                    StartDate = startDate,
                    EndDate = endDate,
                    Location = LocationTextBox.Text,
                    CategoryId = CategoryCombobox.SelectedIndex + 1,
                    Description = EventTextBox.Text
                };

                EntriesData.AddEntryToDb(entry);
                EntriesData.DeleteEntryFromDb(_eventId);
                //MessageBox.Show("Wpis został pomyślnie dodany!");

                EventListWindow eventListWindow = new EventListWindow(entry.StartDate);
                eventListWindow.Show();
                this.Close();

                Settings.needToRefreshMainWindow = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Nieprawidłowy format daty. Proszę użyć poprawnego formatu, np. 'yyyy-MM-dd HH:mm'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        /// <summary>
        /// Otwiera okno edycji kategorii wydarzenia.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryWindowButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow categoryWindow = new CategoryWindow();
            categoryWindow.Show();
        }
        /// <summary>
        /// Obsługuje zmianę wyboru w polu wyboru kategorii. (Obecnie niewykorzystane.)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /// <summary>
        /// Wypełnia pole wyboru kategorii dostępnych kategorii z bazy danych.
        /// </summary>
        void fillCategoryCombobox()
        {
            CategoryCombobox.Items.Clear();
            List<EntryCategory> categories = EntryCategoriesData.GetAllCategories();
            foreach (EntryCategory category in categories)
            {
                CategoryCombobox.Items.Add($"{category.Id}. {category.Name}");
            }
        }
    }
}



