using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Math;

namespace ComivoyagerNext.Methods
{
    internal struct BurnoutSolver
    {
        private readonly Random random;

        private readonly int itherationsCount;
        private readonly double initialTemperature;


        public BurnoutSolver(int itherationsCount, double initialTemperature)
        {
            this.random = new Random();

            this.itherationsCount = itherationsCount;
            this.initialTemperature = initialTemperature;
        }

        public (int[] path, double energy) FoldCitiesOrder(ReadOnlySpan<Point> cities)
        {
            Span<int> currentBestOrder = new int[cities.Length];

            for (int i = 0; i < currentBestOrder.Length; i++)
            {
                currentBestOrder[i] = i;
            }

            var currentBestEnergy = Energy(cities, currentBestOrder);

            Span<int> currentOrder = new int[cities.Length];
            currentBestOrder.CopyTo(currentOrder);

            var currentEnergy = currentBestEnergy;

            Span<int> candidateOrder = new int[cities.Length];
            currentBestOrder.CopyTo(candidateOrder);

            for (int i = 0; i < itherationsCount; i++)
            {
                currentOrder.CopyTo(candidateOrder);

                UpdateCandidate(candidateOrder);

                var candidateEnergy = Energy(cities, candidateOrder);

                var temperature = GetTemperature(i);

                if(candidateEnergy < currentEnergy)
                {
                    currentEnergy = candidateEnergy;
                    candidateOrder.CopyTo(currentOrder);
                }
                else
                {
                    var prohability = GetTransitionProbability(candidateEnergy - currentEnergy, temperature);

                    if (IsTransistion(prohability))
                    {
                        currentEnergy = candidateEnergy;
                        candidateOrder.CopyTo(currentOrder);
                    }
                }

                if(currentEnergy < currentBestEnergy)
                {
                    currentBestEnergy = currentEnergy;
                    currentOrder.CopyTo(currentBestOrder);
                }
            }

            return (currentBestOrder.ToArray(), currentEnergy);
        }

        private double GetTemperature(int index) => initialTemperature * 0.1 / (index + 1);

        private static double GetTransitionProbability(double deltaEnergy, double temperature) => Exp(-deltaEnergy / temperature);

        private bool IsTransistion(double prohability) => random.NextDouble() <= prohability;

        private double Energy(ReadOnlySpan<Point> points, ReadOnlySpan<int> indices)
        {
            var sum = 0.0;
            
            for (int i = 0; i < indices.Length; i++)
            {
                var start = points[indices[i % indices.Length]];
                var end = points[indices[(i + 1) % indices.Length]];

                var diffX = end.X - start.X;
                var diffY = end.Y - start.Y;

                sum += Sqrt(diffX * diffX + diffY * diffY);
            }

            return sum;
        }

        private void UpdateCandidate(Span<int> array)
        {
            var start = random.Next(array.Length);
            var end = random.Next(array.Length + 1);

            if(start > end)
            {
                (start, end) = (end, start);
            }

            array[start..end].Reverse();
        }
    }
}
