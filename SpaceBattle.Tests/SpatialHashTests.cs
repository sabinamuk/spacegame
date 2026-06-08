using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class SpatialHashTests
{
    private static Mock<ICollidable> MakeAt(int x, int y)
    {
        var mock = new Mock<ICollidable>();
        mock.Setup(c => c.GetPosition()).Returns(new Vector(x, y));
        mock.Setup(c => c.GetVelocity()).Returns(new Vector(0, 0));
        mock.Setup(c => c.GetShapeId()).Returns("ship");
        return mock;
    }

    [Fact]
    public void GetNearby_ReturnsObjectInSameCell()
    {
        var hash = new SpatialHash(cellSize: 10);
        var obj = MakeAt(5, 5);
        hash.Add("obj1", obj.Object);

        var nearby = hash.GetNearby(new Vector(5, 5)).ToList();

        Assert.Single(nearby);
        Assert.Equal("obj1", nearby[0].Id);
    }

    [Fact]
    public void GetNearby_ReturnsObjectInAdjacentCell()
    {
        var hash = new SpatialHash(cellSize: 10);
        var obj = MakeAt(9, 9);
        hash.Add("obj1", obj.Object);

        // позиция в соседней ячейке
        var nearby = hash.GetNearby(new Vector(11, 11)).ToList();

        Assert.Single(nearby);
    }

    [Fact]
    public void GetNearby_DoesNotReturnFarObject()
    {
        var hash = new SpatialHash(cellSize: 10);
        var obj = MakeAt(0, 0);
        hash.Add("obj1", obj.Object);

        var nearby = hash.GetNearby(new Vector(50, 50)).ToList();

        Assert.Empty(nearby);
    }

    [Fact]
    public void Remove_ObjectNoLongerInNearby()
    {
        var hash = new SpatialHash(cellSize: 10);
        var obj = MakeAt(5, 5);
        hash.Add("obj1", obj.Object);
        hash.Remove("obj1");

        var nearby = hash.GetNearby(new Vector(5, 5)).ToList();

        Assert.Empty(nearby);
    }

    [Fact]
    public void Remove_NonExistingId_DoesNotThrow()
    {
        var hash = new SpatialHash(cellSize: 10);

        var ex = Record.Exception(() => hash.Remove("unknown"));

        Assert.Null(ex);
    }
}
