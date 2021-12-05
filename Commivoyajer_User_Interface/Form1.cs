using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Commivoyajer_Core.Methods;
using Commivoyager.DyncamicProgramming;
using Commivoyajer_Core.Models;
using Commivoyajer_Core.PreparationMethods;
using Commivoyajer_Framework.Methods;

namespace Commivoyajer_User_Interface
{
    public partial class Form1 : Form
    {
        private readonly PrepareDataClass _prepareData;
        private readonly BruteForceMethod _bruteForceMethod;
        private readonly GreedyMethod _greedyMethod;
        private readonly BranchAndBoundMethod _branchAndBoundMethod;
        public Form1()
        {
            _prepareData = new PrepareDataClass();
            _bruteForceMethod = new BruteForceMethod();
            _greedyMethod = new GreedyMethod();
            _branchAndBoundMethod = new BranchAndBoundMethod();
            InitializeComponent();
        }

        private void ShowDataInUI(List<Input> coords, Output output)
        {
            var sequenceString = "";
            foreach (var city in output.Sequence)
            {
                var cityToAdd = coords.Find(x => x.Id == city);
                sequenceString += city.ToString() + "-";
                chart.Series[1].Points.AddXY(cityToAdd.XCoord, cityToAdd.YCoord);
            }
            var firstCity = coords.Find(x => x.Id == output.Sequence[0]);
            chart.Series[1].Points.AddXY(firstCity.XCoord, firstCity.YCoord);

            calculationTimeTextBox.Text = output.CalculationTime.ToString();
            //journeyLengthTextBox.Text = Math.Round(output.JourneyLength, 2).ToString();
            //sequenceTextBox.Text = sequenceString + output.Sequence[0].ToString();
        }

        private void colorizeGraphButton_Click(object sender, EventArgs e)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var result = new GeneticAlgorithmGraphColoring()
                .ColorizeGraph(GeneticAlgorithmGraphColoring.GenerateGraph(Convert.ToInt32(graphDimensionTextBox.Text)),
                Convert.ToInt32(populationSizeTextBox.Text), Convert.ToDouble(mutatuionProbability1TextBox.Text),
                Convert.ToDouble(mutatuionProbability2TextBox.Text), Convert.ToInt32(generationTextBox.Text));
            stopwatch.Stop();

            ShowGraphInUI(result.Item1, result.Item2, result.Item3);
            calculationTimeTextBox.Text = stopwatch.ElapsedMilliseconds.ToString() + " ms";
            colorsCountTextBox.Text = result.Item1.ToString();
        }

        private void ShowGraphInUI(int numberOfColors, int[] colorSequence, Graph[] graph)
        {
            chart.Series.Clear();
            chart.Series.Add("Nodes");
            chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart.Series[0].Points.Clear();
            var x = 0.0;
            var y = 0.0;
            var coords = new List<double[]>();
            for (int i = 0; i < graph.Length; i++)
            {
                double k = (double)i / (double)graph.Length < 0.5 ? 1 : -1;
                chart.Series[0].Points.AddXY(x, y);
                coords.Add(new double[] { x, y });
                x += 5.0;
                y += 5.0 * k;
            }

            for (int i = 0; i < graph.Length; i++)
            {
                if (chart.Series.IsUniqueName($"Edges of {i} node"))
                {
                    chart.Series.Add($"Edges of {i} node");
                    chart.Series[i + 1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    chart.Series[i + 1].Points.AddXY(coords[i][0], coords[i][1]);
                }
                else
                {
                    chart.Series[i + 1].Points.Clear();
                    chart.Series[i + 1].Points.AddXY(coords[i][0], coords[i][1]);
                }

                for (int j = 0; j < graph[i].node.Length; j++)
                {
                    if (graph[i].node[j] == 1)
                    {
                        chart.Series[i + 1].Points.AddXY(coords[j][0], coords[j][1]);
                        chart.Series[i + 1].Points.AddXY(coords[i][0], coords[i][1]);
                    }
                }
            }
        }
    }
}
