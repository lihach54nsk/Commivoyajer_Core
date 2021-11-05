using Commivoyajer_Core.Models;

namespace Comivoyager.StrictMethods.Methods
{
    public class BruteForceMethod
    {
        public Output GetBruteForceMethod(double[,] dis_matrix) // Сама функция перебора
        {
            int[] cityArray = new int[dis_matrix.GetLength(0) - 1];
            int[] answerArray = new int[dis_matrix.GetLength(0) - 1];
            int[] finalanswerArray = new int[dis_matrix.GetLength(0)];
            double minimalDistance = 0;
            double newDistance = 0;

            for (int i = 0; i < dis_matrix.GetLength(0) - 1; i++)
                cityArray[i] = i + 1;

            //answerArray = cityArray;     
            cityArray.CopyTo(answerArray, 0);

            minimalDistance = CalculateDistance(dis_matrix, cityArray);

            while (permNxt(cityArray, 0))
            {
                newDistance = CalculateDistance(dis_matrix, cityArray);
                if (minimalDistance > newDistance)
                {
                    minimalDistance = newDistance;
                    cityArray.CopyTo(answerArray, 0);
                }
            }
            finalanswerArray[0] = 1;
            for (int j = 1; j <= answerArray.Length; j++)
                finalanswerArray[j] = answerArray[j - 1] + 1;

            // answer_array - is answer
            return new Output
            {
                Sequence = finalanswerArray,
                JourneyLength = 0,
                CalculationTime = 0,
                VariantsCount = 0
            };
        }

        private int[] Swap(int[] ar, int i, int j) // доп.функция генерации перестановок маршрута
        {
            int a = ar[i];
            ar[i] = ar[j];
            ar[j] = a;
            return ar;
        }

        private bool permNxt(int[] ar, int a) // доп.функция генерации маршрута
        {
            int rt = ar.Length - 1;
            int i = rt - 1;
            int j = rt;

            while (i >= a && ar[i] >= ar[i + 1])
                i--;

            if (i < a)
                return false;

            j = rt;

            while (ar[i] >= ar[j])
                j--;

            ar = Swap(ar, i, j);
            a = i + 1;

            while (a < rt)
                ar = Swap(ar, a++, rt--);

            return true;
        }

        private double CalculateDistance(double[,] dis_matrix, int[] order) // доп.функция вычисления длины пути по маршруту
        {
            double dist = dis_matrix[0, order[0]] + dis_matrix[order[order.Length - 1], 0];
            int i;

            for (i = 1; i < order.Length; i++)
            {
                dist = dist + dis_matrix[order[i - 1], order[i]];

            }
            return dist;
        }
    }
}