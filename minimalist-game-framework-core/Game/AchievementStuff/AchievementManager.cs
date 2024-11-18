using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

// To add an achievement, add it first to the AchievementsList.txt file
// Then add it to the AchievementName enum
// Then wherever the achievement is unlocked in code, invoke an event and
// subscribe to the event here.

class AchievementManager
{
    // not thread-safe, singleton pattern
    private static AchievementManager instance = null;
    private List<Achievement> _allAchievements = new List<Achievement> ();

    private AchievementManager()
    {
    }

    public static AchievementManager Instance()
    {
        if (instance == null)
        {
            instance = new AchievementManager();
            instance.ReadAchievements();
        }
        return instance;
    }

    private void ReadAchievements()
    {
        try
        {
            StreamReader sr = new StreamReader("Assets\\AchievementsList.txt");

            while(sr.Peek() != -1)
            {
                String display = sr.ReadLine();
                AchievementName name = (AchievementName)Enum.Parse(typeof(AchievementName), RemoveSpaces(display), true);

                String desc = sr.ReadLine();
                _allAchievements.Add(new Achievement(display, desc, name));

                sr.ReadLine();
            }
        }
        catch(FileNotFoundException)
        {
            Console.WriteLine("file not found");
        }
    }

    public List<Achievement> GetAchievements()
    {
        return _allAchievements;
    }

    public void Subscribe()
    {
        ActiveLevel.GameWon += instance.Unlock;
        Hellhound.HellhoundKilled += instance.Unlock;
        LevelManager.AllCoinsCollected += instance.Unlock;
        LevelManager.Pacifist += instance.Unlock;
        LevelManager.TutorialComplete += instance.Unlock;
    }

    private void Unlock(AchievementName name)
    {
        foreach(Achievement a in _allAchievements)
        {
            if(a.Name.Equals(name))
            {
                a.Unlocked = true;
            }
        }
    }

    private String RemoveSpaces(String s)
    {
        return Regex.Replace(s, @"\s", string.Empty);
    }
}