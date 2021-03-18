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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RG_PaintDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Point MousePosition { get; }
        public int setter { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnCanvasMouseRightClick(object sender, MouseButtonEventArgs e)
        {
            double x_coord = e.GetPosition(PaintingCanvas).X;
            double y_coord = e.GetPosition(PaintingCanvas).Y;
            // TODO - handle mouse right click
            if(setter == 1)
            {
                DrawEllipse ellipseWindow = new DrawEllipse();
                ellipseWindow.ShowDialog();

                var ellipse = ellipseWindow.ellipseObject;
                if(ellipse != null)
                {
                    ellipse.SetValue(Canvas.LeftProperty, x_coord);
                    ellipse.SetValue(Canvas.TopProperty, y_coord);
                    PaintingCanvas.Children.Add(ellipse);
                }
            }
            else if(setter == 2)
            {
                DrawRectangleWindow drawRectangleWindow = new DrawRectangleWindow();
                drawRectangleWindow.Show();
            }
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            setter = 1;
            EllipseButton.BorderBrush = Brushes.Blue;
            EllipseButton.Background = Brushes.LightGray;
            EllipseButton.BorderThickness = new Thickness(2);

            // reset other buttons
            RectangleButton.BorderBrush = null;
            RectangleButton.Background = Brushes.Black;

            PolygonButton.BorderBrush = null;
            PolygonButton.Background = Brushes.Black;

            ImageButton.BorderBrush = null;
            ImageButton.Background = Brushes.Black;

            UndoButton.BorderBrush = null;
            UndoButton.Background = Brushes.Black;

            RedoButton.BorderBrush = null;
            RedoButton.Background = Brushes.Black;

            ClearButton.BorderBrush = null;
            ClearButton.Background = Brushes.Black;

        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            setter = 2;
            RectangleButton.BorderBrush = Brushes.Blue;
            RectangleButton.Background = Brushes.LightGray;
            RectangleButton.BorderThickness = new Thickness(2);
            
            // reset other buttons
            EllipseButton.BorderBrush = null;
            EllipseButton.Background = Brushes.Black;

            PolygonButton.BorderBrush = null;
            PolygonButton.Background = Brushes.Black;

            ImageButton.BorderBrush = null;
            ImageButton.Background = Brushes.Black;

            UndoButton.BorderBrush = null;
            UndoButton.Background = Brushes.Black;

            RedoButton.BorderBrush = null;
            RedoButton.Background = Brushes.Black;

            ClearButton.BorderBrush = null;
            ClearButton.Background = Brushes.Black;

        }
    }
}
