namespace SpaceBattle.Lib;

public class CollisionDetector : ICollisionDetector
{
    private readonly ICollisionMap _map;

    public CollisionDetector(ICollisionMap map)
    {
        _map = map;
    }

    public bool Collides(ICollidable a, ICollidable b)
        => CollidesAt(a.GetShapeId(), b.GetShapeId(), a.GetPosition(), b.GetPosition())
        || CollidesAt(a.GetShapeId(), b.GetShapeId(),
               a.GetPosition() + a.GetVelocity(),
               b.GetPosition() + b.GetVelocity());

    private bool CollidesAt(string shapeA, string shapeB, Vector posA, Vector posB)
        => _map.Contains(shapeA, shapeB, posB[0] - posA[0], posB[1] - posA[1]);
}
