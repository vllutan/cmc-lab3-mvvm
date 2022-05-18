using Model;
using System;
using Xunit;

namespace Model.Tests
{
    public class ModelTest
    {
        [Fact]
        public void TestLinear()
        {
            MeasuredData md = new MeasuredData(5, 0, 2, SPf.linear);
            Assert.Equal(5, md.len);
            Assert.Equal(0, md.segment0, 10);
            Assert.Equal(2, md.segment1, 10);
            for (int i = 0; i < md.data.Length; ++i)
            {
                Assert.Equal(2 * md.nodes[i], md.data[i], 10);
            }
        }

        [Fact]
        public void TestCubic()
        {
            MeasuredData md = new MeasuredData(5, 0, 2, SPf.cubic);
            Assert.Equal(5, md.len);
            Assert.Equal(0, md.segment0, 10);
            Assert.Equal(2, md.segment1, 10);
            for (int i = 0; i < md.data.Length; ++i)
            {
                Assert.Equal(Math.Pow(md.nodes[i], 3) - 5 * md.nodes[i] + 11, md.data[i], 10);
            }
        }

        [Fact]
        public void TestDefaultMD()
        {
            MeasuredData md = new MeasuredData();
            Assert.Equal(3, md.len);
            Assert.Equal(0, md.segment0, 10);
            Assert.Equal(1, md.segment1, 10);
            Assert.Equal(0, md.int_limits0, 10);
            Assert.Equal(1, md.int_limits1, 10);
            Assert.Equal((double)md.function, (double)SPf.linear, 10);
            for (int i = 0; i < md.data.Length; ++i)
            {
                Assert.Equal(2 * md.nodes[i], md.data[i], 10);
            }
        }

        [Fact]
        public void TestDefaultSplParameters()
        {
            SplineParameters spl = new SplineParameters();
            Assert.Equal(300, spl.len);
            Assert.Equal(0, spl.segment[0], 10);
            Assert.Equal(1, spl.segment[1], 10);
            Assert.Equal(2, spl.derivative[0], 10);
            Assert.Equal(2, spl.derivative[1], 10);
        }

        [Fact]
        public void TestSplDataDefault()
        {
            MeasuredData md = new MeasuredData();
            SplineParameters spl = new SplineParameters();
            SplinesData sd = new SplinesData(md, spl);
            Assert.Equal(0, sd.integral, 10);
        }

        [Fact]
        public void TestSplData()
        {
            MeasuredData md = new MeasuredData(10, -5, 5, SPf.linear);
            SplineParameters spl = new SplineParameters(400, new double[] { -5, 5 }, new double[] { 1, 1 });
            SplinesData sd = new SplinesData(md, spl);
            Assert.Equal(0, sd.integral, 10);
        }
    }
}