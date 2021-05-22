using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Commivoyajer_Core.Methods;
using Commivoyager.DyncamicProgramming;
using Commivoyajer_Core.Models;
using Commivoyajer_Core.PreparationMethods;

namespace Commivoyajer_User_Interface
{
    public partial class Form1 : Form
    {
        private readonly PrepareDataClass _prepareData;
        private readonly BruteForceMethod _bruteForceMethod;
        public Form1()
        {
            _prepareData = new PrepareDataClass();
            _bruteForceMethod = new BruteForceMethod();
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
            result.JourneyLength = _prepareData.CalculateJourneylangth(input, result.Sequence);

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

            var result = PathFinder.FindTheWay(input);

            watch.Stop();

            result.CalculationTime = watch.ElapsedMilliseconds;
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
        }

        private void ShowDataInUI(List<Input> coords, Output output)
        {
            foreach (var city in output.Sequence)
            {
                var cityToAdd = coords.Find(x => x.Id == city);
                chart.Series[1].Points.AddXY(cityToAdd.XCoord, cityToAdd.YCoord);
            }

            calculationTimeTextBox.Text = output.CalculationTime.ToString();
            journeyLengthTextBox.Text = Math.Round(output.JourneyLength, 2).ToString();
        }
    }
}
