using Xunit;
using SquareEquationLib;


namespace Square.UnitTests.Equation
{
    public class SquareEquationTests
    {
        [Fact]
        public void Test1()
        {
            bool result = false;
            var squareEquation = new SquareEquation();
            double[] ans = squareEquation.Solve(1, 0, -1);
            double[] r_ans = new double[2] { -1, 1 };

            if (Math.Abs(ans[0] - r_ans[0]) < 1e-5 & Math.Abs(ans[1] - r_ans[1]) < 1e-5)
            {
                result = true;
            }

            Assert.True(result, "Correct");
        }

        [Fact]
        public void Test5()
        {
            bool result = false;
            var squareEquation = new SquareEquation();
            double[] ans = squareEquation.Solve(1, 3, -4);
            double[] r_ans = new double[2] { -4, 1 };

            if (Math.Abs(ans[0] - r_ans[0]) < 1e-5 & Math.Abs(ans[1] - r_ans[1]) < 1e-5)
            {
                result = true;
            }

            Assert.True(result, "Correct");
        }

        [Fact]
        public void Test2()
        {
            bool result = false;
            var squareEquation = new SquareEquation();
            double[] ans = squareEquation.Solve(1, -4, 4);
            double[] r_ans = new double[1] { 2 };

            if (Math.Abs(ans[0] - r_ans[0]) < 1e-5)
            {
                result = true;
            }

            Assert.True(result, "Correct");
        }

        [Fact]
        public void Test3()
        {
            bool result = false;
            var squareEquation = new SquareEquation();
            double[] ans = squareEquation.Solve(1, -5, 9);
            double[] r_ans = new double[] { };

            if (ans.Length == 0 & r_ans.Length == 0)
            {
                result = true;
            }

            Assert.True(result, "Correct");
        }

        [Theory]
        [InlineData(0, 1, 5)]
        [InlineData(1, 0, double.NaN)]
        [InlineData(1, 32, double.PositiveInfinity)]
        [InlineData(1, -446, double.NegativeInfinity)]
        [InlineData(1, double.NaN, 0)]
        [InlineData(1, double.PositiveInfinity, 4)]
        [InlineData(1, double.NegativeInfinity, 4)]
        [InlineData(double.NaN, -4, 4)]
        [InlineData(double.PositiveInfinity, 5, 4)]
        [InlineData(double.NegativeInfinity, 6, 0.11111)]
        public void Test4(double a, double b, double c)
        {
            var squareEquation = new SquareEquation();
            Assert.Throws<ArgumentException>(() => squareEquation.Solve(a, b, c));
        }
    }
}