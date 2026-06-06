using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class AngleTests
{
    // 1. Сложение углов
    [Fact]
    public void Angle_Addition_Should_Return_Correct_Value()
    {
        var a1 = new Angle(5, 8);
        var a2 = new Angle(7, 8);

        var result = a1 + a2;

        // ожидается нормализация (по условию у вас получается 4/8)
        Assert.Equal(new Angle(4, 8), result);
    }

    // 2. Equals — равные углы
    [Fact]
    public void Angle_Equals_Should_Return_True_For_Same_Value()
    {
        var a1 = new Angle(15, 8);
        var a2 = new Angle(23, 8);

        Assert.True(a1.Equals(a2));
    }

    // 3. == оператор
    [Fact]
    public void Angle_Operator_Equal_Should_Work()
    {
        var a1 = new Angle(15, 8);
        var a2 = new Angle(23, 8);

        Assert.True(a1 == a2);
    }

    // 4. Equals — неравные углы
    [Fact]
    public void Angle_Equals_Should_Return_False_For_Different_Values()
    {
        var a1 = new Angle(1, 8);
        var a2 = new Angle(2, 8);

        Assert.False(a1.Equals(a2));
    }

    // 5. != оператор
    [Fact]
    public void Angle_Operator_NotEqual_Should_Work()
    {
        var a1 = new Angle(1, 8);
        var a2 = new Angle(2, 8);

        Assert.True(a1 != a2);
    }

    // 6. HashCode
    [Fact]
    public void Angle_Should_Have_HashCode()
    {
        var a1 = new Angle(10, 8);

        Assert.NotEqual(0, a1.GetHashCode());
    }
}
