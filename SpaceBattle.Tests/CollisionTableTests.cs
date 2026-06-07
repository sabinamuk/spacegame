using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class PrepareCollisionDataCommandTests
{
    [Fact]
    public void Execute_SingleCellShapes_AddsOverlapEntry()
    {
        var shapeA = new ShapeFootprint("ship", [(0, 0)]);
        var shapeB = new ShapeFootprint("torpedo", [(0, 0)]);
        var map = new CollisionMap();

        new PrepareCollisionDataCommand(shapeA, shapeB, map).Execute();

        Assert.True(map.Contains("ship", "torpedo", 0, 0));
    }

    [Fact]
    public void Execute_MultiCellShapeA_AddsEntryForEachCell()
    {
        var shapeA = new ShapeFootprint("ship", [(0, 0), (1, 0), (-1, 0)]);
        var shapeB = new ShapeFootprint("torpedo", [(0, 0)]);
        var map = new CollisionMap();

        new PrepareCollisionDataCommand(shapeA, shapeB, map).Execute();

        Assert.True(map.Contains("ship", "torpedo", 0, 0));
        Assert.True(map.Contains("ship", "torpedo", 1, 0));
        Assert.True(map.Contains("ship", "torpedo", -1, 0));
    }

    [Fact]
    public void Execute_MultiCellBothShapes_AddsAllPairCombinations()
    {
        var shapeA = new ShapeFootprint("A", [(0, 0), (1, 0)]);
        var shapeB = new ShapeFootprint("B", [(0, 0), (0, 1)]);
        var map = new CollisionMap();

        new PrepareCollisionDataCommand(shapeA, shapeB, map).Execute();

        Assert.True(map.Contains("A", "B", 0, 0));
        Assert.True(map.Contains("A", "B", 0, -1));
        Assert.True(map.Contains("A", "B", 1, 0));
        Assert.True(map.Contains("A", "B", 1, -1));
        Assert.Equal(4, map.GetEntries().Count);
    }

    [Fact]
    public void Execute_DisjointPositions_DoesNotAddFalsePositives()
    {
        var shapeA = new ShapeFootprint("ship", [(0, 0)]);
        var shapeB = new ShapeFootprint("torpedo", [(0, 0)]);
        var map = new CollisionMap();

        new PrepareCollisionDataCommand(shapeA, shapeB, map).Execute();

        Assert.False(map.Contains("ship", "torpedo", 1, 0));
        Assert.False(map.Contains("ship", "torpedo", 0, 1));
    }
}

public class SaveLoadCollisionDataTests : IDisposable
{
    private readonly string _filePath = Path.GetTempFileName();

    [Fact]
    public void SaveThenLoad_RoundTrip_ProducesIdenticalEntries()
    {
        var source = new CollisionMap();
        source.Add("ship", "torpedo", 0, 0);
        source.Add("ship", "torpedo", 1, 0);

        new SaveCollisionDataCommand(source, _filePath).Execute();

        var target = new CollisionMap();
        new LoadCollisionDataCommand(target, _filePath).Execute();

        foreach (var e in source.GetEntries())
            Assert.True(target.Contains(e.ShapeA, e.ShapeB, e.RelX, e.RelY));

        Assert.Equal(source.GetEntries().Count, target.GetEntries().Count);
    }

    [Fact]
    public void Save_CreatesValidJsonFile()
    {
        var map = new CollisionMap();
        map.Add("ship", "torpedo", 1, 2);

        new SaveCollisionDataCommand(map, _filePath).Execute();

        var content = File.ReadAllText(_filePath);
        Assert.Contains("ShapeA", content);
        Assert.Contains("ShapeB", content);
    }

    public void Dispose()
    {
        if (File.Exists(_filePath))
            File.Delete(_filePath);
    }
}
