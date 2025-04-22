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
    /// Logika interakcji dla klasy EventListWindow.xaml
    /// </summary>
    public partial class EventListWindow : Window
    {
        private DateTime date_;
        public EventListWindow(DateTime date)
        {
            InitializeComponent();
            date_ = date;
            ShowEventsInListbox();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EventsListbox.SelectedItem != null)
            {
                string strId = EventsListbox.SelectedItem.ToString().Split(':')[0];
                int eventId = int.Parse(strId);
                //MessageBox.Show($"{eventId}");
                var eventDetailWindow = new EventDetailWindow(eventId); // Przekazujesz wybrany element do okienka
                eventDetailWindow.Show(); // Uruchamiasz nowe okienko
                this.Close();
            }
        }

        private void ShowEventsInListbox()
        {
            List<Entry> listOfEvents = EntriesData.GetEntriesByDate(date_);
            EventsListbox.Items.Clear();

            if (listOfEvents.Count == 0)
            {
                EventsListbox.Items.Add("Brak wydarzeń na ten dzień.");
                return;
            }

            foreach (Entry entry in listOfEvents)
            {
                string formattedEntry = $"{entry.Id}: {entry.StartDate:dd.MM.yyyy HH:mm} - {entry.Title}";
                EventsListbox.Items.Add(formattedEntry);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EventForm eventForm = new EventForm(date_);
            eventForm.Show();
            this.Close();
        }
    }
    
}
