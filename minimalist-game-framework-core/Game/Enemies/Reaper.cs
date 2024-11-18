using System;
using System.Collections.Generic;
using System.Text;

class Reaper : GameObject
{
    public bool _overlaps = false;
    private bool _isFacingRight = true;
    public Reaper(float x, float y, float xs, float ys, Texture t) : base(x, y, xs, ys, t, Layers.Enemy)
    {
        Layer = Layers.Enemy;
        Collidable = true;
        this._dY = 0;
        this._dX = 1f;
    }

    public override void Update()
    {
        _dY += _aY;

        DebugDraw.Add(this.GetBounds());

        //float check = 0;

        /*
        if (_dX > 0)
        {
            check = TileManager.TileWidth;
        }
        else
        {
            check = -1f * TileManager.TileWidth;
        }
        */


        if (this.NewBounds(0, -1).Overlaps(Player.player.GetBounds())
                || this.NewBounds(_dX,0).Overlaps(Player.player.GetBounds())
                )
        {
            if (!Player.player.IsInvulnerable())
            {
                Player.player.Damage(1);
                Player.player.SetInvulnerable(.5f);
                Player.player._dY = -10;
            }
            _overlaps = true;
        }
        else
        {
            _overlaps = false;
        }

        float direction = _isFacingRight ? 1 : -1;

        if (WillOverlap((0.5f + this.Scale.X) * direction, this.Scale.Y, CollisionHandler.GetCollisions(this)))
        {
            _dX = 1f * direction;
        }
        else
        {
            _isFacingRight = !_isFacingRight;
        }

        if(_isFacingRight)
        {
            this.sprite.SetDirection(TextureMirror.None);
        } 
        else
        {
            this.sprite.SetDirection(TextureMirror.Horizontal);
        }

        this.MoveX(_dX);

        if (this.WillOverlap(0, _dY, CollisionHandler.GetCollisions(this)))
        {
            _dY = 0;
        }

        if(this.Overlaps(CollisionHandler.GetCollisions(this))) 
        {
            this.MoveY(this.UnclipVertical());
        }

        this.MoveY(_dY);
    }
}