using Commivoyajer_Core.Models;

namespace Commivoyajer_Core.Methods
{
    public class BruteForceMethod
    {
        public Output GetBruteForceMethod(double[,] dis_matrix) // Сама функция перебора
        {
            int[] cityArray = new int[dis_matrix.GetLength(0)];
            int[] answerArray = new int[dis_matrix.GetLength(0)];
            double minimalDistance = 0;
            double newDistance = 0;

            for (int i = 0; i < dis_matrix.GetLength(0); i++)
                cityArray[i] = i + 1;

            answerArray = cityArray;
            minimalDistance = CalculateDistance(dis_matrix, cityArray);

            while (permNxt(cityArray, 0))
            {
                newDistance = CalculateDistance(dis_matrix, cityArray);
                if (minimalDistance > newDistance)
                {
                    minimalDistance = newDistance;
                    answerArray = cityArray;
                }
            }

            // Вывод пути и длины пути в текстбокс
            // answer_array - is answer
            return new Output
            {
                Sequence = answerArray,
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

            while ((i >= a) && (ar[i] >= ar[i]))
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
            double dist = 0;
            int i;

            for (i = 0; i < order.Length; i++)
            {
                if (i == 0)
                {
                    dist += dis_matrix[0, order[i]];
                }
                else if (i > 0 && i < order.Length - 1)
                {
                    dist += dis_matrix[order[i], order[i - 1]];
                }
                else if (i == order.Length - 1)
                {
                    dist += dis_matrix[order[i - 1], 0];
                }
            }
            return dist;
        }
    }
}
