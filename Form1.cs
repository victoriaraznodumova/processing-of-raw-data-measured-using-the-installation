using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
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
        public string fileName_u;
        public string fileName_i;
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
                double duration = getStringArray(fileName).Length / 10;
                TimeSpan timeSpan = TimeSpan.FromSeconds(duration);
                string formattedTime = $"{(int)timeSpan.TotalHours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
                labelDuration.Text = $"Продолжительность: {formattedTime}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string[] getOldLines(string fileName)
        {
            string[] oldLines = File.ReadAllLines(fileName);
            return oldLines;
        }



        public string[] getStringArray(string fileName)
        {
            string[] oldLines = getOldLines(fileName);

            string[] stringLines = new string[oldLines.Length / 2 + (oldLines.Length % 2 == 0 ? 0 : 1)];
            for (int i = 0; i < oldLines.Length; i += 2)
            {
                stringLines[i / 2] = oldLines[i];
            }
            return stringLines;
        }

        public double[] getLastDoubleArray(string[] stringLines)
        {
            double[] lastDoubleArray = new double[80];

            string[] values = stringLines[stringLines.Length - 1].Split('\t');

            for (int i = 0; i < lastDoubleArray.Length; i++)
            {
                lastDoubleArray[i] = Convert.ToDouble(values[i]);
            }


          /*  Console.WriteLine("проверка массива");

            for (int i = 0; i < lastDoubleArray.Length; i++)
            {
                Console.WriteLine(lastDoubleArray[i]);
            }
          */


            return lastDoubleArray;
        }



        private void comboItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedElement = comboItem.SelectedItem.ToString();
        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            string fileName = fileName_u;
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

            string[] oldLines = getOldLines(fileName);
            //string[] oldLines = File.ReadAllLines(fileName);
            string[] lines = new string[oldLines.Length / 2 + (oldLines.Length % 2 == 0 ? 0 : 1)];

            try
            {
                if (selectedElement == "Сигналы Ib(t)")
                {
                    fileName = fileName_i;
                    double[] signalData_u = getLastDoubleArray(getStringArray(fileName));
                    PlotSignal(fileName, signalData_u);
                }
                else if (selectedElement == "Сигналы Ub(t)")
                {
                    
                    fileName = fileName_u;
                    double[] signalData_i = getLastDoubleArray(getStringArray(fileName));
                    PlotSignal(fileName, signalData_i);
                }
                else if (selectedElement == "Спектр сигнала")
                {
                    fileName = fileName_i;
                    //double[] signalData_i = getLastDoubleArray(getStringArray(fileName));
                    PlotSignal_new(fileName);
                }
                else if (selectedElement == "График p(t)")
                {
                    //double[] signalData_i = getLastDoubleArray(getStringArray(fileName));
                    PlotPSignal(fileName_i, fileName_u);
                }
                else if (selectedElement == "Кривые P(t), Q(t), S(t)")
                {
                    PlotSignal_QS(fileName_i, fileName_u);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
            }
        }

        private void PlotSignal(string fileName, double[] signalData)
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

            if (fileName == fileName_u)
            {
                chart1.ChartAreas[0].AxisX.Title = "хуй с U";
                chart1.ChartAreas[0].AxisY.Title = "хуи с U";
            }
            else if (fileName == fileName_i)
            {
                chart1.ChartAreas[0].AxisX.Title = "хуй с I";
                chart1.ChartAreas[0].AxisY.Title = "хуи с I";
            }

           
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;


             chart1.ChartAreas[0].AxisX.Minimum = 0; // Минимальное значение оси X
             chart1.ChartAreas[0].AxisX.Maximum = signalData.Length * secondsPer; // Максимальное значение оси X
             chart1.ChartAreas[0].AxisY.Minimum = signalData.Min(); // Минимальное значение оси Y (минимальное значение данных)
             chart1.ChartAreas[0].AxisY.Maximum = signalData.Max(); // Максимальное значение оси Y (максимальное значение данных

            /*
             chart1.ChartAreas[0].AxisX.Minimum = 0;
             chart1.ChartAreas[0].AxisX.Maximum = Math.Round(signalData.Length * secondsPer, 2);
             chart1.ChartAreas[0].AxisY.Minimum = Math.Round(signalData.Min(), 2);
             chart1.ChartAreas[0].AxisY.Maximum = Math.Round(signalData.Max(), 2);
            */
            //chart1.Legends.Add(new Legend());

            //chart1.Update();
        }

        private void PlotSignal_new(string fileName)
        {
            double[] signalData = getLastDoubleArray(getStringArray(fileName));

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
            List<double> mnim = new List<double>();
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

            chart1.ChartAreas[0].AxisX.Minimum = Math.Round(freq.Min(),2);
            chart1.ChartAreas[0].AxisX.Maximum = Math.Round(freq.Max(), 2); 
            chart1.ChartAreas[0].AxisY.Minimum = Math.Round(mnim.Min(), 1); 
            chart1.ChartAreas[0].AxisY.Maximum = Math.Round(mnim.Max(), 1); 

            chart1.Series.Add(series);
        }


        private void PlotPSignal(string fileName_i, string fileName_u)
        {
            string[] stringLines_U = getStringArray(fileName_u);
            string[] stringLines_I = getStringArray(fileName_i);

            List<double> doubleLines_U = new List<double>();
            List<double> doubleLines_I = new List<double>();


            for (int i = 0; i < stringLines_U.Length; i++)
            {
                string line = stringLines_U[i]; 

                string[] parts = line.Split('\t');

                foreach (string part in parts)
                {
                    double number;
                    if (double.TryParse(part, out number))
                    {
                        doubleLines_U.Add(number);
                    }

                }
            }
            Console.WriteLine(doubleLines_U[0].GetType());
            

            for (int i = 0; i < 10 && i < stringLines_I.Length; i++)
            {
                string line = stringLines_I[i]; 

                string[] parts = line.Split('\t');

                foreach (string part in parts)
                {
                    double number;
                    if (double.TryParse(part, out number))
                    {
                        doubleLines_I.Add(number);
                    }

                }
            }
            double[] signalData = new double[800];

            for (int i = 0; i < signalData.Length; i++)
            {
                signalData[i] = doubleLines_U[i] * doubleLines_I[i];
            }


            chart1.Series.Clear();
            Series series = new Series();
            series.ChartType = SeriesChartType.Line;

            double secondsPer = 0.00125;
            for (int i = 0; i < signalData.Length; i++)
            {
                series.Points.AddXY(i * secondsPer, signalData[i]);
            }

            chart1.Series.Add(series);

            chart1.ChartAreas[0].AxisX.Title = "хуй с p";
            chart1.ChartAreas[0].AxisY.Title = "хуи с p";

            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;


            chart1.ChartAreas[0].AxisX.Minimum = 0; 
            chart1.ChartAreas[0].AxisX.Maximum = signalData.Length * secondsPer;
            chart1.ChartAreas[0].AxisY.Minimum = signalData.Min(); 
            chart1.ChartAreas[0].AxisY.Maximum = signalData.Max(); 

        }

        private void PlotSignal_QS(string fileName_i, string fileName_u)
        {
            string[] stringLines_U = getStringArray(fileName_u);
            string[] stringLines_I = getStringArray(fileName_i);

            List<double> doubleLines_U = new List<double>();
            List<double> doubleLines_I = new List<double>();
            

            for (int i = 0; i < stringLines_U.Length; i++)
            {
                string line = stringLines_U[i];

                string[] parts = line.Split('\t');

                foreach (string part in parts)
                {
                    double number;
                    if (double.TryParse(part, out number))
                    {
                        doubleLines_U.Add(number);
                    }

                }
            }
            Console.WriteLine(stringLines_U.Length);
            Console.WriteLine(doubleLines_U.Count);


            //вычисление P
            //double[] doubtsP = new double[75];
            List<double> doubtsP = new List<double>();
            double p;
            for (int i = 0; i< doubleLines_U.Count - 1; i+= 800) //800 
            {
                p = 0;
                for (int j = 0; j < 800; j++)
                {
                    p += doubleLines_U[j] * doubleLines_I[j];
                    
                }

                doubtsP.Add( 1 / 800 * p);
            }



           /* double[] signalData = new double[800];

            for (int i = 0; i < signalData.Length; i++)
            {
                signalData[i] = doubleLines_U[i] * doubleLines_I[i];
            }


           // double[] p = new double[800]; 

            double dt = 1.0 / 800;

            double integral = 0;
            for (int i = 1; i < signalData.Length; i++)
            {
                integral += 0.5 * (signalData[i - 1] + signalData[i]) * dt;
            }*/

            chart1.Series.Clear();
            Series series = new Series();
            series.ChartType = SeriesChartType.Line;

            series.Name = "P";
            series.Color = Color.Red;

            double secondsPer = 1;
            for (int i = 0; i < doubtsP.Count-1; i++)
            {
                series.Points.AddXY(i * secondsPer, doubtsP[i]);
            }
            chart1.Series.Add(series);
            










            


            
        }





    
    }
}
