using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Commivoyager.DyncamicProgramming;
using Commivoyajer_Core.Models;
using Commivoyajer_Core.PreparationMethods;

namespace Commivoyajer_User_Interface
{
    public partial class Form1 : Form
    {
        private readonly PrepareDataClass _prepareData;
        public Form1()
        {
            _prepareData = new PrepareDataClass();
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
    }
}
