
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
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.calculationTimeTextBox = new System.Windows.Forms.TextBox();
            this.colorizeGraphButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.generationTextBox = new System.Windows.Forms.TextBox();
            this.mutatuionProbability1TextBox = new System.Windows.Forms.TextBox();
            this.mutatuionProbability2TextBox = new System.Windows.Forms.TextBox();
            this.graphDimensionTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.populationSizeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.colorsCountTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
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
            series1.Name = "Nodes";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(818, 507);
            this.chart.TabIndex = 0;
            this.chart.Text = "Chart";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(839, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Время работы метода: ";
            // 
            // calculationTimeTextBox
            // 
            this.calculationTimeTextBox.Location = new System.Drawing.Point(971, 177);
            this.calculationTimeTextBox.Name = "calculationTimeTextBox";
            this.calculationTimeTextBox.ReadOnly = true;
            this.calculationTimeTextBox.Size = new System.Drawing.Size(100, 20);
            this.calculationTimeTextBox.TabIndex = 7;
            // 
            // colorizeGraphButton
            // 
            this.colorizeGraphButton.Location = new System.Drawing.Point(836, 141);
            this.colorizeGraphButton.Name = "colorizeGraphButton";
            this.colorizeGraphButton.Size = new System.Drawing.Size(245, 26);
            this.colorizeGraphButton.TabIndex = 12;
            this.colorizeGraphButton.Text = "Раскрасить граф";
            this.colorizeGraphButton.UseVisualStyleBackColor = true;
            this.colorizeGraphButton.Click += new System.EventHandler(this.colorizeGraphButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(836, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Количество поколений: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(836, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Вероятность мутации 1: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(836, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Вероятность мутации 2: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(836, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Размерность графа: ";
            // 
            // generationTextBox
            // 
            this.generationTextBox.Location = new System.Drawing.Point(971, 9);
            this.generationTextBox.Name = "generationTextBox";
            this.generationTextBox.Size = new System.Drawing.Size(100, 20);
            this.generationTextBox.TabIndex = 17;
            this.generationTextBox.Text = "10000";
            // 
            // mutatuionProbability1TextBox
            // 
            this.mutatuionProbability1TextBox.Location = new System.Drawing.Point(971, 35);
            this.mutatuionProbability1TextBox.Name = "mutatuionProbability1TextBox";
            this.mutatuionProbability1TextBox.Size = new System.Drawing.Size(100, 20);
            this.mutatuionProbability1TextBox.TabIndex = 18;
            this.mutatuionProbability1TextBox.Text = "0,4";
            // 
            // mutatuionProbability2TextBox
            // 
            this.mutatuionProbability2TextBox.Location = new System.Drawing.Point(971, 61);
            this.mutatuionProbability2TextBox.Name = "mutatuionProbability2TextBox";
            this.mutatuionProbability2TextBox.Size = new System.Drawing.Size(100, 20);
            this.mutatuionProbability2TextBox.TabIndex = 19;
            this.mutatuionProbability2TextBox.Text = "0,2";
            // 
            // graphDimensionTextBox
            // 
            this.graphDimensionTextBox.Location = new System.Drawing.Point(971, 87);
            this.graphDimensionTextBox.Name = "graphDimensionTextBox";
            this.graphDimensionTextBox.Size = new System.Drawing.Size(100, 20);
            this.graphDimensionTextBox.TabIndex = 20;
            this.graphDimensionTextBox.Text = "20";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(836, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Размер популяции: ";
            // 
            // populationSizeTextBox
            // 
            this.populationSizeTextBox.Location = new System.Drawing.Point(971, 115);
            this.populationSizeTextBox.Name = "populationSizeTextBox";
            this.populationSizeTextBox.Size = new System.Drawing.Size(100, 20);
            this.populationSizeTextBox.TabIndex = 22;
            this.populationSizeTextBox.Text = "200";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(839, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Количество цветов: ";
            // 
            // colorsCountTextBox
            // 
            this.colorsCountTextBox.Location = new System.Drawing.Point(971, 203);
            this.colorsCountTextBox.Name = "colorsCountTextBox";
            this.colorsCountTextBox.ReadOnly = true;
            this.colorsCountTextBox.Size = new System.Drawing.Size(100, 20);
            this.colorsCountTextBox.TabIndex = 24;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 527);
            this.Controls.Add(this.colorsCountTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.populationSizeTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.graphDimensionTextBox);
            this.Controls.Add(this.mutatuionProbability2TextBox);
            this.Controls.Add(this.mutatuionProbability1TextBox);
            this.Controls.Add(this.generationTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.colorizeGraphButton);
            this.Controls.Add(this.calculationTimeTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox calculationTimeTextBox;
        private System.Windows.Forms.Button colorizeGraphButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox generationTextBox;
        private System.Windows.Forms.TextBox mutatuionProbability1TextBox;
        private System.Windows.Forms.TextBox mutatuionProbability2TextBox;
        private System.Windows.Forms.TextBox graphDimensionTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox populationSizeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox colorsCountTextBox;
    }
}

