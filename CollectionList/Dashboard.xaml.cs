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
    }
}
