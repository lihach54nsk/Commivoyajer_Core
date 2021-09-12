using ComivoyagerNext.ViewModels;
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

namespace ComivoyagerNext
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Polyline? pathLine;

        private readonly MainPageViewModel viewModel = new();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = viewModel;

            viewModel.OnPathChanged += ViewModel_OnPathChanged;
        }

        private void ViewModel_OnPathChanged(object sender, MainPageViewModel.PathEventInfo e)
        {
            if (pathLine != null)
            {
                dotsCanvas.Children.Remove(pathLine);
            }

            var line = new Polyline
            {
                Points = new PointCollection(e.Path.Select(x => new Point(x.X, x.Y))),
                Stroke = Brushes.Green,
                StrokeThickness = 2,
            };

            pathLine = line;

            dotsCanvas.Children.Add(line);
        }

        private void dotsCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Canvas canvas)
            {
                var circle = new Ellipse()
                {
                    Stroke = Brushes.Blue,
                    Fill = Brushes.Blue,
                    Width = 10,
                    Height = 10,
                };

                var position = e.GetPosition(canvas);

                canvas.Children.Add(circle);
                Canvas.SetTop(circle, position.Y);
                Canvas.SetLeft(circle, position.X);

                viewModel.AddDot(position.X, position.Y);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            dotsCanvas.Children.Clear();
            viewModel.ClearDots();
        }
    }
}
