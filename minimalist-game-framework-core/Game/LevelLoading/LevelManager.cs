using System;
using System.Collections.Generic;
using System.Text;

class LevelManager
{
    public static Level CurrLevel;
    private BackgroundHandler _bgHandler;
    public int _currLevelNum { get; set; }

    private Level[] _levels = new Level[4];
    public bool GameWon { get; set; }

    public Timer timer; // exception to naming convention since it can't be Timer
    private ActiveLevel _currState;

    public static event Action<AchievementName> AllCoinsCollected = delegate { };
    public static event Action<AchievementName> Pacifist = delegate { };
    public static event Action<AchievementName> TutorialComplete = delegate { };
    public LevelManager(ActiveLevel currState)
    {
        _currState = currState;
    }
    public void UpdateBackground()
    {
        _bgHandler.Update();
    }

    public void LevelSetUp()
    {
        _levels[0] = new Level("Assets\\LevelData\\TestLevel.txt", 0, "Sound\\audio.mp3");  // tutorial
        _levels[1] = new Level("Assets\\LevelData\\Level1.txt", 1, "Sound\\audio.mp3");     // level 1
        _levels[2] = new Level("Assets\\LevelData\\Level2.txt", 2, "Sound\\audio.mp3");     // level 2
        _levels[3] = new Level("Assets\\LevelData\\BossLevel.txt", 3, "Sound\\audio.mp3");  // level 3 (boss)
        timer = new Timer();
    }

    public void SelectLevel(int l)
    {
        CurrLevel = _levels[l];
        _currLevelNum = l;
    }

    public void LoadLevel(Player player)
    {
        if (!GameWon)
        {
            CurrLevel = _levels[_currLevelNum];
            // loading the next level
            if (CurrLevel.IsComplete)
            {
                Engine.DrawRectSolid(new Bounds2(Vector2.Zero, Game.Resolution), new Color(23, 23, 31));

                if (Coin.collected == CurrLevel.TotalCoins && _currLevelNum != 0)
                {
                    AllCoinsCollected?.Invoke(AchievementName.Greedy);
                }

                if (!CurrLevel.Attacked && _currLevelNum != 0)
                {
                    AllCoinsCollected?.Invoke(AchievementName.Pacifist);
                }

                if (_currLevelNum == 0)
                {
                    TutorialComplete?.Invoke(AchievementName.Rookie);
                }

                _currLevelNum++;
                Camera.Clear();
                player.Reset();
                timer.Reset();

                Camera.InitializeGameObjects();
                CollisionHandler.Add(player);
                Camera.AddGameObject(player);

                player.SetHealthBar(new Vector2(25, 25), new Vector2(441, 69), "Sprites\\Player\\HealthBar", new Bounds2(320, 80, 2030, 160));
                player.SetShieldBar(new Vector2(25, 25), new Vector2(441, 69), "Sprites\\Player\\ShieldBar", new Bounds2(384, 240, 912, 96));

                _currState.ChangeLevel();
                Coin.collected = 0;
            }

            // loading the current level
            if (!CurrLevel.IsLoaded)
            {
                CurrLevel.LoadLevel();
                Game.ScoreManager.AddLevel(CurrLevel);
                Console.WriteLine(CurrLevel._backgrounds.ToString());
                _bgHandler = new BackgroundHandler(CurrLevel._backgrounds);
              
                // dialogue/text boxes for tutorial level
                if (_currLevelNum == 0) // tutorial level
                {
                    new TextBox("Press the up arrow to jump", Game.Resolution.X / 7, Game.Resolution.Y / 3, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("Press r and l arrows to move.", Game.Resolution.X / 7, Game.Resolution.Y / 2, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("This platform will break if you stand on it.", Game.Resolution.X / 2, Game.Resolution.Y / 2, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("This platform is slippery.", Game.Resolution.X / 1, Game.Resolution.Y / 2, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("Press Z, X, C, to attack this hellhound.", Game.Resolution.X / (float) 0.7, Game.Resolution.Y / 2, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("Press the up arrow to climb the ladder.", Game.Resolution.X / (float) 0.6, Game.Resolution.Y / 4, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("Avoid the reaper!", Game.Resolution.X / (float) 0.5, Game.Resolution.Y / 3, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("Press right arrow and up arrow to climb wall.", Game.Resolution.X / (float) 0.45, Game.Resolution.Y / 5, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("Touch the portal to teleport.", Game.Resolution.X / (float)0.38, Game.Resolution.Y / 4, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("Avoid the lasers!", Game.Resolution.X / (float)0.33, Game.Resolution.Y / 5, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("Collect coins to gain points.", Game.Resolution.X / (float)0.30, Game.Resolution.Y / 5, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("Press Z, X, or C to destroy this wall.", Game.Resolution.X / (float)0.28, Game.Resolution.Y / 2, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("You can't climb this wall.", Game.Resolution.X / (float)0.25, Game.Resolution.Y / 2, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("Don't touch the spikes.", Game.Resolution.X / (float)0.23, Game.Resolution.Y / 2, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("Watch out for falling projectiles!", Game.Resolution.X / (float)0.21, Game.Resolution.Y / 2, 10, 10, Game.FontSmall, Color.Black, false);
                    new TextBox("Touch the spirit to win.", Game.Resolution.X / (float)0.19, Game.Resolution.Y / 2, 10, 10, Game.FontSmall, Color.Black, false);
                }

                // if the game has been won
                if (_currLevelNum >= _levels.Length)
                {
                    GameWon = true;
                }
            }
        }
    }
}
