using Xunit;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class GameObjectRepositoryTests
{
    [Fact]
    public void Add_Object()
    {
        var repository =
            new GameObjectRepository();

        var obj = new object();

        repository.Add(
            "ship",
            obj);

        Assert.True(
            repository.Contains("ship"));
    }

    [Fact]
    public void Get_Added_Object()
    {
        var repository =
            new GameObjectRepository();

        var obj = new object();

        repository.Add(
            "ship",
            obj);

        Assert.Equal(
            obj,
            repository.Get("ship"));
    }

    [Fact]
    public void Repository_Count_Is_One()
    {
        var repository =
            new GameObjectRepository();

        repository.Add(
            "ship",
            new object());

        Assert.Equal(
            1,
            repository.Count());
    }

    [Fact]
    public void Repository_Count_Is_Two()
    {
        var repository =
            new GameObjectRepository();

        repository.Add(
            "ship",
            new object());

        repository.Add(
            "torpedo",
            new object());

        Assert.Equal(
            2,
            repository.Count());
    }

    [Fact]
    public void Add_With_Same_Id_Replaces_Object()
    {
        var repository =
            new GameObjectRepository();

        var first = new object();

        var second = new object();

        repository.Add(
            "ship",
            first);

        repository.Add(
            "ship",
            second);

        Assert.Equal(
            second,
            repository.Get("ship"));
    }

    [Fact]
    public void Contains_Returns_False_For_Missing_Object()
    {
        var repository =
            new GameObjectRepository();

        Assert.False(
            repository.Contains("unknown"));
    }

    [Fact]
    public void Get_Unknown_Object_Throws_Exception()
    {
        var repository =
            new GameObjectRepository();

        Assert.Throws<KeyNotFoundException>(
            () => repository.Get("unknown"));
    }
}
