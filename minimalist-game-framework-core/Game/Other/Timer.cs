using System;
using System.Collections.Generic;
using System.Text;

class Timer
{
    private double _time;

    public Timer()
    {
        _time = 0;
    }

    public double GetTime()
    {
        _time += Engine.TimeDelta;
        return _time;
    }

    public void Reset()
    {
        _time = 0;
    }
}