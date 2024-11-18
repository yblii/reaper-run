using System;

class DroppingPlatform : Platform
{
	private double _timer = 0;
	private bool _isBreaking { get; set; }
	private bool _isBroken { get; set; }

	public DroppingPlatform(float x, float y, float xs, float ys, Texture t) : base(x, y, xs, ys, t)
	{
		_isBreaking = false;
		_isBroken = false;
	}

	public override void Update()
    {
		if(this.GetBounds().NextToY(Player.player.GetBounds()))
        {
			_timer += Engine.TimeDelta;
        }

		if(!_isBroken && _timer > 1)
        {
			_isBroken = true;
			//Engine.PlaySound(Engine.LoadSound("Sound\\DroppingPlatform.Break.mp3"));
			//Console.WriteLine("breaking");
		}

		if(_isBroken)
        {
			MoveY(8);
        }
    }
}
