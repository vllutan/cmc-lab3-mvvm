using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace ViewModel
{
    public class ViewData : INotifyPropertyChanged, IDataErrorInfo
    {
        public SplinesData splData { get; set; } = new();
        public ChartData cd { get; set; } = new();

        public int Irreg_Len
        {
            get { return splData.mdata.len; }
            set { splData.mdata.len = value; OnPropertyChanged(nameof(Irreg_Len)); Exists = false; }
        }
        public int Reg_Len
        {
            get { return splData.spl_param.len; }
            set { splData.spl_param.len = value; OnPropertyChanged(nameof(Reg_Len)); }
        }
        public double Segment0
        {
            get { return splData.mdata.segment0; }
            set { splData.mdata.segment0 = value; OnPropertyChanged(nameof(Segment0)); Exists = false; }
        }
        public double Segment1
        {
            get { return splData.mdata.segment1; }
            set { splData.mdata.segment1 = value; OnPropertyChanged(nameof(Segment1)); Exists = false; }
        }
        public SPf Function
        {
            get { return splData.mdata.function; }
            set { splData.mdata.function = value; OnPropertyChanged(nameof(Function)); Exists = false; }
        }

        public double[] Irreg_Nodes
        {
            get { return splData.mdata.nodes; }
            set { splData.mdata.nodes = value; OnPropertyChanged(nameof(Irreg_Nodes)); }
        }
        public double[] Irreg_Data
        {
            get { return splData.mdata.data; }
            set { splData.mdata.data = value; OnPropertyChanged(nameof(Irreg_Data)); }
        }
        public double Int_limits0
        {
            get { return splData.mdata.int_limits0; }
            set { splData.mdata.int_limits0 = value; OnPropertyChanged(nameof(Int_limits0)); }
        }
        public double Int_limits1
        {
            get { return splData.mdata.int_limits1; }
            set { splData.mdata.int_limits1 = value; OnPropertyChanged(nameof(Int_limits1)); }
        }

        public double[] Derivative
        {
            get { return splData.spl_param.derivative; }
            set { splData.spl_param.derivative = value; OnPropertyChanged(nameof(Derivative)); }
        }
        public double[] Reg_Nodes
        {
            get { return splData.grid; }
            set { splData.grid = value; OnPropertyChanged(nameof(Reg_Nodes)); }
        }
        public double[] Spline_Data
        {
            get { return splData.spline_data; }
            set { splData.spline_data = value; OnPropertyChanged(nameof(Spline_Data)); }
        }

        public ObservableCollection<string> _Str
        {
            get { return splData.mdata.Str; }
            set { splData.mdata.Str = value; OnPropertyChanged(nameof(_Str)); }
        }

        public string _Info
        {
            get { return splData.Info;  }
            //set { splData.Info = value; OnPropertyChanged(nameof(_Info)); }
        }
        public bool Exists
        {
            get { return splData.mdata.exists; }
            set { splData.mdata.exists = value; OnPropertyChanged(nameof(Exists)); }
        }

        public ObservableCollection<KeyValuePair<SPf, string>> possible_funcs { get; set; } = new();

        private KeyValuePair<SPf, string> selectedFunc;
        public KeyValuePair<SPf, string> SelectedFunc
        {
            get { return selectedFunc; }
            set
            {
                selectedFunc = value;
                OnPropertyChanged(nameof(SelectedFunc));
            }
        }

        public bool IsCorrectMD()
        {
            return splData.mdata.IfCorrect();
        }

        public bool IsCorrectSP()
        {
            return splData.spl_param.IfCorrect();
        }
        public ViewData()
        {
            possible_funcs = new()
            {
                new KeyValuePair<SPf, string>(SPf.linear, "Linear"),
                new KeyValuePair<SPf, string>(SPf.cubic, "Cubic"),
                new KeyValuePair<SPf, string>(SPf.random, "Random")
            };
            selectedFunc = possible_funcs[0];
            splData = new();
            cd = new();
            SetMeasuredData = new RelayCommand(_ =>
            {
                Function = SelectedFunc.Key;
                splData.mdata.SetData();
                splData.spl_param.segment[0] = Segment0;
                splData.spl_param.segment[1] = Segment1;
                cd.MakePlot(Irreg_Nodes, Irreg_Data, 2, "Points");
                OnPropertyChanged(nameof(_Str));
            }
            , (_) => IsCorrectMD());

            SetSpline = new RelayCommand(_ =>
            {
                splData.GetSplineAndIntegral();
                OnPropertyChanged(nameof(Spline_Data));
                OnPropertyChanged(nameof(Reg_Nodes));
                //MessageBox.Show($"{Reg_Nodes[0].ToString()} {Reg_Nodes[5].ToString()}");
                cd.MakePlot(Reg_Nodes, Spline_Data, 1, "Points");
                OnPropertyChanged(nameof(_Info));
                
            }
            , (_) => IsCorrectSP() && Exists);

            ClearChart = new RelayCommand(_ =>
            {
                cd.Clear();
                Exists = false;
                splData.integral = 0;
                _Str = new() { " "};
                OnPropertyChanged(nameof(_Str));
                OnPropertyChanged(nameof(_Info));
                //OnPropertyChanged(nameof(splData));
                //OnPropertyChanged(nameof(splData.mdata));
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property_name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
            }
        }

        public RelayCommand SetMeasuredData { get; private set; }
        public RelayCommand SetSpline { get; private set; }
        public RelayCommand ClearChart { get; private set; }

        public string Error => throw new NotImplementedException();

        public string this[string propertyName]
        {
            get
            {
                string error = string.Empty;
                
                switch (propertyName)
                {
                    case nameof(Irreg_Len):
                        if (Irreg_Len < 3)
                         error = "The number of nodes must be greater than 2."; 
                        break;

                    case nameof(Reg_Len):
                        if (Reg_Len < 3)
                            error = "The number of nodes must be greater than 2.";
                        break;

                    case nameof(Segment0):
                        if (Segment0 >= Segment1)
                         error = "Left limit of irregular grid must be lesser than the right limit."; 
                        break;

                    case nameof(Segment1):
                        if (Segment0 >= Segment1)
                         error = "Left limit of irregular grid must be lesser than the right limit."; 
                        break;

                    case nameof(Int_limits0):
                        if (Int_limits0 < Segment0 || Int_limits1 > Segment1 || Int_limits0 >= Int_limits1)
                            error = "Integral limits must be different.";
                        break;

                    case nameof(Int_limits1):
                        if (Int_limits0 < Segment0 || Int_limits1 > Segment1 || Int_limits0 >= Int_limits1)
                            error = "Integral limits must be different.";
                        break;

                    default:
                        error = "";
                        break;
                }

                return error;
            }
        }
    }
}