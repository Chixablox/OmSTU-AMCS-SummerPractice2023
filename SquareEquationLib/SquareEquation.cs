using System;

namespace SquareEquationLib;

public class SquareEquation
{
    public double[] Solve(double a, double b, double c)
    {
        //Пользовался double.Epsilon, оно не работало, решил взять из презентации - заработало
        double eps = 1e-5;
        if (Math.Abs(a) < eps)
        {
            throw new System.ArgumentException();
        }
        else if (double.IsNaN(a) || double.IsNegativeInfinity(a) || double.IsPositiveInfinity(a))
        {
            throw new ArgumentException();
        }
        else if (double.IsNaN(b) || double.IsNegativeInfinity(b) || double.IsPositiveInfinity(b))
        {
            throw new ArgumentException();
        }
        else if (double.IsNaN(c) || double.IsNegativeInfinity(c) || double.IsPositiveInfinity(c))
        {
            throw new ArgumentException();
        }
        else
        {
            double x1;
            double x2;
            double D = Math.Pow(b, 2) - 4 * a * c;
            if (D >= eps)
            {
                if(b!=0)
                {
                    x1 = -(b + Math.Sign(b) * Math.Sqrt(D)) / 2;
                }
                else
                {
                    x1=-b/2*a;
                }
                x2 = c / x1;
                double[] ans = new double[2] { x1, x2 };
                return ans;
            }
            else if (Math.Abs(D) < eps)
            {
                x1 = -(b + Math.Sign(b) * Math.Sqrt(D)) / 2;
                double[] ans = new double[1] { x1 };
                return ans;
            }
            else
            {
                double[] ans = new double[] { };
                return ans;
            }

        }
    }
}
