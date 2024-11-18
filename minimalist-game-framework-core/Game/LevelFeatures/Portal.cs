using System;
using System.Collections.Generic;
using System.Text;

class Portal : GameObject
{
    private float _moveX;
    private float _moveY;

    private Animator _anim;
    public Portal(float x, float y, float xs, float ys, Texture t, float moveX, float moveY) : base(x, y, xs, ys, t, Layers.Portal)
    {
        Layer = Layers.Portal;
        Collidable = false;
        this._moveX = moveX;
        this._moveY = moveY;

        _anim = new Animator(this.sprite, "Assets\\AnimationData\\PortalAnimData.txt");
        _anim.ChangeAction(Actions.Idle);
    }

    public override void Update()
    {
        _anim.Update();
        if (this.GetBounds().OverlapsT(Player.player.GetBounds()))
        {
            Player.player.MoveX(_moveX);
            Player.player.MoveY(_moveY);
            Console.WriteLine("dx: " + _moveX + "dy: " + _moveY);
            Player.player._dY = 0;
        }
    }
}
