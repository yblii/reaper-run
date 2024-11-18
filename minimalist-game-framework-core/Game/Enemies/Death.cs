using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

internal class Death : Alive
{
    private float BossActionCD = 3;

    private float ActionCD = 5;
    private float CurActionDuration = 0;
    private float timer = 0;
    private Random random = new Random();

    private Vector2 CurPos;
    private Vector2 NextPos;

    private BossAction _currentState;
    private Animator _anim;

    public Death(float x, float y) : 
        base(x, y, TileManager.TileWidth * 8, TileManager.TileHeight * 8, 
            Engine.LoadTexture("Sprites\\Death.png"), 3, 0, 0, Layers.Player)
    {
        Collidable = true;
        this._dX = 0;
        this._dY = 1f;
        this.IsStatic = true;

        /*
         * “Among Us (Kill) - Sound Effect (HD)”
            Gaming Sound FX
            https://www.youtube.com/watch?v=34ewE9biJRE

        */
        this.OnDeath = Engine.LoadSound("Sound\\Death.Death.mp3");


        _currentState = BossAction.Idle;
        this.invulnerable = true;

        _anim = new Animator(this.sprite, "Assets\\AnimationData\\DeathAnimData.txt");
        _anim.ChangeAction(Actions.Idle);
    }

    public override void DrawObj()
    {
        Engine.DrawRectEmpty(this.GetBounds(), Color.Black);
        base.DrawObj();
    }

    public override void Kill()
    {
        Game.GetCurrentScore().ChangeScore(Score.BOSS);
        Game.StateManager.PushState(new WinScreen());
    }

    public override void Update()
    {
        _anim.Update();
        _currentState = GenerateAction();

        switch (_currentState)
        {
            case BossAction.Idle:
                Idle();
                break;

            case BossAction.Charge:
                Charge();
                break;

            case BossAction.Shoot:
                Shoot();
                break;

            case BossAction.Recover:
                Recover();
                break;                
        }

        if(CurActionDuration > 0)
        {
            CurActionDuration -= Engine.TimeDelta;
        }
        else
        {
            CurActionDuration = 0;
        }

        if (CurActionDuration <= 0 && _currentState != BossAction.Idle 
            && _currentState != BossAction.Recover && _currentState != BossAction.Charge)
        {
            CurActionDuration = actionDuration[(int)BossAction.Charge];
            _currentState = BossAction.Charge;
        }

        CheckPlayer();
        base.Update();
    }

    public void Shoot()
    {
        if (timer <= 0.33f)
        {
            timer = 1;
            this.Position = new Vector2(this.Position.X, 
                                random.Next((int) Game.Resolution.Y - (int) this.Scale.Y + 1));
            new Fireball(this.Position.X - 74, this.Position.Y + (this.Scale.Y) / 2);
        }
        else
        {
            timer -= Engine.TimeDelta;
        }
    }

    public override void Damage(float damage)
    {
        invulnerable = true;
        base.Damage(1);

        NextPos = new Vector2(Game.Resolution.X * 3 / 4 , Game.Resolution.Y / 2 - this.Scale.Y / 2);
        CurPos = this.Position;
        CurActionDuration = actionDuration[(int)BossAction.Recover];
        _currentState = BossAction.Recover;
    }

    public void Charge()
    {
        if (CurActionDuration >= 2)
        {
            CurPos = this.Position;
            NextPos = Player.player.Position;
        }
        else if (CurActionDuration > 0.02)
        {
            invulnerable = false;

            this.floatBoss(NextPos.X + (Player.player.Scale.X / 2), NextPos.Y + (Player.player.Scale.Y / 2)
                            - (this.Scale.Y / 2), 2);

            if (this.CheckPlayer())
            {
                this.Position = CurPos;
                CurActionDuration = 0;
            }
        }
        else
        {
            this.Position = CurPos;
        }
    }

    public void floatBoss(float x, float y, float time)
    {
        this.MoveX(Engine.TimeDelta * (x - CurPos.X) / time);
        this.MoveY(Engine.TimeDelta * (y - CurPos.Y) / time);
    }

    public void Recover()
    {
        floatBoss(NextPos.X, NextPos.Y, actionDuration[(int) BossAction.Recover]);
    }

    public bool CheckPlayer()
    {
        if (this.GetBounds().Overlaps(Player.player.GetBounds()))
        {
            Player.player.Damage(1);
            Player.player.MoveX(-50);
            Player.player._dY = -15;
            return true;
        }
        return false;
    }

    public void Idle()
    {
        Move();
    }

    public void Move()
    {
        if (this.Position.Y >= 640 - this.Scale.Y || this.Position.Y <= 0)
        {
            this._dY *= -1;
        }
        this.MoveY(_dY);
    }

    enum BossAction
    {
        Idle = 0,
        Recover = 1,
        Charge = 2,
        Shoot = 3,
    }

    public static int[] actionDuration = {0, 1, 3, 3};

    private BossAction GenerateAction()
    {
        if (_currentState.Equals(BossAction.Idle))
        {
            ActionCD -= Engine.TimeDelta;
        }
        
        if(ActionCD <= 0)
        {
            Array actions = Enum.GetValues(typeof(BossAction));
            Console.WriteLine(actions.Length);
            ActionCD = BossActionCD;

            int i = 3 + random.Next(actions.Length - 3);
            BossAction action = (BossAction) actions.GetValue(i);


            CurActionDuration = actionDuration[(int) action];
            return action;
        }

        if(!(CurActionDuration > 0))
        {
            return BossAction.Idle;
        }
        else
        {
            return _currentState;
        }
    }
}