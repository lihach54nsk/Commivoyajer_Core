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
            if (alpha + beta != 1.0)
            {
                throw new ArgumentException("alpha + beta should be equal 1");
            }

            this.random = new Random();

            this.itherationsCount = itherationsCount;
            this.antsCount = antsCount;
            this.beta = beta;
            this.alpha = alpha;
            this.evaporationRate = evaporationRate;
            this.pheromoneProductionIntesity = pheromoneProductionIntesity;
        }

        public (int[] path, double energy) FoldCitiesOrder(ReadOnlySpan<Point> cities)
        {
            var partialPaths = Helpers.BuildPartialPaths(cities);

            var antPaths = new int[antsCount, cities.Length];

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
            }

            throw new NotImplementedException();
        }

        private void UpdateCandidate(double[,] pheromoneMap, double[,] partialPaths, int[,] antPaths)
        {
            for (int i = 0; i < antPaths.GetLength(0); i++)
            {
                antPaths[i, 0] = 0;
            }

            for (int i = 1; i < antPaths.GetLength(1); i++)
            {
                for (int j = 0; j < antPaths.GetLength(0); j++)
                {
                    var coin = random.NextDouble();

                    var basis = 0.0;

                    for (int k = i; k < antPaths.GetLength(1); k++)
                    {
                        basis += Math.Pow(pheromoneMap[antPaths[j, k - 1], j], alpha) * Math.Pow(partialPaths[antPaths[j, k - 1], j], beta);
                    }

                    var probability = 0.0;
                    var selectedIndex = 0;

                    for (int k = i; k < antPaths.GetLength(1); k++)
                    {
                        probability += Math.Pow(pheromoneMap[antPaths[j, k - 1], j], alpha) * Math.Pow(partialPaths[antPaths[j, k - 1], j], beta) / basis;

                        selectedIndex = k;

                        if (coin < probability)
                        {
                            break;
                        }
                    }

                    (antPaths[j, i], antPaths[j, selectedIndex]) = (antPaths[j, selectedIndex], antPaths[j, i]);
                }

                for (int j = 0; j < pheromoneMap.GetLength(0); j++)
                {
                    for (int k = 0; k < pheromoneMap.GetLength(0); k++)
                    {
                        var spawnedPheromone = 0.0;

                        for (int i1 = 0; i1 < antPaths.GetLength(0); i1++)
                        {
                            if(antPaths[i1, i - 1] == j && antPaths[i1, i] == k)
                            {
                                // Maybe wrong
                                spawnedPheromone += pheromoneProductionIntesity / partialPaths[j, k];
                            }
                        }

                        pheromoneMap[j, k] = (1.0 - evaporationRate) * pheromoneMap[j, k] + spawnedPheromone;
                    }
                }
            }
        }
    }
}
