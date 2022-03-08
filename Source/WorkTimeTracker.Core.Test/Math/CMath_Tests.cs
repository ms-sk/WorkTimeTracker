using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkTimeTracker.Core.Math;

namespace WorkTimeTracker.Core.Test.Math
{
    [TestClass]
    public sealed class CMathTests
    {
        [TestMethod]
        [DataRow(-1.30, "-1.25")]
        [DataRow(0.0, "0.0")]
        [DataRow(0.13, "0.25")]
        [DataRow(0.4, "0.5")]
        [DataRow(0.67, "0.75")]
        [DataRow(2.75325234, "2.75")]
        [DataRow(3.46414, "3.5")]
        [DataRow(100.0235325, "100.00")]
        public void RoundQuarter(double value, string expected)
        {
            var expectedValue = decimal.Parse(expected, CultureInfo.InvariantCulture.NumberFormat);

            var result = CMath.RoundQuarter(value);
            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        [DataRow("-1", "0.0")]
        [DataRow("0.0", "0.0")]
        [DataRow("5.90", "0.0")]
        [DataRow("6.5", "0.5")]
        [DataRow("8.00", "0.5")]
        [DataRow("9.99", "0.75")]
        [DataRow("11", "1")]
        [DataRow("152", "1")]
        public void CalculateBreak(string hours, string expected)
        {
            var value = double.Parse(hours, CultureInfo.InvariantCulture.NumberFormat);
            var expectedValue = decimal.Parse(expected, CultureInfo.InvariantCulture.NumberFormat);

            var result = CMath.CalculateBreak(value);
            Assert.AreEqual(expectedValue, result);
        }
    }
}
