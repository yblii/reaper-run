using System;
using System.Collections.Generic;
using System.Text;

class LoseScreen : IGameState
{
    private Texture _background;

    private List<Level> _newLevels = new List<Level>();

    public LoseScreen()
    {
        _background = Engine.LoadTexture("Sprites\\Backgrounds\\MenuBG.png");

        // for each level played, write the score to the appropriate scoreboard txt file IF they player is not a guest
        foreach (Level level in Game.ScoreManager.PlayedLevels)
        {
            // write to file
            if (!level.IsSaved)
            {
                // save level to display later
                _newLevels.Add(level);
                Console.WriteLine("NEW LEVEL SAVED");
                Console.WriteLine(_newLevels.Count);

                // mark level as saved
                level.IsSaved = true;

                // if not a guest
                if (!level.score.GetName().Equals(""))
                {
                    // determine which level it is (which files to write to)
                    string fileW = Game.ReadWriteFiles[level.LvlNum - 1];

                    // collect all scores
                    List<Score> scores = new List<Score>();
                    scores.Add(level.score);

                    // write scores to scoreboard
                    FileIO.WriteScoreboard(fileW, scores);
                }
            }
        }
    }

    public void Draw()
    {
        Engine.DrawTexture(_background, Vector2.Zero, size: Game.Resolution);
        Engine.DrawString("Better luck next time!", new Vector2(Game.Resolution.X / 2, 200), Color.Black, Game.FontLarge, TextAlignment.Center);
        Engine.DrawString("Looks like someone is going to be scrubbing the toilets of the underworld...", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y - 80), Color.Black, Game.FontSmall, TextAlignment.Center);
        Engine.DrawString("R to restart", new Vector2(Game.Resolution.X / 2, 600), Color.Black, Game.FontSmall, TextAlignment.Center);

        // for each level played, display the score
        for (int i = 0; i < _newLevels.Count; i++)
        {
            Level level = _newLevels[i];
            Console.WriteLine(level.score.ToString());
            Console.WriteLine(_newLevels.Count);
            // draw the score
            if (level.score.GetName().Equals(""))   // for when the player didn't enter a name
            {
                Engine.DrawString("LEVEL " + level.LvlNum + " " + " Score: " + Math.Round(level.score.GetScore(), 0), new Vector2(Game.Resolution.X / 2, 300 + i * 50), Color.Black, Game.FontMedium, TextAlignment.Center);
            }
            else   // for when the player entered a name
            {
                Engine.DrawString(level.score.GetName() + "'s " + "LEVEL " + level.LvlNum + " " + "Score: " + Math.Round(level.score.GetScore(), 0), new Vector2(Game.Resolution.X / 2, 300 + i * 50), Color.Black, Game.FontMedium, TextAlignment.Center);
            }
        }
    }

    public void Update(StateManager manager)
    {
        // to restart
        if (Engine.GetKeyDown(Key.R))
        {
            Camera.Clear();
            manager.SetBaseState(new StartMenu());
        }
    }

    public void Enter(StateManager manager)
    {
    }
}
