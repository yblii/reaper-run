using System;

class Obstacle : GameObject
{
	protected int attack;

	public Obstacle(float x, float y, float width, float height, Texture t) : base(x, y, width, height, t, Layers.Obstacle)
	{
		Layer = Layers.Obstacle;
		attack = 1;
	}

    public override void Update()
    {
		if(Player.player.GetBounds().Overlaps(this.GetBounds())) 
		{
			Player.player.Damage(attack);
		}
    }
}
