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

namespace KalendarzApp.UI
{
    /// <summary>
    /// Okno wyświetlające listę wydarzeń przypisanych do konkretnej daty.
    /// Umożliwia otwarcie szczegółów wybranego wydarzenia lub dodanie nowego.
    /// </summary>
    public partial class EventListWindow : Window
    {
        private DateTime date_;

        /// <summary>
        /// Konstruktor okna listy wydarzeń. Inicjalizuje komponenty i ładuje wydarzenia z wybranej daty.
        /// </summary>
        /// <param name="date">Data, dla której mają być wyświetlone wydarzenia.</param>
        public EventListWindow(DateTime date)
        {
            InitializeComponent();
            date_ = date;
            ShowEventsInListbox();
        }

        /// <summary>
        /// Obsługuje zmianę zaznaczenia w liście wydarzeń.
        /// Otwiera nowe okno z szczegółami wybranego wydarzenia.
        /// </summary>
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EventsListbox.SelectedItem != null)
            {
                string strId = EventsListbox.SelectedItem.ToString().Split(':')[0];
                try
                {
                    int eventId = int.Parse(strId);
                    //MessageBox.Show($"{eventId}");
                    var eventDetailWindow = new EventDetailWindow(eventId);
                    eventDetailWindow.Show();
                    this.Close();
                } catch(Exception ex) { }
            }
        }

        /// <summary>
        /// Pobiera z bazy danych wydarzenia przypisane do danej daty i wyświetla je w liście.
        /// </summary>
        private void ShowEventsInListbox()
        {
            List<Entry> listOfEvents = EntriesData.GetEntriesByDate(date_);
            EventsListbox.Items.Clear();

            if (listOfEvents.Count == 0)
            {
                EventsListbox.Items.Add("Brak wydarzeń na ten dzień");
                return;
            }

            foreach (Entry entry in listOfEvents)
            {
                string formattedEntry = $"{entry.Id}: {entry.StartDate:dd.MM.yyyy HH:mm} - {entry.Title}";
                EventsListbox.Items.Add(formattedEntry);
            }
        }


        /// <summary>
        /// Obsługuje kliknięcie przycisku „Dodaj wydarzenie”.
        /// Otwiera nowe okno formularza wydarzenia i zamyka bieżące.
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EventForm eventForm = new EventForm(date_);
            eventForm.Show();
            this.Close();
        }
    }
    
}
