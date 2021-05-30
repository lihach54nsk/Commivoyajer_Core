using System.Linq;
using Commivoyajer_Core.Models;
using System.Collections.Generic;

namespace Commivoyajer_Framework.Methods
{
    public class GreedyMethod
    {
        public Output GetGreedyMethod(double[,] input)
        {
            var output = new List<Output>();

            for (int i = 0; i < input.GetLength(0); i++)
            {
                var sequence = new int[input.GetLength(0)];
                var cityIndex = i + 1;
                var journeyLength = 0.0;
                sequence[0] = cityIndex;

                for (int j = 1; j < input.GetLength(0); j++)
                {
                    var row = GetMatrixRow(input, cityIndex);
                    cityIndex = GetIndexOfMinimalPossiblePath(row, sequence);
                    sequence[j] = cityIndex + 1;
                    journeyLength += row[cityIndex];
                }

                var lastCityRow = GetMatrixRow(input, cityIndex);
                journeyLength += lastCityRow[i];

                output.Add(new Output
                {
                    Sequence = sequence,
                    JourneyLength = journeyLength,
                    CalculationTime = 0.0,
                    VariantsCount = 0
                });
            }

            return GetAnswer(output);
        }

        private double[] GetMatrixRow(double[,] matrix, int rowIndex)
        {
            var result = new double[matrix.GetLength(0)];

            for (var i = 0; i < matrix.GetLength(0); i++)
                result[i] = matrix[rowIndex, i];

            return result;
        }

        private int GetIndexOfMinimalPossiblePath(double[] row, int[] sequence)
        {
            var resultIndex = -1;
            var distance = -1.0;

            for (var i = 0; i < row.Length; i++)
            {
                var currentCity = sequence.FirstOrDefault(x => x == i + 1);

                if (currentCity == 0)
                    continue;

                if (distance == -1.0 && row[i] != 0)
                {
                    distance = row[i];
                    resultIndex = i;
                }

                if (distance > row[i])
                {
                    distance = row[i];
                    resultIndex = i;
                }
            }

            return resultIndex;
        }

        private Output GetAnswer(List<Output> output)
        {
            var resultIndex = 0;
            var resultLength = 0.0;
            var index = 0;

            foreach(var variant in output)
            {
                if (resultLength > variant.JourneyLength)
                {
                    resultIndex = index;
                    resultLength = variant.JourneyLength;
                }
                index++;
            }
            return output[resultIndex];
        }
    }
}
