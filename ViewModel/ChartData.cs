using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace ViewModel
{
    public class ChartData
    {
        public SeriesCollection sc { get; set; }
        public Func<double, string> Formatter { get; set; }
        public ChartData()
        {
            sc = new();
            Formatter = value => value.ToString("F3");
        }

        public void MakePlot(double[] grid, double[] data, int mode, string title)
        {
            try
            {
                ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();
                for (int i = 0; i < data.Length; i++)
                {
                    values.Add(new(grid[i], data[i]));
                }
                if (mode == 1) sc.Add(new LineSeries { Title = title, Values = values, PointGeometry = null });
                else sc.Add(new ScatterSeries { Title = title, Values = values, PointGeometry = DefaultGeometries.Circle });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void Clear()
        {
            sc.Clear();
        }
    }
}