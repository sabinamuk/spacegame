namespace SpaceBattle.Lib;


public interface IRotatingObject
{
    Angle Angle { get; set; }

    Angle AngularVelocity { get; }
}

public class RotateCommand : ICommand
{
    private readonly IRotatingObject obj;

    public RotateCommand(IRotatingObject obj)
    {
        this.obj = obj;
    }

    public void Execute()
    {
        obj.Angle = obj.Angle + obj.AngularVelocity;
    }
}
