using System;
using System.Collections.Generic;
using System.Text;

class ClimbableWall : GameObject
{
    public ClimbableWall(float x, float y, float xs, float ys, Texture t) : base(x, y, xs, ys, t, Layers.Wall)
    {
        Layer = Layers.Wall;
        Collidable = true;
    }

    public override void Update()
    {
        if (this.GetBounds().NextToX(Player.player.GetBounds()) && !Player.player.IsGrounded)
        {
            Player.player.ColWall = true;
        }
    }
}
