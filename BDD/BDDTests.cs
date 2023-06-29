namespace BDD;
using SquareEquationLib;
using TechTalk.SpecFlow;


[Binding]
public class StepDefinitions
{
    private double eps = 1e-5;
    private double[] coef = new double[3];
    private double[] ans = new double[0];
    private Exception r_exp = new Exception();
    private SquareEquation squareEquation = new SquareEquation();
    [When("вычисляются корни квадратного уравнения")]
    public void CalculatedTheRootsOfTheSquareEquation()
    {
        try 
        {
            ans = squareEquation.Solve(coef[0],coef[1],coef[2]);
        }
        catch (Exception e)
        {
            r_exp =  e;
        }
    }
    [Given(@"Квадратное уравнение с коэффициентами \((.*), (.*), (.*)\)")]
    public void GivenSqaureEquationCoefficients(string a1, string b1, string c1) 
    {
        string[] in_coef = new string[] {a1, b1, c1};
        
        for (int i = 0; i < 3; i++)
        {
            if (in_coef[i] == "Double.NegativeInfinity")
                coef[i] = double.NegativeInfinity;
            else if (in_coef[i] == "Double.PositiveInfinity")
                coef[i] = double.PositiveInfinity;
            else if (in_coef[i] == "NaN")
                coef[i] = double.NaN;
            else
                coef[i] = Convert.ToDouble(in_coef[i]);
        }
    }
    [Then("выбрасывается исключение ArgumentException")]
    public void ThrowingArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => throw r_exp);
    }
    
    [Then(@"квадратное уравнение имеет один корень (.*) кратности два")]
    public void QuadraticEquationHasOneRoot(double x1)
    {
        double[] r_ans = new double[] {x1};
        bool result = false;
        
        if (Math.Abs(ans[0] - r_ans[0]) < eps)
        {
            result = true;
        }

        Assert.True(result, "Incorrect");
    }

    [Then(@"квадратное уравнение имеет два корня \((.*), (.*)\) кратности один")]
    public void QuadraticEquationHasTwoRoots(double x1, double x2)
    {
        double[] r_ans = new double[] {x1, x2};
        bool result = false;
        Array.Sort(r_ans);
        
        if (Math.Abs(ans[0] - r_ans[0]) < eps & Math.Abs(ans[1] - r_ans[1]) < eps)
        {
            result = true;
        }

        Assert.True(result, "Incorrect");
    }

    [Then(@"множество корней квадратного уравнения пустое")]
    public void SqaureEquationHasNoRoots()
    {
        double[] r_ans = new double[]{};
        bool result = false;
        if (ans.Length == 0 & r_ans.Length == 0)
        {
            result = true;
        }

        Assert.True(result, "Incorrect");
    }
}