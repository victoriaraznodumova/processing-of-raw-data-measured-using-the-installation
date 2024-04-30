
namespace ДЗ1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button1 = new System.Windows.Forms.Button();
            this.labelDuration = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.comboItem = new System.Windows.Forms.ComboBox();
            this.buttonPlot = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.labelNew = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(204, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Считать файл Ub(t)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelDuration
            // 
            this.labelDuration.BackColor = System.Drawing.SystemColors.Control;
            this.labelDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDuration.Location = new System.Drawing.Point(804, 30);
            this.labelDuration.Name = "labelDuration";
            this.labelDuration.Size = new System.Drawing.Size(334, 35);
            this.labelDuration.TabIndex = 1;
            this.labelDuration.Text = "Продолжительность";
            this.labelDuration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelName
            // 
            this.labelName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelName.Location = new System.Drawing.Point(289, 30);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(479, 35);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Имя файла";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboItem
            // 
            this.comboItem.FormattingEnabled = true;
            this.comboItem.Items.AddRange(new object[] {
            "Сигналы Ub(t)",
            "Сигналы Ib(t)",
            "Спектр сигнала",
            "График p(t)",
            "Кривые P(t), Q(t), S(t)",
            "Гармоники(амплитуда)",
            "Гармоники(фаза)"});
            this.comboItem.Location = new System.Drawing.Point(26, 174);
            this.comboItem.Name = "comboItem";
            this.comboItem.Size = new System.Drawing.Size(204, 28);
            this.comboItem.TabIndex = 3;
            this.comboItem.SelectedIndexChanged += new System.EventHandler(this.comboItem_SelectedIndexChanged);
            // 
            // buttonPlot
            // 
            this.buttonPlot.Location = new System.Drawing.Point(289, 167);
            this.buttonPlot.Name = "buttonPlot";
            this.buttonPlot.Size = new System.Drawing.Size(186, 40);
            this.buttonPlot.TabIndex = 4;
            this.buttonPlot.Text = "Построить график";
            this.buttonPlot.UseVisualStyleBackColor = true;
            this.buttonPlot.Click += new System.EventHandler(this.buttonPlot_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(26, 92);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(204, 35);
            this.button3.TabIndex = 5;
            this.button3.Text = "Считать файл Ib(t)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelNew
            // 
            this.labelNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelNew.ForeColor = System.Drawing.Color.Black;
            this.labelNew.Location = new System.Drawing.Point(289, 89);
            this.labelNew.Name = "labelNew";
            this.labelNew.Size = new System.Drawing.Size(479, 38);
            this.labelNew.TabIndex = 6;
            this.labelNew.Text = "Имя файла";
            this.labelNew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(26, 242);
            this.chart1.Name = "chart1";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Color = System.Drawing.Color.Navy;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(657, 322);
            this.chart1.TabIndex = 8;
            this.chart1.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 606);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.labelNew);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonPlot);
            this.Controls.Add(this.comboItem);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelDuration);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Добавить файл";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelDuration;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.ComboBox comboItem;
        private System.Windows.Forms.Button buttonPlot;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label labelNew;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}

