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

namespace RG_PaintDemo
{
    /// <summary>
    /// Interaction logic for DrawPolygonWindow.xaml
    /// </summary>
    public partial class DrawPolygonWindow : Window
    {
        public Polygon polygonObject = new Polygon();
        private PointCollection points;

        public DrawPolygonWindow()
        {
            InitializeComponent();
        }

        public DrawPolygonWindow(PointCollection points):this()
        {
            this.points = points;
        }

        public bool validate()
        {
            if (fillColorIn.SelectedColor == null)
            {
                fillColorIn.BorderBrush = Brushes.Red;
                fillColorIn.BorderThickness = new Thickness(1);
                MessageBox.Show("Fill color must be selected.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                fillColorIn.BorderBrush = Brushes.DarkGray;
            }

            if (borderColorIn.SelectedColor == null)
            {
                borderColorIn.BorderBrush = Brushes.Red;
                borderColorIn.BorderThickness = new Thickness(1);
                MessageBox.Show("Border color must be selected.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                borderColorIn.BorderBrush = Brushes.DarkGray;
            }

            if (borderThicknessIn.Text.Trim().Equals("") || int.Parse(borderThicknessIn.Text) < 1)
            {
                borderThicknessIn.BorderBrush = Brushes.Red;
                borderThicknessIn.BorderThickness = new Thickness(1);
                MessageBox.Show("Border thickness must be at least 1.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                borderThicknessIn.BorderBrush = Brushes.DarkGray;
            }

            return true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {
                polygonObject = new Polygon()
                {
                    Fill = new SolidColorBrush(fillColorIn.SelectedColor.Value),
                    Stroke = new SolidColorBrush(borderColorIn.SelectedColor.Value),
                    StrokeThickness = int.Parse(borderThicknessIn.Text),
                    Points = points
                };

                Close();
            }
        }
    }
}
