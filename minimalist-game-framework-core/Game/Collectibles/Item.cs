using System;
using System.Collections.Generic;
using System.Text;

class Item : GameObject
{
    private static Random _random = new Random();

    public enum ItemWeights
    {
        HealthPotW = 4,
        ShieldW = 4,
        JumpW = 2,
    }

    public Item(float x, float y, float xs, float ys, Texture t, int layer) : base(x, y, xs, ys, t, layer)
    {
        Collidable = false;
    }

    public virtual void OnCollision()
    {
        this.Destroy();
    }

    public static void GenerateObject(float x, float y, int layer)
    {
        int randomNumber = _random.Next(1, TotalWeight() + 1);

        if (randomNumber <= (int) ItemWeights.HealthPotW)
        {
            new HealthPotion(x, y, layer);
            return;
        }
        randomNumber -= (int)ItemWeights.HealthPotW;
        if(randomNumber <= (int)ItemWeights.ShieldW)
        {
            new Shield(x, y, layer);
            return;
        }
        randomNumber -= (int)ItemWeights.JumpW;
        if(randomNumber <= (int)ItemWeights.JumpW)
        {
            new JumpPotion(x, y);
            return;
        }
    }

    public static int TotalWeight()
    {
        int total = 0;
        foreach(int i in Enum.GetValues(typeof(ItemWeights)))
        {
            total += i;
        }
        return total;
    }

    public override void Update()
    {
        if (this.GetBounds().Overlaps(Player.player.GetBounds()))
        {
            this.OnCollision();
        }
    }
}