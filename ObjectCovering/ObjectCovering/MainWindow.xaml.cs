using Newtonsoft.Json;
using ObjectCovering.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ObjectCovering
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Scene fileScene;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadSceneFromFile(object sender, RoutedEventArgs e)
        {
            sceneCanvas.Background = Brushes.Black;
            string file = "";
            try
            {
                using (StreamReader sr = new StreamReader("scene.json"))
                {
                    file += sr.ReadToEnd();
                }

            }
            catch (IOException)
            {
                MessageBoxResult result = MessageBox.Show("Brakuje pliku konfiguracyjnego", "VirtualCamera", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                {
                    Environment.Exit(1);
                }
            }
            fileScene = JsonConvert.DeserializeObject<Scene>(file);
            fileScene.MakeCameraPosZeroPoint();
            fileScene.SortPolygons();
            fileScene.From3Dto2D();
            DrawScene();
        }

        private void DrawScene()
        {
            sceneCanvas.Children.Clear();
            Random rnd = new Random();
            foreach (var polygon in fileScene.Polygons)
            {
                Trace.WriteLine(polygon);
                PointCollection polygon2D = new PointCollection();
                foreach (var point in polygon.Points2D)
                {
                    Point point2D = new Point(point[0] + 400, point[1] + 310);
                    polygon2D.Add(point2D);
                }
                var brush = PickBrush(rnd);
                Polygon canvasPolygon = new Polygon()
                {
                    Points = polygon2D,
                    Stroke = brush,
                    Fill = brush,
                    StrokeThickness = 2
                };
                sceneCanvas.Children.Add(canvasPolygon);
            }
        }

        private Brush PickBrush(Random rnd)
        {
            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            return (Brush)properties[random].GetValue(null, null);
        }

        private void KeyboardKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D)
            {
                fileScene.MoveCamera("D");
                DrawScene();
            }
            else if (e.Key == Key.A)
            {
                fileScene.MoveCamera("A");
                DrawScene();
            }
            else if (e.Key == Key.W)
            {
                fileScene.MoveCamera("W");
                DrawScene();
            }
            else if (e.Key == Key.S)
            {
                fileScene.MoveCamera("S");
                DrawScene();
            }
            else if (e.Key == Key.Down)
            {
                fileScene.MoveCamera("Down");
                DrawScene();
            }
            else if (e.Key == Key.Up)
            {
                fileScene.MoveCamera("Up");
                DrawScene();
            }
            else if (e.Key == Key.Right)
            {
                fileScene.RotateScene("Y", 1);
                DrawScene();
            }
            else if (e.Key == Key.Left)
            {
                fileScene.RotateScene("Y", -1);
                DrawScene();
            }
            else if (e.Key == Key.O)
            {
                fileScene.RotateScene("X", -1);
                DrawScene();
            }
            else if (e.Key == Key.P)
            {
                fileScene.RotateScene("X", 1);
                DrawScene();
            }
            else if (e.Key == Key.K)
            {
                fileScene.RotateScene("Z", -1);
                DrawScene();
            }
            else if (e.Key == Key.L)
            {
                fileScene.RotateScene("Z", 1);
                DrawScene();
            }
            else if (e.Key == Key.OemPlus)
            {
                fileScene.ZoomScene("+");
                DrawScene();
            }
            else if (e.Key == Key.OemMinus)
            {
                fileScene.ZoomScene("-");
                DrawScene();
            }
        }
    }
}
