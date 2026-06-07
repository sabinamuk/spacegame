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
    public void Collides_CurrentPositionCollides_ReturnsTrue()
    {
        var map = new CollisionMap();
        map.Add("ship", "torpedo", 0, 0);
        var detector = new CollisionDetector(map);

        var a = MakeCollidable("ship", 5, 5, 0, 0);
        var b = MakeCollidable("torpedo", 5, 5, 0, 0);

        Assert.True(detector.Collides(a.Object, b.Object));
    }

    [Fact]
    public void Collides_NextPositionCollides_ReturnsTrue()
    {
        var map = new CollisionMap();
        map.Add("ship", "torpedo", 0, 0);
        var detector = new CollisionDetector(map);

        // ship=(0,0) vel=(2,0), torpedo=(3,0) vel=(-1,0) → следующий тик: оба в (2,0)
        var a = MakeCollidable("ship", 0, 0, 2, 0);
        var b = MakeCollidable("torpedo", 3, 0, -1, 0);

        Assert.True(detector.Collides(a.Object, b.Object));
    }

    [Fact]
    public void Collides_NeitherCurrentNorNext_ReturnsFalse()
    {
        var map = new CollisionMap();
        map.Add("ship", "torpedo", 0, 0);
        var detector = new CollisionDetector(map);

        var a = MakeCollidable("ship", 0, 0, 1, 0);
        var b = MakeCollidable("torpedo", 10, 0, 0, 0);

        Assert.False(detector.Collides(a.Object, b.Object));
    }

    [Fact]
    public void Collides_ChecksBothCurrentAndNext()
    {
        var mockMap = new Mock<ICollisionMap>();
        mockMap.Setup(m => m.Contains(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(false);
        var detector = new CollisionDetector(mockMap.Object);

        var a = MakeCollidable("ship", 0, 0, 1, 0);
        var b = MakeCollidable("torpedo", 2, 0, 0, 0);

        detector.Collides(a.Object, b.Object);

        mockMap.Verify(m => m.Contains(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
    }
}

public class CollisionMapTests
{
    private readonly CollisionMap _map = new();

    [Fact]
    public void Contains_AfterAdd_ReturnsTrue()
    {
        _map.Add("ship", "torpedo", 0, 0);
        Assert.True(_map.Contains("ship", "torpedo", 0, 0));
    }

    [Fact]
    public void Contains_NotAdded_ReturnsFalse()
    {
        Assert.False(_map.Contains("ship", "torpedo", 5, 5));
    }

    [Fact]
    public void Contains_Symmetric_ReturnsTrue()
    {
        _map.Add("ship", "torpedo", 3, -2);
        Assert.True(_map.Contains("torpedo", "ship", -3, 2));
    }

    [Fact]
    public void Add_Duplicate_DoesNotDuplicate()
    {
        _map.Add("A", "B", 0, 0);
        _map.Add("A", "B", 0, 0);
        Assert.Single(_map.GetEntries());
    }

    [Fact]
    public void GetEntries_ReturnsAllAdded()
    {
        _map.Add("A", "B", 0, 0);
        _map.Add("A", "B", 1, 0);
        Assert.Equal(2, _map.GetEntries().Count);
    }
}
