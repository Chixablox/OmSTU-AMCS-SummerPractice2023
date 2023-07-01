namespace spacebattletests;
using SpaceBattle;
using TechTalk.SpecFlow;


[Binding,Scope(Feature = "Перемещение корабля")]
public class MovmentTests
{
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
        
        if (Math.Abs(r_ans[0]-coords[0])<1e-5 & Math.Abs(r_ans[1]-coords[1])<1e-5)
        {
            result = true;
        }

        Assert.True(result, "Incorrect");
    }
}



[Binding,Scope(Feature = "Движение с расходом топлива")]
public class MovementWithFuel
{
    private double fuelQuantity;
    private Exception r_exp = new Exception();
    private SpaceShip spaceShip = new SpaceShip();
    [When("происходит прямолинейное равномерное движение без деформации")]
    public void CalculatedTheMovementWithFuelOfTheSpaceShip()
    {
        try 
        {
            fuelQuantity = spaceShip.MovementWithFuel();
        }
        catch (Exception e)
        {
            r_exp =  e;
        }
    }

    [Given(@"космический корабль имеет топливо в объеме (.*) ед")]
    public void GivenTheFuelQuantityOfTheSpaceship(double x) 
    {
        double in_fuelQuantity = x;
        spaceShip.SetSpaceShipFuelQuantity(in_fuelQuantity);
    }

    [Given(@"имеет скорость расхода топлива при движении (.*) ед")]
    public void GivenTheFuelFlow(double x_speed) 
    {
        double in_fuelFlow = x_speed; 
        spaceShip.SetSpaceShipFuelFlow(in_fuelFlow);
    }

    [Then(@"новый объем топлива космического корабля равен (.*) ед")]
    public void CalculationOfTheNewFuelVolumeOfTheSpaceship(double x1)
    {
        double r_ans = x1;
        bool result = false;
        
        if (Math.Abs(fuelQuantity-x1)<1e-5)
        {
            result = true;
        }

        Assert.True(result, "Incorrect");
    }

    [Then(@"возникает ошибка Exception")]
    public void ThrowingException()
    {
        Assert.ThrowsAsync<Exception>(() => throw r_exp);
    }
}

[Binding,Scope(Feature = "Вращение коробля")]
public class RotationAroundAxisTests
{
    private double angleOfInclination;
    private Exception r_exp = new Exception();
    private SpaceShip spaceShip = new SpaceShip();
    
    [When("происходит вращение вокруг собственной оси")]
    public void CalculatedRotationAroundItsOwnAxisOfTheSpaceShip()
    {
        try 
        {
            angleOfInclination = spaceShip.RotationAroundAxis();
        }
        catch (Exception e)
        {
            r_exp =  e;
        }
    }

    [Given(@"космический корабль имеет угол наклона (.*) град к оси OX")]
    public void GivenAngleOfInclinationOfTheSpaceShip(double x) 
    {
        double in_angleOfInclination = x;
        spaceShip.SetSpaceShipAngleOfInclination(in_angleOfInclination);
    }

    [Given(@"имеет мгновенную угловую скорость (.*) град")]
    public void GivenTheAngularSpeedOfTheSpaceShip(double x_speed) 
    {
        double in_angularSpeed = x_speed; 
        spaceShip.SetSpaceShipAngularSpeed(in_angularSpeed);
    }

    [Given("невозможно изменить уголд наклона к оси OX космического корабля")]
    public void GivenTheFalseAbilityToChangeTheAngleOfTheSpaceShip() 
    {
        bool in_ability = false; 
        spaceShip.SetSpaceShipAbilityToChangeTheAngle(in_ability);
    }

    [Given("космический корабль, угол наклона которого невозможно определить")]
    public void GivenTheUndefinedAngleOfTheSpaceShip() 
    {
        double in_angleOfInclination = double.NaN;
        spaceShip.SetSpaceShipAngleOfInclination(in_angleOfInclination);
    }

    [Given("мгновенную угловую скорость невозможно определить")]
    public void GivenTheUndefinedAngulanSpeedOfTheSpaceShip() 
    {
        double in_angularSpeed = double.NaN; 
        spaceShip.SetSpaceShipAngularSpeed(in_angularSpeed);
    }

    [Then(@"угол наклона космического корабля к оси OX составляет (.*) град")]
    public void ThrowingException(double x1)
    {
        double r_ans = x1;
        bool result = false;
        
        if (Math.Abs(angleOfInclination-x1)<1e-5)
        {
            result = true;
        }

        Assert.True(result, "Incorrect");
    }

    [Then(@"возникает ошибка Exception")]
    public void ThrowingException()
    {
        Assert.ThrowsAsync<Exception>(() => throw r_exp);
    }

}