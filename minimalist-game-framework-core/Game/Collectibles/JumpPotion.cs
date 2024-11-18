using System;
using System.Collections.Generic;

internal class JumpPotion : Item
{
    public JumpPotion(float x, float y) : 
        //HARDCODED LAYER
        base(x, y, TileManager.TileWidth, TileManager.TileHeight, Engine.LoadTexture("Sprites\\Shield.png"), 8)
    {

    }

    public override void OnCollision()
    {
        Player.player.playerStatus.Add(new SuperJump(Player.player));

        CollisionHandler.Remove(this);
        Camera.RemoveGameObject(this);
    }
}