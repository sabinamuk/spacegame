namespace SpaceBattle.Lib;

public class ObjectShape
{
    public string Type { get; }
    public int Radius { get; }

    public ObjectShape(string type, int radius)
    {
        Type = type;
        Radius = radius;
    }
}
