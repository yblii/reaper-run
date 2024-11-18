using System;

class VariableFrictionPlatform : Platform
{
	private float _mu = 0.3f;
	public VariableFrictionPlatform(float x, float y, float xs, float ys, Texture t) : base(x, y, xs, ys, t)
	{
		
	}

	public override void Update()
    {
        if (this.GetBounds().NextToY(Player.player.GetBounds()))
		{
			Player.player.Friction = _mu;
            Player.player.maxSpeed = 8;
        }
    }
}
