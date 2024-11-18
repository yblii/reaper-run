using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

class Hellhound : Enemy
{
    public bool _overlaps = false;

    private float _detectionRange = 300;
    private float _attackRange = 10;

    private float _anticipation = 0.3f;
    private float _anticipationTimer = 0;
    private float _runSpeed = 4;
    private float _walkSpeed = 0.5f;

    private Actions _currentState;

    private Animator _anim;

    public static event Action<AchievementName> HellhoundKilled = delegate { };
    public Hellhound(float x, float y, float xs, float ys, Texture t) : base(x, y, xs, ys, t, 6, 0, 0, Layers.Enemy)
    {
        Layer = Layers.Enemy;
        Collidable = true;
        _isFacingRight = false;

        _anim = new Animator(sprite, "Assets\\AnimationData\\HellhoundAnimData.txt");
        _currentState = Actions.Idle;

        this._dY = 0;
        this._dX = -0.5f;


        /*
         * “Death – wolf”
            OCMaster
            https://www.voicy.network/sounds/GXP6wamyBUmutdRUmJGbIw-death-wolf

         */
        this.OnDeath = Engine.LoadSound("Sound\\Hellhound.Death.mp3");

        this.Scale = new Vector2(xs - 20, ys * 0.8f);
        this.SpriteOffset = new Vector2(0, - ys * 0.2f);
    }

    public override void Update()
    {
        DebugDraw.Add(this.GetBounds());
        _anim.Update();

        // gravity
        _dY += _aY;

        float direction = _isFacingRight ? 1 : -1;

        switch (this._currentState)
        {
            case Actions.Idle:
                _anim.ChangeAction(Actions.Walk);

                // resetting range
                _detectionRange = 300;

                // patrol on platform
                if (WillOverlap((0.5f + this.Scale.X) * direction, this.Scale.Y, CollisionHandler.GetCollisions(this)))
                {
                    _dX = _walkSpeed * direction;
                }
                else
                {
                    _isFacingRight = !_isFacingRight;
                }

                // check for player
                Bounds2 detection;

                if(_isFacingRight)
                {
                    detection = new Bounds2(this.GetBounds().Max.X, this.Position.Y, _detectionRange, this.Scale.Y);
                } 
                else
                {
                    detection = new Bounds2(this.GetBounds().Min.X - _detectionRange, this.Position.Y, _detectionRange, this.Scale.Y);
                }

                if(Player.player.GetBounds().Overlaps(detection))
                {
                    _currentState = Actions.Run;
                }
                DebugDraw.Add(detection);

                break;

            case Actions.Run:
                _anim.ChangeAction(Actions.Run);

                _detectionRange *= 2;

                // follow player depending on position
                if(Player.player.GetBounds().Position.X < Position.X)
                {
                    _isFacingRight = false;
                } 
                else
                {
                    _isFacingRight = true;
                }

                _dX = _runSpeed * direction;

                // checking if player is out of range
                if (Math.Abs(Player.player.Position.X - this.Position.X) > _detectionRange * 2 || Math.Abs(Player.player.Position.Y - this.Position.Y) > _detectionRange / 2)
                {
                    _currentState = Actions.Idle;
                    break;
                }

                // checking if player is within attack range
                if(Math.Abs(this.GetBounds().GetXOverlap(Player.player.GetBounds())) < _attackRange)
                {
                    _currentState = Actions.Anticipation;
                    break;
                }
                break;

            case Actions.Anticipation:
                _anim.ChangeAction(Actions.Anticipation);

                // pause before attack to give player warning
                _dX = 0;

                _anticipationTimer += Engine.TimeDelta;
                if(_anticipationTimer > _anticipation)
                {
                    _currentState = Actions.Attack;
                    _anticipationTimer = 0;
                }

                break;

            case Actions.Attack:
                _anim.ChangeAction(Actions.Attack);

                // short burst of speed
                _dX = direction * _attackRange;
                Bounds2 newBounds = new Bounds2(this.Position + new Vector2(_dX, 0), this.Scale);


                if(newBounds.Overlaps(Player.player.GetBounds()))
                {
                    if (!Player.player.IsInvulnerable())
                    {
                        Player.player.Damage(1);
                        Player.player.SetInvulnerable(.5f);
                    }
                }
                _currentState = Actions.Recovery;

                break;

            case Actions.Recovery:
                _anim.ChangeAction(Actions.Anticipation);

                _anticipationTimer += Engine.TimeDelta;
                if (_anticipationTimer > _anticipation)
                {
                    _currentState = Actions.Run;
                    _anticipationTimer = 0;
                }

                break;
        }

        if(_isFacingRight)
        {
            sprite.SetDirection(TextureMirror.Horizontal);
        } 
        else
        {
            sprite.SetDirection(TextureMirror.None);
        }

        if (this.WillOverlap(0, (float)_dY, CollisionHandler.GetCollisions(this)))
        {
            _dY = 0;
        }

        if (this.WillOverlap((float)_dX, 0, CollisionHandler.GetCollisions(this)))
        {
            _dX = 0;
        }

        if(this.Overlaps(CollisionHandler.GetCollisions(this)))
        {
            this.MoveY(this.UnclipVertical());
        }

        this.MoveX(_dX);
        this.MoveY(_dY);

        base.Update();
        this.sprite.SetBlendMode(TextureBlendMode.Normal);
    }

    public override void Kill()
    {
        Item.GenerateObject(this.Position.X, this.Position.Y, Layer);
        HellhoundKilled?.Invoke(AchievementName.Killer);
        
        // increase score by 2 points
        Game.GetCurrentScore().ChangeScore(Score.HELLHOUND);
        
        base.Kill();
    }
}