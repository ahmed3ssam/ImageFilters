using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZGraphTools;

namespace ImageFilters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte[,] ImageMatrix;
        int Wmax = 3;
        int T = 1;
        int SelectedFilterID = 0;
        int UsedAlgorithm = 0;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
        }


        private void btnGen_Click(object sender, EventArgs e)
        {
            if (SelectedFilterID == 0)
            {
                ImageOperations.DisplayImage(ImageFilters.AlphaTrim.ApplyFilter(ImageMatrix, Wmax, UsedAlgorithm, T), pictureBox2);
            }
            else
            {
                ImageOperations.DisplayImage(AdaptiveMedianFilter.ApplyFilter(ImageMatrix, Wmax, UsedAlgorithm), pictureBox2);
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbAlgorithm.Visible = true;
            lbl_algorithm.Visible = true;
            if (cbFilter.SelectedIndex == 0)
            {
                label1.Visible = true;
                maxWindowSize.Visible = true;
                label2.Visible = true;
                trimmingValue.Visible = true;
                SelectedFilterID = 0;

                cbAlgorithm.Items.Clear();

                cbAlgorithm.Items.Add("Counting Sort");
                cbAlgorithm.Items.Add("Kth Smallest/Largest");
            }
            else
            {
                label1.Visible = true;
                maxWindowSize.Visible = true;
                label2.Visible = false;
                trimmingValue.Visible = false;
                SelectedFilterID = 1;

                cbAlgorithm.Items.Clear();

                cbAlgorithm.Items.Add("Quick Sort");
                cbAlgorithm.Items.Add("Counting Sort");
            }
        }

        private void cbAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UsedAlgorithm = cbAlgorithm.SelectedIndex;
        }

        private void maxWindowSize_ValueChanged(object sender, EventArgs e)
        {
            Wmax = (int)maxWindowSize.Value;
        }

        private void trimmingValue_ValueChanged(object sender, EventArgs e)
        {
            T = (int)trimmingValue.Value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AdptiveMedian(object sender, EventArgs e)
        {
            // Make up some data points from the N, N log(N) functions
            int N = 10;
            double[] x_values = new double[N];
            double[] y_quicksort = new double[N];
            double[] y_countingsort = new double[N];

            for (int i = 0; i < N; i++)
            {
                int window_size = 3 + i * 2;

                x_values[i] = window_size;
                var start = System.Environment.TickCount;
                ImageFilters.AlphaTrim.ApplyFilter(ImageMatrix, window_size, 0, T);
                var end = System.Environment.TickCount;
                y_quicksort[i] = end - start;
                start = System.Environment.TickCount;
                ImageFilters.AlphaTrim.ApplyFilter(ImageMatrix, window_size, 1, T);
                end = System.Environment.TickCount;
                y_countingsort[i] = end - start;
            }

            //Create a graph and add two curves to it
            ZGraphForm ZGF = new ZGraphForm("Alpha Trim Graph", "Window Size", "Time");
            ZGF.add_curve("Algorithm = Quick Sort", x_values, y_quicksort, Color.Red);
            ZGF.add_curve("Algorithm = Counting Sort", x_values, y_countingsort, Color.Blue);
            ZGF.Show();
        }

        private void Alpha_Trim(object sender, EventArgs e)
        {
            // Make up some data points from the N, N log(N) functions
            int N = 10;
            double[] x_values = new double[N];
            double[] Y_Counting = new double[N];
            double[] Kth_Select = new double[N];

            for (int i = 0; i < N; i++)
            {
                int window_size = 3 + i * 2;

                x_values[i] = window_size;
                var start = System.Environment.TickCount;
                AlphaTrim.ApplyFilter(ImageMatrix, window_size, 0, T);
                var end = System.Environment.TickCount;
                Y_Counting[i] = end - start;
                start = System.Environment.TickCount;
                AlphaTrim.ApplyFilter(ImageMatrix, window_size, 1, T);
                end = System.Environment.TickCount;
                Kth_Select[i] = end - start;
            }

            //Create a graph and add two curves to it
            ZGraphForm ZGF = new ZGraphForm("Alpha Trim Graph", "Window Size", "Time");
            ZGF.add_curve("Algorithm = Counting Sort", x_values, Y_Counting, Color.Red);
            ZGF.add_curve("Algorithm = Kth Select", x_values, Kth_Select, Color.Blue);
            ZGF.Show();
        }
    }
}