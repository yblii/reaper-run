using System;
using System.Collections.Generic;
using System.Text;

class Spirit : GameObject
{
    private Level _curr;
    private Animator _anim;
    public Spirit(float x, float y, float xs, float ys, Texture t, Level level) : base(x, y, xs, ys, t, Layers.Spirit)
    {
        Layer = Layers.Spirit;
        _curr = level;
        Collidable = false;

        _anim = new Animator(this.sprite, "Assets\\AnimationData\\SpiritAnimData.txt");
        _anim.ChangeAction(Actions.Idle);
    }

    public override void Update()
    {
        _anim.Update();
        if(this.GetBounds().OverlapsT(Player.player.GetBounds()) && !_curr.IsComplete)
        {
            // increase score by 5 points (winning the game prize)
            Game.GetCurrentScore().ChangeScore(Score.SPIRIT);

            _curr.IsComplete = true;
        }
    }
}
