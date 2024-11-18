using System;
using System.Collections.Generic;

class Projectile : Obstacle
{
    protected float dx;
    protected float dy;
    protected float ay;

    public static readonly float DefaultWidth = 64;
    public static readonly float DefaultHeight = 64;

    public Projectile(float x, float y, float width, float height) : base(x, y, width, height, Engine.LoadTexture("Sprites\\DelayedProj.png"))
    {
        Layer = Layers.Obstacle;
        dx = 0;
        dy = 0;
        ay = GameObject.GRAVITY;
    }

    public void SetInitialVelocity(Vector2 velocity)
    {
        this.dx = velocity.X;
        this.dy = velocity.Y;
    }

    public override void Update()
    {
        base.Update();
        dy += ay;

        if (this.WillOverlap(dx, dy, CollisionHandler.GetCollisions(this)))
        {
            dy = 0;
            dx = 0;
        }

        this.MoveY(dy);
        this.MoveX(dx);
    }
}