using System;
using ViewModel;
using Xunit;

namespace ViewModel.Tests
{
    public class ViewModelTests
    {
        [Fact]
        public void TestViewDataDefault()
        {
            ViewData vd = new ViewData();
            Assert.Equal(3, vd.Irreg_Len);
            Assert.Equal(300, vd.Reg_Len);
            Assert.Equal(0, vd.Segment0, 10);
            Assert.Equal(1, vd.Segment1, 10);
            for (int i = 0; i < vd.Irreg_Data.Length; ++i)
            {
                Assert.Equal(2 * vd.Irreg_Nodes[i], vd.Irreg_Data[i], 10);
            }
            Assert.Equal(2, vd.Derivative[0], 10);
            Assert.Equal(2, vd.Derivative[1], 10);
            Assert.Equal(0, vd.Int_limits0, 10);
            Assert.Equal(1, vd.Int_limits1, 10);
            Assert.True(vd.IsCorrectMD());
            Assert.True(vd.IsCorrectSP());
            Assert.False(vd.Exists);
        }

        [Fact]
        public void TestViewDataMD()
        {
            ViewData vd = new();
            vd.Irreg_Len = 10;
            Assert.Equal(10, vd.Irreg_Len);
            vd.Segment0 = -5;
            vd.Segment1 = 5;
            Assert.Equal(-5, vd.Segment0, 10);
            Assert.Equal(5, vd.Segment1, 10);
            Assert.True(vd.IsCorrectMD());
            Assert.True(vd.SetMeasuredData.CanExecute(1));
            vd.SetMeasuredData.Execute(1);
            Assert.True(vd.Exists);
        }

        [Fact]
        public void TestViewDataSP()
        {
            ViewData vd = new();
            vd.Reg_Len = 500;
            Assert.Equal(500, vd.Reg_Len);
            vd.Derivative[0] = 1;
            vd.Derivative[1] = 3;
            Assert.Equal(1, vd.Derivative[0], 10);
            Assert.Equal(3, vd.Derivative[1], 10);
            vd.Int_limits0 = 0;
            vd.Int_limits1 = 0.5;
            Assert.Equal(0, vd.Int_limits0, 10);
            Assert.Equal(0.5, vd.Int_limits1, 10);
            Assert.True(vd.IsCorrectSP());
            vd.SetMeasuredData.Execute(1);
            Assert.True(vd.SetSpline.CanExecute(1));
            vd.SetSpline.Execute(1);
            vd.Irreg_Len = 50;
            Assert.False(vd.Exists);
        }

        [Fact]
        public void TestClear()
        {
            ViewData vd = new();
            vd.ClearChart.Execute(1);
            Assert.Equal(0, vd.splData.integral, 10);
            Assert.False(vd.Exists);
        }

        [Fact]
        public void TestError1()
        {
            ViewData vd = new();
            vd.Irreg_Len = 1;
            Assert.False(vd.IsCorrectMD());
            Assert.False(vd.SetMeasuredData.CanExecute(1));
        }

        [Fact]
        public void TestError2()
        {
            ViewData vd = new();
            vd = new();
            vd.SetMeasuredData.Execute(1);
            vd.Reg_Len = 1;
            Assert.False(vd.IsCorrectSP());
            Assert.False(vd.SetSpline.CanExecute(1));
        }

        [Fact]
        public void TestError3()
        {
            ViewData vd = new();
            vd.Segment1 = 1;
            vd.Int_limits0 = -2;
            vd.Int_limits1 = 1;
            Assert.False(vd.IsCorrectMD());
            Assert.False(vd.SetMeasuredData.CanExecute(1));
        }

        [Fact]
        public void TestError4()
        {
            ViewData vd = new();
            vd.Segment0 = 0;
            vd.Segment1 = 5;
            vd.Int_limits0 = 1;
            vd.Int_limits1 = 10;
            Assert.False(vd.IsCorrectMD());
            Assert.False(vd.SetMeasuredData.CanExecute(1));
        }

        [Fact]
        public void TestError5()
        {
            ViewData vd = new();
            vd.Segment0 = 0;
            vd.Segment1 = 5;
            vd.Int_limits0 = 5;
            vd.Int_limits1 = 5;
            Assert.False(vd.IsCorrectMD());
            Assert.False(vd.SetMeasuredData.CanExecute(1));
        }

        [Fact]
        public void TestError6()
        {
            ViewData vd = new();
            vd.Segment0 = 10;
            vd.Segment1 = 5;
            vd.Int_limits0 = 5;
            vd.Int_limits1 = 10;
            Assert.False(vd.IsCorrectMD());
            Assert.False(vd.SetMeasuredData.CanExecute(1));
        }
    }
}