using CollectionList.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace CollectionList
{
    public partial class Dashboard : Window
    {
        private int loveCount = 0;

        public Dashboard()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Cost(object sender, RoutedEventArgs e)
        {
            Window1 cost = new Window1();
            cost.Show();
            Close();
        }

        private void Button_Draw(object sender, RoutedEventArgs e)
        {
            Window3 draw = new Window3();
            draw.Show();
            Close();
        }

        private void Button_Collection(object sender, RoutedEventArgs e)
        {
            Window2 collection = new Window2();
            collection.Show();
            Close();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Dashboard_Loaded(object sender, RoutedEventArgs e)
        {
            // Загрузить значение ProgressBar и TextBlock из настроек приложения
            double progressBarValue = Settings.Default.ProgressBarState;
            string textBlockText = Settings.Default.ProgressTextState;

            // Проверить, что значение ProgressBar в диапазоне от его минимального до максимального значения
            if (progressBarValue >= ProgressBar.Minimum && progressBarValue <= ProgressBar.Maximum)
            {
                // Установить значение ProgressBar, если оно в допустимом диапазоне
                ProgressBar.Value = progressBarValue;
            }

            // Установить значение TextBlock
            ProgressText.Text = textBlockText;

            // Загрузить значение loveCount из настроек приложения
            loveCount = Settings.Default.LoveCountState;

            // Вывести текущую дату в TextBlock1
            DateTime startDate = new DateTime(2020, 11, 15);
            TimeSpan timeSinceStart = DateTime.Today - startDate;
            int daysSinceStart = (int)timeSinceStart.TotalDays;

            // Вывести дату в TextBlock1
            TextBlock1.Text = startDate.ToString("dd.MM.yyyy");

            // Вывести количество дней в TextBlock2
            TextBlock2.Text = daysSinceStart.ToString();
        }

        private void Dashboard_Closing(object sender, CancelEventArgs e)
        {
            // Сохранить значение ProgressBar, TextBlock и loveCount в настройках приложения
            Settings.Default.ProgressBarState = ProgressBar.Value;
            Settings.Default.ProgressTextState = ProgressText.Text;
            Settings.Default.LoveCountState = loveCount;
            Settings.Default.Save();
        }

        private void LOVE_Click(object sender, RoutedEventArgs e)
        {
            ProgressBar.Value += 1;
            ProgressText.Text = ProgressBar.Value.ToString() + "%";
            if (ProgressBar.Value == ProgressBar.Maximum)
            {
                MessageBox.Show("Любовь усилена!", "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Information);
                ProgressBar.Value = 0;
                ProgressText.Text = "0%";
                loveCount++;
                loveCountText.Text = loveCount.ToString();
            }
        }
    }
}
