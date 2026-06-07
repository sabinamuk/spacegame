namespace SpaceBattle.Lib;

public class CollidableAdapter : ICollidable
{
    private readonly IDictionary<string, object> _obj;

    public CollidableAdapter(IDictionary<string, object> obj)
    {
        _obj = obj;
    }

    public string GetShapeId() => (string)_obj["shapeId"];
    public Vector GetPosition() => (Vector)_obj["position"];
    public Vector GetVelocity() => (Vector)_obj["velocity"];
}
