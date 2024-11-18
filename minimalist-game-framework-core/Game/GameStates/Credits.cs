using System;
using System.Collections.Generic;
using System.Text;

class Credits : IGameState
{
    private Texture _background;

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
        Engine.DrawTexture(_background, Vector2.Zero, size: Game.Resolution);

        string[] credits = {    "Designed by The Pookie Bears TM",
                                "Copyright 2024   Aylin Ozdemir, Brenda Li, Connor Nguyen, Vasudha Narayanan",
                                "Special thanks to Ms. Kankelborg and our mentor Andrew Farrier",
                                "Thank you to other mentors: Scott Matloff, Madelyn Thornberry, Jeffrey Tippet, Andrew Martz",
                                "Music from: HeatleyBros - Royalty Free Video Game Music",
                                "Font Pixel UniCode by IviLand" };

        string title = "Credits";
        Engine.DrawString(title, new Vector2(Game.Resolution.X / 2, 30), Color.Black, Game.FontLarge, TextAlignment.Center);
        for (int i = 0; i < credits.Length; i++)
        {
            Engine.DrawString(credits[i], new Vector2(Game.Resolution.X / 2, 100 + 50 * i), Color.Black, Game.FontSmall, TextAlignment.Center);
        }

        Engine.DrawString("ESC to go back", new Vector2(15, 10), Color.Black, Game.FontXSmall);
    }

    void IGameState.Enter(StateManager manager)
    {
        _background = Engine.LoadTexture("Sprites\\Backgrounds\\MenuBG.png");
    }
}
