namespace SpaceBattle.Lib;

public class MovingObjectAdapter : IMovable
{
    private readonly IDictionary<string, object> _obj;

    public MovingObjectAdapter(IDictionary<string, object> obj)
    {
        _obj = obj;
    }

    public Vector Position
    {
        get => (Vector)_obj["position"];
        set => _obj["position"] = value;
    }

    public Vector Velocity => (Vector)_obj["velocity"];
}
