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
    /// Interaction logic for DrawRectangleWindow.xaml
    /// </summary>
    public partial class DrawRectangleWindow : Window
    {
        public Rectangle rectangleObject = new Rectangle();
        public DrawRectangleWindow()
        {
            InitializeComponent();
        }

        public DrawRectangleWindow(Rectangle rectangle) : this()
        {
            Title = "Modify Rectangle";
            drawButton.Content = "Apply";
            rectangleObject = rectangle;
            UpdateProperties(rectangle.Width, rectangle.Height, rectangle.Fill, rectangle.Stroke, rectangle.StrokeThickness);
        }

        private void UpdateProperties(double width, double height, Brush fill, Brush border, double borderThickness)
        {
            widthIn.Value = (int)width;
            heightIn.Value = (int)height;
            fillColorIn.SelectedColor = (Color)fill.GetValue(SolidColorBrush.ColorProperty);
            borderColorIn.SelectedColor = (Color)border.GetValue(SolidColorBrush.ColorProperty);
            borderThicknessIn.Value = (int)borderThickness;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public bool validate()
        {
            if (widthIn.Text.Trim().Equals("") || int.Parse(widthIn.Text) < 10)
            {
                widthIn.BorderBrush = Brushes.Red;
                widthIn.BorderThickness = new Thickness(1);
                MessageBox.Show("Width must be at least 10.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            {
                widthIn.BorderBrush = Brushes.DarkGray;
            }

            if (heightIn.Text.Trim().Equals("") || int.Parse(heightIn.Text) < 10)
            {
                heightIn.BorderBrush = Brushes.Red;
                heightIn.BorderThickness = new Thickness(1);
                MessageBox.Show("Height must be at least 10.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                widthIn.BorderBrush = Brushes.DarkGray;
            }

            if (fillColorIn.SelectedColor == null)
            {
                fillColorIn.BorderBrush = Brushes.Red;
                fillColorIn.BorderThickness = new Thickness(1);
                MessageBox.Show("Fill color must be selected.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                widthIn.BorderBrush = Brushes.DarkGray;
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
                widthIn.BorderBrush = Brushes.DarkGray;
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
                widthIn.BorderBrush = Brushes.DarkGray;
            }

            return true;
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {
                rectangleObject = new Rectangle()
                {
                    Width = int.Parse(widthIn.Text),
                    Height = int.Parse(heightIn.Text),
                    Fill = new SolidColorBrush(fillColorIn.SelectedColor.Value),
                    Stroke = new SolidColorBrush(borderColorIn.SelectedColor.Value),
                    StrokeThickness = int.Parse(borderThicknessIn.Text)
                };

                Close();
            }
        }
    }
}
