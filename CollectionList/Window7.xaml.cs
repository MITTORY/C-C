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
    public partial class Window7 : Window
    {
        public string Value12 { get; set; }
        public string Value13 { get; set; }

        public Window7()
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
            Value12 = LostMoneyBox.Text;
            Value13 = window1.textLost.Text;
            Close();
        }

        private void AddMoneyBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LostMoneyBox.Clear();
        }

        private void Button_AddMoney_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
