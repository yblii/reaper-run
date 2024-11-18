class ProjectileSpawner : GameObject
{
    private float _interval;
    private float _timer;

    public ProjectileSpawner(float x, float y, float width, float height) : base(x, y, width, height, Engine.LoadTexture("Sprites\\ProjectileSpawner.png"), Layers.Obstacle)
    {
        Layer = Layers.Obstacle;
        _interval = 2;
        _timer = 0;
    }

    public override void Update()
    {
        if(_timer < _interval)
        {
            _timer += Engine.TimeDelta;
        }
        else
        {
            _timer = 0;
            new DelayedProjectile(this.GetCenter().X - Projectile.DefaultWidth / 2, this.GetCenter().Y, Projectile.DefaultWidth, Projectile.DefaultHeight);
        }

        base.Update();
    }
}