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
    /// Okno formularza służącego do tworzenia nowego wydarzenia w kalendarzu.
    /// Automatycznie ustawia datę i proponowaną godzinę rozpoczęcia oraz zakończenia.
    /// </summary>
    public partial class EventForm : Window
    {
        private DateTime _date;

        /// <summary>
        /// Konstruktor formularza wydarzenia. Inicjalizuje komponenty i wypełnia pola domyślnymi wartościami.
        /// </summary>
        /// <param name="date">Data, na którą ma zostać dodane wydarzenie.</param>
        public EventForm(DateTime date)
        {
            InitializeComponent();
            _date = date;
            fillTextEntries();
            fillCategoryCombobox();
        }

        /// <summary>
        /// Wypełnia pola daty rozpoczęcia i zakończenia domyślnymi wartościami (następna pełna godzina i godzina później).
        /// </summary>
        public void fillTextEntries()
        {
            DateTime currentDate = DateTime.Now;

            int startHourHour = currentDate.Hour + 1;
            int startHourMinute = currentDate.Minute;
            int endHourHour = startHourHour + 1;
            int endHourMinute = startHourMinute;

            string startDateTimeFormatted = $"{_date:yyyy-MM-dd} {startHourHour:D2}:{startHourMinute:D2}";
            string endDateTimeFormatted = $"{_date:yyyy-MM-dd} {endHourHour:D2}:{endHourMinute:D2}";

            StartDateTextBox.Text = startDateTimeFormatted;
            EndDateTextBox.Text = endDateTimeFormatted;
        }


        /// <summary>
        /// Obsługuje zmianę tekstu w polach tekstowych. (Obecnie nieużywana metoda.)
        /// </summary>
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Obsługuje zmianę tekstu w polach tekstowych. (Obecnie nieużywana metoda.)
        /// </summary>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Obsługuje kliknięcie przycisku „Zapisz”.
        /// Waliduje dane wejściowe, tworzy nowy wpis i zapisuje go w bazie danych.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EventTextBox.Text) || string.IsNullOrWhiteSpace(StartDateTextBox.Text))
            {
                MessageBox.Show("Proszę wypełnić wszystkie wymagane pola.");
                return;
            }

            try
            {
                Entry entry = new Entry
                {
                    Title = EventTextBox.Text,
                    StartDate = DateTime.Parse(StartDateTextBox.Text),
                    EndDate = DateTime.Parse(EndDateTextBox.Text),
                    Location = LocationTextBox.Text,
                    CategoryId = CategoryCombobox.SelectedIndex + 1,
                    Description = EventTextBox.Text
                };

                EntriesData.AddEntryToDb(entry);

                EventListWindow eventListWindow = new EventListWindow(_date);
                eventListWindow.Show();
                this.Close();

                Settings.needToRefreshMainWindow = true;
    }
            catch (FormatException)
            {
                MessageBox.Show("Nieprawidłowy format daty. Proszę użyć poprawnego formatu, np. 'yyyy-MM-dd'.");
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
        /// Obsługuje zmianę wybranej kategorii w comboboxie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /// <summary>
        /// Wypełnia combobox kategoriami z bazy danych.
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
