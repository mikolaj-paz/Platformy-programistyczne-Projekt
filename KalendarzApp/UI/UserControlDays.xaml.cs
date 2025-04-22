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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KalendarzApp.Models;
using KalendarzApp.Data;
using KalendarzApp.UI;

namespace KalendarzApp
{
    /// <summary>
    /// Logika interakcji dla klasy UserControlDays.xaml
    /// </summary>
    public partial class UserControlDays : UserControl
    {
        int day_, month_, year_;
        DateTime date_;
        public UserControlDays(int numday, int month, int year)
        {
            InitializeComponent();
            day_ = numday;
            month_ = month;
            year_ = year;
            date_ = new DateTime(year_, month_, day_);
            ShowEventsInListbox();
        }

        public void days()
        {
            lbDays.Content = day_.ToString();

            DateTime now = DateTime.Now;
            if (day_ == now.Day &&
                month_ == now.Month &&
                year_ == now.Year)
            {
                gridDay.Background = new SolidColorBrush(Color.FromRgb(255, 128, 128));
            }


        }

        private void EventsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //public void mark_day(object sender, RoutedEventArgs e)
        //{
        //    if (checkBox.IsChecked == false)
        //    {
        //        checkBox.IsChecked = true;
        //        dayUC.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        //    }
        //    else
        //    {
        //        checkBox.IsChecked = false;
        //        dayUC.Background = new SolidColorBrush(Color.FromRgb(209, 209, 209));
        //    }
        //}

        public void openEventListWindow(object sender, RoutedEventArgs e)
        {
            EventListWindow eventListWindow = new EventListWindow(date_);
            eventListWindow.Show();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

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
                string formattedEntry = $"{entry.StartDate:HH:mm} - {entry.Title}";
                EventsListbox.Items.Add(formattedEntry);
            }
        }
    }
}
