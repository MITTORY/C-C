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

namespace CollectionList
{
    /// <summary>
    /// Логика взаимодействия для Window6.xaml
    /// </summary>
    public partial class Window6 : Window
    {
        public string Value10 { get; set; }
        public string Value11 { get; set; }

        public Window6()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_AddMoney_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            Value10 = AddMoneyBox.Text;
            Value11 = window1.textBalance.Text;
            Close();
        }

        private void AddMoneyBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddMoneyBox.Clear();
        }

        private void Button_AddMoney_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
