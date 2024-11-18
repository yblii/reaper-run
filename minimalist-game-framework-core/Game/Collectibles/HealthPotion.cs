using System;
using System.Collections.Generic;
using System.Text;

class HealthPotion : Item
{ 
    public HealthPotion(float x, float y, int layer) : 
        base(x, y, TileManager.TileWidth, TileManager.TileHeight, Engine.LoadTexture("Sprites\\Potion.png"), layer)
    {
    }

    public override void OnCollision()
    {
        Player.player.Heal(2);
        CollisionHandler.Remove(this);
        Camera.RemoveGameObject(this);
    }
}