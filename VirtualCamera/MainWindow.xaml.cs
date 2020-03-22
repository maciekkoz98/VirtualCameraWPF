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
            Scene fileScene = JsonConvert.DeserializeObject<Scene>(file);
           // fileScene.MakeCameraPosZeroPoint();
            fileScene.From3Dto2D();
            List<double[,]> pointsList = fileScene.CreatePointsList();
            foreach (double[,] points in pointsList)
            {
                DrawRectangle(points);
            }
        }

        private void DrawRectangle(double[,] points)
        {
            for (int i = 0; i < points.GetLength(0); i++)
            {
                Line line;
                if (i == points.GetLength(0) - 1)
                {
                    line = new Line
                    {
                        X1 = 400 + points[i, 0],
                        Y1 = 310 + points[i, 1],
                        X2 = 400 + points[0, 0],
                        Y2 = 310 + points[0, 1],
                        Stroke = Brushes.Green,
                        StrokeThickness = 2
                    };

                }
                else
                {
                    line = new Line
                    {
                        X1 = 400 + points[i, 0],
                        Y1 = 310 + points[i, 1],
                        X2 = 400 + points[i + 1, 0],
                        Y2 = 310 + points[i + 1, 1],
                        //Stroke = Brushes.White,
                        StrokeThickness = 2
                    };
                }
                if (i % 4 == 0)
                {
                    line.Stroke = Brushes.Purple;
                }
                else if (i % 4 == 1)
                {
                    line.Stroke = Brushes.Yellow;
                }
                else if (i % 4 == 2)
                {
                    line.Stroke = Brushes.Orange;
                }
                sceneCanvas.Children.Add(line);
            }

        }

        private void KeyboardKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D)
            {
                Line child = (Line)sceneCanvas.Children[0];
                child.X1 += 50;
            }
            else if (e.Key == Key.A)
            {
                Line child = (Line)sceneCanvas.Children[0];
                child.X1 -= 50;
            }
        }
    }
}
