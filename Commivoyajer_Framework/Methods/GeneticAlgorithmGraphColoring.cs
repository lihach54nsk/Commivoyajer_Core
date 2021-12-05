using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commivoyajer_Framework.Methods
{
    public sealed class Graph
    {
        public int[] node;

        public Graph(int size)
        {
            node = new int[size];
        }
    }

    public sealed class GeneticAlgorithmGraphColoring
    {
        private int maxNumberOfColors = 0;

        /// <summary>
        /// Colorizes the graph
        /// </summary>
        /// <param name="graph">Adjacency matrix</param>
        public Tuple<int, int[], Graph[]> ColorizeGraph(Graph[] graph, int populationSize, 
            double mutationProbability1, double mutationProbability2, int maxGenerationCount)
        {
            maxNumberOfColors = 1;

            for (int i = 0; i < graph.Length; i++)
                if (graph[i].node.Sum() > maxNumberOfColors)
                    maxNumberOfColors = graph[i].node.Sum() + 1;

            var condition = true;
            var fitnessIndividual = new int[graph.Length];
            var bestFitness = 0.0;

            var numberOfColors = maxNumberOfColors;
            while (condition && numberOfColors > 0)
            {
                var individual = CreateIndividual(graph.Length, numberOfColors);
                var population = CreatePopulation(individual, populationSize, graph.Length, numberOfColors);
                bestFitness = Fitness(graph, population[0]);
                fitnessIndividual = population[0];
                var gen = 0;
                while (bestFitness != 0 && gen != maxGenerationCount)
                {
                    condition = false;
                    gen++;
                    population = RouletteWhellSelection(graph, population);
                    var newPopulation = new List<int[]>();
                    population = ShufflePopulation(population);
                    for (int i = 0; i < populationSize - 1; i += 2)
                    {
                        var children = Crossover(population[i], population[i + 1], graph.Length);
                        newPopulation.Add(children.Item1);
                        newPopulation.Add(children.Item2);
                    }

                    for (int i = 0; i < newPopulation.Count; i++)
                    {
                        newPopulation[i] = gen < 200
                            ? Mutation(newPopulation[i], graph.Length, numberOfColors, mutationProbability1)
                            : Mutation(newPopulation[i], graph.Length, numberOfColors, mutationProbability2);
                    }

                    population = newPopulation;
                    bestFitness = Fitness(graph, population[0]);
                    fitnessIndividual = population[0];

                    for (int i = 0; i < population.Count; i++)
                    {
                        if (Fitness(graph, individual) < bestFitness)
                        {
                            bestFitness = Fitness(graph, individual);
                            fitnessIndividual = individual;
                        }
                    }
                }
            }
            return new Tuple<int, int[], Graph[]>(numberOfColors, fitnessIndividual, graph);
        }

        /// <summary>
        /// Selection with random
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="population"></param>
        /// <returns></returns>
        private List<int[]> RouletteWhellSelection(Graph[] graph, List<int[]> population)
        {
            var newPopulation = new List<int[]>();
            var totalFitness = 0.0;

            foreach (var individual in population)
                totalFitness += 1.0 / (1.0 + Fitness(graph, individual));

            var cumulativeFitness = new List<double>();
            var cumulativeFitnessSum = 0.0;

            for (int i = 0; i < population.Count; i++)
            {
                cumulativeFitnessSum += 1.0 / (1.0 + Fitness(graph, population[i])) / totalFitness;
                cumulativeFitness.Add(cumulativeFitnessSum);
            }

            var random = new Random();
            for (int i = 0; i < population.Count; i++)
            {
                var roulette = random.NextDouble();
                for (int j = 0; j < population.Count; j++)
                {
                    if (roulette <= cumulativeFitness[j])
                    {
                        newPopulation.Add(population[j]);
                        break;
                    }
                }
            }

            return newPopulation;
        }

        /// <summary>
        /// Selection without random
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="population"></param>
        /// <returns></returns>
        private List<int[]> TournamentSelection(Graph[] graph, List<int[]> population)
        {
            var newPopulation = new List<int[]>();
            for (int j = 0; j < 2; j++)
            {
                population = ShufflePopulation(population);

                for (int i = 0; i < population.Count - 1; i += 2)
                {
                    if (Fitness(graph, population[i]) < Fitness(graph, population[i + 1]))
                        newPopulation.Add(population[i]);
                    else
                        newPopulation.Add(population[i + 1]);
                }
            }
            return newPopulation;
        }


        private int[] Mutation(int[] individual, int n, int numberOfColors, double probability)
        {
            var random = new Random();
            var check = random.NextDouble();

            if (check <= probability)
                individual[random.Next(n - 1)] = random.Next(1, numberOfColors);

            return individual;
        }

        private Tuple<int[], int[]> Crossover(int[] parent1, int[] parent2, int n)
        {
            var random = new Random();
            var position = random.Next(2, n - 2);
            var child1 = new int[parent1.Length];
            var child2 = new int[parent2.Length];

            for (int i = 0; i < position + 1; i++)
            {
                child1[i] = parent1[i];
                child1[i] = parent2[i];
            }

            for (int i = position + 1; i < n; i++)
            {
                child1[i] = parent2[i];
                child2[i] = parent1[i];
            }

            return new Tuple<int[], int[]>(child1, child2);
        }

        private double Fitness(Graph[] graph, int[] individual)
        {
            var fitness = 0;

            for (int i = 0; i < graph.Length; i++)
                for (int j = i; j < graph.Length; j++)
                    if (individual[i] == individual[j] && graph[i].node[j] == 1)
                        fitness += 1;

            return fitness;
        }

        private List<int[]> CreatePopulation(int[] individuals, int populationSize, int n, int numberOfColors)
        {
            var population = new List<int[]>();

            for (int i = 0; i < populationSize; i++)
                population.Add(CreateIndividual(n, numberOfColors));

            return population;
        }

        private int[] CreateIndividual(int n, int numberOfColors)
        {
            var individuals = new int[n];
            var random = new Random();

            for (int i = 0; i < n; i++)
                individuals[i] = random.Next(1, numberOfColors);

            return individuals;
        }

        private List<int[]> ShufflePopulation(List<int[]> population)
        {
            for (int i = 0; i < population.Count; i++)
                population[i] = ShuffleArray(population[i]);

            return population;
        }

        private int[] ShuffleArray(int[] array)
        {
            var random = new Random();
            return array.OrderBy(x => random.Next()).ToArray();
        }

        /// <summary>
        /// Creates a graph as a adjacency matrix
        /// </summary>
        /// <param name="n">Graph dimension</param>
        public static Graph[] GenerateGraph(int size)
        {
            var result = new Graph[size];

            var random = new Random();

            for (var i = 0; i < size; i++)
            {
                result[i] = new Graph(size);
                result[i].node = new int[size];

                for (var j = 0; j < size; j++)
                    result[i].node[j] = random.Next(2);
            }

            for (var i = 0; i < size; i++)
                for (var j = 0; j < i; j++)
                    result[i].node[j] = result[j].node[i];

            return result;
        }
    }
}
