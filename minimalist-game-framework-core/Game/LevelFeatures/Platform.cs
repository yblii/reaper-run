using System;
using System.Collections.Generic;
using System.Text;

class Platform : GameObject
{
    public static readonly Color StationaryColor = Color.ForestGreen;
    public static readonly Color DropColor = Color.DeepPink;
    public static readonly Color FrictionColor = Color.DarkOrchid;

    public Platform(float x, float y, float xs, float ys, Texture t) : base(x, y, xs, ys, t, Layers.Platform)
    {
        Layer = Layers.Platform;
        Collidable = true;
    }
}
