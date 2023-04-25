using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CollectionList
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
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

        private void Button_Home(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            Close();
        }

        private void Button_Cost(object sender, RoutedEventArgs e)
        {
            Window1 cost = new Window1();
            cost.Show();
            Close();
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

        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (canvas != null)
            {
                var drawingAttributes = canvas.DefaultDrawingAttributes;
                Double newSize = Math.Round(BrushRadiusSlider.Value, 0);
                drawingAttributes.Width = newSize;
                drawingAttributes.Height = newSize;
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            canvas.Background = new SolidColorBrush(Colors.White);
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(canvas);
            ImageBrush imageBrush = new ImageBrush(renderTargetBitmap);
            canvas.Strokes.Clear();
            canvas.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp) | *.jpg; *.jpeg; *.png; *.bmp";

            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;
                BitmapImage image = new BitmapImage(new Uri(filename));
                canvas.Background = new ImageBrush(image);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg|BMP (*.bmp)|*.bmp";
            if (dialog.ShowDialog() == true)
            {
                var encoder = new PngBitmapEncoder();
                var brush = canvas.Background as ImageBrush;
                if (brush != null && brush.ImageSource != null)
                {
                    var bmp = new RenderTargetBitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight, 96, 96, PixelFormats.Default);
                    bmp.Render(canvas);
                    encoder.Frames.Add(BitmapFrame.Create(bmp));
                    using (FileStream stream = new FileStream(dialog.FileName, FileMode.Create))
                    {
                        encoder.Save(stream);
                    }
                }
            }
        }

        private void Black_Click(object sender, RoutedEventArgs e)
        {
            canvas.DefaultDrawingAttributes.Color = Colors.Black;
        }

        private void White_Click(object sender, RoutedEventArgs e)
        {
            canvas.DefaultDrawingAttributes.Color = Colors.White;
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            canvas.DefaultDrawingAttributes.Color = Colors.Red;
        }

        private void Orange_Click(object sender, RoutedEventArgs e)
        {
            canvas.DefaultDrawingAttributes.Color = Colors.Orange;
        }

        private void Yellow_Click(object sender, RoutedEventArgs e)
        {
            canvas.DefaultDrawingAttributes.Color = Colors.Yellow;
        }

        private void Green_Click(object sender, RoutedEventArgs e)
        {
            canvas.DefaultDrawingAttributes.Color = Colors.Green;
        }

        private void BlueLight_Click(object sender, RoutedEventArgs e)
        {
            canvas.DefaultDrawingAttributes.Color = Colors.AliceBlue;
        }

        private void Blue_Click(object sender, RoutedEventArgs e)
        {
            canvas.DefaultDrawingAttributes.Color = Colors.Blue;
        }

        private void Purple_Click(object sender, RoutedEventArgs e)
        {
            canvas.DefaultDrawingAttributes.Color = Colors.Purple;
        }

        private void Pensil_Click(object sender, RoutedEventArgs e)
        {
            canvas.EditingMode = InkCanvasEditingMode.Ink;
            DrawingAttributes pencilDA = new DrawingAttributes();
            pencilDA.Color = Colors.Black;
            pencilDA.IsHighlighter = false;
            pencilDA.StylusTip = StylusTip.Ellipse;
            Double newSize = Math.Round(BrushRadiusSlider.Value, 0);
            pencilDA.Width = newSize;
            pencilDA.Height = newSize;
            canvas.DefaultDrawingAttributes = pencilDA;
        }

        DrawingAttributes highlighterDA;
        private void Brush_Click(object sender, RoutedEventArgs e)
        {
            canvas.EditingMode = InkCanvasEditingMode.Ink;
            highlighterDA = new DrawingAttributes();
            highlighterDA.Color = Colors.Black;
            highlighterDA.IsHighlighter = true;
            highlighterDA.IgnorePressure = true;
            Double newSize = Math.Round(BrushRadiusSlider.Value, 0);
            highlighterDA.Width = newSize;
            highlighterDA.Height = newSize;
            canvas.DefaultDrawingAttributes = highlighterDA;
        }

        private void Eraser_Click(object sender, RoutedEventArgs e)
        {

            if (canvas.EditingMode == InkCanvasEditingMode.Ink)
                canvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            else
                canvas.EditingMode = InkCanvasEditingMode.Ink;

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            canvas.Strokes.Clear();
            canvas.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void FBlack_Click(object sender, RoutedEventArgs e)
        {
            canvas.Background = new SolidColorBrush(Colors.Black);
        }

        private void FWhite_Click(object sender, RoutedEventArgs e)
        {
            canvas.Background = new SolidColorBrush(Colors.White);
        }

        private void FRed_Click(object sender, RoutedEventArgs e)
        {
            canvas.Background = new SolidColorBrush(Colors.Red);
        }

        private void FOrange_Click(object sender, RoutedEventArgs e)
        {
            canvas.Background = new SolidColorBrush(Colors.Orange);
        }

        private void FYellow_Click(object sender, RoutedEventArgs e)
        {
            canvas.Background = new SolidColorBrush(Colors.Yellow);
        }

        private void FGreen_Click(object sender, RoutedEventArgs e)
        {
            canvas.Background = new SolidColorBrush(Colors.Green);
        }

        private void FBlueLight_Click(object sender, RoutedEventArgs e)
        {
            canvas.Background = new SolidColorBrush(Colors.AliceBlue);
        }

        private void FBlue_Click(object sender, RoutedEventArgs e)
        {
            canvas.Background = new SolidColorBrush(Colors.Blue);
        }

        private void FPurple_Click(object sender, RoutedEventArgs e)
        {
            canvas.Background = new SolidColorBrush(Colors.Purple);
        }
    }
}
