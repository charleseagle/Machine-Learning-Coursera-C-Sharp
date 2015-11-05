using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LinearRegression
{
    class Program
    {
        public static object PublicVariables { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// 
        //Load data from the txt file.
        public void FillArray(out double[,] data1)
        {
            string[] lines = File.ReadAllLines(@"E:\Machine learning\Week_2\C#\ex1data1.txt");
            //string[][] data1 = lines.Select(line => line.Split(',').ToArray()).ToArray();

            data1 = new double[lines.Length, 2];

            //string[] data1 = File.ReadAllText(@"E:\Machine learning\Week_2\C#\ex1data1.txt").Split(',');

            for (int i = 0; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(',');
                //int j = 0;
                for (int j = 0; j < fields.Length; j++)
                {
                    if (j == 2) break;
                    data1[i, j] = Convert.ToDouble(fields[j]);
                    
                    //Console.WriteLine(data1[i, j]);
                    //Console.ReadLine();
                }
            }            
        }

        //Array multiplication fuction.
        public double[,] ArrayMultiply(double[,] x1, double[,] x2)
        {
            var x = new double[x1.GetLength(0), x2.GetLength(1)];
            for (int i = 0; i < x1.GetLength(0); i++)
            {
                for (int j = 0; j < x2.GetLength(1); j++)
                {
                    for (int k = 0; k < x2.GetLength(0); k++)
                    {
                        x[i, j] += x1[i, k] * x2[k, j];
                    }
                }
            }
            return x;
        }

        //Array minus fuction.
        public double[,] ArrayMinus(double[,] x1, double[,] x2)
        {
            var x = new double[x1.GetLength(0), x1.GetLength(1)];
            for (int i = 0; i < x1.GetLength(0); i++)
            {
                for (int j = 0; j < x1.GetLength(1); j++)
                {
                    x[i, j] = x1[i, j] - x2[i, j];
                }
            }
            return x;
        }

        //Costfunction for linear regression.
        public double ComputeCost(double[,] X, double[,] y, double[,] theta)
        {
            double m = y.Length;
            double J = 0.0;
            double[,] temp1 = ArrayMultiply(X, theta);
            double[,] temp2 = ArrayMinus(temp1, y);
            var temp = new double[temp2.GetLength(1), temp2.GetLength(0)];
            for (int i = 0; i < temp2.GetLength(0); i++)
            {
                for(int j = 0; j < temp2.GetLength(1); j ++)
                {
                    temp[j, i] = temp2[i, j];
                }
            }
            double[,] temp3 = ArrayMultiply(temp, temp2);
            
            J = 1 / (2 * m) * temp3[0,0];
            
            return J;
        }

        //Gradient descent for linear regression.
        public Tuple<double[,], double[,]> Gradient(double[,] X, double[,] y, 
            double[,] theta, double alpha, int num_iters)
        {
            double m = y.GetLength(0);
            double[,] J_history = new double[num_iters,1];
            for (int i = 0; i < num_iters; i++)
            {
                double[,] temp1 = ArrayMultiply(X, theta);
                double[,] temp2 = ArrayMinus(temp1, y);
                var temp = new double[X.GetLength(1), X.GetLength(0)];
                for (int k = 0; k < X.GetLength(0); k++)
                {
                    for (int j = 0; j < X.GetLength(1); j++)
                    {
                        temp[j, k] = X[k, j];
                    }
                }
                double[,] temp3 = ArrayMultiply(temp, temp2);
                for (int j = 0; j < temp3.GetLength(0); j++)
                {
                    temp3[j, 0] = (alpha / m) * temp3[j, 0];
                }
                theta = ArrayMinus(theta, temp3);
                J_history[i,0] = ComputeCost(X, y, theta);                  
            }
            return new Tuple<double[,], double[,]>(theta, J_history);
        }
        

        public Tuple<double[,], double[,], double, double, double, double[,]> LinearFit()
        {
            Program t = new Program();
            double[,] data;
            t.FillArray(out data);
            var X = new double[data.GetLength(0), 2];
            var y = new double[data.GetLength(0), 1];
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0) { X[i, j] = 1; }
                    else { X[i, j] = data[i, 0]; }
                }
            }
            var theta = new double[2, 1];
            for (int i = 0; i < 2; i++)
            {
                theta[i, 0] = 0;
            }


            for (int i = 0; i < data.GetLength(0); i++)
            {
                y[i, 0] = data[i, 1];
            }

            int iterations = 1500;
            double alpha = 0.01;
            double InitialCost = t.ComputeCost(X, y, theta);
            
            var Gradient = t.Gradient(X, y, theta, alpha, iterations);

            double[,] a = new double[1, 2] { { 1, 3.5 } };
            double[,] predict1 = ArrayMultiply(a, Gradient.Item1);

            double[,] b = new double[1, 2] { { 1, 7 } };
            double[,] predict2 = ArrayMultiply(b, Gradient.Item1);

            return new Tuple<double[,], double[,], double, double, double, double[,]>
                (Gradient.Item1, X, InitialCost, predict1[0,0]*1000, predict2[0, 0] * 10000, y);
        }

        static void Main(string[] args)
        {
            Program t = new Program();
            var Fit = t.LinearFit();
            
            Console.WriteLine("Initial cost: {0}", Fit.Item3);
            Console.WriteLine("Running gradient descent.");
            
            Console.WriteLine("Theta found by gradient descent: {0}, {1}", Fit.Item1[0, 0], Fit.Item1[1, 0]);
            //Console.ReadLine();
            Console.ReadLine();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Form2 MyForm = new Form2();
            Application.Run(new Form2());

            Console.WriteLine("For population = 35,000, we predict a profit of {0}.", Fit.Item4);
            Console.WriteLine("For population = 70,000, we predict a profit of {0}.", Fit.Item5);
            Console.ReadLine();

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
            
        }     
    }

}
