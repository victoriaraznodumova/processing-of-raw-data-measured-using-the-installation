using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data;
using System.Linq;
using MathNet.Numerics.IntegralTransforms;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using FftSharp;

namespace ДЗ1
{
    public partial class Form1 : Form
    {
        private string selectedElement;
        private string fileName_u;
        private string fileName_i;
        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName_u = openFileDialog.FileName;
                ReadData(fileName_u);
                //string shortFileName = Path.GetFileName(fileName);
                labelName.Text = $"{fileName_u}";
            } 
        }

        public void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName_i = openFileDialog.FileName;
                ReadData(fileName_i);
                //string shortFileName = Path.GetFileName(fileName);
                labelNew.Text = $"{fileName_i}";
            }
        }

        public void ReadData(string fileName)
        {
            try
            {
                string[] oldLines = File.ReadAllLines(fileName);

                string[] lines = new string [oldLines.Length / 2 +(oldLines.Length % 2 == 0 ? 0 : 1)];

                for (int i = 0; i < oldLines.Length; i+=2)
                {
                    lines[i / 2] = oldLines[i];
                    
                }

                double duration = lines.Length / 10;
                TimeSpan timeSpan = TimeSpan.FromSeconds(duration);
                string formattedTime = $"{(int)timeSpan.TotalHours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
                labelDuration.Text = $"Продолжительность: {formattedTime}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void comboItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedElement = comboItem.SelectedItem.ToString();
        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            string fileName = "";
            if (string.IsNullOrEmpty(selectedElement))
            {
                MessageBox.Show("Выберите элемент в ComboBox.");
                return;
            }

            string filePath1 = labelName.Text;
            string filePath2 = labelNew.Text;
            if (!File.Exists(filePath1)) //|| (!File.Exists(filePath2)))
            {
                MessageBox.Show("Файл не найден.");
                return;
            }

            try
            {
                if ((selectedElement == "Сигналы Ib(t)") || (selectedElement == "Спектр сигнала"))
                {
                    fileName = fileName_i;
                }
                else if (selectedElement == "Сигналы Ub(t)")
                {
                    fileName = fileName_u;
                }

                string[] oldLines = File.ReadAllLines(fileName);
                string[] lines = new string[oldLines.Length / 2 + (oldLines.Length % 2 == 0 ? 0 : 1)];
                PlotSignal(oldLines);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
            }
        }

        private void PlotSignal(string[] lines)
        {
            if (lines.Length == 0)
            {
                MessageBox.Show("Файл пуст.");
                return;
            }

            string lastLine = lines.Last();

            string[] values = lastLine.Split('\t');

            double[] signalData = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {

                if (!double.TryParse(values[i], out signalData[i]))
                {
                    MessageBox.Show($"Невозможно преобразовать значение \"{values[i]}\" в число.");
                    return;
                }
            }

            if ((selectedElement == "Сигналы Ub(t)") || (selectedElement == "Сигналы Ib(t)"))
            {
                chart1.Series.Clear();
                Series series = new Series();
                series.ChartType = SeriesChartType.Line;

                double secondsPer = 0.1;
                for (int i = 0; i < signalData.Length; i++)
                {
                    series.Points.AddXY(i * secondsPer, signalData[i]);
                }

                chart1.Series.Add(series);

                chart1.ChartAreas[0].AxisX.Title = "Time (seconds)";
                chart1.ChartAreas[0].AxisY.Title = "Signal Value";

                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;

                chart1.Legends.Add(new Legend());

                chart1.Update();
            }
            else if (selectedElement == "Спектр сигнала")
            {
                int N = signalData.Length;
                double delta_t = 0.0025;
                double f = 50.0;
                double delta_f = 0.015625 / delta_t;
                double[] freq = new double[N];
                System.Numerics.Complex[] result = new System.Numerics.Complex[N];
                List<double> result_double = new List<double>();

                double[] new_result = new double[64];
                for (int i = 0; i < new_result.Length; i++)
                {
                    new_result[i] = signalData[i];
                }

                for (int k = 0; k < 64; k++)
                {
                    for (int n = 0; n < 64; n++)
                    {
                        double arg = -2 * Math.PI * k * n / 64;
                        var complex = new System.Numerics.Complex(Math.Cos(-arg), Math.Sin(arg));               
                        result[k] += signalData[n] * complex;

                    }
                }
               
                for (int i = 0; i < N; i++)
                {
                    freq[i] = i * delta_f; //x
                }

                System.Numerics.Complex[] fft = FftSharp.FFT.Forward(new_result);
                List<double> mnim = new List<double> ();
                foreach (var complexNumber in fft)
                {
                    double im = complexNumber.Imaginary;
                    double rel = complexNumber.Real;
                    double absmnim = Math.Sqrt(Math.Pow(im, 2) + Math.Pow(rel, 2));
                    mnim.Add(absmnim);
                }   

                chart1.Series.Clear();
                chart1.ChartAreas[0].AxisX.Title = "Частота";
                chart1.ChartAreas[0].AxisY.Title = "Амплитуда";

                Series series = new Series();
                series.ChartType = SeriesChartType.Line;

                for (int i = 0; i < mnim.Count; i++)
                {
                    series.Points.AddXY(freq[i], mnim[i]);
                }

                chart1.Series.Add(series);
            }
        }

        

    }
}
