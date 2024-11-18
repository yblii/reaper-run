using System;
using System.Collections.Generic;

static class DebugDraw
{
    private static HashSet<Bounds2> _toDraw = new HashSet<Bounds2>();

    public static void DrawAll()
    {
        foreach (Bounds2 bounds in _toDraw)
        {
            Engine.DrawRectEmpty(bounds, Color.Red);
        }
        _toDraw.Clear();
    }

    public static void Add(Bounds2 bounds)
    {
        _toDraw.Add(bounds);
    }
}