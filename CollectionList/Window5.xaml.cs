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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CollectionList
{
    public partial class Window5 : System.Windows.Window
    {
        public string Value3 { get; set; }
        public string Value4 { get; set; }

        public Window5()
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

        private void Button_ADD_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            Value3 = Name.Text; 
            Value4 = Money.Text;
        
            window1.EditBox.IsEnabled = true;
            Close();
        }

        private void Button_ADD_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
