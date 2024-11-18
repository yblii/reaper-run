using System;
using System.Collections.Generic;

class Game
{
    // display information
    public static readonly string Title = "Reaper Run";
    public static readonly Vector2 Resolution = new Vector2(1088, 640);

    public static bool GameLost = false;
    public static bool GameWon = false;

    public static StateManager StateManager;
    public static ScoreManager ScoreManager;

    // fonts
    private static readonly string font = "Pixel-UniCode.ttf";
    public static readonly Font FontTiny = Engine.LoadFont(font, 11);
    public static readonly Font FontXSmall = Engine.LoadFont(font, 25);
    public static readonly Font FontSmall = Engine.LoadFont(font, 32);
    public static readonly Font FontMedium = Engine.LoadFont(font, 48);
    public static readonly Font FontLarge = Engine.LoadFont(font, 64);
    public static readonly Font FontXLarge = Engine.LoadFont(font, 96);

    // files
    public static readonly string[] ReadWriteFiles = { "Assets\\LevelData\\Level1Scoreboard.txt", "Assets\\LevelData\\Level2Scoreboard.txt", "Assets\\LevelData\\Level3Scoreboard.txt" };

    public Game()
    {
        Camera.InitializeGameObjects();
        StateManager = new StateManager();
        StateManager.PushState(new StartMenu());
        ScoreManager = new ScoreManager();

        /*
         * “8 Bit Halloween - Dark Spooky Game Music By HeatleyBros”
            HeatleyBros - Royalty Free Video Game Music
            https://www.youtube.com/watch?v=Z5P8Fm9dcp4

        */
        Engine.PlayMusic(Engine.LoadMusic("Sound\\GameMusic.mp3"), true);
    }

    // UPDATE LOOP!
    public void Update()
    {
        StateManager.Update();
    }

    public static Level GetCurrentLevel()
    {
       return ScoreManager.LastPlayedLevel;
    }

    public static Score GetCurrentScore()
    {
        return GetCurrentLevel().score;
    }
}
