using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class CheckCollisionCommandTests
{
    private static Mock<ICollidable> MakeCollidable(string shapeId = "ship")
    {
        var mock = new Mock<ICollidable>();
        mock.Setup(c => c.GetShapeId()).Returns(shapeId);
        mock.Setup(c => c.GetPosition()).Returns(new Vector(0, 0));
        mock.Setup(c => c.GetVelocity()).Returns(new Vector(0, 0));
        return mock;
    }

    [Fact]
    public void Execute_CollisionDetected_ExecutesOnCollision()
    {
        var self = MakeCollidable("ship");
        var other = MakeCollidable("torpedo");
        var onCollision = new Mock<ICommand>();
        var detector = new Mock<ICollisionDetector>();
        var repo = new Mock<ICollidableRepository>();

        repo.Setup(r => r.GetAll()).Returns([("other-1", other.Object)]);
        detector.Setup(d => d.Collides(self.Object, other.Object)).Returns(true);

        new CheckCollisionCommand("self-id", self.Object, repo.Object, detector.Object, onCollision.Object).Execute();

        onCollision.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Execute_NoCollision_DoesNotExecuteOnCollision()
    {
        var self = MakeCollidable();
        var other = MakeCollidable("torpedo");
        var onCollision = new Mock<ICommand>();
        var detector = new Mock<ICollisionDetector>();
        var repo = new Mock<ICollidableRepository>();

        repo.Setup(r => r.GetAll()).Returns([("other-1", other.Object)]);
        detector.Setup(d => d.Collides(It.IsAny<ICollidable>(), It.IsAny<ICollidable>())).Returns(false);

        new CheckCollisionCommand("self-id", self.Object, repo.Object, detector.Object, onCollision.Object).Execute();

        onCollision.Verify(c => c.Execute(), Times.Never);
    }

    [Fact]
    public void Execute_SkipsSelfById()
    {
        var self = MakeCollidable();
        var onCollision = new Mock<ICommand>();
        var detector = new Mock<ICollisionDetector>();
        var repo = new Mock<ICollidableRepository>();

        repo.Setup(r => r.GetAll()).Returns([("self-id", self.Object)]);

        new CheckCollisionCommand("self-id", self.Object, repo.Object, detector.Object, onCollision.Object).Execute();

        detector.Verify(d => d.Collides(It.IsAny<ICollidable>(), It.IsAny<ICollidable>()), Times.Never);
    }

    [Fact]
    public void Execute_StopsAfterFirstCollision()
    {
        var self = MakeCollidable();
        var other1 = MakeCollidable("torpedo");
        var other2 = MakeCollidable("asteroid");
        var onCollision = new Mock<ICommand>();
        var detector = new Mock<ICollisionDetector>();
        var repo = new Mock<ICollidableRepository>();

        repo.Setup(r => r.GetAll()).Returns([("obj-1", other1.Object), ("obj-2", other2.Object)]);
        detector.Setup(d => d.Collides(self.Object, other1.Object)).Returns(true);
        detector.Setup(d => d.Collides(self.Object, other2.Object)).Returns(true);

        new CheckCollisionCommand("self-id", self.Object, repo.Object, detector.Object, onCollision.Object).Execute();

        onCollision.Verify(c => c.Execute(), Times.Once);
    }
}
