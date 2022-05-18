using ViewModel;
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

namespace WpfApp1
{




    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 



    //public static class Cmd
    //{
    //    public static readonly RoutedUICommand MeasuredData = new
    //        (
    //            "MeasuredData",
    //            "MeasuredData",
    //            typeof(Cmd),
    //            new InputGestureCollection()
    //            {
    //            new KeyGesture(Key.D1, ModifierKeys.Control)
    //            }
    //        );

    //    public static readonly RoutedUICommand Splines = new
    //    (
    //        "Splines",
    //        "Splines",
    //        typeof(Cmd),
    //        new InputGestureCollection()
    //        {
    //            new KeyGesture(Key.D2, ModifierKeys.Control)
    //        }
    //    );
    //}
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewData vd = new ViewData();
            DataContext = vd;
            //MessageBox.Show($"{vd.possible_funcs}");
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //   // DataContext = this;
        //    //function_choice.SelectedItem = button_lin;
        //}

        //private void MD_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = vd.splData.mdata.IfCorrect();
        //}

        //private void MD_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    SPf spf = new SPf();
        //    if (function_choice.SelectedItem == button_lin) spf = SPf.linear;
        //    else if (function_choice.SelectedItem == button_cub) spf = SPf.cubic;
        //    else if (function_choice.SelectedItem == button_rand) spf = SPf.random;
        //    else if (function_choice.SelectedItem == null) { MessageBox.Show("Choose function"); return; }


        //    vd.splData.mdata.function = spf;
        //    vd.splData.mdata.SetData();
        //    vd.splData.spl_param.segment[0] = vd.splData.mdata.segment0;
        //    vd.splData.spl_param.segment[1] = vd.splData.mdata.segment1;
        //    vd.cd.MakePlot(vd.splData.mdata.nodes, vd.splData.mdata.data, 2, "Points");

        //}

        //private void Spl_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = vd.splData.spl_param.IfCorrect() && vd.splData.mdata.exists && vd.splData.mdata.IfCorrect();
        //}

        //private void Spl_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    vd.splData.GetSplineAndIntegral();
        //    vd.cd.MakePlot(vd.splData.grid, vd.splData.spline_data, 1, "Points");
        //}

        //private void clear_click(object sender, RoutedEventArgs e)
        //{
        //    vd.Clear();
        //}


    }
}
