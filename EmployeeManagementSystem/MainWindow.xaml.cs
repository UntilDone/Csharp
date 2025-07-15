using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Media;

namespace BrandNewStart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DrawingVisual rainVisual;
        private List<RainDrop> rainDrops = new();
        private Random random = new();
        private int rainDropCount = 200;
        private MediaPlayer rainSound = new();

        public MainWindow()
        {
            InitializeComponent();
            rainVisual = new DrawingVisual();
            var host = new DrawingVisualHost(rainVisual);
            Content = host;

            CompositionTarget.Rendering += OnRender;

            for (int i = 0; i < rainDropCount; i++)
            {
                rainDrops.Add(new RainDrop
                {
                    X = random.NextDouble() * SystemParameters.PrimaryScreenWidth,
                    Y = random.NextDouble() * SystemParameters.PrimaryScreenHeight,
                    Length = random.Next(10, 20),
                    Speed = random.NextDouble() * 8 + 2
                });
            }

            // Rain sound loop
            rainSound.Open(new Uri("rain.mp3", UriKind.Relative));
            rainSound.MediaEnded += (s, e) => rainSound.Position = TimeSpan.Zero;
            rainSound.Play();
        }

        private void OnRender(object sender, EventArgs e)
        {
            using DrawingContext dc = rainVisual.RenderOpen();
            dc.DrawRectangle(Brushes.DarkSlateGray, null, new Rect(0, 0, Width, Height));
            Brush rainBrush = Brushes.LightBlue;

            foreach (var drop in rainDrops)
            {
                drop.Y += drop.Speed;
                if (drop.Y > Height)
                {
                    drop.Y = -drop.Length;
                    drop.X = random.NextDouble() * Width;
                }

                Point start = new(drop.X, drop.Y);
                Point end = new(drop.X, drop.Y + drop.Length);
                dc.DrawLine(new Pen(rainBrush, 1), start, end);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class RainDrop
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Length { get; set; }
        public double Speed { get; set; }
    }

    public class DrawingVisualHost : FrameworkElement
    {
        private readonly Visual _visual;
        public DrawingVisualHost(Visual visual) => _visual = visual;
        protected override int VisualChildrenCount => 1;
        protected override Visual GetVisualChild(int index) => _visual;
    }
}
