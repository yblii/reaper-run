using System;
using System.Collections.Generic;

internal class Spike : Obstacle
{
    public Spike(float x, float y) : base(x, y, TileManager.TileWidth, TileManager.TileHeight,
        Engine.LoadTexture("Sprites\\Terrain\\Spike.png"))
    {
        attack = 20;
        Layer = Layers.Obstacle;
    }
}