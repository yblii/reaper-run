using System;
using System.Collections.Generic;
using System.Text;

/// SCORING SYSTEM ///
/// - all scores start at 0
/// - 1 point for every coin collected
/// - 1 point for each health bar or shield left at the end of the level
/// - 2 points for every hellhound killed
/// - 5 points for getting the spirit (finishing the level)
/// - 5 points for killing the boss


class Score : IComparable<Score>
{
    public const int COIN = 1;
    public const int HEALTH = 1;
    public const int HELLHOUND = 2;
    public const int SPIRIT = 5;
    public const int BOSS = 5;

    private string _name;
    private double _score;

    public Score(string name)
    {
        _name = name;
        _score = 0;
    }
    public Score(double score)
    {
        _name = "";
        _score = score;
    }
    public Score(string name, double score)
    {
        _name = name;
        _score = score;
    }
    public override string ToString()
    {
        return _name + "   " + Math.Round(_score, 0);
    }
    public string GetName()
    {
        return _name;
    }
    public double GetScore()
    {
        return _score;
    }
    public void ChangeName(string s)
    {
        _name = s;
    }
    public void ChangeScore(double d)
    {
        _score += d;
    }
    public int CompareTo(Score other)
    {
        // if the other score is greater than this one, return 1
        // if the current score is less than the other one, return 1
        if (other.GetScore() > this._score)
        {
            return 1;
        }
        // if the other score is less that this one, return -1
        // if the current score is greater than the other one, return -1
        else if (other.GetScore() < this._score)
        {
            return -1;
        }
        // if the other score is the same as this one, return 0
        return 0;
    }
}
