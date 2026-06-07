namespace SpaceBattle.Lib;

public interface ICollidable
{
    string GetShapeId();
    Vector GetPosition();
    Vector GetVelocity();
}
