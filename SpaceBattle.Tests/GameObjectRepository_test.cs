using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class GameObjectRepository_test
{
    private readonly IGameObjectRepository _repo = new GameObjectRepository();

    [Fact]
    public void Set_And_Get_ReturnsStoredObject()
    {
        var obj = new Dictionary<string, object> { ["hp"] = 100 };
        _repo.Set("obj1", obj);

        Assert.Same(obj, _repo.Get("obj1"));
    }

    [Fact]
    public void Get_MissingKey_Throws()
    {
        Assert.Throws<KeyNotFoundException>(() => _repo.Get("missing"));
    }

    [Fact]
    public void Remove_DeletesObject()
    {
        var obj = new Dictionary<string, object>();
        _repo.Set("obj1", obj);
        _repo.Remove("obj1");

        Assert.Throws<KeyNotFoundException>(() => _repo.Get("obj1"));
    }

    [Fact]
    public void GetIds_ReturnsAllStoredIds()
    {
        _repo.Set("a", new Dictionary<string, object>());
        _repo.Set("b", new Dictionary<string, object>());

        Assert.Contains("a", _repo.GetIds());
        Assert.Contains("b", _repo.GetIds());
    }
}
