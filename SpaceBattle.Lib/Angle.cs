using System.Linq;
using System;

namespace SpaceBattle.Lib;


public class Angle
{
    private static int denominator = 8;
    private readonly int value;

    public Angle(int value, int denominator)
    {
        Angle.denominator = denominator;
        this.value = Normalize(value);
    }

    private int Normalize(int v)
    {
        v %= denominator;
        if (v < 0) v += denominator;
        return v;
    }

    public static Angle operator +(Angle a, Angle b)
    {
        return new Angle(a.value + b.value, denominator);
    }

    public override bool Equals(object obj)
    {
        if (obj is not Angle other) return false;
        return value == other.value;
    }

    public override int GetHashCode()
    {
        return value.GetHashCode();
    }

    public static bool operator ==(Angle a, Angle b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

    public static bool operator !=(Angle a, Angle b)
    {
        return !(a == b);
    }

    public static implicit operator double(Angle a)
    {
        return 2 * Math.PI * a.value / denominator;
    }
}