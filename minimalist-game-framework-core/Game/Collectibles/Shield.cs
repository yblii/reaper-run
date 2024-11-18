using System;
using System.Collections.Generic;
using System.Text;

class Shield : Item
{
    public Shield(float x, float y, int layer) :
    base(x, y, TileManager.TileWidth, TileManager.TileHeight, Engine.LoadTexture("Sprites\\Shield.png"), layer)
    {

    }

    public override void OnCollision()
    {
        Player.player.Shield(2);
        base.OnCollision();
    }
}