using Comivoyager.StrictMethods.Methods;
using ComivoyagerNext.Methods;
using Commivoyajer_Core.Models;
using System;
using System.Collections.ObjectModel;

namespace ComivoyagerNext.ViewModels
{
    public enum StrictMethods
    {
        BrutForce,
        BranchAndBorders,
        DynamicProgramming,
        Greedy
    }

    public class StrictMethodsListItem
    {
        public string Name { get; init; } = string.Empty;

        public StrictMethods Method { get; init; }
    }


    public class StrictMethodsViewModel
    {
        public ObservableCollection<StrictMethodsListItem> StrictMethodsList { get; } = new()
        {
            new StrictMethodsListItem() { Name = "Полный перебор", Method = StrictMethods.BrutForce },
            new StrictMethodsListItem() { Name = "Ветвей и границ", Method = StrictMethods.BranchAndBorders },
            new StrictMethodsListItem() { Name = "Динамического программирования", Method = StrictMethods.DynamicProgramming },
            new StrictMethodsListItem() { Name = "Жадный алгоритм", Method = StrictMethods.Greedy },
        };

        public StrictMethods SelectedMethod { get; set; }

        internal Output FindTheWay(ReadOnlySpan<Point> cities)
        {
            var ways = Helpers.BuildPartialPaths(cities);

            switch (SelectedMethod)
            {
                case StrictMethods.BrutForce:
                    return new BruteForceMethod().GetBruteForceMethod(ways);
                case StrictMethods.BranchAndBorders:
                    return new BranchAndBoundMethod().Branch_and_bound(ways);
                case StrictMethods.DynamicProgramming:
                    var result =  DyncamicProgrammingSolver.FindTheWay(ways);
                    for (var i = 0; i < result.Sequence.Length; i++)
                        result.Sequence[i] = result.Sequence[i] + 1;
                    return result;
                case StrictMethods.Greedy:
                    return new GreedyMethod().GetGreedyMethod(ways);
                default:
                    break;
            }

            throw new NotImplementedException();
        }
    }
}