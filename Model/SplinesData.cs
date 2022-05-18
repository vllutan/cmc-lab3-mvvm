using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Model
{
    public class SplinesData : INotifyPropertyChanged
    {
        public MeasuredData mdata { get; set; }
        public SplineParameters spl_param { get; set; }

        public double[] grid { get; set; }
        public double[] spline_data { get; set; }
        public double integral { get; set; }

        public SplinesData()
        {
            mdata = new MeasuredData();
            spl_param = new SplineParameters();
            integral = 0;
            spline_data = new double[spl_param.len];
            grid = new double[spl_param.len];
        }
        public SplinesData(MeasuredData md, SplineParameters sp)
        {
            mdata = md;
            spl_param = sp;
            integral = 0;
            spline_data = new double[spl_param.len];
            grid = new double[spl_param.len];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property_name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
            }
        }

        [DllImport("\\..\\..\\..\\..\\x64\\Debug\\Dll3.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CalcMKL(int nx, double[] x, int ny, double[] y, double[] lim, int ns, double[] site, double[] coeff,
                  double[] result, ref int error, double[] int_left, double[] int_right, double[] int_res);

        public int GetSplineAndIntegral()
        {
            try
            {
                int error = 0;
                double[] coeff = new double[4 * (mdata.len - 1)];
                double[] left = new double[1] { mdata.int_limits0 };
                double[] right = new double[1] { mdata.int_limits1 };
                double[] intg = new double[1];
                spline_data = new double[spl_param.len];
                grid = new double[spl_param.len];
                for (int i = 0; i < spl_param.len; i++)
                    grid[i] = spl_param.segment[0] + i * (spl_param.segment[1] - spl_param.segment[0]) / (spl_param.len - 1);

                int ret = CalcMKL(mdata.len, mdata.nodes, 1, mdata.data, spl_param.derivative, spl_param.len, spl_param.segment, coeff,
                    spline_data, ref error, left, right, intg);

                //foreach (var i in spline_data) Console.WriteLine(i.ToString());

                if (error != 0)
                {
                    Console.WriteLine("Error from MKL: " + error);
                    throw new Exception(error.ToString());
                    return ret + 10;
                }

                integral = intg[0];
                OnPropertyChanged(nameof(Info));
                return ret + 20;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -3;
            }
        }

        public string Info
        {
            get
            {
                string ret = string.Empty;
                ret += $"derivatives:\n\tleft: {spl_param.derivative[0]},  right: {spl_param.derivative[1]}\n";
                ret += $"integral: {integral}\n";
                return ret;
            }
        }
    }


}
