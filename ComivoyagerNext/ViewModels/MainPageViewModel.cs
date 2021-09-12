using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ComivoyagerNext.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
       

        public enum SimulationMode { Random, Genetic }

        public delegate void PathChangedEvent(object sender, PathEventInfo e);

        public readonly ref struct PathEventInfo
        {
            public DotModel[] Path { get; init; }
        };

        private readonly Dispatcher mainWindowDispatcher;
        private readonly Random random = new();

        private double executionTime;
        private double wayLength;
        private string sequence = "";

        public ObservableCollection<DotModel> Dots { get; } = new();

        public event PathChangedEvent? OnPathChanged;

        public event PropertyChangedEventHandler? PropertyChanged;

        public SimulationMode Mode { get; set; }

        public double ExecutionTime
        {
            get => executionTime;
            set
            {
                executionTime = value;
                OnPropertyChanged();
            }
        }

        public double WayLength
        {
            get => wayLength;
            set
            {
                wayLength = value;
                OnPropertyChanged();
            }
        }

        public string Sequence
        {
            get => sequence;
            set
            {
                sequence = value;
                OnPropertyChanged();
            }
        }

        public MainPageViewModel(Dispatcher mainWindowDispatcher)
        {
            this.mainWindowDispatcher = mainWindowDispatcher;
        }

        public void AddDot(double x, double y)
        {
            Dots.Add(new DotModel(Dots.Count, x, y));
        }

        public void ClearDots()
        {
            Dots.Clear();
        }

        public Task SimulateAsync()
        {
            switch (Mode)
            {
                case SimulationMode.Random:
                    return SimulateRandom();
                case SimulationMode.Genetic:
                    break;
                default:
                    break;
            }
            return Task.CompletedTask;
        }

        public async Task SimulateRandom()
        {
            Monitor.Enter(Dots);

            var dots = Dots.ToArray();

            Monitor.Exit(Dots);

            var tempList = new List<DotModel>(dots.Length);
            var nextPath = new List<DotModel>(dots.Length);
            var minPathLength = double.PositiveInfinity;

            while (true)
            {
                tempList.Clear();
                nextPath.Clear();

                tempList.AddRange(dots);

                while(tempList.Count > 0)
                {
                    var position = random.Next(0, tempList.Count);

                    var value = tempList[position];

                    nextPath.Add(value);

                    tempList.RemoveAt(position);
                }

                var currentPathLength = PathLength(nextPath);

                if(currentPathLength < minPathLength)
                {
                    minPathLength = currentPathLength;

                    await Task.Delay(1000);

                    await UpdatePathAsync(0.0, minPathLength, nextPath);
                }
            }

           
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private async Task UpdatePathAsync(double executionTime, double wayLength, IEnumerable<DotModel> dots)
        {
            await mainWindowDispatcher.InvokeAsync(() =>
            {
                var sb = new StringBuilder();

                OnPathChanged?.Invoke(this, new PathEventInfo { Path = dots.ToArray() });

                ExecutionTime = executionTime;

                WayLength = wayLength;

                foreach (var item in dots)
                {
                    sb.Append(item.Number.ToString()).Append(" -> ");
                }

                Sequence = sb.ToString();
            });
        }

        private double PathLength(IEnumerable<DotModel> dots)
        {
            DotModel? lastDot = null;

            double sum = 0.0;

            foreach (var item in dots)
            {
                if (lastDot != null)
                {
                    var xOffset = lastDot.X - item.X;
                    var yOffset = lastDot.Y - item.Y;
                    var distance = Math.Sqrt(xOffset * xOffset + yOffset * yOffset);

                    sum += distance;
                }

                lastDot = item;
            }

            return sum;
        }
    }
}
