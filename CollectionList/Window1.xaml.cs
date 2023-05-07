using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CollectionList.Properties;
using LiveCharts;
using LiveCharts.Wpf;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ControlzEx.Standard;
using System.Security.Policy;
using System.Windows.Forms;

namespace CollectionList
{
    public partial class Window1 : System.Windows.Window
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Window5 window5 = new Window5();
            window5.Show();
            window5.Closed += (s, args) =>
            {
                double textValue = Convert.ToDouble(window5.Value4);
                double textMoney = Convert.ToDouble(textLost.Text);
                double textBalan = Convert.ToDouble(textBalance.Text);

                double resultLost = textValue + textMoney;
                double resultBalance = textBalan - textValue;

                textLost.Text = Convert.ToString(resultLost);
                textBalance.Text = Convert.ToString(resultBalance);

                if (textMoney < textValue)
                {
                    MuchCost.Text = Convert.ToString(window5.Value3 + " - " + window5.Value4 + " рублей");
                }

                ListCost.Items.Add(window5.Value3 + " - " + window5.Value4 + " рублей" + " [" + Data.Text + "]");
            };
        }

        private void Down_button_Click(object sender, EventArgs e)
        {
            if (ListCost.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Выберите элемент для перемещения вниз!", "Ошибка!");
            }
            else if (ListCost.SelectedIndex < ListCost.Items.Count - 1)
            {
                int index = ListCost.SelectedIndex;
                string text = ListCost.SelectedItem.ToString();
                ListCost.Items.RemoveAt(index);
                ListCost.Items.Insert(index + 1, text);
                ListCost.SelectedIndex = index + 1;
            }
        }

        private void Up_button_Click(object sender, EventArgs e)
        {
            if (ListCost.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Выберите элемент для перемещения вверх!", "Ошибка!");
            }
            else if (ListCost.SelectedIndex > 0)
            {
                int index = ListCost.SelectedIndex;
                string text = ListCost.SelectedItem.ToString();
                ListCost.Items.RemoveAt(index);
                ListCost.Items.Insert(index - 1, text);
                ListCost.SelectedIndex = index - 1;
            }
        }


        private void Delete_button_Click(object sender, EventArgs e)
        {
            if (ListCost.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Выберите элемент для удаления!", "Ошибка!");
            }
            else
            {
                ListCost.Items.Remove(ListCost.SelectedItem);
            }
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            ListCost.Items.Clear();
            textBalance.Text = "0";
            textLost.Text = "0";
            MuchCost.Clear();
        }

        private void listBox1_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListCost.SelectedItem != null)
            {
                string selectedItem = ListCost.SelectedItem.ToString();
                EditBox.Text = selectedItem;
                EditBox.IsEnabled = true;
            }
        }

        private void Save_button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter writter = new StreamWriter(dlg.FileName);

                for (int i = 0; i < ListCost.Items.Count; i++)
                {
                    writter.WriteLine(ListCost.Items[i].ToString());
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
                        ListCost.Items.Add(line);
                }
            }
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            ListCost.Items.Clear();
            string[] listBoxState = Settings.Default.ListCostState.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in listBoxState)
            {
                ListCost.Items.Add(item);
            }
            textBalance.Text = Settings.Default.TextBalanceState;
            textLost.Text = Settings.Default.TextLostState;
            MuchCost.Text = Settings.Default.TextMuchState;
        }

        private void Window1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string listBoxState = "";
            foreach (object item in ListCost.Items)
            {
                listBoxState += item.ToString() + "|";
            }
            Settings.Default.ListCostState = listBoxState;
            Settings.Default.TextBalanceState = textBalance.Text;
            Settings.Default.TextLostState = textLost.Text;
            Settings.Default.TextMuchState = MuchCost.Text;
            Settings.Default.Save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window4 window4 = new Window4();
            window4.Show();
            window4.Closed += (s, args) =>
            {
                if (!string.IsNullOrEmpty(window4.Value1))
                {
                    textBalance.Text = window4.Value1;
                    ListCost.Items.Add(window4.Value1 + " рублей");
                }
            };
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListCost.Items.Add("       У вас осталось: " + textBalance.Text + " рублей\n" + "Общая сумма затрат: " + textLost.Text + " рублей");
        }

        private void EditBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (ListCost.SelectedItem != null)
                {
                    int selectedIndex = ListCost.SelectedIndex;
                    ListCost.Items[selectedIndex] = EditBox.Text;
                    EditBox.Clear();
                    EditBox.IsEnabled = false;
                }
            }
        }

        private void AddMoney_Click(object sender, RoutedEventArgs e)
        {
            Window6 window6 = new Window6();
            window6.Show();
            window6.Closed += (s, args) =>
            {
                double textAddMoney = Convert.ToDouble(window6.AddMoneyBox.Text);
                double textBalanceMoney = Convert.ToDouble(textBalance.Text);

                double resultAddMoney = textAddMoney + textBalanceMoney;

                textBalance.Text = Convert.ToString(resultAddMoney);
            };
        }

        private void LostMoney_Click(object sender, RoutedEventArgs e)
        {
            Window7 window7 = new Window7();
            window7.Show();
            window7.Closed += (s, args) =>
            {
                double textLostyMoney = Convert.ToDouble(window7.LostMoneyBox.Text);
                double textLouseMoney = Convert.ToDouble(textLost.Text);

                double resultLostyMoney = textLouseMoney - textLostyMoney;

                textLost.Text = Convert.ToString(resultLostyMoney);
            };
        }
    }
}
