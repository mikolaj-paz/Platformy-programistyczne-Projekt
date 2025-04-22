using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using KalendarzApp.Models;
using KalendarzApp.Data;

namespace KalendarzApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int month, year;
        string[] monthNames = { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" };
        
        public MainWindow()
        {
            InitializeComponent();
            startDisplay();
            StartClock();
        }

        private void displayCalendar(int month, int year)
        {
            lbMonthYear.Content = monthNames[month - 1] + " " + year;

            DateTime startOfTheMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int daysOfTheWeek = Convert.ToInt32(startOfTheMonth.DayOfWeek.ToString("d"));

            for (int i = 1; i < daysOfTheWeek; i++)
            {
                UserControl1 ucBlank = new UserControl1();
                dayContainer.Children.Add(ucBlank);
            }
            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucDays = new UserControlDays(i, month, year);
                //ucDays.days(i, month, year);
                ucDays.days();
                dayContainer.Children.Add(ucDays);
            }
        }

        private void startDisplay()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            displayCalendar(month, year);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            month--;
            if (month == 0)
            {
                year--;
                month = 12;
            }
            dayContainer.Children.Clear();
            displayCalendar(month, year);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            month++;
            if (month == 13)
            {
                year++;
                month = 1;
            }
            dayContainer.Children.Clear();
            displayCalendar(month, year);
        }


        private DispatcherTimer timer;
        private void StartClock()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ClockText.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}