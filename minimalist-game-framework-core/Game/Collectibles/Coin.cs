using System;
using System.Collections.Generic;
using System.Text;

class Coin : Item
{
    public static int collected = 0;
    public static float coinCD = 1;
    public static float currentCD = 1;
    private static Random _random = new Random();

    public Coin(float x, float y) :
            base(x, y, TileManager.TileWidth, TileManager.TileHeight, Engine.LoadTexture("Sprites\\Coin.png"), 8)
    {

    }

    public override void OnCollision()
    {
        collected++;
        base.OnCollision();

        // increase score by 1 point
        Game.GetCurrentScore().ChangeScore(Score.COIN);
    }

    public static void GenerateCoin()
    {
        currentCD -= Engine.TimeDelta;

        if(currentCD <= 0)
        {
            new Coin(_random.Next(0, (int) Game.Resolution.X + 1), 
                _random.Next(0, (int) Game.Resolution.Y + 1));
            currentCD = coinCD;
        }
    }
}