using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Math;

namespace ComivoyagerNext.Methods
{
    struct RandomSelectionSolver
    {
        private readonly Random random;

        private readonly int itherationsCount;

        public RandomSelectionSolver(int itherationsCount)
        {
            this.random = new Random();
            this.itherationsCount = itherationsCount;
        }


        public (int[] path, double energy) FoldCitiesOrder(Span<Point> cities)
        {
            Span<int> currentBestOrder = stackalloc int[cities.Length];

            for (int i = 0; i < currentBestOrder.Length; i++)
            {
                currentBestOrder[i] = i;
            }

            var currentBestLength = PathLength(cities, currentBestOrder);

            var currentBestLengthNorm = PathLengthNorm(cities, currentBestOrder);

            Span<int> candidateOrder = stackalloc int[cities.Length];
            currentBestOrder.CopyTo(candidateOrder);

            for (int i = 0; i < itherationsCount; i++)
            {
                UpdateCandidate(candidateOrder);

                var currentLengthNorm = PathLengthNorm(cities, candidateOrder);

                if(currentLengthNorm < currentBestLengthNorm)
                {
                    currentBestLengthNorm = currentLengthNorm;

                    currentBestLength = PathLength(cities, candidateOrder);
                    candidateOrder.CopyTo(currentBestOrder);
                }
            }

            return (currentBestOrder.ToArray(), currentBestLength);
        }

        private double PathLength(Span<Point> points, Span<int> indices)
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

        private double PathLengthNorm(Span<Point> points, Span<int> indices)
        {
            var sum = 0.0;
            for (int i = 0; i < indices.Length - 1; i++)
            {
                var start = points[indices[i]];
                var end = points[indices[i + 1]];

                var diffX = end.X - start.X;
                var diffY = end.Y - start.Y;

                sum += diffX * diffX + diffY * diffY;
            }

            var borderStart = points[indices[^1]];
            var borderEnd = points[indices[0]];

            var diffXBorder = borderEnd.X - borderStart.X;
            var diffYBorder = borderEnd.Y - borderStart.Y;

            sum += diffXBorder * diffXBorder + diffYBorder * diffYBorder;

            return sum;
        }

        private void UpdateCandidate(Span<int> array)
        {
            var length = array.Length;

            var i = random.Next(length);
            var j = random.Next(length);

            if (i > j)
            {
                (i, j) = (j, i);
            }

            array[i..j].Reverse();
        }
    }
}
