namespace SpaceBattle;
public class SpaceShip
{
    
    public double[] position = new double[2]{double.NaN, double.NaN};
    public double[] speed = new double[2]{double.NaN, double.NaN};
    public bool possibility_of_movement = true;

    public SpaceShip()
    {}

    public void SetSpaceShipPosition(double[] position){
        this.position = position;
    }

    public void SetSpaceShipSpeed(double[] speed){
        this.speed = speed;
    }

    public void SetSpaceShipPossOfMove(bool possibility_of_movement){
        this.possibility_of_movement = possibility_of_movement;
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
 }
