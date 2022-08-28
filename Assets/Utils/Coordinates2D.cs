using System;

public struct Coordinates2D : IEquatable<Coordinates2D>
{
    public Coordinates2D(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static bool operator ==(Coordinates2D firstCoordinates, Coordinates2D secondCoordinates)
    {
        return Math.Abs(firstCoordinates.X - secondCoordinates.X) < Constants.FloatComparisionPrecision &&
               Math.Abs(firstCoordinates.Y - secondCoordinates.Y) < Constants.FloatComparisionPrecision;
    }
    
    public bool Equals(Coordinates2D other)
    {
        return X.Equals(other.X) && Y.Equals(other.Y);
    }

    public override bool Equals(object obj) => Equals(obj is Coordinates2D coordinates2D ? 
        coordinates2D : default);

    public static bool operator !=(Coordinates2D firstCoordinates, Coordinates2D secondCoordinates) =>
        !(firstCoordinates == secondCoordinates);
    
    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = X.GetHashCode();
            hashCode = (hashCode * 397) ^ Y.GetHashCode();
            return hashCode;
        }
    }
    
    public float X;
    public float Y;
}
