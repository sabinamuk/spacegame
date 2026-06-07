using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class LookupCollisionDetectorTests
{
    private static IDictionary<string, object> MakeObject(string type, int x, int y) =>
        new Dictionary<string, object>
        {
            ["type"] = type,
            ["position"] = new Vector(x, y)
        };

    [Fact]
    public void Detect_KeyInTable_ReturnsTrue()
    {
        var table = new Dictionary<(string, string, int, int), bool>
        {
            { ("ship", "torpedo", 0, 0), true }
        };
        var detector = new LookupCollisionDetector(table);

        var result = detector.Detect(MakeObject("ship", 5, 5), MakeObject("torpedo", 5, 5));

        Assert.True(result);
    }

    [Fact]
    public void Detect_KeyInTable_ReturnsFalse()
    {
        var table = new Dictionary<(string, string, int, int), bool>
        {
            { ("ship", "torpedo", 0, 0), false }
        };
        var detector = new LookupCollisionDetector(table);

        var result = detector.Detect(MakeObject("ship", 5, 5), MakeObject("torpedo", 5, 5));

        Assert.False(result);
    }

    [Fact]
    public void Detect_KeyNotInTable_ReturnsFalse()
    {
        var detector = new LookupCollisionDetector(new Dictionary<(string, string, int, int), bool>());

        var result = detector.Detect(MakeObject("ship", 0, 0), MakeObject("torpedo", 10, 10));

        Assert.False(result);
    }

    [Fact]
    public void Detect_SymmetricLookup_Works()
    {
        var table = new Dictionary<(string, string, int, int), bool>
        {
            { ("ship", "torpedo", 1, 0), true }
        };
        var detector = new LookupCollisionDetector(table);

        // torpedo относительно ship: dx=-1, dy=0 — должен найти симметричную запись
        var result = detector.Detect(MakeObject("torpedo", 4, 5), MakeObject("ship", 5, 5));

        Assert.True(result);
    }

    [Fact]
    public void Detect_UsesRelativePosition()
    {
        var table = new Dictionary<(string, string, int, int), bool>
        {
            { ("ship", "torpedo", 2, 3), true },
            { ("ship", "torpedo", 0, 0), false }
        };
        var detector = new LookupCollisionDetector(table);

        var collides = detector.Detect(MakeObject("ship", 7, 8), MakeObject("torpedo", 5, 5));
        var noCollision = detector.Detect(MakeObject("ship", 5, 5), MakeObject("torpedo", 5, 5));

        Assert.True(collides);
        Assert.False(noCollision);
    }
}
