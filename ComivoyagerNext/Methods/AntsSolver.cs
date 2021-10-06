using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComivoyagerNext.Methods
{
    struct AntsSolver
    {
        private readonly Random random;

        private readonly int itherationsCount;

        private readonly int antsCount;

        private readonly double alpha;

        private readonly double beta;

        private readonly double evaporationRate;

        private readonly double pheromoneProductionIntesity;

        public AntsSolver(
            int itherationsCount,
            int antsCount,
            double alpha,
            double beta,
            double evaporationRate,
            double pheromoneProductionIntesity)
        {
            random = new Random();

            NormalizeCoefficients(ref alpha, ref beta);

            this.itherationsCount = itherationsCount;
            this.antsCount = antsCount;
            this.beta = beta;
            this.alpha = alpha;
            this.evaporationRate = evaporationRate;
            this.pheromoneProductionIntesity = pheromoneProductionIntesity;
        }

        public (int[] path, double pathLength) FoldCitiesOrder(ReadOnlySpan<Point> cities)
        {
            var partialPaths = Helpers.BuildPartialPaths(cities);

            var bestPath = new int[cities.Length];
            var bestPathLength = double.PositiveInfinity;

            var antPaths = new int[antsCount][];

            for (int i = 0; i < antPaths.Length; i++)
            {
                antPaths[i] = new int[cities.Length];

                for (int j = 0; j < antPaths[i].Length; j++)
                {
                    antPaths[i][j] = j;
                }
            }

            var pheromoneMap = new double[cities.Length, cities.Length];

            for (int i = 0; i < pheromoneMap.GetLength(0); i++)
            {
                for (int j = 0; j < pheromoneMap.GetLength(1); j++)
                {
                    pheromoneMap[i, j] = 1.0;
                }
            }

            for (int i = 0; i < itherationsCount; i++)
            {
                UpdateCandidate(pheromoneMap, partialPaths, antPaths);
                UpdateBestPath(partialPaths, antPaths, bestPath, ref bestPathLength);
            }

            return (bestPath, bestPathLength);
        }

        private void UpdateCandidate(double[,] pheromoneMap, double[,] partialPaths, int[][] antPaths)
        {
            for (int i = 0; i < antPaths.Length; i++)
            {
                var path = antPaths[i];
                var startIndex = random.Next(antPaths[i].Length);

                (path[startIndex], path[0]) = (path[0], path[startIndex]);
            }

            for (int i = 1; i < antPaths[0].Length; i++)
            {
                for (int j = 0; j < antPaths.Length; j++)
                {
                    var path = antPaths[j];

                    var basis = 0.0;

                    for (int k = i; k < path.Length; k++)
                    {
                        var partialStart = path[i - 1];
                        var partialEnd = path[k];

                        basis += Math.Pow(pheromoneMap[partialStart, partialEnd], alpha) * Math.Pow(partialPaths[partialStart, partialEnd], beta);
                    }

                    var coin = random.NextDouble();

                    var probability = 0.0;
                    var selectedIndex = 0;

                    for (int k = i; k < antPaths[j].Length; k++)
                    {
                        var partialStart = path[i - 1];
                        var partialEnd = path[k];

                        probability += Math.Pow(pheromoneMap[partialStart, partialEnd], alpha) * Math.Pow(partialPaths[partialStart, partialEnd], beta) / basis;

                        selectedIndex = k;

                        if (coin < probability)
                        {
                            break;
                        }
                    }

                    (path[i], path[selectedIndex]) = (path[selectedIndex], path[i]);
                }

                for (int j = 0; j < pheromoneMap.GetLength(0); j++)
                {
                    for (int k = 0; k < pheromoneMap.GetLength(1); k++)
                    {
                        pheromoneMap[j, k] = (1.0 - evaporationRate) * pheromoneMap[j, k];
                    }
                }

                for (int j = 0; j < antPaths.Length; j++)
                {
                    var start = antPaths[j][i - 1];
                    var end = antPaths[j][i];

                    pheromoneMap[start, end] += pheromoneProductionIntesity / partialPaths[start, end];
                }
            }
        }

        private void UpdateBestPath(double[,] partialPaths, int[][] antsPaths, Span<int> bestPath, ref double bestPathLength)
        {
            for (int i = 0; i < antsPaths.GetLength(0); i++)
            {
                var path = antsPaths[i];

                var currentLength = 0.0;

                for (int j = 1; j < path.Length + 1; j++)
                {
                    currentLength += partialPaths[path[(j - 1) % path.Length], path[j % path.Length]];
                }

                if(currentLength < bestPathLength)
                {
                    path.AsSpan().CopyTo(bestPath);
                    bestPathLength = currentLength;
                }
            }
        }

        private static void NormalizeCoefficients(ref double alpha, ref double beta)
        {
            var baseValue = alpha + beta;

            alpha = alpha / baseValue;
            beta = beta / baseValue;
        }
    }
}
