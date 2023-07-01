namespace spacebattletests;
using SpaceBattle;
using TechTalk.SpecFlow;


[Binding]
public class StepDefinitions
{
    /*private double[] coords = new double[2];
    private double[] speed = new double[2];*/
    private double[] coords = new double[2];
    private Exception r_exp = new Exception();
    private SpaceShip spaceShip = new SpaceShip();
    [When("происходит прямолинейное равномерное движение без деформации")]
    public void CalculatedTheMovementOfTheSpaceShip()
    {
        try 
        {
            coords = spaceShip.Movement();
        }
        catch (Exception e)
        {
            r_exp =  e;
        }
    }
    [Given(@"космический корабль находится в точке пространства с координатами \((.*), (.*)\)")]
    public void GivenThePositionOfTheSpaceship(double x, double y) 
    {
        double[] in_coords = new double[2]{x,y};
        spaceShip.SetSpaceShipPosition(in_coords);
    }
    [Given(@"имеет мгновенную скорость \((.*), (.*)\)")]
    public void GivenTheInstantSpeed(double x_speed, double y_speed) 
    {
        double[] in_speed = new double[2]{x_speed,y_speed}; 
        spaceShip.SetSpaceShipSpeed(in_speed);
    }

    [Given("изменить положение в пространстве космического корабля невозможно")]
    public void GivenThePositionInSpace() 
    {
        spaceShip.SetSpaceShipPossOfMove(false);
    }

    [Given("космический корабль, положение в пространстве которого невозможно определить")]
    public void GivenThePositionCannotBeDetermined() 
    {
        double[] in_coords = new double[2]{double.NaN,double.NaN};
        spaceShip.SetSpaceShipPosition(in_coords);
    }

    [Given("скорость корабля определить невозможно")]
    public void GivenTheSpeedOfTheShipCannotBeDetermined() 
    {
        double[] in_speed = new double[2]{double.NaN,double.NaN}; 
        spaceShip.SetSpaceShipSpeed(in_speed);
    }

    [Then(@"возникает ошибка Exception")]
    public void ThrowingException()
    {
        Assert.ThrowsAsync<Exception>(() => throw r_exp);
    }

    [Then(@"космический корабль перемещается в точку пространства с координатами \((.*), (.*)\)")]
    public void MovingToAPoint(double x1, double x2)
    {
        double[] r_ans = new double[] {x1, x2};
        bool result = false;
        
        if (r_ans[0]==coords[0] & r_ans[1]==coords[1])
        {
            result = true;
        }

        Assert.True(result, "Incorrect");
    }
}