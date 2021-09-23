using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Math;

namespace ComivoyagerNext.Methods
{
    internal static class Helpers
    {
        public static double[,] BuildPartialPaths(ReadOnlySpan<Point> points)
        {
            var result = new double[points.Length, points.Length];

            for (var i = 0; i < result.GetLength(0); i++)
            {
                for (int j = i + 1; j < result.GetLength(1); j++)
                {
                    var start = points[i];
                    var end = points[j];

                    var diffX = end.X - start.X;
                    var diffY = end.Y - start.Y;

                    result[i, j] = Sqrt(diffX * diffX + diffY * diffY);
                    result[j, i] = result[i, j];
                }
            }

            return result;
        }

        public static double PathLength(ReadOnlySpan<Point> points, ReadOnlySpan<int> indices)
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
    }
}
