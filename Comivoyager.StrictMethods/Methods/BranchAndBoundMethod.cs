using Commivoyajer_Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Comivoyager.StrictMethods.Methods
{
    public partial class BranchAndBoundMethod
    {
        //int[] result_sequence;
        List<int> result_mas_sequence;
        double result_time = 0;
        int count_var = 0;

        private struct Res
        {
            public double time;
            public int count_var;
            public List<int> result_mas_sequence;

            public Res(double time, int count_var, List<int> res_seq)
            {
                this.time = time;
                this.count_var = count_var;
                result_mas_sequence = res_seq;
            }
        }


        //метод ветвей и границ
        public Output Branch_and_bound(double[,] ways)
        {
            int vertex_count = ways.GetLength(0);
            double[,] matrix = (double[,])ways.Clone();

            double[] colors = Enumerable.Repeat<double>(1, vertex_count).ToArray();
            double upper_bound = Greedy(matrix, 0, colors.ToArray()); // верхняя оценка в корне
            double lower_bound = Lower_estimate_root(matrix, vertex_count); // нижняя оценка в корене
            double[] ub = new double[vertex_count - 1];
            double[] lb = new double[vertex_count - 1];

            Res result = Branching(matrix, 0, vertex_count, colors.ToArray(), upper_bound); // результат

            //результат

            for (int i = 0; i < result.result_mas_sequence.Count; i++)
                result.result_mas_sequence[i] = result.result_mas_sequence[i] + 1;

            return new Output
            {
                Sequence = result.result_mas_sequence.ToArray(),
                CalculationTime = 0,
                JourneyLength = result.time,
                VariantsCount = result.count_var
            };
        }

        //расчёт верхней оценки "жадный алгоритм"
        private double Greedy(double[,] matrix, int current_line, double[] colors)
        {
            colors[current_line] = 2;
            if (!colors.Contains(1))
                return matrix[current_line, 0];
            double min;
            int i_min = current_line;
            min = int.MaxValue;
            for (int i = 0; i < matrix.GetLength(0); i++)
                if (i != current_line && colors[i] == 1)
                    if (matrix[current_line, i] < min)
                    {
                        min = matrix[current_line, i];
                        i_min = i;
                    }
            return min + Greedy(matrix, i_min, colors);
        }

        //расчёт нижней оценки в корне
        private double Lower_estimate_root(double[,] matrix, int vertex_count)
        {
            double[] min_line = Enumerable.Repeat(double.MaxValue, vertex_count).ToArray(); // минимумы по строкам
            double[] min_column = min_line.ToArray(); // минимумы по столбцам

            for (int i = 0; i < vertex_count; i++)
            {
                for (int j = 0; j < vertex_count; j++)
                {
                    if (i != j)
                    {
                        min_column[i] = min_column[i] < matrix[i, j] ? min_column[i] : matrix[i, j];
                        min_line[i] = min_line[i] < matrix[i, j] ? min_line[i] : matrix[i, j];
                    }
                }
            }
            double mls = min_line.Sum();
            double mcs = min_column.Sum();
            return mls < mcs ? mcs : mls;
        }

        //расчёт нижней оценки рекурсивно
        private double Lower_estimate(double[,] matrix, int current_line, int vertex_count, double[] colors)
        {
            colors[current_line] = 2;
            double[] min_line = Enumerable.Repeat(double.MaxValue, vertex_count).ToArray(); // минимумы по строкам

            for (int i = 0; i < vertex_count; i++)
            {
                for (int j = 0; j < vertex_count; j++)
                {
                    if (i != j && colors[i] == 1)
                    {
                        min_line[i] = min_line[i] < matrix[i, j] ? min_line[i] : matrix[i, j];
                    }
                    else
                        min_line[i] = 0;
                }
            }

            return min_line.Sum();
        }

        //ветвление
        private Res Branching(double[,] matrix, int current_line, int vertex_count, double[] colors, double last_ub)
        {
            colors[current_line] = 2;
            if (!colors.Contains(1))
            {
                List<int> to_res = new List<int>();
                to_res.Add(0);
                return new Res(matrix[current_line, 0], 1, to_res);
            }
            double[] ub = new double[vertex_count];
            double[] lb = new double[vertex_count];
            List<Res> results = new List<Res>();
            Res out_res;
            for (int i = 0; i < vertex_count; i++)
            {
                if (colors[i] == 1)
                {
                    ub[i] = matrix[0, i] + Greedy(matrix, i, colors.ToArray()); // верхняя оценка
                    lb[i] = matrix[0, i] + Lower_estimate(matrix, i, vertex_count, colors.ToArray()); // нижняя оценка
                    if (lb[i] <= last_ub)
                    {
                        out_res = Branching(matrix, i, vertex_count, colors.ToArray(), ub[i]); // ветвление дальше
                        out_res.time += matrix[current_line, i];
                        out_res.result_mas_sequence.Add(i);
                        results.Add(out_res);
                    }
                }
            }
            Res min_res = results[0];
            for (int i = 1; i < results.Count; i++)
            {
                if (results[i].time <= min_res.time)
                {
                    if (results[i].time == min_res.time)
                    {
                        min_res.count_var += results[i].count_var;
                    }
                    else
                    {
                        min_res = results[i];
                    }
                }
            }
            return min_res;
        }

    }
}
