using System;

class Enemy : Alive
{
	private int _critLocation;
	private CritPoint _crit;
	private Vector2 _critOffset;

	protected bool _isFacingRight;
	public static event Action<AchievementName> CritHit = delegate { };

	public Enemy(float x, float y, float xs, float ys, Texture t, float health, float armor, float curArmor, int layer) : base(x, y, xs, ys, t, health, armor, curArmor, layer)
	{
		GenerateCritPoint();
	}

	public override void Update()
    {
		if (_isFacingRight)
        {
			_critOffset = new Vector2(this.Scale.X - _crit.Scale.X, (1 - _critLocation / 2f) * this.Scale.Y - 10);
        } 
		else
        {
			_critOffset = new Vector2(0, (1 - _critLocation / 2f) * this.Scale.Y - 10);
		}

		_crit.Position = this.Position + _critOffset;
		DebugDraw.Add(_crit.GetBounds());

		base.Update();
	}

	private void GenerateCritPoint()
    {
		Random rand = new Random();
		_critLocation = rand.Next(0, 3);

		switch(_critLocation)
        {
			case 0:
				_crit = new CritPoint(this.Position.X, this.Position.Y, Actions.Attack_low);
				break;
			case 1:
				_crit = new CritPoint(this.Position.X, this.Position.Y, Actions.Attack_mid);
				break;
			case 2:
				_crit = new CritPoint(this.Position.X, this.Position.Y, Actions.Attack_high);
				break;
        }
	}

	public void Damage(int dmg, Actions attack)
    {
		if(_crit != null && attack.Equals(_crit.AtkPosition))
        {
			base.Damage(dmg * 2);
			_crit.Destroy();
        } 
		else
        {
			base.Damage(dmg);
        }

        if(Player.player.Position.X < this.Position.X)
		{
			this.MoveX(20);
			this._isFacingRight = false;
		} 
		else
        {
			this.MoveX(-20);
			this._isFacingRight = true;
		}

		this.MoveY(-5);
		this.sprite.SetBlendMode(TextureBlendMode.Additive);
    }

    public override void Kill()
    {
		_crit.Destroy();
        base.Kill();
    }
}
