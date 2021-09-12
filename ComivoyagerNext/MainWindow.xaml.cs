﻿using ComivoyagerNext.ViewModels;
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
        readonly MainPageViewModel viewModel = new();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = viewModel;
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
