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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            DrawEllipse ellipseWindow = new DrawEllipse();
            ellipseWindow.Show();
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            DrawRectangleWindow drawRectangleWindow = new DrawRectangleWindow();
            drawRectangleWindow.Show();
        }
    }
}
