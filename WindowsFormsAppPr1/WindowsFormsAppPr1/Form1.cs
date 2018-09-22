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
        List<Result2> list2 = new List<Result2>();
        List<Result3> list3 = new List<Result3>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
                series.Points.Clear();
            foreach (var series in chart2.Series)
                series.Points.Clear();
            foreach (var series in chart3.Series)
                series.Points.Clear();
            list.Clear();
            list2.Clear();
            list3.Clear();
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
            int m, n;
            double lambda;
            try
            {
                m = Convert.ToInt32(textBox1.Text);
                n = Convert.ToInt32(textBox2.Text);
                lambda = Convert.ToDouble(textBox3.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            #region Res1
            int T = 580;
            Result result;
            do
            {
                result = new Result(m, n, lambda);
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
            #endregion Res1
            #region Res2
            T = 580;
            do
            {
                result = new Result2(m, n, lambda);
                result.T = T;
                result.Calc();
                list2.Add(result as Result2);
                T += 460;
            } while (result.Rt >= 0.05);
            dataGridView2.DataSource = list2;
            dataGridView2.Columns[0].DisplayIndex = 3;
            foreach (Result2 item in list2)
            {
                chart2.Series["Rt"].Points.AddXY(item.T, item.Rt);
                chart2.Series["Ft"].Points.AddXY(item.T, item.Ft);
                chart2.Series["Nt"].Points.AddXY(item.T, item.Nt);
            }
            #endregion Res2
            #region Res3
            T = 580;
            do
            {
                result = new Result3(m, n, lambda);
                result.T = T;
                result.Calc();
                list3.Add(result as Result3);
                T += 115;
            } while (result.Rt >= 0.05);
            dataGridView3.DataSource = list3;
            foreach (Result item in list3)
            {
                chart3.Series["Rt"].Points.AddXY(item.T, item.Rt);
                chart3.Series["Ft"].Points.AddXY(item.T, item.Ft);
            }
            #endregion Res3
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }

    public class Result
    {
        public int T { get; set; }
        public double Rt { get; set; }
        public double Ft { get; set; }

        protected int n;
        protected int m;
        protected double Lambda;

        public Result(int m, int n, double Lambda)
        {
            this.m = m;
            this.n = n;
            this.Lambda = Lambda;
        }

        public virtual void Calc()
        {
            Rt = Math.Exp(-m * Lambda * T);
            Ft = 1 - Rt;
        }

    }

    public class Result2 : Result
    {
        public double Nt { get; set; }

        public Result2(int m, int n, double Lambda): base(m, n, Lambda) {  }

        public override void Calc()
        {
            Rt = 1 - (1 - Math.Exp(-Lambda * T));
            Ft = Math.Pow(1 - Math.Exp(-Lambda * T), n);
            Nt = n * (1 - Math.Exp(-Lambda * T));
        }
    }

    public class Result3 : Result
    {
        public Result3(int m, int n, double Lambda): base(m, n, Lambda) {  }

        public override void Calc()
        {
            Rt = 1 - (1 - Math.Exp(-Lambda * m * T));
            Ft = Math.Pow(1 - Math.Exp(-Lambda * m * T), n);
        }

    }

}
