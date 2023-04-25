using ControlzEx.Standard;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using CollectionList.Properties;

namespace CollectionList
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            Loaded += Window1_Loaded;
            Closing += Window1_Closing;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Home(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            Close();
        }

        private void Button_Draw(object sender, RoutedEventArgs e)
        {
            Window3 draw = new Window3();
            draw.Show();
            Close();
        }

        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Collection(object sender, RoutedEventArgs e)
        {
            Window2 collection = new Window2();
            collection.Show();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                System.Windows.Forms.MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            listBoxCost.Items.Add(textBox2.Text + " - " + textBox3.Text + " рублей");
            double textBox5Value = Convert.ToDouble(textBox5.Text);
            double textBox3Value = Convert.ToDouble(textBox3.Text);
            double textBox6Value = Convert.ToDouble(textBox6.Text);
            double result = textBox5Value - textBox3Value;
            double result1 = textBox3Value + textBox6Value;
            textBox5.Text = result.ToString();
            textBox6.Text = result1.ToString();
            textBox2.Clear();
            textBox3.Clear();
            Save_button.IsEnabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double value) && value >= 0) // преобразование строки в целочисленное значение и проверка на неотрицательность
            {
                listBoxCost.Items.Add("Начальное состояние: " + value.ToString() + " рублей");
                textBox5.Text = value.ToString();
                textBox1.Clear();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Введите корректное значение!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox5.Text, out double value) && value >= 0 | double.TryParse(textBox6.Text, out double value1) && value1 >= 0) // преобразование строки в целочисленное значение и проверка на неотрицательность
            {
                listBoxCost.Items.Add("У вас осталось " + value.ToString() + " рублей\n" + "Общая сумма затрат " + value1.ToString() + " рублей");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Введите начальное состояние!");
            }
        }

        private void Down_button_Click(object sender, EventArgs e)
        {
            if (listBoxCost.SelectedIndex < listBoxCost.Items.Count)
            {
                int index = listBoxCost.SelectedIndex;
                String text = listBoxCost.SelectedItem.ToString();
                listBoxCost.Items.RemoveAt(listBoxCost.SelectedIndex);
                listBoxCost.Items.Insert(index + 1, text);
            }
        }

        private void Up_button_Click(object sender, EventArgs e)
        {
            if (listBoxCost.SelectedIndex > 0)
            {
                int index = listBoxCost.SelectedIndex;
                String text = listBoxCost.SelectedItem.ToString();
                listBoxCost.Items.RemoveAt(listBoxCost.SelectedIndex);
                listBoxCost.Items.Insert(index - 1, text);
            }
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            if (listBoxCost.SelectedIndex != 0)
                listBoxCost.Items.RemoveAt(listBoxCost.SelectedIndex);
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            listBoxCost.Items.Clear();
            textBox5.Clear();
            textBox6.Clear();
            Save_button.IsEnabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBoxCost.SelectedItem != null)
            {
                int selectedIndex = listBoxCost.SelectedIndex;
                listBoxCost.Items[selectedIndex] = textBox4.Text;
                textBox4.Clear();
                textBox4.IsEnabled = false;
                button4.IsEnabled = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Введите начальное состояние!\n\tНапример: 37500");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Введите в первую строку название, а во вторую цену!");
        }

        private void listBox1_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBoxCost.SelectedItem != null)
            {
                string selectedItem = listBoxCost.SelectedItem.ToString();
                textBox4.Text = selectedItem;
                textBox4.IsEnabled = true;
                button4.IsEnabled = true;
            }
        }

        private void Save_button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter writter = new StreamWriter(dlg.FileName);

                for (int i = 0; i < listBoxCost.Items.Count; i++)
                {
                    writter.WriteLine(listBoxCost.Items[i].ToString());
                }
                System.Windows.Forms.MessageBox.Show("Сохранение прошло успешно!");
                writter.Close();
            }
            dlg.Dispose();
        }

        private void Load_button_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;


                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string[] lines = File.ReadAllLines(openFileDialog.FileName);
                    foreach (string line in lines)
                        listBoxCost.Items.Add(line);

                }
            }
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            listBoxCost.Items.Clear();
            string[] listBoxState = Settings.Default.ListBoxState.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in listBoxState)
            {
                listBoxCost.Items.Add(item);
            }
            textBox5.Text = Settings.Default.TextBox5State;
            textBox6.Text = Settings.Default.TextBox6State;
        }

        private void Window1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string listBoxState = "";
            foreach (object item in listBoxCost.Items)
            {
                listBoxState += item.ToString() + "|";
            }
            Settings.Default.ListBoxState = listBoxState;
            Settings.Default.TextBox5State = textBox5.Text;
            Settings.Default.TextBox6State = textBox6.Text;
            Settings.Default.Save();
        }
    }
}
