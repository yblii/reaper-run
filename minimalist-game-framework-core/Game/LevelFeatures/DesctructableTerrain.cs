using System;
using System.Collections.Generic;
using System.Text;

internal class DestructableTerrain : Alive
{
    public DestructableTerrain(float x, float y, float xs, float ys, Texture t)
        : base(x, y, xs, ys, t, 2, 0, 0, Layers.Wall)
    {
        Collidable = true;

        /*
         * “Stone breaking sound effect (no copyright)”
            world of sound effects
            https://www.youtube.com/watch?v=5I3aa_v8Oms
         */
        this.OnDeath = Engine.LoadSound("Sound\\Platform.Break.mp3");
    }

    public override void Update()
    {
        DebugDraw.Add(this.GetBounds());
    }
}
