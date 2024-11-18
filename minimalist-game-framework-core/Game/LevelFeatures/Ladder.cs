using System;
using System.Collections.Generic;
using System.Text;

class Ladder : GameObject
{
    public Ladder(float x, float y, float xs, float ys, Texture t) : base(x, y, xs, ys, t, Layers.Ladder)
    {
        Layer = Layers.Ladder;
        Collidable = false;
    }

    public override void Update()
    {
        if (this.GetBounds().Overlaps(Player.player.GetBounds()))
        {
            Player.player.ColLadder = true;
        }
    }
}
