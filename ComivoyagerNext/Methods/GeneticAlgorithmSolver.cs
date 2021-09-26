using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComivoyagerNext.Methods
{
    internal struct GeneticAlgorithmSolver
    {
        private readonly Random random;

        private readonly int generationSize;

        private readonly int generationsCount;

        private readonly int candidateGenerationSize;

        private readonly int vipGenCount;

        private readonly double mutataionPropability;

        public GeneticAlgorithmSolver(int generationSize, int generationsCount, int candidateGenerationSize, int vipGenCount, double mutataionPropability)
        {
            random = new Random();

            this.generationSize = generationSize;
            this.generationsCount = generationsCount;
            this.candidateGenerationSize = candidateGenerationSize;
            this.vipGenCount = vipGenCount;
            this.mutataionPropability = mutataionPropability;
        }

        public (int[] path, double pathLength) FoldCitiesOrder(ReadOnlySpan<Point> cities)
        {
            var partialPathLengths = Helpers.BuildPartialPaths(cities);

            var currentGeneration = CreateDefaultGeneration(cities.Length);

            var currentGenerationLengths = new double[currentGeneration.Length];

            var candidateGeneration = new int[candidateGenerationSize][];

            for (int i = 0; i < generationsCount; i++)
            {
                UpdateLengths(currentGeneration, partialPathLengths, currentGenerationLengths);

                Array.Sort(currentGenerationLengths, currentGeneration);

                FillVipGens(currentGeneration, candidateGeneration);

                Crossover(currentGeneration, candidateGeneration);

                Mutate(candidateGeneration);
            }

            throw new NotImplementedException();
        }

        public int[][] CreateDefaultGeneration(int length)
        {
            var result = new int[generationSize][];

            Array.Fill(result, new int[length]);

            Span<int> candidatePath = stackalloc int[length];

            for (int i = 0; i < candidatePath.Length; i++)
            {
                candidatePath[i] = i;
            }

            for (int i = 0; i < result.Length; i++)
            {
                candidatePath.CopyTo(result[i]);

                for (int j = 0; j < candidatePath.Length; j++)
                {
                    var replacePosition = random.Next(j, candidatePath.Length);

                    (candidatePath[j], candidatePath[replacePosition]) = (candidatePath[replacePosition], candidatePath[j]);
                }
            }

            return result;
        }

        private static void UpdateLengths(int[][] paths, double[,] partialPaths, double[] lengths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                var sumLength = 0.0;

                for (int j = 1; j < paths[i].Length; j++)
                {
                    sumLength += partialPaths[paths[i][j - 1], paths[i][j]];
                }

                lengths[i] = sumLength;
            }
        }

        private void FillVipGens(int[][] currentGeneration, int[][] candidateGeneration)
        {
            for (int i = 0; i < vipGenCount; i++)
            {
                currentGeneration[i].CopyTo(candidateGeneration[i], candidateGeneration[i].Length);
            }
        }

        private void Crossover(int[][] currentGeneration, int[][] candidateGeneration)
        {
            for (int i = vipGenCount; i < candidateGeneration.Length; i++)
            {
                var candidate = candidateGeneration[i];

                // We are politically correct here 
                var parent1 = currentGeneration[random.Next(currentGeneration.Length)];
                var parent2 = currentGeneration[random.Next(currentGeneration.Length)];

                parent1.CopyTo(candidate, candidate.Length);

                var cutGenPosition1 = random.Next(candidate.Length);
                var cutGenPosition2 = random.Next(candidate.Length + 1);

                if(cutGenPosition1 > cutGenPosition2)
                {
                    (cutGenPosition1, cutGenPosition2) = (cutGenPosition2, cutGenPosition1);
                }

                for (int j = cutGenPosition1; j < cutGenPosition2; j++)
                {
                    var replacePosition = Array.FindIndex(candidate, x => x == parent2[j]);

                    (candidate[j], candidate[replacePosition]) = (candidate[replacePosition], candidate[j]);
                }

                var parentGen = parent2.AsSpan()[cutGenPosition1..cutGenPosition2];

                var candidateGen = candidate.AsSpan()[cutGenPosition1..cutGenPosition2];

                parentGen.CopyTo(candidateGen);
            }
        }

        private void Mutate(int[][] candidateGeneration)
        {
            foreach (var candidate in candidateGeneration)
            {
                if(random.NextDouble() < mutataionPropability)
                {
                    var swap1 = random.Next(candidate.Length);
                    var swap2 = random.Next(candidate.Length);

                    (candidate[swap1], candidate[swap2]) = (candidate[swap2], candidate[swap1]);
                }
            }
        }
    }
}
