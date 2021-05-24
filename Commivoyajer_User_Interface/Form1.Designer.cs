
namespace Commivoyajer_User_Interface
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.inputDataGridView = new System.Windows.Forms.DataGridView();
            this.XCoord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YCoord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bruteForceButton = new System.Windows.Forms.Button();
            this.branchAndBoundMethodButton = new System.Windows.Forms.Button();
            this.dynamicProgrammingMethodButton = new System.Windows.Forms.Button();
            this.greedyMethodButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.calculationTimeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.journeyLengthTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(12, 12);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            series1.Legend = "Legend1";
            series1.Name = "Cities";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Red;
            series2.Legend = "Legend1";
            series2.Name = "Path";
            this.chart.Series.Add(series1);
            this.chart.Series.Add(series2);
            this.chart.Size = new System.Drawing.Size(525, 353);
            this.chart.TabIndex = 0;
            this.chart.Text = "Chart";
            // 
            // inputDataGridView
            // 
            this.inputDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.XCoord,
            this.YCoord});
            this.inputDataGridView.Location = new System.Drawing.Point(543, 12);
            this.inputDataGridView.Name = "inputDataGridView";
            this.inputDataGridView.Size = new System.Drawing.Size(245, 164);
            this.inputDataGridView.TabIndex = 1;
            // 
            // XCoord
            // 
            this.XCoord.HeaderText = "X";
            this.XCoord.Name = "XCoord";
            // 
            // YCoord
            // 
            this.YCoord.HeaderText = "Y";
            this.YCoord.Name = "YCoord";
            // 
            // bruteForceButton
            // 
            this.bruteForceButton.Location = new System.Drawing.Point(544, 183);
            this.bruteForceButton.Name = "bruteForceButton";
            this.bruteForceButton.Size = new System.Drawing.Size(244, 26);
            this.bruteForceButton.TabIndex = 2;
            this.bruteForceButton.Text = "Полный перебор";
            this.bruteForceButton.UseVisualStyleBackColor = true;
            this.bruteForceButton.Click += new System.EventHandler(this.bruteForceButton_Click);
            // 
            // branchAndBoundMethodButton
            // 
            this.branchAndBoundMethodButton.Location = new System.Drawing.Point(543, 215);
            this.branchAndBoundMethodButton.Name = "branchAndBoundMethodButton";
            this.branchAndBoundMethodButton.Size = new System.Drawing.Size(245, 26);
            this.branchAndBoundMethodButton.TabIndex = 3;
            this.branchAndBoundMethodButton.Text = "Метод ветвей и границ";
            this.branchAndBoundMethodButton.UseVisualStyleBackColor = true;
            this.branchAndBoundMethodButton.Click += new System.EventHandler(this.branchAndBoundMethodButton_Click);
            // 
            // dynamicProgrammingMethodButton
            // 
            this.dynamicProgrammingMethodButton.Location = new System.Drawing.Point(543, 247);
            this.dynamicProgrammingMethodButton.Name = "dynamicProgrammingMethodButton";
            this.dynamicProgrammingMethodButton.Size = new System.Drawing.Size(245, 26);
            this.dynamicProgrammingMethodButton.TabIndex = 4;
            this.dynamicProgrammingMethodButton.Text = "Динамическое программирование";
            this.dynamicProgrammingMethodButton.UseVisualStyleBackColor = true;
            this.dynamicProgrammingMethodButton.Click += new System.EventHandler(this.dynamicProgrammingMethodButton_Click);
            // 
            // greedyMethodButton
            // 
            this.greedyMethodButton.Location = new System.Drawing.Point(543, 279);
            this.greedyMethodButton.Name = "greedyMethodButton";
            this.greedyMethodButton.Size = new System.Drawing.Size(245, 26);
            this.greedyMethodButton.TabIndex = 5;
            this.greedyMethodButton.Text = "Жадный алгоритм";
            this.greedyMethodButton.UseVisualStyleBackColor = true;
            this.greedyMethodButton.Click += new System.EventHandler(this.greedyMethodButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 377);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Время работы метода: ";
            // 
            // calculationTimeTextBox
            // 
            this.calculationTimeTextBox.Location = new System.Drawing.Point(144, 374);
            this.calculationTimeTextBox.Name = "calculationTimeTextBox";
            this.calculationTimeTextBox.ReadOnly = true;
            this.calculationTimeTextBox.Size = new System.Drawing.Size(100, 20);
            this.calculationTimeTextBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 405);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Длина пути: ";
            // 
            // journeyLengthTextBox
            // 
            this.journeyLengthTextBox.Location = new System.Drawing.Point(144, 402);
            this.journeyLengthTextBox.Name = "journeyLengthTextBox";
            this.journeyLengthTextBox.ReadOnly = true;
            this.journeyLengthTextBox.Size = new System.Drawing.Size(100, 20);
            this.journeyLengthTextBox.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.journeyLengthTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.calculationTimeTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.greedyMethodButton);
            this.Controls.Add(this.dynamicProgrammingMethodButton);
            this.Controls.Add(this.branchAndBoundMethodButton);
            this.Controls.Add(this.bruteForceButton);
            this.Controls.Add(this.inputDataGridView);
            this.Controls.Add(this.chart);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.DataGridView inputDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn XCoord;
        private System.Windows.Forms.DataGridViewTextBoxColumn YCoord;
        private System.Windows.Forms.Button bruteForceButton;
        private System.Windows.Forms.Button branchAndBoundMethodButton;
        private System.Windows.Forms.Button dynamicProgrammingMethodButton;
        private System.Windows.Forms.Button greedyMethodButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox calculationTimeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox journeyLengthTextBox;
    }
}

