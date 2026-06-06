using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class GameObjectRepositoryTests
{
    private readonly IGameObjectRepository _repo = new GameObjectRepository();

    [Fact]
    public void Get_ExistingObject_ReturnsIt()
    {
        var obj = new Dictionary<string, object> { ["hp"] = 100 };
        _repo.Set("ship1", obj);

        var result = _repo.Get("ship1");

        Assert.Same(obj, result);
    }

    [Fact]
    public void Get_NonExistingId_Throws()
    {
        Assert.Throws<KeyNotFoundException>(() => _repo.Get("unknown"));
    }

    [Fact]
    public void Set_OverwritesExistingObject()
    {
        var first = new Dictionary<string, object> { ["hp"] = 100 };
        var second = new Dictionary<string, object> { ["hp"] = 50 };

        _repo.Set("ship1", first);
        _repo.Set("ship1", second);

        Assert.Same(second, _repo.Get("ship1"));
    }

    [Fact]
    public void Remove_ExistingObject_RemovesIt()
    {
        _repo.Set("ship1", new Dictionary<string, object>());
        _repo.Remove("ship1");

        Assert.Throws<KeyNotFoundException>(() => _repo.Get("ship1"));
    }

    [Fact]
    public void Remove_NonExistingId_DoesNotThrow()
    {
        var ex = Record.Exception(() => _repo.Remove("unknown"));
        Assert.Null(ex);
    }
}
