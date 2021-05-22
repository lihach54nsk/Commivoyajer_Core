using Commivoyajer_Core.Models;
using System;
using System.Collections.Generic;

namespace Commivoyajer_Core.PreparationMethods
{
    public class PrepareDataClass
    {
        public double[,] PrepareData(List<Input> input)
        {
            var result = new double[input.Count, input.Count];

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input.Count; j++)
                    result[i, j] = GetRoadLength(input[i], input[j]);
            }

            return result;
        }

        public double CalculateJourneylangth(double[,] input, int[] sequence)
        {
            double result = 0;

            for (int i = 0; i < sequence.Length - 1; i++)
                result += input[sequence[i], sequence[i + 1]];

            return result;
        }

        private double GetRoadLength(Input firstCity, Input secondCity)
            => Math.Sqrt((secondCity.XCoord - firstCity.XCoord) * (secondCity.XCoord - firstCity.XCoord)
            + (secondCity.YCoord - firstCity.YCoord) * (secondCity.YCoord - firstCity.YCoord));
    }
}
