namespace SpaceBattle.Lib;

public class Vector
{
    private readonly int[] _coordinates;

    public Vector(params int[] coordinates)
    {
        _coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
    }

    public int this[int index] => _coordinates[index];

    public int Size => _coordinates.Length;

    public static Vector operator +(Vector a, Vector b)
    {
        if (a.Size != b.Size)
            throw new ArgumentException("Vectors must have same dimension.");

        return new Vector(
            a._coordinates.Zip(b._coordinates, (x, y) => x + y).ToArray()
        );
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Vector other || Size != other.Size)
            return false;

        return _coordinates.Zip(other._coordinates, (x, y) => x == y)
                           .All(equal => equal);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Size, string.Join(",", _coordinates));
    }

    public static bool operator ==(Vector a, Vector b)
    {
        return Equals(a, b);
    }

    public static bool operator !=(Vector a, Vector b)
    {
        return !Equals(a, b);
    }
}
