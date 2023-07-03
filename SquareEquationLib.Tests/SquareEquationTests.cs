using Xunit;
using SquareEquationLib;


namespace Square.UnitTests.Equation
{
    public class SquareEquationTests
    {
        [Fact]
        public void Test1()
        {
            var squareEquation = new SquareEquation();
            double[] ans = squareEquation.Solve(1, 0, -1);
            double[] r_ans = new double[2] { -1, 1 };

            Assert.Equal(r_ans, ans);
        }

        [Fact]
        public void Test2()
        {
            var squareEquation = new SquareEquation();
            double[] ans = squareEquation.Solve(1, 3, -4);
            double[] r_ans = new double[2] { -4, 1 };

            Assert.Equal(r_ans, ans);
        }

        [Fact]
        public void Test3()
        {
            var squareEquation = new SquareEquation();
            double[] ans = squareEquation.Solve(1, -4, 4);
            double[] r_ans = new double[1] { 2 };

            Assert.Equal(r_ans, ans);
        }

        [Fact]
        public void Test4()
        {
            var squareEquation = new SquareEquation();
            double[] ans = squareEquation.Solve(1, -5, 9);
            double[] r_ans = new double[] { };

            Assert.Equal(r_ans, ans);
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
        public void Test5(double a, double b, double c)
        {
            var squareEquation = new SquareEquation();
            Assert.Throws<ArgumentException>(() => squareEquation.Solve(a, b, c));
        }
    }
}