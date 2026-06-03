using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class VectorTests
{
    [Fact]
    public void Add_ValidVectors_ReturnsResult()
    {
        var v1 = new Vector(1, -1, 2);
        var v2 = new Vector(-1, 1, -2);

        var result = v1 + v2;

        Assert.Equal(new Vector(0, 0, 0), result);
    }

    [Fact]
    public void Add_FirstVectorLonger_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Vector(1, 2, 3) + new Vector(1, 2));
    }

    [Fact]
    public void Add_SecondVectorLonger_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Vector(1, 2) + new Vector(1, 2, 3));
    }

    [Fact]
    public void Equals_SameCoordinates_ReturnsTrue()
    {
        Assert.True(new Vector(1, 2).Equals(new Vector(1, 2)));
    }

    [Fact]
    public void OperatorEqual_SameCoordinates_ReturnsTrue()
    {
        Assert.True(new Vector(1, 2) == new Vector(1, 2));
    }

    [Fact]
    public void Equals_DifferentCoordinates_ReturnsFalse()
    {
        Assert.False(new Vector(1, 2).Equals(new Vector(2, 1)));
    }

    [Fact]
    public void OperatorNotEqual_DifferentCoordinates_ReturnsTrue()
    {
        Assert.True(new Vector(1, 2) != new Vector(2, 1));
    }

    [Fact]
    public void GetHashCode_ReturnsValue()
    {
        var hash = new Vector(1, 2).GetHashCode();

        Assert.NotEqual(0, hash);
    }
}
