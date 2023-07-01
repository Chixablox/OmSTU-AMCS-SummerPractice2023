namespace SpaceBattle;
public class SpaceShip
{
    
    public double[] position = new double[2]{double.NaN, double.NaN};
    public double[] speed = new double[2]{double.NaN, double.NaN};
    public bool possibility_of_movement = true;
    public double fuelQuantity = 0;
    public double fuelFlow = double.NaN;
    public double angleOfInclination = 0;
    public double angularSpeed = double.NaN;
    public bool abilityToChangeTheAngle = true;
    public SpaceShip()
    {}

    public void SetSpaceShipPosition(double[] position){
        this.position = position;
    }

    public void SetSpaceShipSpeed(double[] speed){
        this.speed = speed;
    }

    public void SetSpaceShipPossOfMove (bool possibility_of_movement){
        this.possibility_of_movement = possibility_of_movement;
    }

    public void SetSpaceShipFuelQuantity (double fuelQuantity){
        this.fuelQuantity = fuelQuantity;
    }
    public void SetSpaceShipAngleOfInclination(double angleOfInclination){
        this.angleOfInclination = angleOfInclination;
    }
    public void SetSpaceShipFuelFlow(double fuelFlow){
        this.fuelFlow = fuelFlow;
    }
    public void SetSpaceShipAngularSpeed(double angularSpeed){
        this.angularSpeed = angularSpeed;
    }

    public void SetSpaceShipAbilityToChangeTheAngle(bool abilityToChangeTheAngle){
        this.abilityToChangeTheAngle = abilityToChangeTheAngle;
    }

    public double[] Movement (){
        if(double.IsNaN(position[0]) || double.IsNaN(position[1])){
            throw new Exception();
        }
        else if (double.IsNaN(speed[0]) || double.IsNaN(speed[1])){
            throw new Exception();
        }
        else if (possibility_of_movement==false){
            throw new Exception();
        }
        else{
            position[0] += speed[0];
            position[1] += speed[1];
            return position;
        }
    }
    public double MovementWithFuel (){

        if((fuelQuantity-fuelFlow)<=(-1e-5)){
            throw new Exception();
        }
        else{
            fuelQuantity -= fuelFlow;
            return fuelQuantity;
        }
    }
    public double RotationAroundAxis (){

        if(double.IsNaN(angleOfInclination)){
            throw new Exception();
        }
        else if(double.IsNaN(angularSpeed)){
            throw new Exception();
        }
        else if(abilityToChangeTheAngle == false){
            throw new Exception();
        }
        else{
            angleOfInclination += angularSpeed;
            return angleOfInclination;
        }

    }
 }
