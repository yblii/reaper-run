using System;

internal class SuperJump : Status
{
    public SuperJump(GameObject o) : base(o)
    {
        duration = 5;
    }

    public override void apply(GameObject o)
    {
        try
        {
            ((Player)o).jumpHeight = -13;
        }
        catch(Exception e)
        {

        }
    }

    public override void cleanse(GameObject o)
    {
        try
        {
            ((Player)o).jumpHeight = -9;
        }
        catch (Exception e)
        {

        }
    }
}