using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppPr1
{
    public partial class Form1 : Form
    {
        List<Result> list = new List<Result>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int T = 580;
            Result result;
            do
            {
                result = new Result(3, 4, 0.0007);
                result.T = T;
                result.Calc();
                list.Add(result);
                T += 115;
            } while (result.Rt >= 0.05);
            dataGridView1.DataSource = list;
            foreach (Result item in list)
            {
                chart1.Series["Rt"].Points.AddXY(item.T, item.Rt);
                chart1.Series["Ft"].Points.AddXY(item.T, item.Ft);
            }
        }
    }

    class Result
    {
        public int T { get; set; }
        public double Rt { get; set; }
        public double Ft { get; set; }

        private int n;
        private int m;
        private double Lambda;

        public Result(int m, int n, double Lambda)
        {
            this.m = m;
            this.n = n;
            this.Lambda = Lambda;
        }

        public void Calc()
        {
            Rt = Math.Exp(-m * Lambda * T);
            Ft = 1 - Rt;
        }

    }
}
