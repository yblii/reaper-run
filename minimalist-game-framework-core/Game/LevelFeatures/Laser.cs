using System;
using System.Collections.Generic;
using System.Text;

internal class Laser : Obstacle
{
    // currently only goes up and down

    private float _startY;
    private float _endY;

    private bool _goingUp;

    public Laser(float x, float y, float width, float height) : base(x, y, width, height, Engine.LoadTexture("Sprites\\Laser.png"))
    {
        attack = 20;
        _startY = y;
        _endY = y - 200;

        _goingUp = true;
    }

    public override void Update()
    {
        if(_goingUp)
        {
            MoveY(-2);
            if(this.Position.Y <= _endY)
            {
                _goingUp = false;
            }
        } 
        else
        {
            MoveY(2);
            if (this.Position.Y >= _startY)
            {
                _goingUp = true;
            }
        }

        base.Update();
    }
}
