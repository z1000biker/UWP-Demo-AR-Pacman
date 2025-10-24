using System;
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.Foundation;

namespace App1
{
    public sealed partial class MainPage : Page
    {
        private MediaCapture? _mediaCapture;
        private DispatcherTimer _timer;
        private double _pacmanX = 50;
        private double _velocity = 3;          // speed (pixels per tick)
        private double _mouthAngle = 30;       // current mouth openness
        private bool _mouthOpening = false;    // animation direction

        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(30) };
            _timer.Tick += OnTick;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            _mediaCapture = new MediaCapture();
            await _mediaCapture.InitializeAsync();
            CameraPreview.Source = _mediaCapture;
            await _mediaCapture.StartPreviewAsync();

            DrawPacman(_mouthAngle);
            _timer.Start();
            //  Set window title
            Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().Title = "Pacman Video UWP AR Demo";
        }

        private async void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            if (_mediaCapture != null)
            {
                await _mediaCapture.StopPreviewAsync();
                _mediaCapture.Dispose();
                _mediaCapture = null;
            }
        }

        // ---- animation step ----
        private void OnTick(object sender, object e)
        {
            // move
            _pacmanX += _velocity;
            Canvas.SetLeft(PacmanCanvas, _pacmanX);

            double canvasWidth = Window.Current.Bounds.Width;
            double pacmanWidth = 200;

            // flip and bounce at edges
            if (_pacmanX + pacmanWidth >= canvasWidth)
            {
                _velocity = -_velocity;
                FlipTransform.ScaleX = -1; // face left
            }
            else if (_pacmanX <= 0)
            {
                _velocity = -_velocity;
                FlipTransform.ScaleX = 1; // face right
            }

            // animate mouth open/close
            if (_mouthOpening)
                _mouthAngle += 2;
            else
                _mouthAngle -= 2;

            if (_mouthAngle >= 45) _mouthOpening = false;
            if (_mouthAngle <= 10) _mouthOpening = true;

            DrawPacman(_mouthAngle);
        }

        // ---- draws a circular Pac-Man with given mouth angle ----
        private void DrawPacman(double mouthAngleDeg)
        {
            double centerX = 100;
            double centerY = 100;
            double radius = 80;

            double startAngle = mouthAngleDeg * Math.PI / 180.0;
            double endAngle = (360 - mouthAngleDeg) * Math.PI / 180.0;

            var figure = new PathFigure { StartPoint = new Point(centerX, centerY), IsClosed = true };
            figure.Segments.Add(new LineSegment
            {
                Point = new Point(centerX + radius * Math.Cos(startAngle),
                                  centerY - radius * Math.Sin(startAngle))
            });

            int steps = 100;
            for (int i = 0; i <= steps; i++)
            {
                double t = startAngle + (endAngle - startAngle) * i / steps;
                double x = centerX + radius * Math.Cos(t);
                double y = centerY - radius * Math.Sin(t);
                figure.Segments.Add(new LineSegment { Point = new Point(x, y) });
            }

            figure.Segments.Add(new LineSegment { Point = new Point(centerX, centerY) });

            var geometry = new PathGeometry();
            geometry.Figures.Add(figure);
            PacmanPath.Data = geometry;
        }
    }
}
