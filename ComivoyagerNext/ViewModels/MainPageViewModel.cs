using ComivoyagerNext.Methods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        public enum SimulationMode { Random, Genetic, Burnout }

        public delegate void PathChangedEvent(object sender, PathEventInfo e);

        public readonly ref struct PathEventInfo
        {
            public DotModel[] Path { get; init; }
        };

        static int seed = Environment.TickCount;

        static readonly Random random = new Random();

        private readonly Dispatcher mainWindowDispatcher;
        private readonly object lockUI = new();

        private double executionTime;
        private double wayLength;
        private string sequence = "";

        public RandomSelectionViewModel RandomSelectionViewModel { get; } = new();

        public BurnoutViewModel BurnoutViewModel { get; } = new();

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

        public async Task SimulateAsync()
        {
            var sw = new Stopwatch();
            sw.Start();
            switch (Mode)
            {
                case SimulationMode.Random:
                    await SimulateRandomAsync();
                    break;
                case SimulationMode.Genetic:
                    break;
                case SimulationMode.Burnout:
                    await SimulateBurnoutAsync();
                    break;
                default:
                    break;
            }
            sw.Stop();

            await mainWindowDispatcher.InvokeAsync(() =>
            {
                ExecutionTime = sw.ElapsedMilliseconds;
            });
        }

        public async Task SimulateBurnoutAsync()
        {
            var dotsSnapshot = Dots.ToArray();

            var cities = Dots.Select(x => new Point { X = x.X, Y = x.Y }).ToArray();

            var solver = new BurnoutSolver(BurnoutViewModel.ItherationsCount, BurnoutViewModel.InitialTemperature);

            var (path, length) = await Task.Run(() => solver.FoldCitiesOrder(cities));

            OnPathChanged?.Invoke(this, new PathEventInfo { Path = path.Select(x => dotsSnapshot[x]).ToArray() });

            WayLength = length;
        }


        public async Task SimulateRandomAsync()
        {
            var dotsSnapshot = Dots.ToArray();

            var cities = Dots.Select(x => new Point { X = x.X, Y = x.Y }).ToArray();

            var solver = new RandomSelectionSolver(RandomSelectionViewModel.ItherationsCount);

            var (path, length) = await Task.Run(() => solver.FoldCitiesOrder(cities));

            OnPathChanged?.Invoke(this, new PathEventInfo { Path = path.Select(x => dotsSnapshot[x]).ToArray() });

            WayLength = length;
        }

        public async Task SimulateRandom()
        {
            var dots = Dots.ToArray();

            var minPathLength = double.PositiveInfinity;

            var tasks = new Task[Environment.ProcessorCount];

            for (int i = 0; i < tasks.Length; i++)
            {
                var localRandom = new Random(random.Next());

                tasks[i] = Task.Run(async () =>
                {
                    var tempList = new List<DotModel>(dots.Length);
                    var nextPath = new List<DotModel>(dots.Length);
                    var knownPaths = new double[dots.Length, dots.Length];
                    var itherations = RandomSelectionViewModel.ItherationsCount;

                    for (int i = 0; i < knownPaths.GetLength(0); i++)
                    {
                        for (int j = 0; j < knownPaths.GetLength(1); j++)
                        {
                            knownPaths[i, j] = double.NegativeInfinity;
                        }
                    }

                    for (int i = 0; i < itherations; i++)
                    {
                        tempList.Clear();
                        nextPath.Clear();

                        tempList.AddRange(dots);

                        while (tempList.Count > 0)
                        {
                            var position = localRandom.Next(0, tempList.Count);

                            var value = tempList[position];

                            nextPath.Add(value);

                            tempList.RemoveAt(position);
                        }

                        var currentPathLength = PathEstimate(nextPath, knownPaths, minPathLength);

                        if (currentPathLength < minPathLength)
                        {

                            double localMinPathLength;
                            bool updateUi = true;

                            do
                            {
                                localMinPathLength = minPathLength;

                                if (currentPathLength >= localMinPathLength)
                                {
                                    updateUi = false;
                                    break;
                                }
                            }
                            while (localMinPathLength != Interlocked.CompareExchange(ref minPathLength, currentPathLength, localMinPathLength));

                            if(updateUi)
                            {
                                await UpdatePathAsync(PathLength(nextPath), nextPath);
                            }
                        }
                    }
                });
            }

            await Task.WhenAll(tasks);
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private async Task UpdatePathAsync(double wayLength, IEnumerable<DotModel> dots)
        {
            await mainWindowDispatcher.InvokeAsync(() =>
            {
                var sb = new StringBuilder();

                foreach (var item in dots)
                {
                    sb.Append(item.Number.ToString()).Append(" -> ");
                }

                lock (lockUI)
                {
                    OnPathChanged?.Invoke(this, new PathEventInfo { Path = dots.ToArray() });

                    WayLength = wayLength;

                    Sequence = sb.ToString();
                }
            });
        }

        private double PathLength(IEnumerable<DotModel> dots)
        {
            DotModel? firstDot = null;
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

            if (firstDot != null && lastDot != null)
            {
                var xOffset = lastDot.X - firstDot.X;
                var yOffset = lastDot.Y - firstDot.Y;
                var distance = Math.Sqrt(xOffset * xOffset + yOffset * yOffset);

                sum += distance;
            }

            return sum;
        }

        private double PathEstimate(IEnumerable<DotModel> dots, double[,] knownPaths, double bestValue)
        {
            DotModel? firstDot = null;
            DotModel? lastDot = null;

            double sumEstimate = 0.0;

            foreach (var item in dots)
            {
                if (lastDot != null)
                {
                    if(knownPaths[lastDot.Number, item.Number] < 0)
                    {
                        var xOffset = lastDot.X - item.X;
                        var yOffset = lastDot.Y - item.Y;

                        knownPaths[lastDot.Number, item.Number] = xOffset * xOffset + yOffset * yOffset;
                        knownPaths[item.Number, lastDot.Number] = knownPaths[lastDot.Number, item.Number];
                    }

                    var distance = knownPaths[lastDot.Number, item.Number];

                    sumEstimate += distance;

                    if(sumEstimate > bestValue)
                    {
                        return double.PositiveInfinity;
                    }
                } else
                {
                    firstDot = item;
                }

                lastDot = item;
            }

            if(firstDot != null && lastDot != null)
            {
                if (knownPaths[lastDot.Number, firstDot.Number] < 0)
                {
                    var xOffset = lastDot.X - firstDot.X;
                    var yOffset = lastDot.Y - firstDot.Y;

                    knownPaths[lastDot.Number, firstDot.Number] = xOffset * xOffset + yOffset * yOffset;
                    knownPaths[firstDot.Number, lastDot.Number] = knownPaths[lastDot.Number, firstDot.Number];
                }

                sumEstimate += knownPaths[firstDot.Number, lastDot.Number];
            }

            return sumEstimate;
        }
    }
}
