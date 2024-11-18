using System;

class ActiveLevel : IGameState
{
    private Player _player;
    public LevelManager _lvlManager;

    private bool _isScrolling = true;
    private bool _debugMode = false;

    public static bool paused = false;
    private static float _hitlagTime = 0;
    private bool _changingLevel = false;

    public static event Action<AchievementName> GameWon = delegate { };

    public TextBox ScoreText;

    public ActiveLevel(string name, int level)
    {
        _lvlManager = new LevelManager(this);
        _lvlManager.SelectLevel(level);
    }

    public void Enter(StateManager manager)
    {
        Camera.InitializeGameObjects();
        manager.PushState(new LoadingScreen(this));
    }

    public void InitiateLevel()
    {
        _lvlManager.LevelSetUp();
        CollisionHandler.GenerateGrid();
        AchievementManager.Instance().Subscribe();
        _isScrolling = false;

        Camera.InitializeGameObjects();
        _player = new Player(50, 50, 48, 96, Engine.LoadTexture("Sprites\\Player\\Player.png"));
        _player.SetHealthBar(new Vector2(25, 25), new Vector2(441, 69), "Sprites\\Player\\HealthBar", new Bounds2(320, 80, 2030, 160));
        _player.SetShieldBar(new Vector2(25, 25), new Vector2(441, 69), "Sprites\\Player\\ShieldBar", new Bounds2(384, 240, 912, 96));

        _lvlManager.LoadLevel(_player);
        _lvlManager.UpdateBackground();

        ScoreText = new TextBox("SCORE:   0", Game.Resolution.X - 20, 5, 10, 10, Game.FontSmall, Color.Black, true, TextAlignment.Right);

        Camera.DrawAll();
    }

    public void Draw()
    {
        Camera.DrawAll();

        if(_debugMode)
        {
            DebugDraw.DrawAll();
        }

        if (Engine.GetKeyDown(Key.M))
        {
            Engine.StopMusic();
        }

        // draw score
        string score = Game.GetCurrentLevel().score.GetScore().ToString();
        ScoreText.ChangeText("SCORE:   " + score);
    }

    public void Update(StateManager manager)
    {
        if(_changingLevel)
        {
            manager.PushState(new LoadingScreen(this));
            _changingLevel = false;
        }

        DebugCheck();
        ScrollLogic();

        // hitlag stuff
        if (_hitlagTime > 0)
        {
            paused = true;
            _hitlagTime -= Engine.TimeDelta;
        }
        else
        {
            paused = false;
        }

        // pause
        if(!paused)
        {
            Camera.UpdateAll();
        }

        _lvlManager.LoadLevel(_player);
        _lvlManager.UpdateBackground();

        // pause game
        if(Engine.GetKeyDown(Key.Escape))
        {
            manager.PushState(new PauseMenu(this));
        }

        WinLoseConditions(manager);
    }

    public  void ChangeLevel()
    {
        _changingLevel = true;
    }

    public static void Hitlag()
    {
        _hitlagTime = 0.1f;
    }

    public Player GetPlayer()
    {
        return _player;
    }

    private void DebugCheck()
    {
        // toggle debug draw
        if (Engine.GetKeyDown(Key.D))
        {
            _debugMode = !_debugMode;
        }

        // toggle scroll
        if (Engine.GetKeyDown(Key.S))
        {
            _isScrolling = !_isScrolling;
        }
    }

    private void WinLoseConditions(StateManager manager)
    {
        // win-lose states
        if (_player.GetBounds().Position.Y > Game.Resolution.Y)
        {
            Game.GetCurrentLevel().EndGame();
            manager.PushState(new LoseScreen());
        }

        if (_lvlManager.GameWon)
        {
            Game.GetCurrentLevel().EndGame();
            manager.PushState(new WinScreen());
            GameWon?.Invoke(AchievementName.OfficiallyCertified);
        }
    }

    private void ScrollLogic()
    {
        // scrolling
        if (_isScrolling)
        {
            Camera.Scroll(-1);
        }
        else
        {
            Camera.Scroll(0);
        }

        // start scroll when player moves
        if (!_isScrolling && (Engine.GetKeyDown(Key.Right) || Engine.GetKeyDown(Key.Left)))
        {
            _isScrolling = true;
        }
    }
}
