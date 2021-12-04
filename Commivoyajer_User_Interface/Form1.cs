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

        private void bruteForceButton_Click(object sender, EventArgs e)
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            var coords = new List<Input>();

            for (int i = 0; i < inputDataGridView.Rows.Count - 1; i++)
            {
                coords.Add(new Input
                {
                    Id = i + 1,
                    XCoord = Convert.ToDouble(inputDataGridView.Rows[i].Cells[0].Value),
                    YCoord = Convert.ToDouble(inputDataGridView.Rows[i].Cells[1].Value)
                });

                chart.Series[0].Points.AddXY(Convert.ToDouble(inputDataGridView.Rows[i].Cells[0].Value),
                    Convert.ToDouble(inputDataGridView.Rows[i].Cells[1].Value));
            }

            var input = _prepareData.PrepareData(coords);

            var time = new Stopwatch();
            time.Start();
            var result = _bruteForceMethod.GetBruteForceMethod(input);
            time.Stop();

            result.CalculationTime = time.ElapsedMilliseconds;
            result.JourneyLength = _prepareData.CalculateJourneyLength(input, result.Sequence);

            ShowDataInUI(coords, result);
        }

        private void branchAndBoundMethodButton_Click(object sender, EventArgs e)
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            var coords = new List<Input>();

            for (int i = 0; i < inputDataGridView.Rows.Count - 1; i++)
            {
                coords.Add(new Input
                {
                    Id = i + 1,
                    XCoord = Convert.ToDouble(inputDataGridView.Rows[i].Cells[0].Value),
                    YCoord = Convert.ToDouble(inputDataGridView.Rows[i].Cells[1].Value)
                });

                chart.Series[0].Points.AddXY(Convert.ToDouble(inputDataGridView.Rows[i].Cells[0].Value),
                    Convert.ToDouble(inputDataGridView.Rows[i].Cells[1].Value));
            }

            var input = _prepareData.PrepareData(coords);
            var watch = new Stopwatch();
            watch.Start();
            var result = _branchAndBoundMethod.Branch_and_bound(input);
            watch.Stop();

            result.CalculationTime = watch.ElapsedMilliseconds;

            ShowDataInUI(coords, result);
        }

        private void dynamicProgrammingMethodButton_Click(object sender, EventArgs e)
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            var coords = new List<Input>();

            for (int i = 0; i < inputDataGridView.Rows.Count - 1; i++)
            {
                coords.Add(new Input
                {
                    Id = i + 1,
                    XCoord = Convert.ToDouble(inputDataGridView.Rows[i].Cells[0].Value),
                    YCoord = Convert.ToDouble(inputDataGridView.Rows[i].Cells[1].Value)
                });

                chart.Series[0].Points.AddXY(Convert.ToDouble(inputDataGridView.Rows[i].Cells[0].Value),
                    Convert.ToDouble(inputDataGridView.Rows[i].Cells[1].Value));
            }

            var input = _prepareData.PrepareData(coords);

            var watch = new Stopwatch();
            watch.Start();

            var result = DyncamicProgrammingSolver.FindTheWay(input);

            watch.Stop();

            result.CalculationTime = watch.ElapsedMilliseconds;

            for (int i = 0; i < result.Sequence.Length; i++)
            {
                result.Sequence[i] = result.Sequence[i] + 1;
            }

            ShowDataInUI(coords, result);
        }

        private void greedyMethodButton_Click(object sender, EventArgs e)
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            var coords = new List<Input>();

            for (int i = 0; i < inputDataGridView.Rows.Count - 1; i++)
            {
                coords.Add(new Input
                {
                    Id = i + 1,
                    XCoord = Convert.ToDouble(inputDataGridView.Rows[i].Cells[0].Value),
                    YCoord = Convert.ToDouble(inputDataGridView.Rows[i].Cells[1].Value)
                });

                chart.Series[0].Points.AddXY(Convert.ToDouble(inputDataGridView.Rows[i].Cells[0].Value),
                    Convert.ToDouble(inputDataGridView.Rows[i].Cells[1].Value));
            }

            var input = _prepareData.PrepareData(coords);

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = _greedyMethod.GetGreedyMethod(input);
            stopWatch.Stop();

            result.CalculationTime = stopWatch.ElapsedMilliseconds;
            result.JourneyLength = _prepareData.CalculateJourneyLength(input, result.Sequence);

            ShowDataInUI(coords, result);
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
            calculationTimeTextBox.Text = stopwatch.ElapsedMilliseconds.ToString() + " ms";
        }
    }
}
