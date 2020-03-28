using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using VirtualCamera.Utils;

namespace VirtualCamera
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

        private void LoadSceneFromFile(object sender, RoutedEventArgs e)
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
            fileScene.From3Dto2D();
            DrawScene(fileScene.Points2D);
        }

        private void DrawScene(List<List<double>> points)
        {
            sceneCanvas.Children.Clear();
            for (int i = 0; i < points.Count; i += 8)
            {
                DrawRectangle(new List<List<double>> { points[0 + i], points[1 + i], points[5 + i], points[4 + i] }, 0 + i);
                DrawRectangle(new List<List<double>> { points[1 + i], points[2 + i], points[6 + i], points[5 + i] }, 0 + i);
                DrawRectangle(new List<List<double>> { points[2 + i], points[3 + i], points[7 + i], points[6 + i] }, 0 + i);
                DrawRectangle(new List<List<double>> { points[0 + i], points[3 + i], points[7 + i], points[4 + i] }, 0 + i);
            }
        }

        private void DrawRectangle(List<List<double>> points, int color)
        {

            for (int i = 0; i < points.Count; i++)
            {
                Line line;
                if (i == points.Count - 1)
                {
                    line = new Line
                    {
                        X1 = 400 + points[i][0],
                        Y1 = 310 + points[i][1],
                        X2 = 400 + points[0][0],
                        Y2 = 310 + points[0][1],
                        StrokeThickness = 2
                    };
                }
                else
                {
                    line = new Line
                    {
                        X1 = 400 + points[i][0],
                        Y1 = 310 + points[i][1],
                        X2 = 400 + points[i + 1][0],
                        Y2 = 310 + points[i + 1][1],
                        StrokeThickness = 2
                    };
                }
                if (color == 0)
                {
                    line.Stroke = Brushes.White;
                }
                else if (color == 8)
                {
                    line.Stroke = Brushes.Green;
                }
                else if (color == 16)
                {
                    line.Stroke = Brushes.Purple;
                }
                else if (color == 24)
                {
                    line.Stroke = Brushes.Yellow;
                }
                sceneCanvas.Children.Add(line);
            }

        }

        private void KeyboardKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D)
            {
                fileScene.MoveCamera("D");
                DrawScene(fileScene.Points2D);
            }
            else if (e.Key == Key.A)
            {
                fileScene.MoveCamera("A");
                DrawScene(fileScene.Points2D);
            }
            else if (e.Key == Key.W)
            {
                fileScene.MoveCamera("W");
                DrawScene(fileScene.Points2D);
            }
            else if (e.Key == Key.S)
            {
                fileScene.MoveCamera("S");
                DrawScene(fileScene.Points2D);
            }
            else if (e.Key == Key.Down)
            {
                fileScene.MoveCamera("Down");
                DrawScene(fileScene.Points2D);
            }
            else if (e.Key == Key.Up)
            {
                fileScene.MoveCamera("Up");
                DrawScene(fileScene.Points2D);
            }
            else if (e.Key == Key.Right)
            {
                fileScene.RotateScene("Y", 1);
                DrawScene(fileScene.Points2D);
            }
            else if (e.Key == Key.Left)
            {
                fileScene.RotateScene("Y", -1);
                DrawScene(fileScene.Points2D);
            }
            else if (e.Key == Key.O)
            {
                fileScene.RotateScene("X", -1);
                DrawScene(fileScene.Points2D);
            }
            else if (e.Key == Key.P)
            {
                fileScene.RotateScene("X", 1);
                DrawScene(fileScene.Points2D);
            }
            else if (e.Key == Key.K)
            {
                fileScene.RotateScene("Z", -1);
                DrawScene(fileScene.Points2D);
            }
            else if (e.Key == Key.L)
            {
                fileScene.RotateScene("Z", 1);
                DrawScene(fileScene.Points2D);
            }
            else if (e.Key == Key.OemPlus)
            {
                fileScene.ZoomScene("+");
                DrawScene(fileScene.Points2D);
            }
            else if (e.Key == Key.OemMinus)
            {
                fileScene.ZoomScene("-");
                DrawScene(fileScene.Points2D);
            }
        }
    }
}
