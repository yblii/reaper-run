using System;
using System.Collections.Generic;
using System.Text;

internal class Status
{
    public float duration;

    public Status(GameObject o)
    {
        this.apply(o);
    }

    public virtual void apply(GameObject o)
    {

    }

    public virtual void cleanse(GameObject o)
    {

    }

    public static void UpdateStatus(List<Status> status, GameObject o)
    {
        List<Status> temp = new List<Status>(status);
        foreach(Status s in temp)
        {
            s.duration -= Engine.TimeDelta;
            if(s.duration <= 0)
            {
                s.cleanse(o);
                status.Remove(s);
            }
        }
    }
}