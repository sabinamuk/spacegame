using Xunit;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class GameObjectRepositoryTests
{
    [Fact]
    public void Add_And_Get_Works()
    {
        var repo = new GameObjectRepository();

        var obj = new Mock<IGameObject>();
        obj.SetupGet(o => o.Id).Returns("1");

        repo.Add("1", obj.Object);

        Assert.True(repo.Contains("1"));
        Assert.Equal(obj.Object, repo.Get("1"));
    }

    [Fact]
    public void Count_Works()
    {
        var repo = new GameObjectRepository();

        var obj1 = new Mock<IGameObject>();
        obj1.SetupGet(o => o.Id).Returns("1");

        var obj2 = new Mock<IGameObject>();
        obj2.SetupGet(o => o.Id).Returns("2");

        repo.Add("1", obj1.Object);
        repo.Add("2", obj2.Object);

        Assert.Equal(2, repo.Count());
    }

    [Fact]
    public void Contains_Returns_False_When_Missing()
    {
        var repo = new GameObjectRepository();

        Assert.False(repo.Contains("x"));
    }

    [Fact]
    public void Get_Throws_When_NotFound()
    {
        var repo = new GameObjectRepository();

        Assert.Throws<KeyNotFoundException>(() => repo.Get("404"));
    }
}
