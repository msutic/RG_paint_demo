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
        private int clickedRight = 0;

        public Stack<Command> UndoableCommands { get; set; } = new Stack<Command>();
        public Stack<Command> RedoableCommands { get; set; } = new Stack<Command>();

        public MainWindow()
        {
            InitializeComponent();
            RedoButton.IsEnabled = false;
        }

        private void CanvasLeftMouse_Click(object sender, MouseButtonEventArgs e)
        {
            if(setter == 3 && clickedRight >=3)
            {
                DrawPolygonWindow drawPolygonWindow = new DrawPolygonWindow(new PointCollection(points));
                drawPolygonWindow.ShowDialog();

                var polygon = drawPolygonWindow.polygonObject;
                if (polygon != null)
                {
                    PaintingCanvas.Children.Add(polygon);
                    UndoableCommands.Push(new Command("add", PaintingCanvas.Children.IndexOf(polygon), polygon));
                    RedoableCommands.Clear();
                }

                points.Clear();
                clickedRight = 0;
                
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
                    UndoableCommands.Push(new Command("add", PaintingCanvas.Children.IndexOf(ellipse), ellipse));
                    RedoableCommands.Clear();
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
                    UndoableCommands.Push(new Command("add", PaintingCanvas.Children.IndexOf(rectangle), rectangle));
                    RedoableCommands.Clear();
                }
            }
            else if(setter == 3)
            {
                points.Add(position);
                clickedRight += 1;
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
                    UndoableCommands.Push(new Command("add", PaintingCanvas.Children.IndexOf(image), image));
                    RedoableCommands.Clear();
                }
            }

        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            setter = 1;
            EllipseButton.Foreground = Brushes.SkyBlue;

            // reset other buttons
            RectangleButton.Foreground = Brushes.Black;
            
            PolygonButton.Foreground = Brushes.Black;
            
            ImageButton.Foreground = Brushes.Black;
            
            UndoButton.Foreground = Brushes.Black;
            
            RedoButton.Foreground = Brushes.Black;
            
            ClearButton.Foreground = Brushes.Black;

        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            setter = 2;
            RectangleButton.Foreground = Brushes.SkyBlue;
            
            // reset other buttons
            EllipseButton.Foreground = Brushes.Black;
            
            PolygonButton.Foreground = Brushes.Black;
            
            ImageButton.Foreground = Brushes.Black;
            
            UndoButton.Foreground = Brushes.Black;
            
            RedoButton.Foreground = Brushes.Black;
            
            ClearButton.Foreground = Brushes.Black;

        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UndoableCommands.Count > 0)
                {
                    var command = UndoableCommands.Pop();
                    if (command.Action.Equals("add"))
                    {
                        PaintingCanvas.Children.RemoveAt(command.Index);
                        RedoableCommands.Push(new Command("add", command.Index, command.Obj));
                    }
                    else if (command.Action.Equals("clear"))
                    {
                        foreach (UIElement s in command.Obj as List<UIElement>) PaintingCanvas.Children.Add(s);
                        RedoableCommands.Push(new Command("clear", -1, null));
                    }
                    RedoButton.IsEnabled = true;
                }
            }
            catch { }
        }

        private void PolygonButton_Click(object sender, RoutedEventArgs e)
        {
            setter = 3;
            PolygonButton.Foreground = Brushes.SkyBlue;

            // reset other buttons
            EllipseButton.Foreground = Brushes.Black;
            
            RectangleButton.Foreground = Brushes.Black;
            
            ImageButton.Foreground = Brushes.Black;
            
            UndoButton.Foreground = Brushes.Black;
            
            RedoButton.Foreground = Brushes.Black;
            
            ClearButton.Foreground = Brushes.Black;
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            setter = 4;
            ImageButton.Foreground = Brushes.SkyBlue;

            // reset other buttons
            EllipseButton.Foreground = Brushes.Black;
            
            RectangleButton.Foreground = Brushes.Black;
            
            PolygonButton.Foreground = Brushes.Black;
            
            UndoButton.Foreground = Brushes.Black;
            
            RedoButton.Foreground = Brushes.Black;
            
            ClearButton.Foreground = Brushes.Black;
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
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RedoableCommands.Count > 0)
                {
                    var command = RedoableCommands.Pop();
                    if (command.Action.Equals("add"))
                    {
                        PaintingCanvas.Children.Insert(command.Index, (UIElement)command.Obj);
                        UndoableCommands.Push(new Command("add", command.Index, command.Obj));
                    }
                    else if (command.Action.Equals("clear"))
                    {
                        UndoableCommands.Push(new Command("clear", -1, (from UIElement el in PaintingCanvas.Children select el).ToList()));
                        PaintingCanvas.Children.Clear();
                    }
                }
            }
            catch { }
            if (!(RedoableCommands.Count > 0)) RedoButton.IsEnabled = false;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (PaintingCanvas.Children.Count > 0) {
                UndoableCommands.Push(new Command("clear", -1, (from UIElement el in PaintingCanvas.Children select el).ToList()));
                PaintingCanvas.Children.Clear();
                RedoButton.IsEnabled = false;
            }
        }
    }
}
