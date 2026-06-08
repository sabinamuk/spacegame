namespace SpaceBattle.Lib;

public class CollisionDetector : ICollisionDetector
{
    private readonly ICollisionMap _map;

    public CollisionDetector(ICollisionMap map)
    {
        _map = map;
    }

    public bool Collides(ICollidable a, ICollidable b)
    {
        int relX = b.GetPosition()[0] - a.GetPosition()[0];
        int relY = b.GetPosition()[1] - a.GetPosition()[1];
        int relVX = b.GetVelocity()[0] - a.GetVelocity()[0];
        int relVY = b.GetVelocity()[1] - a.GetVelocity()[1];
        return _map.Contains(a.GetShapeId(), b.GetShapeId(), relX, relY, relVX, relVY);
    }
}
