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

        public (int[] path, double energy) FoldCitiesOrder(Span<Point> cities)
        {
            Span<int> currentBestOrder = stackalloc int[cities.Length];

            for (int i = 0; i < currentBestOrder.Length; i++)
            {
                currentBestOrder[i] = i;
            }

            var currentBestEnergy = Energy(cities, currentBestOrder);

            Span<int> currentOrder = stackalloc int[cities.Length];
            currentBestOrder.CopyTo(currentBestOrder);

            var currentEnergy = currentBestEnergy;

            Span<int> candidateOrder = stackalloc int[cities.Length];
            currentBestOrder.CopyTo(candidateOrder);

            for (int i = 0; i < itherationsCount; i++)
            {
                UpdateCandidate(candidateOrder);

                var temperature = CurrentTemperature(i);

                var candidateEnergy = Energy(cities, candidateOrder);

                if(candidateEnergy < currentEnergy)
                {
                    currentEnergy = candidateEnergy;
                    candidateOrder.CopyTo(currentOrder);
                }
                else
                {
                    var prohability = GetTransitionProbability(candidateEnergy - currentEnergy, temperature);

                    if(prohability < 0.8)
                    {
                        Console.WriteLine(true);
                    }

                    if (IsTransistion(prohability))
                    {
                        currentEnergy = candidateEnergy;
                        candidateOrder.CopyTo(currentOrder);
                        Console.WriteLine("Transition");
                    }
                    else
                    {
                        Console.WriteLine("Not transition");
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

        private double CurrentTemperature(int index) => initialTemperature - initialTemperature * index / itherationsCount;

        private static double GetTransitionProbability(double deltaEnergy, double temperature) => Exp(-deltaEnergy / temperature);

        private bool IsTransistion(double prohability) => random.NextDouble() <= prohability;

        private double Energy(Span<Point> points, Span<int> indices)
        {
            var sum = 0.0;
            for (int i = 0; i < indices.Length - 1; i++)
            {
                var start = points[indices[i]];
                var end = points[indices[i + 1]];

                var diffX = end.X - start.X;
                var diffY = end.Y - start.Y;

                sum += Sqrt(diffX * diffX + diffY * diffY);
            }

            var borderStart = points[indices[^1]];
            var borderEnd = points[indices[0]];

            var diffXBorder = borderEnd.X - borderStart.X;
            var diffYBorder = borderEnd.Y - borderStart.Y;

            sum += Sqrt(diffXBorder * diffXBorder + diffYBorder * diffYBorder);

            return sum;
        }

        private void UpdateCandidate(Span<int> array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var j = random.Next(i, array.Length);

                (array[i], array[j]) = (array[j], array[i]);
            }
        }
    }
}
