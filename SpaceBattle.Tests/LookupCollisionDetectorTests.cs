using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class CollisionDetectorTests
{
    private static Mock<ICollidable> MakeCollidable(string shapeId, int x, int y, int vx, int vy)
    {
        var mock = new Mock<ICollidable>();
        mock.Setup(c => c.GetShapeId()).Returns(shapeId);
        mock.Setup(c => c.GetPosition()).Returns(new Vector(x, y));
        mock.Setup(c => c.GetVelocity()).Returns(new Vector(vx, vy));
        return mock;
    }

    [Fact]
    public void Collides_StaticOverlap_ReturnsTrue()
    {
        var map = new CollisionMap();
        map.Add("ship", "torpedo", 0, 0, 0, 0);
        var detector = new CollisionDetector(map);

        var a = MakeCollidable("ship", 5, 5, 0, 0);
        var b = MakeCollidable("torpedo", 5, 5, 0, 0);

        Assert.True(detector.Collides(a.Object, b.Object));
    }

    [Fact]
    public void Collides_FastObjectTunnels_ReturnsTrue()
    {
        var shapeA = new ShapeFootprint("torpedo", [(0, 0)]);
        var shapeB = new ShapeFootprint("ship", [(0, 0)]);
        var map = new CollisionMap();

        new PrepareCollisionDataCommand(shapeA, shapeB, map, maxVelocity: 10).Execute();

        var torpedo = MakeCollidable("torpedo", 0, 0, 10, 0);
        var ship = MakeCollidable("ship", 5, 0, 0, 0);

        Assert.True(new CollisionDetector(map).Collides(torpedo.Object, ship.Object));
    }

    [Fact]
    public void Collides_NoCollision_ReturnsFalse()
    {
        var map = new CollisionMap();
        map.Add("ship", "torpedo", 0, 0, 0, 0);
        var detector = new CollisionDetector(map);

        var a = MakeCollidable("ship", 0, 0, 1, 0);
        var b = MakeCollidable("torpedo", 10, 0, 0, 0);

        Assert.False(detector.Collides(a.Object, b.Object));
    }
}

public class CollisionMapTests
{
    private readonly CollisionMap _map = new();

    [Fact]
    public void Contains_AfterAdd_ReturnsTrue()
    {
        _map.Add("ship", "torpedo", 0, 0, 0, 0);
        Assert.True(_map.Contains("ship", "torpedo", 0, 0, 0, 0));
    }

    [Fact]
    public void Contains_NotAdded_ReturnsFalse()
    {
        Assert.False(_map.Contains("ship", "torpedo", 5, 5, 0, 0));
    }

    [Fact]
    public void Contains_Symmetric_ReturnsTrue()
    {
        _map.Add("ship", "torpedo", 3, -2, 1, 0);
        Assert.True(_map.Contains("torpedo", "ship", -3, 2, -1, 0));
    }

    [Fact]
    public void Add_Duplicate_DoesNotDuplicate()
    {
        _map.Add("A", "B", 0, 0, 0, 0);
        _map.Add("A", "B", 0, 0, 0, 0);
        Assert.Single(_map.GetEntries());
    }

    [Fact]
    public void GetEntries_ReturnsAllAdded()
    {
        _map.Add("A", "B", 0, 0, 0, 0);
        _map.Add("A", "B", 1, 0, 0, 0);
        Assert.Equal(2, _map.GetEntries().Count);
    }
}
