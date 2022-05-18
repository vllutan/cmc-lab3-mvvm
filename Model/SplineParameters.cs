using System.ComponentModel;

namespace Model
{
    public class SplineParameters : IDataErrorInfo
    {
        public int len { get; set; }
        public double[] segment { get; set; }
        public double[] derivative { get; set; }

        public bool IfCorrect()
        {
            return (len >= 3);
        }

        public string Error => throw new NotImplementedException();

        public string this[string propertyName]
        {
            get
            {
                string error = string.Empty;

                switch (propertyName)
                {
                    case nameof(len):
                        if (len < 3)
                            error = "The number of nodes must be greater than 2.";
                        break;

                    default:
                        error = "";
                        break;
                }

                return error;
            }
        }

        public SplineParameters()
        {
            len = 300;
            segment = new double[2] { 0, 1 };
            derivative = new double[2] { 2, 2 };
        }

        public SplineParameters(int l, double[] seg, double[] der)
        {
            len = l;
            segment = seg;
            derivative = der;
        }
    }


}
