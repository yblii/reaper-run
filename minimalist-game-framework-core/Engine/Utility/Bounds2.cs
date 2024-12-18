﻿using System;
using System.Collections.Generic;

struct Bounds2
{
    public Vector2 Position;
    public Vector2 Size;

    public Vector2 Min => Position;
    public Vector2 Max => Position + Size;

    /// <summary>
    /// Creates a new 2D bounds rectangle.
    /// </summary>
    /// <param name="position">The origin of the bounds.</param>
    /// <param name="size">The size of the bounds.</param>
    public Bounds2(Vector2 position, Vector2 size)
    {
        Position = position;
        Size = size;
    }

    /// <summary>
    /// Creates a new 2D bounds rectangle.
    /// </summary>
    /// <param name="x">The X component of the origin of the bounds.</param>
    /// <param name="y">The Y component of the origin of the bounds.</param>
    /// <param name="width">The width of the bounds.</param>
    /// <param name="height">The height of the bounds.</param>
    public Bounds2(float x, float y, float width, float height)
    {
        Position = new Vector2(x, y);
        Size = new Vector2(width, height);
    }

    public override string ToString()
    {
        return string.Format("({0}, {1})", Position, Size);
    }

    /// <summary>
    /// Returns true if a point is within these bounds.
    /// </summary>
    /// <param name="point">The point to test.</param>
    public bool Contains(Vector2 point)
    {
        return Min.X <= point.X && point.X <= Max.X && Min.Y <= point.Y && point.Y <= Max.Y;
    }

    /// <summary>
    /// Returns true if another bounds rectangle overlaps these bounds.
    /// </summary>
    /// <param name="bounds">The bounds to test.</param>
    public bool Overlaps(Bounds2 bounds)
    {
        return !(bounds.Max.X < Min.X || bounds.Min.X > Max.X || bounds.Max.Y < Min.Y || bounds.Min.Y > Max.Y);
    }

    public bool OverlapsT(Bounds2 bounds)
    {
        return !(bounds.Max.X <= Min.X || bounds.Min.X >= Max.X || bounds.Max.Y <= Min.Y || bounds.Min.Y >= Max.Y);
    }

    public bool NextToX(Bounds2 bounds)
    {
        return !(bounds.Max.X + 1 <= Min.X || bounds.Min.X - 1 >= Max.X || bounds.Max.Y <= Min.Y || bounds.Min.Y >= Max.Y);
    }
    
    public bool NextToY(Bounds2 bounds)
    {
        return !(bounds.Max.X <= Min.X || bounds.Min.X >= Max.X || bounds.Max.Y + 1 <= Min.Y || bounds.Min.Y - 1 >= Max.Y);
    }

    public bool OverlapsX(Bounds2 bounds)
    {
        return !(bounds.Max.X <= Min.X || bounds.Min.X >= Max.X);
    }

    public bool OverlapsY(Bounds2 bounds)
    {
        return !(bounds.Max.Y < Min.Y || bounds.Min.Y > Max.Y);
    }

    // returns shortest distance out of given bounds
    public float GetXOverlap(Bounds2 bounds)
    {
        float dist1;
        float dist2;

        dist1 = bounds.Min.X - Max.X;
        dist2 = bounds.Max.X - Min.X;

        if(Math.Abs(dist1) > Math.Abs(dist2))
        {
            return dist2;
        }

        return dist1;
    }

    public float FixYOverlap(Bounds2 bounds)
    {
        return Min.Y - bounds.Max.Y;
    }
}
