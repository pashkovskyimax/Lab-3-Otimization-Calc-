using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab_3_Optimization_Calc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            BTNCalcul.MouseEnter += MEBTN;
            BTNCalcul.MouseLeave += MLBTN;

        }

        Dictionary<string, double> dictMatrixA = new Dictionary<string, double>(),
                                   dictMatrixB = new Dictionary<string, double>(),
                                   dictMatrixResRecursAlg = new Dictionary<string, double>();

        int sizeMatrix = 0;

        int amountOperationRecurse = 0, amountOperationLocalRecurse = 0;

        private void CreateMatrixA()
        {
            int n = sizeMatrix;

            for (int i = 0;i < sizeMatrix;i++)
            {
                for (int j = 0; j < sizeMatrix; j++)
                {
                    if (i == j)
                    {
                        dictMatrixA.Add(i + ";" + j, n--);
                    }

                    else
                    {
                        dictMatrixA.Add(i + ";" + j, 0);
                    }
                }
            }
        }

        private void CreateMatrixB()
        {
            Random rand = new Random();

            int n = sizeMatrix;

            for (int i = 0; i < sizeMatrix; i++)
            {
                for (int j = 0; j < sizeMatrix; j++)
                {
                    if (i <= j)
                    {
                        dictMatrixB.Add(i + ";" + j, rand.Next(0, 100));
                    }

                    else
                    {
                        dictMatrixB.Add(i + ";" + j, 0);
                    }
                }
            }
        }

        private void ShowMatrix()
        {
            for (int i = 0; i < sizeMatrix; i++)
            {
                for (int j = 0; j < sizeMatrix; j++)
                {
                    TBLTypeMatrixA.Text += dictMatrixA[i + ";" + j] + "\t";
                    TBLTypeMatrixB.Text += dictMatrixB[i + ";" + j] + "\t";
                }

                TBLTypeMatrixA.Text += "\n";
                TBLTypeMatrixB.Text += "\n";
            }
        }
        
        private void BTNAcceptSizeMatrix(object sender, RoutedEventArgs e)
        {
            sizeMatrix = int.Parse(TBSizeMatrix.Text);

            CreateMatrixA();

            CreateMatrixB();

            ShowMatrix();

            GRDStartMatrix.Visibility = Visibility.Visible;
        }

        private void MEBTN(object sender, MouseEventArgs e)
        {
             Button b = sender as Button;

             b.Foreground = Brushes.Green;
        }

        private void MLBTN(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;

            b.Foreground = Brushes.Black;
        }

        private int RecurseMultiplyMatrix(int i, int j, int k, double value)
        {
            string key = "";

            key = i + ";" + j;
            
            
            if (k != sizeMatrix)
            {
                value += dictMatrixA[i + ";" + k] * dictMatrixB[k + ";" + j];
                k++;

                amountOperationLocalRecurse++;

                return RecurseMultiplyMatrix(i, j, k, value);     
            }
            else
            {
                dictMatrixResRecursAlg.Add(key, value);

                if (j < sizeMatrix)
                {
                    j++;
                }

                if (j == sizeMatrix)
                {
                    j = 0;
                    i++;
                }


                if (i < sizeMatrix)
                {
                     return RecurseMultiplyMatrix(i, j, 0, 0);
                }
                
                else
                {
                    return 0;
                }
            }

        }

        private void MultiplyMatrix()
        {
            double value;

            string key = "";

            for (int i = 0; i < sizeMatrix; i++)
            {
                for (int j = 0; j < sizeMatrix; j++)
                {
                    key = i + ";" + j;

                    value = 0;
                    for (int k = 0; k < sizeMatrix; k++)
                    {
                        value += dictMatrixA[i + ";" + k] * dictMatrixB[k + ";" + j];

                        amountOperationRecurse++;
                    }

                    dictMatrixResRecursAlg.Add(key, value);
                }
            }
        }

        private void ShowResult()
        {
            for (int i = 0; i < sizeMatrix; i++)
            {
                for (int j = 0; j < sizeMatrix; j++)
                {
                    TBLTypeMatrixRESRecursAlg.Text += dictMatrixResRecursAlg[i + ";" + j] + "\t";
                }
                
                TBLTypeMatrixRESRecursAlg.Text += "\n";

            }
        }

        private void ShowRecurseResult()
        {
            for (int i = 0; i < sizeMatrix; i++)
            {
                for (int j = 0; j < sizeMatrix; j++)
                {
                    TBLTypeMatrixRESLocalRecursAlg.Text += dictMatrixResRecursAlg[i + ";" + j] + "\t";
                }

                TBLTypeMatrixRESLocalRecursAlg.Text += "\n";
            }
        }

        private void BTNCalculation(object sender, RoutedEventArgs e)
        {
            MultiplyMatrix();
            ShowResult();

            dictMatrixResRecursAlg = new Dictionary<string, double>();

            RecurseMultiplyMatrix(0, 0, 0, 0);
            ShowRecurseResult();

            LBLRecurseAlg.Content += amountOperationRecurse.ToString();

            LBLLocalRecurseAlg.Content += amountOperationLocalRecurse.ToString();

            GRDResults.Visibility = Visibility.Visible;
        }
    }
}
