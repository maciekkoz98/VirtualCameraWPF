using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //load text file and create the scene
            sceneCanvas.Background = Brushes.Black;
            Line line = new Line
            {
                X1 = 20,
                X2 = 20,
                Y1 = 99,
                Y2 = 50
            };
            line.Stroke = Brushes.White;
            line.StrokeThickness = 2;
            sceneCanvas.Children.Add(line);
        }

        private void KeyboardKeyPressed(object sender, KeyEventArgs e)
        {
            Trace.WriteLine("Przed if");
            if (e.Key == Key.D)
            {
                Trace.WriteLine("w IF");
                Line child = (Line)sceneCanvas.Children[0];
                child.X1 += 50;
                //sceneCanvas.Children.Remove(child);
            } else if (e.Key == Key.A)
            {
                Line child = (Line)sceneCanvas.Children[0];
                child.X1 -= 50;
            }
        }
    }
}
