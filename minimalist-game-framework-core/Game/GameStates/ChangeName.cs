using System;
using System.Collections.Generic;
using System.Text;

class ChangeName : IGameState
{
    private string _typed = "";
    private Texture _background;

    public void Update(StateManager manager)
    {
        if (Engine.GetKeyDown(Key.Escape))
        {
            manager.PopState();
        }

        // player is finished typing
        if (Engine.GetKeyDown(Key.Return))
        {
            Game.ScoreManager.ChangeName(_typed);
            manager.PopState();
        }
    }

    public void Draw()
    {
        Engine.DrawTexture(_background, Vector2.Zero, size: Game.Resolution);

        Engine.DrawString("Change Username", new Vector2(Game.Resolution.X / 2, 30), Color.Black, Game.FontLarge, TextAlignment.Center);
        Engine.DrawRectSolid(new Bounds2(new Vector2(30, 100), new Vector2(1020, 500)), new Color(255, 255, 255, 150));

        if (Engine.GetKeyDown(Key.Backspace) && _typed.Length > 0)
        {
            _typed = _typed.Substring(0, _typed.Length - 1);
        }
        if (_typed.Length < 3)
        {
            _typed += Engine.TypedText;
        }
        _typed = _typed.ToUpper();
        Engine.DrawString("Current name: " + Game.ScoreManager.GetName(), new Vector2(400, 300), Color.Black, Game.FontMedium);
        Engine.DrawString("Enter a new name (3 letters only): " + _typed, new Vector2(150, 400), Color.Black, Game.FontMedium);

        Engine.DrawString("ESC to go back", new Vector2(15, 10), Color.Black, Game.FontXSmall);
    }

    void IGameState.Enter(StateManager manager)
    {
        _background = Engine.LoadTexture("Sprites\\Backgrounds\\MenuBG.png");
    }
}

