using System;
using System.Collections.Generic;
using System.Text;

class Level
{
    // constants
    public int TileWidth = 32;
    public int TileHeight = 32;

    public bool IsComplete = false;
    public bool IsLoaded = false;

    private char[][] _levelLayout = null;
    public String[] _backgrounds { get; }

    public Score score; // exception to naming convention since it can't be Score

    public int LvlNum;

    public Music levelMusic;
    public bool HasScore = true;

    public bool IsSaved;
    public int TotalCoins = 0;
    public bool Attacked = false;
    public bool Damaged = false;

    public Level(string filename, int lvlNum, String musicPath)
    {
        _levelLayout = FileIO.ReadLevel(filename);

        Console.WriteLine(filename);
        _backgrounds = FileIO.GetBackground(filename);

        score = new Score(Game.ScoreManager.GetName());
        this.LvlNum = lvlNum;

        IsSaved = false;
    }
    public void LoadLevel()
    {


        Vector2 portal1 = new Vector2(-1, -1);
        Vector2 portal2 = new Vector2(-1, -1);

        // create tiles
        for (int i = 0; i < _levelLayout.Length; i++)
        {
            for (int j = 0; j < _levelLayout[i].Length; j++)
            {
                char c = _levelLayout[i][j];
                int x = j * TileManager.TileWidth;
                int y = i * TileManager.TileHeight;
                if (c == '#')
                {
                    // draw a stationary platform
                    new Platform(x, y, TileManager.TileWidth, TileManager.TileHeight, Engine.LoadTexture("Sprites\\Terrain\\RegularPlatform.png"));
                }
                else if (c == 'X')
                {
                    // draw a wall
                    new Platform(x, y, TileManager.TileWidth, TileManager.TileHeight, Engine.LoadTexture("Sprites\\Terrain\\RegularPlatform.png"));
                }
                else if (c == '$')
                {
                    // draw a dropping platform
                    new DroppingPlatform(x, y, TileManager.TileWidth, TileManager.TileHeight, Engine.LoadTexture("Sprites\\Terrain\\DroppingPlatform1.png"));
                }
                else if (c == '&')
                {
                    // draw a variable friction platform
                    new VariableFrictionPlatform(x, y, TileManager.TileWidth, TileManager.TileHeight, Engine.LoadTexture("Sprites\\Terrain\\VariableFrictionPlatform1.png"));
                }
                else if (c == 'S')
                {
                    // draw a spirit
                    new Spirit(x, y - TileManager.TileHeight, TileManager.TileWidth, TileManager.TileHeight, Engine.LoadTexture("Sprites\\Spirit.png"), this);
                }
                else if (c == 'L')
                {
                    // draw a ladder
                    new Ladder(x, y, TileManager.TileWidth, TileManager.TileHeight, Engine.LoadTexture("Sprites\\Ladder.png"));
                }
                else if (c == 'W')
                {
                    // draw a wall
                    new ClimbableWall(x, y, TileManager.TileWidth, TileManager.TileHeight, Engine.LoadTexture("Sprites\\Terrain\\ClimbableWall.png"));
                }
                else if (c == 'H')
                {
                    // draw a hellhound
                    new Hellhound(x, y - 96, 128, 96, Engine.LoadTexture("Sprites\\Hellhound.png"));
                }
                else if (c == 'R')
                {
                    new Reaper(x, y - 64, 64, 64, Engine.LoadTexture("Sprites\\Reaper.png"));
                }
                else if (c == 'B')
                {
                    // draw a boss
                    new Death(x, y);
                }
                else if (c == 'Z')
                {
                    // draw laser
                    new Laser(x, y, TileManager.TileWidth, TileManager.TileHeight / 2);
                }
                else if (c == 'F')
                {
                    // draw projectile spawner
                    new ProjectileSpawner(x, y, TileManager.TileWidth * 1.5f, TileManager.TileHeight * 1.5f);
                }
                else if (c == 'D')
                {
                    new DestructableTerrain(x, y, TileManager.TileWidth, TileManager.TileHeight, Engine.LoadTexture("Sprites\\Terrain\\BreakableWall.png"));
                }
                else if (c == 'c')
                {
                    new Coin(x, y);
                    TotalCoins++;
                }
                else if (c == '^')
                {
                    new Spike(x, y);
                }
                else if (c == 'P')
                {
                    portal1.X = x;
                    portal1.Y = y;
                }
                else if (c == 'p')
                {
                    portal2.X = x;
                    portal2.Y = y;
                }
            }
        }
        if (portal1.X != -1 && portal2.X != -1)
        {
            // draw portal
            float dx = portal2.X - portal1.X;
            float dy = portal2.Y - portal1.Y;
            new Portal(portal1.X, portal1.Y - 32, TileManager.TileWidth * 2f, TileManager.TileHeight * 2f, Engine.LoadTexture("Sprites\\Portal.png"), dx, dy);
        }
        IsLoaded = true;
    }

    public void EndGame()
    {
        IsComplete = true;
        // increase score by total health bar or shield left at the end of the level
        float health = Game.StateManager.GetCurrentState().GetPlayer()._health + Game.StateManager.GetCurrentState().GetPlayer()._armor;
        Game.GetCurrentScore().ChangeScore(health);
        Console.WriteLine("health: " + health);
    }
}
