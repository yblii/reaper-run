class DelayedProjectile : Projectile
{
    private bool _isDetonating;

    private float _timer;
    private float _timeToDetonate;
    private bool _exploded;

    private Animator _anim;

    public DelayedProjectile(float x, float y, float width, float height) : base(x, y, width, height)
    {
        Layer = Layers.Obstacle;
        _isDetonating = false;
        Collidable = false;
        _timeToDetonate = 1;
        _exploded = false;

        _anim = new Animator(this.sprite, "Assets\\AnimationData\\ProjAnimData.txt");
        _anim.ChangeAction(Actions.Idle);
    }

    public override void Update()
    {
        dy += ay;
        _anim.Update();

        if (this.WillOverlap(dx, dy, CollisionHandler.GetCollisions(this)))
        {
            dy = 0;
            dx = 0;
            _isDetonating = true;
        }

        if(_isDetonating)
        {
            _timer += Engine.TimeDelta;
        }

        if (_timer >= _timeToDetonate || Player.player.GetBounds().Overlaps(this.GetBounds()))
        {
            if(Player.player.GetBounds().Overlaps(this.GetBounds()) && !_exploded)
            {
                Player.player.Damage(attack);
                _exploded = true;
            }
            _anim.ChangeAction(Actions.Attack);

            if(_anim.AnimationComplete())
            {
                Destroy();
            }
        }

        _anim.ChangeAction(Actions.Idle);

        this.MoveY(dy);
        this.MoveX(dx);
    }
}