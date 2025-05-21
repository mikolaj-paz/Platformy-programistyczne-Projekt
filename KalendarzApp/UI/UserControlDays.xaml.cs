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
using KalendarzApp.Services;

namespace KalendarzApp
{
    /// <summary>
    /// Logika interakcji dla kontrolki reprezentującej pojedynczy dzień w kalendarzu.
    /// Zawiera dane o dacie, wyświetla wydarzenia i prognozę pogody.
    /// </summary>
    public partial class UserControlDays : UserControl
    {
        int day_, month_, year_, daysInMonth_;
        DateTime date_;
        string avgDayTemp_;
        string dayConditionText_;
        string dayConditionIcon_;

        /// <summary>
        /// Konstruktor kontrolki dnia. Ustawia datę, wyświetla wydarzenia i pobiera prognozę pogody.
        /// </summary>
        /// <param name="numday">Numer dnia.</param>
        /// <param name="month">Numer miesiąca.</param>
        /// <param name="daysInMonth">Liczba dni w miesiącu.</param>
        /// <param name="year">Rok.</param>
        public UserControlDays(int numday, int month, int daysInMonth, int year)
        {
            InitializeComponent();
            day_ = numday;
            month_ = month;
            year_ = year;
            daysInMonth_ = daysInMonth;
            date_ = new DateTime(year_, month_, day_);
            ShowEventsInListbox();

            uploadWeatherForecast();
        }


        /// <summary>
        /// Ustawia numer dnia w kontrolce oraz podświetla dzisiejszy dzień innym kolorem tła.
        /// </summary>
        public void days()
        {
            lbDays.Content = day_.ToString();

            DateTime now = DateTime.Now;
            int currentDay = now.Day;
            int currentMonth = now.Month;
            int currentYear = now.Year;
            //int daysInCurrentMonth = DateTime.DaysInMonth(currentYear, currentMonth);

            if (day_ == currentDay &&
                month_ == currentMonth &&
                year_ == currentYear)
            {
                gridDay.Background = new SolidColorBrush(Color.FromRgb(255, 128, 128));
            }
        }

        /// <summary>
        /// Odpowiada za zaznaczanie wydarzenia w mini listboxie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        /// <summary>
        /// Otwiera nowe okno z listą wydarzeń przypisanych do danej daty.
        /// </summary>
        public void openEventListWindow(object sender, RoutedEventArgs e)
        {
            EventListWindow eventListWindow = new EventListWindow(date_);
            eventListWindow.Show();
        }


        /// <summary>
        /// aktualnie nie używane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Pobiera z bazy danych wydarzenia przypisane do daty i wyświetla je w liście.
        /// </summary>
        private void ShowEventsInListbox()
        {
            List<Entry> listOfEvents = EntriesData.GetEntriesByDate(date_);
            EventsListbox.Items.Clear();

            if (listOfEvents.Count == 0)
            {
                EventsListbox.Items.Add("Brak wydarzeń");
                return;
            }

            foreach (Entry entry in listOfEvents)
            {
                string formattedEntry = $"{entry.StartDate:HH:mm} - {entry.Title}";
                EventsListbox.Items.Add(formattedEntry);
            }
        }

        /// <summary>
        /// Asynchronicznie pobiera prognozę pogody dla daty i aktualizuje interfejs pogodowy w kontrolce.
        /// </summary>
        public async void uploadWeatherForecast()
        {
            DateTime now = DateTime.Now;
            int currentDay = now.Day;
            int currentMonth = now.Month;
            int currentYear = now.Year;
            int daysInCurrentMonth = DateTime.DaysInMonth(currentYear, currentMonth);

            int dayNum = whichDayToForcast();

            if (dayNum != -1)
            {
                var index = await new ForecastService().GetForecastAsync("Warszawa");
                string message = "";

                var temp = index.Forecast.ForecastDays[dayNum].Day.AvgTempC;
                var conditionText = index.Forecast.ForecastDays[dayNum].Day.Condition.Text;
                var conditionIcon = index.Forecast.ForecastDays[dayNum].Day.Condition.Icon;

                avgDayTemp_ = $"{temp}°C";
                dayConditionText_ = $"{conditionText}";
                dayConditionIcon_ = $"https:{conditionIcon}"; // pełny link

                TempLabel.Content = avgDayTemp_;
                IconImage.Source = new BitmapImage(new Uri(dayConditionIcon_));
            }
            //MessageBox.Show($"{avgDayTemp_} {dayConditionText_} {dayConditionIcon_}");
        }

        /// <summary>
        /// Określa, dla którego z najbliższych trzech dni należy pobrać prognozę pogody.
        /// Zwraca indeks dnia (0 - dziś, 1 - jutro, 2 - pojutrze) lub -1, jeśli data nie znajduje się w tym zakresie.
        /// </summary>
        /// <returns>Indeks dnia prognozy lub -1, jeśli niedostępny.</returns>
        public int whichDayToForcast()
        {
            DateTime now = DateTime.Now;
            int currentDay = now.Day;
            int currentMonth = now.Month;
            int currentYear = now.Year;
            int daysInCurrentMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int dayNum = -1;

            if (year_ == currentYear)
                if (month_ == currentMonth)
                    if (day_ == currentDay)
                        return 0;
                    else if (day_ == currentDay + 1)
                        return 1;
                    else if (day_ == currentDay + 2)
                        return 2;
                else if (month_ == currentMonth + 1 && day_ == daysInCurrentMonth - 1)
                    if (day_ == 1)
                        return 2;
                else if (month_ == currentMonth + 1 && day_ == daysInCurrentMonth)
                    if (day_ == 1)
                        return 1;
                    else if (day_ == 2)
                        return 2;
            else if (year_ == currentYear + 1 && month_ == 1)
                if (day_ == daysInCurrentMonth - 1)
                    if (day_ == 1)
                        return 2;
                else if (day_ == daysInCurrentMonth)
                    if (day_ == 1)
                        return 1;
                    else if (day_ == 2)
                        return 2;
            return -1;
        }
    }


}
