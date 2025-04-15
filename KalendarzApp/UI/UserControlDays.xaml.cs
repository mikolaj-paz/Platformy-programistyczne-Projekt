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

namespace KalendarzApp
{
    /// <summary>
    /// Logika interakcji dla klasy UserControlDays.xaml
    /// </summary>
    public partial class UserControlDays : UserControl
    {
        public UserControlDays()
        {
            InitializeComponent();
        }

        public void days(int numday, int month, int year)
        {
            lbDays.Content = numday.ToString();

            DateTime now = DateTime.Now;
            int curr_day = now.Day;
            int curr_month = now.Month;
            int curr_year = now.Year;

            if (numday == curr_day &&
                month == curr_month &&
                year == curr_year)
            {
                gridDay.Background = new SolidColorBrush(Color.FromRgb(255, 128, 128));
            }   
        }

        public void mark_day(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
                dayUC.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
            else
            {
                checkBox.IsChecked = false;
                dayUC.Background = new SolidColorBrush(Color.FromRgb(209, 209, 209));
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
