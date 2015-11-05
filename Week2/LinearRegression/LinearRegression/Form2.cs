using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LinearRegression
{
    public partial class Form2 : Form
    {
        //private Program MyNameClass;
        //private object testBox1;

        //public void Ourputmessage(string[,] data1)
        //{
        //    this.testBox1 = data1;
        //}
        public Form2()
        {
            InitializeComponent();
        }

        //private void For2_Load(object sender, EventArgs e)
        //{
        //    Program exampled = new Program(this);
        //}

        //public void test(string test)
        //{
        //    testBox1.Test = test;
        //}

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program t = new Program();
            double[,] theArray;
            t.FillArray(out theArray);

            for (int i = 0; i < theArray.GetLength(0); i++)
            {
                chart1.Series["Original data"].Points.AddXY
                                (theArray[i,0], theArray[i, 1]);

            }

            chart1.Series["Original data"].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series["Original data"].Color = Color.Red;
            

            chart1.ChartAreas[0].AxisX.Maximum = 25;
            chart1.ChartAreas[0].AxisX.Minimum = 4;

            chart1.Legends["Original data"].Position.Auto = false;
            chart1.Legends["Original data"].Position = new ElementPosition(30, 5, 50, 5);


        }

        private void LinearRegression_Click(object sender, EventArgs e)
        {
            Program t = new Program();
            double[,] theArray;
            t.FillArray(out theArray);

            for (int i = 0; i < theArray.GetLength(0); i++)
            {
                chart1.Series["Original data"].Points.AddXY
                                (theArray[i, 0], theArray[i, 1]);

            }

            chart1.Series["Original data"].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series["Original data"].Color = Color.Blue;


            chart1.ChartAreas[0].AxisX.Maximum = 25;
            chart1.ChartAreas[0].AxisX.Minimum = 4;

            chart1.Legends["Original data"].Position.Auto = false;
            chart1.Legends["Original data"].Position = new ElementPosition(30, 5, 50, 5);

            var Fit = t.LinearFit();
            double[,] FitResult = t.ArrayMultiply(Fit.Item2, Fit.Item1);
            for (int i = 0; i < theArray.GetLength(0); i++)
            {
                chart1.Series["Linear fit"].Points.AddXY
                                (theArray[i, 0], FitResult[i, 0]);

            }
            chart1.Series["Linear fit"].ChartType =
                                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Linear fit"].Color = Color.Red;
        }
    }
}
