using System;
using System.Collections.Generic;

internal class Fireball : Projectile
{
    private Animator _anim;

    public Fireball(float x, float y) : base(x, y, 1.5f * TileManager.TileWidth, 1.5f * TileManager.TileHeight)
    {
        Layer = Layers.Obstacle;
        Collidable = false;
        dx = -6;
        dy = 0;
        ay = 0;

        _anim = new Animator(this.sprite, "Assets\\AnimationData\\ProjAnimData.txt");
        this.sprite._rotation = 90;
    }

    public override void Update()
    {
        _anim.Update();
        
        if (this.GetBounds().Overlaps(Player.player.GetBounds()))
        {
            dx = 0;
            _anim.ChangeAction(Actions.Attack);

            if (_anim.AnimationComplete())
            {
                Player.player.Damage(1);
                this.sprite.SetSource(new Bounds2(Vector2.Zero, Vector2.Zero));
                Destroy();
                GameObject.toDestroy.Add(this);
            }
        }

        this.MoveX(dx);
        _anim.ChangeAction(Actions.Idle);
    }
}