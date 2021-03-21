using Microsoft.Win32;
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
    /// Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window
    {
        public string ImgDest { get; set; }
        public Rectangle r = new Rectangle(); 
        public Image imgObject;
        public ImageWindow()
        {
            InitializeComponent();
        }

        private void ImagePathInButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select an image";
            dialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == true)
            {
                ImgDest = dialog.FileName;
                imgPhoto.Source = new BitmapImage(new Uri(dialog.FileName));
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {
                r = new Rectangle()
                {
                    Fill = new ImageBrush(imgPhoto.Source),
                    Width = int.Parse(widthIn.Text),
                    Height = int.Parse(heightIn.Text)
                };
                Close();
            }
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

            if(imgPhoto.Source == null)
            {
                MessageBox.Show("Invalid image source.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
