using System;
using System.Collections.Generic;
using System.Text;

class Scoreboard : IGameState
{
    private List<Score> _level1;
    private List<Score> _level2;
    private List<Score> _level3;
    
    public Scoreboard()
    {
        // get scores for level 1
        _level1 = FileIO.ReadScoreboard(Game.ReadWriteFiles[0]);

        // get scores for level 2
        _level2 = FileIO.ReadScoreboard(Game.ReadWriteFiles[1]);

        // get scores for level 3
        _level3 = FileIO.ReadScoreboard(Game.ReadWriteFiles[2]);
    }

    public void Update(StateManager manager)
    {
        // to go back
        if (Engine.GetKeyDown(Key.Escape) || Engine.GetKeyDown(Key.Backspace))
        {
            manager.PopState();
        }
    }

    public void Draw()
    {
        Texture t = Engine.LoadTexture("Sprites\\Backgrounds\\ScoreboardBackground.png");
        Engine.DrawTexture(t, Vector2.Zero);
        Engine.DrawString("Scoreboard", new Vector2(Game.Resolution.X / 2, 30), Color.Black, Game.FontLarge, TextAlignment.Center);
        Bounds2 rectBounds = new Bounds2(new Vector2(30, 100), new Vector2(1020, 500));
        Engine.DrawRectSolid(rectBounds, new Color(255, 255, 255, 150));

        // display level 1 scores
        Engine.DrawString("Level 1", new Vector2(rectBounds.Size.X / 4 + rectBounds.Position.X, 200), Color.Black, Game.FontLarge, TextAlignment.Center);
        int i = 0;
        foreach (Score s in _level1)
        {
            Engine.DrawString(s.ToString(), new Vector2(rectBounds.Size.X / 4 + rectBounds.Position.X, 250 + 30 * i), Color.Black, Game.FontMedium, TextAlignment.Center);
            i++;
        }

        // display level 2 scores
        Engine.DrawString("Level 2", new Vector2(Game.Resolution.X / 2, 200), Color.Black, Game.FontLarge, TextAlignment.Center);
        i = 0;
        foreach (Score s in _level2)
        {
            Engine.DrawString(s.ToString(), new Vector2(Game.Resolution.X / 2, 250 + 30 * i), Color.Black, Game.FontMedium, TextAlignment.Center);
            i++;
        }

        // display level 3 scores
        Engine.DrawString("Level 3", new Vector2(rectBounds.Size.X * 3 / 4 + rectBounds.Position.X, 200), Color.Black, Game.FontLarge, TextAlignment.Center);
        i = 0;
        foreach (Score s in _level3)
        {
            Engine.DrawString(s.ToString(), new Vector2(rectBounds.Size.X * 3 / 4 + rectBounds.Position.X, 250 + 30 * i), Color.Black, Game.FontMedium, TextAlignment.Center);
            i++;
        }

        Engine.DrawString("ESC to go back", new Vector2(15, 10), Color.Black, Game.FontXSmall);
    }

    void IGameState.Enter(StateManager manager)
    {
        
    }
}

