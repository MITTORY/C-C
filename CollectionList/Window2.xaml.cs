using CollectionList.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CollectionList
{
    public partial class Window2 : System.Windows.Window
    {
        public Window2()
        {
            InitializeComponent();
            Loaded += Window2_Loaded;
            Closing += Window2_Closing;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Home(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            Close();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ComboBox.Items.Clear();
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCost1.SelectedIndex > 0)
            {
                if (listBoxCost1.SelectedIndex != -1)
                {
                    int index = listBoxCost1.SelectedIndex;
                    String text = listBoxCost1.SelectedItem.ToString();
                    listBoxCost1.Items.RemoveAt(listBoxCost1.SelectedIndex);
                    listBoxCost1.Items.Insert(index - 1, text);
                }
            }
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCost1.SelectedIndex < listBoxCost1.Items.Count)
            {
                if (listBoxCost1.SelectedIndex != -1)
                {
                    int index = listBoxCost1.SelectedIndex;
                    String text = listBoxCost1.SelectedItem.ToString();
                    listBoxCost1.Items.RemoveAt(listBoxCost1.SelectedIndex);
                    listBoxCost1.Items.Insert(index + 1, text);
                }
            }
        }

        private void ClearRed_Click(object sender, RoutedEventArgs e)
        {
            listBoxCost1.Items.Clear();
        }

        private void DeleteRed_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCost1.SelectedIndex != -1)
                listBoxCost1.Items.RemoveAt(listBoxCost1.SelectedIndex);
            else
                System.Windows.Forms.MessageBox.Show("Выберите категорию и(или) введите текст!");
        }

        private void OpenRed_Click(object sender, RoutedEventArgs e)
        {
            listBoxCost1.Items.Clear();


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
                        listBoxCost1.Items.Add(line);

                }
            }
        }

        private void SaveRed_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";


            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter writter = new StreamWriter(dlg.FileName);

                for (int i = 0; i < listBoxCost1.Items.Count; i++)
                {
                    writter.WriteLine(listBoxCost1.Items[i].ToString());
                }
                System.Windows.Forms.MessageBox.Show("Сохранение прошло успешно!");
                writter.Close();
            }
            dlg.Dispose();
        }

        private void AddBox_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox.SelectedItem == null || string.IsNullOrWhiteSpace(TextBox.Text))
            {
                System.Windows.Forms.MessageBox.Show("Выберите категорию и(или) введите текст!");
            }
            else
            {
                listBoxCost1.Items.Add("[" + ComboBox.SelectedItem.ToString() + "]" + " " + TextBox.Text.ToString());
                TextBox.Clear();
            }
        }

        private void AddCategor_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CategorBox.Text))
            {
                System.Windows.Forms.MessageBox.Show("Введите текст!");
                return;
            }

            ComboBox.Items.Add(CategorBox.Text);
            CategorBox.Clear();
        }

        private void SaveBox_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCost1.SelectedItem != null)
            {
                int selectedIndex = listBoxCost1.SelectedIndex;
                listBoxCost1.Items[selectedIndex] = TextBox.Text;
                TextBox.Clear();
                SaveBox.IsEnabled = false;
            }
        }

        private void listBoxCost_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBoxCost1.SelectedItem != null)
            {
                string selectedItem = listBoxCost1.SelectedItem.ToString();
                TextBox.Text = selectedItem;
                SaveBox.IsEnabled = true;
            }
        }

        private void AddRazdel_Click(object sender, RoutedEventArgs e)
        {
            listBoxCost1.Items.Add("======================================================");
        }

        private void Window2_Loaded(object sender, RoutedEventArgs e)
        {
            listBoxCost1.Items.Clear();
            string[] listBox2State = Settings.Default.ListBox2State.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in listBox2State)
            {
                listBoxCost1.Items.Add(item);
            }
        }

        private void Window2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string listBox2State = string.Empty;
            foreach (var item in listBoxCost1.Items)
            {
                listBox2State += item.ToString() + "|";
            }
            Settings.Default.ListBox2State = listBox2State;
            Settings.Default.Save();
        }
    }
}