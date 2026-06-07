namespace SpaceBattle.Lib;

public interface ICollisionDetector
{
    bool Detect(IDictionary<string, object> a, IDictionary<string, object> b);
}
