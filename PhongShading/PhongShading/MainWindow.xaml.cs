using PhongSimulator;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhongShading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PhongAgent phongAgent;

        private readonly int canvasWidth = 400;
        private readonly int canvasHeigth = 400;
        private byte[] pixels;
        private int stride;

        public MainWindow()
        {
            phongAgent = new PhongAgent();
            InitializeComponent();
        }

        public void SetupScene(object sender, RoutedEventArgs e)
        {
            RenderTargetBitmap renderBitmap =
                new RenderTargetBitmap(canvasWidth, canvasHeigth, 96, 96, PixelFormats.Pbgra32);
            renderBitmap.Render(sceneCanvas);
            stride = (canvasWidth * renderBitmap.Format.BitsPerPixel + 7) / 8;
            pixels = new byte[canvasHeigth * stride];
            renderBitmap.CopyPixels(new Int32Rect(0, 0, canvasWidth, canvasHeigth), pixels, stride, 0);
        }

        public void NewScene()
        {
            var newPixels = phongAgent.PhongAlgorithm(pixels);
            WriteableBitmap writeableBitmap = new WriteableBitmap(canvasWidth, canvasHeigth, 96, 96, PixelFormats.Pbgra32, BitmapPalettes.WebPaletteTransparent);
            writeableBitmap.WritePixels(new Int32Rect(0, 0, canvasWidth, canvasHeigth), newPixels, stride, 0);
            var image = new Image();
            image.Source = writeableBitmap;
            sceneCanvas.Children.Clear();
            sceneCanvas.Children.Add(image);
        }

        public void MoveLigthSource(object sender, KeyEventArgs e)
        {
            if (matRadio.IsChecked.Value|| mixedRadio.IsChecked.Value || metalRadio.IsChecked.Value)
            {
                switch (e.Key)
                {
                    case Key.W:
                        phongAgent.Up();
                        break;
                    case Key.S:
                        phongAgent.Down();
                        break;
                    case Key.A:
                        phongAgent.MoveLeft();
                        break;
                    case Key.D:
                        phongAgent.MoveRight();
                        break;
                    case Key.G:
                        phongAgent.Forward();
                        break;
                    case Key.H:
                        phongAgent.Backward();
                        break;
                }
                NewScene();
            }
        }

        public void ChangeMaterial(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            switch (radioButton.Name)
            {
                case "matRadio":
                    phongAgent.Kd = 0.75;
                    phongAgent.Ks = 0.25;
                    phongAgent.N = 5;
                    break;
                case "mixedRadio":
                    phongAgent.Kd = 0.5;
                    phongAgent.Ks = 0.5;
                    phongAgent.N = 10;
                    break;
                case "metalRadio":
                    phongAgent.Kd = 0.25;
                    phongAgent.Ks = 0.75;
                    phongAgent.N = 100;
                    break;
            }
            NewScene();
        }
    }
}
