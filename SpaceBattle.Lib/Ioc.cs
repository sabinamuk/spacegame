namespace SpaceBattle.Lib;

public static class Ioc
{
    private static readonly Dictionary<string, Func<Dictionary<string, object>, object>> _deps = new();

    public static void Register(string key, Func<Dictionary<string, object>, object> func)
    {
        _deps[key] = func;
    }

    public static object Resolve(string key, Dictionary<string, object> args)
    {
        return _deps[key](args);
    }
}
