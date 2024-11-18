using System;
using System.Collections.Generic;

class Player : Alive
{
    public static Player player;
    private PlayerInput _input;
    public Animator anim { get; }

    public List<Status> playerStatus = new List<Status>();

    public bool IsGrounded = false;
    public bool ColLadder = false;
    public bool ColWall = false;

    public float Invulnerable = 0;

    public bool IsFacingRight = true;
    private Bounds2 _attackBox;
    public float AttackCD = 0;

    public float jumpHeight = -10f;

    public float maxSpeed = 5.5f;
    private readonly float defaultSpeed = 5.5f;
    
    public float Friction = 1;
    private readonly float defaultFriction = 1;

    public Player(float x, float y, float xs, float ys, Texture t) : base(x, y, xs, ys, t, 4, 1, 0, Layers.Player)
    {
        Layer = Layers.Player;
        _input = new PlayerInput(this);
        Collidable = true;
        player = this;

        anim = new Animator(sprite, "Assets\\AnimationData\\PlayerAnimData.txt");
        sprite.SetSize(new Vector2(xs * 3, ys));

        this._dX = 0;
        this._dY = 0;
        this._aX = 0.9f;
        this._aY = 0.6f;
    }
    public void Reset()
    {
        this.Position = new Vector2(50, 50);
    }

    public override void Update()
    { 
        anim.Update();

        if(ColWall)
        {
            this._aY = 0.6f;
            _dY = 0;
        }

        // basic movement
        _dY += _aY;
        _input.Update();

        if (this.WillOverlap(0, (float) _dY, CollisionHandler.GetCollisions(this)) || ColLadder)
        {
            IsGrounded = _dY >= 0;
            _dY = 0;
        } 
        else
        {
            IsGrounded = false;
        }

        this.MoveY((float)_dY);

        if (!this.WillOverlap(Player.player._dX, 0, CollisionHandler.GetCollisions(player)))
        {
            this.MoveX((float)_dX);
        }
        else
        {
            // moves player as close as possible to wall without being in it
            this.MoveX(this.HorizontalCollision((float)_dX));
        }

        base.Update();

        // prevents multiple hits from registering when colliding with an enemy
        if(this.Invulnerable - Engine.TimeDelta < 0)
        {
            Invulnerable = 0;
        }
        else
        {
            Invulnerable -= Engine.TimeDelta;
        }

        // attack stuff
        if (this.AttackCD - Engine.TimeDelta < 0)
        {
            AttackCD = 0;
        }
        else
        {
            AttackCD -= Engine.TimeDelta;
        }

        // flipping character
        if (IsFacingRight)
        {
            _attackBox = new Bounds2(this.Position + new Vector2(this.Scale.X, 0), this.Scale);
            SpriteOffset = new Vector2(-20, 0);
            sprite.SetDirection(TextureMirror.None);
        }
        else
        {
            _attackBox = new Bounds2(this.Position - new Vector2(this.Scale.X, 0), this.Scale);
            SpriteOffset = new Vector2(-75, 0);
            sprite.SetDirection(TextureMirror.Horizontal);
        }
        ResetPhysics();

        Status.UpdateStatus(playerStatus, this);
        DebugDraw.Add(this.GetBounds());
        DebugDraw.Add(_attackBox);

    }

    private void ResetPhysics()
    {
        Friction = defaultFriction;
        maxSpeed = defaultSpeed;
        _aY = 0.6f;
        ColLadder = false;
        ColWall = false;
    }

    public bool IsInvulnerable()
    {
        return Invulnerable > 0;
    }

    public void SetInvulnerable(float t)
    {
        Invulnerable = t;
    }

    public override void Kill()
    {
        Game.StateManager.PushState(new LoseScreen());
    }

    public void Attack(Actions action)
    {
        foreach (GameObject o in CollisionHandler.GetCollisions(_attackBox))
        {
            if (o.GetBounds().Overlaps(_attackBox)
                && !o.Equals(Player.player))
            {
                if(o is Enemy)
                {
                    Enemy obj = o as Enemy;
                    obj.Damage(1, action);
                    LevelManager.CurrLevel.Attacked = true;
                }
                else if(o is Death)
                {
                    Death d = o as Death;
                    if (!d.invulnerable)
                    {
                        d.Damage(1);
                    }
                }
                else if(o is Alive)
                {
                    Alive obj = o as Alive;
                    obj.Damage(1);
                }
                ActiveLevel.Hitlag();
            }
        }
        AttackCD = 0.2f;

        /*
         * “Sword Swing Sound Effect”
            Sound Effect Database
            https://www.youtube.com/watch?v=rVsvPF_znNI
        */
        switch (action)
        {
            case Actions.Attack_high:
                Engine.PlaySound(Engine.LoadSound("Sound\\Attack.High.mp3"));
                break;

            case Actions.Attack_mid:
                Engine.PlaySound(Engine.LoadSound("Sound\\Attack.Medium.mp3"));
                break;

            case Actions.Attack_low:
                Engine.PlaySound(Engine.LoadSound("Sound\\Attack.Low.mp3"));
                break;
        }
    }
}

