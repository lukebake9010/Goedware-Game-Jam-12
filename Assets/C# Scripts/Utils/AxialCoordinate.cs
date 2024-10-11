using System;
using Unity.Mathematics;
using UnityEngine;

public struct Axial : IEquatable<Axial>, IFormattable
{
    #region Variables

    //
    // Summary:
    //      The first Co-ordinate component.
    //      Named to prevent confusion with cartesian coords.
    //
    public float q;

    //
    // Summary:
    //      The second Co-ordinate component.
    //      Named to prevent confusion with cartesian coords.
    public float r;

    //
    // Summary:
    //      The third Co-ordinate component.
    //      Named to prevent confusion with cartesian coords.
    //      Due to Cubic coordinates being abstracted to 2d, by following the
    //      constraint of: q + r + s = 0, coordinates become {1}--{1} and only
    //      two coordinates need be stored, as the third can be calculated.
    private float s { get { return -q - r; } }

    #endregion

    #region Constants

    private static readonly Axial zeroAxial = new Axial(0f, 0f);
    private static readonly Axial upperLeftAxial = new Axial(0f, -1f);
    private static readonly Axial upperRightAxial = new Axial(1f, -1f);
    private static readonly Axial rightAxial = new Axial(1f, 0f);
    private static readonly Axial lowerRightAxial = new Axial(0f, 1f);
    private static readonly Axial lowerLeftAxial = new Axial(-1f, 1f);
    private static readonly Axial leftAxial = new Axial(-1f, 1f);
    private static readonly Vector3 redundantVector = Vector3.one;

    public float this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return q;
                case 1:
                    return r;
                case 2:
                    return s;
                default:
                    throw new IndexOutOfRangeException("Invalid Axial index!");
            }
        }
        set
        {
            switch (index)
            {
                case 0:
                    q = value;
                    break;
                case 1:
                    r = value;
                    break;
                case 2:
                    throw new IndexOutOfRangeException("Cubic index used on Axial! Please use the two defined Axis when using Axial Coords.");
                default:
                    throw new IndexOutOfRangeException("Invalid Axial index!");
            }
        }
    }
    //
    // Summary:
    //      Retrieves the distance of the coordinate from the origin in a number of "steps".
    public float magnitude
    {
        get
        {
            return (math.abs(q) + math.abs(r) + math.abs(s)) / 2;
        }
    }
    //
    // Summary:
    //      Shorthand for writing: new Axial(0,0)
    public static Axial zero
        => zeroAxial;
    //
    // Summary:
    //      Shorthand for writing: new Axial(0,-1)
    public static Axial upperLeft
        => upperLeftAxial;
    //
    // Summary:
    //      Shorthand for writing: new Axial(1,-1)
    public static Axial upperRight
        => upperRightAxial;
    //
    // Summary:
    //      Shorthand for writing: new Axial(1,0)
    public static Axial right
        => rightAxial;
    //
    // Summary:
    //      Shorthand for writing: new Axial(0,1)
    public static Axial lowerRight
        => lowerRightAxial;
    //
    // Summary:
    //      Shorthand for writing: new Axial(-1,1)
    public static Axial lowerLeft
        => lowerLeftAxial;
    //
    // Summary:
    //      Shorthand for writing: new Axial(-1,0)
    public static Axial left
        => leftAxial;

    // Summary:
    //      Redundant vector perpendicular to the "plane" of the grid in 3d
    //      cartesian space. All component vectors in this direction can be
    //      abstracted away. 
    public static Vector3 redundant
        => redundantVector;

    #endregion

    //
    // Summary:
    //      Creates a new Axial with the given coordinates.
    //
    // Parameters:
    //      q:
    //
    //      r: 
    public Axial(float q, float r)
    {
        this.q = q;
        this.r = r;
    }

    //
    // Summary:
    //      Sets the q and r components of an existing Axial object.
    //
    // Parameters:
    //      newQ:
    //
    //      newR:
    public Axial Set(float newQ, float newR)
    {
        q = newQ;
        r = newR;
        return this;
    }
    //
    // Summary:
    //      Copies the coordinates from another Axial object.
    //
    // Parameters:
    //      axial: The object to copy the coordinates from.
    public Axial Copy(Axial axial)
    {
        q = axial.q;
        r = axial.r;
        return this;
    }
    //
    // Summary:
    //      Returns the distance from this Axial coordinate from another, in a number of steps.
    //
    // Parameters:
    //      origin:
    //          The Axial coordinate to determine the distance from.
    public float Distance(Axial origin)
        => (this - origin).magnitude;
    //
    // Summary:
    //      Returns the distance between two Axial coordinates, in a number of steps.
    //
    // Parameters:
    //      target:
    //          The Axial coordinate to determine the distance to.
    //
    //      origin:
    //          The Axial coordinate to determine the distance from.
    public static float Distance(Axial target, Axial origin)
        => (target - origin).magnitude;

    //
    // Summary:
    //      Linearly interpolates between two Axial coordinates.
    //
    // Parameters:
    //      a:
    //          The first position to interpolate from.
    //
    //      b:
    //          The second position to interpolate to.
    //
    //      t:
    //          The ammount to interpolate, between 0 and 1.
    public static Axial Lerp(Axial a, Axial b, float t)
    {
        t = Mathf.Clamp01(t);
        return a + (b - a) * t;
    }
    //
    // Summary:
    //      Linearly interpolates between two Axial coordinates. Can exceed the bounds.
    //
    // Parameters:
    //      a:
    //          The first position to interpolate from.
    //
    //      b:
    //          The second position to interpolate to.
    //
    //      t:
    //          The ammount to interpolate, with position a at 0 and b at 1. Can exceed these bounds.
    public static Axial LerpUnclamped(Axial a, Axial b, float t)
    {
        return a + (b - a) * t;
    }
    //
    // Summary:
    //      Converts the Axial coordinate to a 2 dimensional Cartesian coordinate upon the "plane"
    //      the hexagonal grid occupies.
    public Vector2 to2DCartesian()
        => new Vector2(
            -r * 0.75f,
            (q - r) * math.sqrt(3) / 2f
        );

    //
    // Summary:
    //      Returns a formatted string for this Axial coordinate.
    //
    // Parameters:
    //      format:
    //          A numeric format string.
    //      
    //      formatProvider:
    //          An object that specifies culture-specific formatting.
    public override string ToString()
        => ToString(null, null);
    //
    // Summary:
    //      Returns a formatted string for this Axial coordinate.
    //
    // Parameters:
    //      format:
    //          A numeric format string.
    //      
    //      formatProvider:
    //          An object that specifies culture-specific formatting.
    public string ToString(string format)
        => ToString(format, null);
    //
    // Summary:
    //      Returns a formatted string for this Axial coordinate.
    //
    // Parameters:
    //      format:
    //          A numeric format string.
    //      
    //      formatProvider:
    //          An object that specifies culture-specific formatting.
    public string ToString(string format, IFormatProvider formatProvider)
        => ((Vector2)this).ToString(format, formatProvider);

    public override int GetHashCode()
    {
        return ~(q.GetHashCode() ^ (r.GetHashCode() << 2));
    }
    //
    // Summary:
    //     Returns true if the given Axial coordinate is exactly equal to this Axial coordinate.
    //
    // Parameters:
    //   other:
    public override bool Equals(object other)
    {
        if (!(other is Axial))
            return false;
        return Equals((Axial)other);
    }
    //
    // Summary:
    //     Returns true if the given Axial coordinate is exactly equal to this Axial coordinate.
    //
    // Parameters:
    //   other:
    public bool Equals(Axial other)
    {
        return q == other.q && r == other.r;
    }

    #region Transforms

    //
    // Summary:
    //      Rounds a decimal Axial coordinate to the nearest whole coordinate.
    public Axial Round()
    {
        float q = math.round(this.q);
        float r = math.round(this.r);
        float s = math.round(this.s);
        float q_diff = math.abs(q - this.q);
        float r_diff = math.abs(r - this.r);
        float s_diff = math.abs(s - this.s);

        if (q_diff > r_diff && q_diff > s_diff)
            q = -r - s;
        else if (r_diff > s_diff)
            r = -q - s;
        //else, s is changed, but s is implied so ¯\_(?)_/¯

        return Copy(new Axial(q, r));
    }
    //
    // Summary:
    //      Rounds a decimal Axial coordinate to the nearest whole coordinate.
    public static Axial Round(Axial axial)
    => axial.Round();

    //
    // Summary:
    //      Rotates an Axial coordinate around the origin by 1/6th of a rotation.
    //
    // Parameters:
    //      clockwise:
    //          Whether the rotation is clockwise or counterclockwise.
    public Axial Rotate(bool clockwise)
        => Rotate(zeroAxial, clockwise);
    //
    // Summary:
    //      Rotates an Axial coordinate around another point by 1/6th of a rotation.
    //
    // Parameters:
    //      origin:
    //          The point to rotate around.
    //
    //      clockwise:
    //          Whether the rotation is clockwise or counterclockwise.
    public Axial Rotate(Axial origin, bool clockwise)
        => Copy(Rotate(this, origin, clockwise));
    //
    // Summary:
    //      Rotates an Axial coordinate around the origin by a multiple of 1/6th of a rotation.
    //
    // Parameters:
    //      clockwise:
    //          Whether the rotation is clockwise or counterclockwise.
    //
    //      iterations:
    //          The number of 1/6th rotations to make.
    public Axial RotateMultiple(bool clockwise, int iterations)
        => RotateMultiple(zeroAxial, clockwise, iterations);
    //
    // Summary:
    //      Rotates an Axial coordinate around another point by a multiple of 1/6th of a rotation.
    //
    // Parameters:
    //      origin:
    //          The point to rotate around.
    //
    //      clockwise:
    //          Whether the rotation is clockwise or counterclockwise.
    //
    //      iterations:
    //          The number of 1/6th rotations to make.
    public Axial RotateMultiple(Axial origin, bool clockwise, int iterations)
    {
        for (int i = 0; i < iterations; i++)
            Copy(Rotate(this, origin, clockwise));
        return this;
    }
    //
    // Summary:
    //      Rotates an Axial coordinate around the origin by 1/6th of a rotation.
    //
    // Parameters:
    //      clockwise:
    //          Whether the rotation is clockwise or counterclockwise.
    //
    public static Axial Rotate(Axial axial, Axial origin, bool clockwise)
    {
        Axial diff = axial - origin;
        float oldS = diff.s;
        if (clockwise)
        {
            diff.q = -diff.r;
            diff.r = -oldS;
        }
        else
        {
            diff.r = -diff.q;
            diff.q = -oldS;
        }
        return diff;
    }
    //
    // Summary:
    //      Rotates an Axial coordinate around the origin by a multiple of 1/6th of a rotation.
    //
    // Parameters:
    //      clockwise:
    //          Whether the rotation is clockwise or counterclockwise.
    //
    //      iterations:
    //          The number of 1/6th rotations to make.
    public static Axial RotateMultiple(Axial axial, Axial origin, bool clockwise, int iterations)
        => (axial - origin).RotateMultiple(origin, clockwise, iterations);

    //
    // Summary:
    //      Reflects an Axial coordinate in the line q = 0.
    public Axial reflectQ()
        => Set(q, s);
    //
    // Summary:
    //      Reflects an Axial coordinate in the line perpendicular to q = 0.
    public Axial reflectRS()
        => Set(-q, -s);
    //
    // Summary:
    //      Reflects an Axial coordinate in the line r = 0.
    public Axial reflectR()
        => Set(s, r);
    //
    // Summary:
    //      Reflects an Axial coordinate in the line perpendicular to r = 0.
    public Axial reflectQS()
        => Set(-s, -r);
    //
    // Summary:
    //      Reflects an Axial coordinate in the line s = 0.
    public Axial reflectS()
        => Set(r, q);
    //
    // Summary:
    //      Reflects an Axial coordinate in the line perpendicular to s = 0.
    public Axial reflectQR()
        => Set(-r, -q);    
    //
    // Summary:
    //      Reflects an Axial coordinate in the line q = 0.
    public static Axial reflectQ(Axial axial)
        => axial.Set(axial.q, axial.s);    
    //
    // Summary:
    //      Reflects an Axial coordinate in the line perpendicular to q = 0.
    public static Axial reflectRS(Axial axial)
        => axial.Set(-axial.q, -axial.s);    
    //
    // Summary:
    //      Reflects an Axial coordinate in the line r = 0.
    public static Axial reflectR(Axial axial)
        => axial.Set(axial.s, axial.r);    
    //
    // Summary:
    //      Reflects an Axial coordinate in the line perpendicular to r = 0.
    public static Axial reflectQS(Axial axial)
        => axial.Set(-axial.s, -axial.r);    
    //
    // Summary:
    //      Reflects an Axial coordinate in the line s = 0.
    public static Axial reflectS(Axial axial)
        => axial.Set(axial.r, axial.q);    
    //
    // Summary:
    //      Reflects an Axial coordinate in the line perpendicular to s = 0.
    public static Axial reflectQR(Axial axial)
        => axial.Set(-axial.r, -axial.q);

    #endregion

    #region Operator Overrides

    public static Axial operator +(Axial a, Axial b)
        => new Axial(a.q + b.q, a.r + b.r);
    public static Axial operator -(Axial a, Axial b)
        => new Axial(a.q - b.q, a.r - b.r);
    public static Axial operator -(Axial a)
        => new Axial(-a.q, -a.r);
    public static Axial operator *(Axial a, float b)
        => new Axial(a.q * b, a.r * b);
    public static Axial operator *(float a, Axial b)
        => new Axial(a * b.q, a * b.r);
    public static Axial operator /(Axial a, float b)
        => new Axial(a.q / b, a.r / b);
    public static bool operator ==(Axial lhs, Axial rhs)
    {
        float num = lhs.q - rhs.q;
        float num2 = lhs.r - rhs.r;
        return num * num + num2 * num2 < 9.99999944E-11f;
    }
    public static bool operator !=(Axial lhs, Axial rhs)
        => !(lhs == rhs);

    public static implicit operator Vector2(Axial a)
        => new Vector2(a.q, a.r);
    public static implicit operator Axial(Vector2 a)
        => new Axial(a.x, a.y);
    public static implicit operator Vector3(Axial a)
        => new Vector3(a.q, a.r, a.s);
    public static implicit operator Axial(Vector3 a)
        => new Axial(a.x, a.y);
    
    #endregion
}