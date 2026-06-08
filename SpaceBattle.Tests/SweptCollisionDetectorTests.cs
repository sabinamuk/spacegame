using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class SweptCollisionDetectorTests
{
    private static IShapeFootprint SingleCell(string id, int x = 0, int y = 0) =>
        new ShapeFootprint(id, new List<(int, int)> { (x, y) });

    private static Mock<ICollidable> MakeCollidable(string shapeId, int x, int y, int vx, int vy)
    {
        var mock = new Mock<ICollidable>();
        mock.Setup(c => c.GetShapeId()).Returns(shapeId);
        mock.Setup(c => c.GetPosition()).Returns(new Vector(x, y));
        mock.Setup(c => c.GetVelocity()).Returns(new Vector(vx, vy));
        return mock;
    }

    private static SweptCollisionDetector MakeDetector(params IShapeFootprint[] shapes)
    {
        var provider = new ShapeProvider();
        foreach (var s in shapes)
            provider.Register(s);
        return new SweptCollisionDetector(provider);
    }

    [Fact]
    public void Collides_StaticOverlap_ReturnsTrue()
    {
        var detector = MakeDetector(SingleCell("A"), SingleCell("B"));

        var a = MakeCollidable("A", 5, 5, 0, 0);
        var b = MakeCollidable("B", 5, 5, 0, 0);

        Assert.True(detector.Collides(a.Object, b.Object));
    }

    [Fact]
    public void Collides_FastObjectTunnels_ReturnsTrue()
    {
        var detector = MakeDetector(SingleCell("A"), SingleCell("B"));

        var a = MakeCollidable("A", 0, 0, 10, 0);
        var b = MakeCollidable("B", 5, 0, 0, 0);

        Assert.True(detector.Collides(a.Object, b.Object));
    }

    [Fact]
    public void Collides_ObjectsMovingApart_ReturnsFalse()
    {
        var detector = MakeDetector(SingleCell("A"), SingleCell("B"));

        var a = MakeCollidable("A", 0, 0, -5, 0);
        var b = MakeCollidable("B", 10, 0, 5, 0);

        Assert.False(detector.Collides(a.Object, b.Object));
    }

    [Fact]
    public void Collides_ObjectsPassEachOther_ReturnsFalse()
    {
        var detector = MakeDetector(SingleCell("A"), SingleCell("B"));

        var a = MakeCollidable("A", 0, 0, 3, 0);
        var b = MakeCollidable("B", 10, 0, -3, 0);

        Assert.False(detector.Collides(a.Object, b.Object));
    }

    [Fact]
    public void Collides_ObjectsMeetMidway_ReturnsTrue()
    {
        var detector = MakeDetector(SingleCell("A"), SingleCell("B"));

        var a = MakeCollidable("A", 0, 0, 5, 0);
        var b = MakeCollidable("B", 10, 0, -5, 0);

        Assert.True(detector.Collides(a.Object, b.Object));
    }

    [Fact]
    public void Collides_MultiCellShape_DetectsCollision()
    {
        var shapeA = new ShapeFootprint("A", new List<(int, int)> { (0, 0), (1, 0) });
        var detector = MakeDetector(shapeA, SingleCell("B"));

        var a = MakeCollidable("A", 0, 0, 20, 0);
        var b = MakeCollidable("B", 5, 0, 0, 0);

        Assert.True(detector.Collides(a.Object, b.Object));
    }

    [Fact]
    public void Collides_SameVelocity_StaticCheck()
    {
        var detector = MakeDetector(SingleCell("A"), SingleCell("B"));

        var a = MakeCollidable("A", 3, 3, 5, 5);
        var b = MakeCollidable("B", 3, 3, 5, 5);

        Assert.True(detector.Collides(a.Object, b.Object));
    }
}
