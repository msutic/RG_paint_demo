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
        private PointCollection points = new PointCollection();
        public int setter { get; set; }
        public List<Shape> Shapes { get; set; } = new List<Shape>();
        public List<Shape> DeletedShapes { get; set; } = new List<Shape>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CanvasLeftMouse_Click(object sender, MouseButtonEventArgs e)
        {
            if(setter == 3)
            {
                DrawPolygonWindow drawPolygonWindow = new DrawPolygonWindow(new PointCollection(points));
                drawPolygonWindow.ShowDialog();

                var polygon = drawPolygonWindow.polygonObject;
                if(polygon != null)
                {
                    PaintingCanvas.Children.Add(polygon);
                    Shapes.Add(polygon);
                }

                points.Clear();
            }
            else
            {
                if (e.OriginalSource is Ellipse)
                {
                    Ellipse SelectedEllipse = (Ellipse)e.OriginalSource;
                    DrawEllipse drawEllipseWindow = new DrawEllipse(SelectedEllipse);
                    drawEllipseWindow.ShowDialog();
                    UpdateObjectValues(PaintingCanvas.Children.IndexOf(SelectedEllipse), drawEllipseWindow.ellipseObject);
                }
                else if(e.OriginalSource is Rectangle)
                {
                    Rectangle SelectedRectangle = (Rectangle)e.OriginalSource;
                    if(SelectedRectangle.Stroke != null)
                    {
                        DrawRectangleWindow drawRectangleWindow = new DrawRectangleWindow(SelectedRectangle);
                        drawRectangleWindow.ShowDialog();
                        UpdateObjectValues(PaintingCanvas.Children.IndexOf(SelectedRectangle), drawRectangleWindow.rectangleObject);
                    }
                    
                }
                else if (e.OriginalSource is Polygon)
                {
                    Polygon SelectedPolygon = (Polygon)e.OriginalSource;
                    DrawPolygonWindow drawPolygonWindow = new DrawPolygonWindow(SelectedPolygon);
                    drawPolygonWindow.ShowDialog();
                    UpdateObjectValues(PaintingCanvas.Children.IndexOf(SelectedPolygon), drawPolygonWindow.polygonObject);
                }

            }
        }
        

        private void OnCanvasMouseRightClick(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(PaintingCanvas);
            double x_coord = position.X;
            double y_coord = position.Y;
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
                    Shapes.Add(ellipse);
                    //ellipse.MouseLeftButtonUp += Ellipse_MouseLeftButtonUp;
                }
            }
            else if(setter == 2)
            {
                DrawRectangleWindow drawRectangleWindow = new DrawRectangleWindow();
                drawRectangleWindow.ShowDialog();

                var rectangle = drawRectangleWindow.rectangleObject;
                if (rectangle != null)
                {
                    rectangle.SetValue(Canvas.LeftProperty, x_coord);
                    rectangle.SetValue(Canvas.TopProperty, y_coord);
                    PaintingCanvas.Children.Add(rectangle);
                    Shapes.Add(rectangle);
                }
            }
            else if(setter == 3)
            {
                points.Add(position);
            }
            else if (setter == 4)
            {
                ImageWindow window = new ImageWindow();
                window.ShowDialog();

                Rectangle image = window.r;
                if(image != null)
                {
                    image.SetValue(Canvas.LeftProperty, x_coord);
                    image.SetValue(Canvas.TopProperty, y_coord);
                    PaintingCanvas.Children.Add(image);
                    Shapes.Add(image);
                }
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

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            Shape s;
            if(Shapes.Count > 0)
            {
                s = Shapes[Shapes.Count - 1];       //get the last shape painted
                Shapes.Remove(s);
                DeletedShapes.Add(s);
                PaintingCanvas.Children.Remove(s);
            }
        }

        private void PolygonButton_Click(object sender, RoutedEventArgs e)
        {
            setter = 3;
            PolygonButton.BorderBrush = Brushes.Blue;
            PolygonButton.Background = Brushes.LightGray;
            PolygonButton.BorderThickness = new Thickness(2);

            // reset other buttons
            EllipseButton.BorderBrush = null;
            EllipseButton.Background = Brushes.Black;

            RectangleButton.BorderBrush = null;
            RectangleButton.Background = Brushes.Black;

            ImageButton.BorderBrush = null;
            ImageButton.Background = Brushes.Black;

            UndoButton.BorderBrush = null;
            UndoButton.Background = Brushes.Black;

            RedoButton.BorderBrush = null;
            RedoButton.Background = Brushes.Black;

            ClearButton.BorderBrush = null;
            ClearButton.Background = Brushes.Black;
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            setter = 4;
            ImageButton.BorderBrush = Brushes.Blue;
            ImageButton.Background = Brushes.LightGray;
            ImageButton.BorderThickness = new Thickness(2);

            // reset other buttons
            EllipseButton.BorderBrush = null;
            EllipseButton.Background = Brushes.Black;

            RectangleButton.BorderBrush = null;
            RectangleButton.Background = Brushes.Black;

            PolygonButton.BorderBrush = null;
            PolygonButton.Background = Brushes.Black;

            UndoButton.BorderBrush = null;
            UndoButton.Background = Brushes.Black;

            RedoButton.BorderBrush = null;
            RedoButton.Background = Brushes.Black;

            ClearButton.BorderBrush = null;
            ClearButton.Background = Brushes.Black;
        }

        private void UpdateObjectValues(int index, object objectToUpdate)
        {
            var fe = objectToUpdate as FrameworkElement;

            PaintingCanvas.Children[index].SetValue(WidthProperty, fe.Width);
            PaintingCanvas.Children[index].SetValue(HeightProperty, fe.Height);

            if (objectToUpdate is Shape shape)
            {
                PaintingCanvas.Children[index].SetValue(Shape.FillProperty, shape.Fill);
                PaintingCanvas.Children[index].SetValue(Shape.StrokeProperty, shape.Stroke);
                PaintingCanvas.Children[index].SetValue(Shape.StrokeThicknessProperty, shape.StrokeThickness);
            }
            else if (objectToUpdate is Image img)
            {
                PaintingCanvas.Children[index].SetValue(Image.SourceProperty, img.Source);
            }
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            Shape s;
            if(DeletedShapes.Count > 0)
            {
                s = DeletedShapes[DeletedShapes.Count - 1];
                DeletedShapes.Remove(s);
                Shapes.Add(s);
                PaintingCanvas.Children.Add(s);
            }
        }
    }
}
