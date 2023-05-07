using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CollectionList
{
    public partial class Window4 : Window
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }

        public Window4()
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

        private void Button_Accept_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            Value1 = textBox1.Text;
            Value2 = window1.textBalance.Text;
            Close();
        }

        private void textBox1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            textBox1.Clear();
        }

        private void Button_Accept_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
