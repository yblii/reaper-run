using System;
using System.Collections.Generic;
using System.Text;

class ScoreManager
{
    // manages the levels that have been played for score handling (scoreboard and saving)

    private string _name;
    public List<Level> PlayedLevels;
    public Level LastPlayedLevel;

    public ScoreManager()
    {
        _name = "";
        PlayedLevels = new List<Level>();
    }

    public ScoreManager(string name)
    {
        this._name = name;
        PlayedLevels = new List<Level>();
    }
    public string GetName()
    {
        return _name;
    }
    public void ChangeName(string name)
    {
        this._name = name;
    }
    public void AddLevel(Level l)
    {
        PlayedLevels.Add(l);
        LastPlayedLevel = l;
    }
}

