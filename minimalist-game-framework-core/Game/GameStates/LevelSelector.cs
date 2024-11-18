using System;
using System.Collections.Generic;
using System.Text;

class LevelSelector : IGameState
{
    private List<Button> _buttons = new List<Button>();
    private Texture _background;

    public void Update(StateManager manager)
    {
        // go back
        if (Engine.GetKeyDown(Key.Escape) || Engine.GetKeyDown(Key.Backspace))
        {
            manager.PopState();
        }

        foreach(Button b in _buttons)
        {
            b.Update(Engine.MousePosition);

            if(Engine.GetMouseButtonDown(MouseButton.Left))
            {
                if(b.MouseOverlap(Engine.MousePosition))
                {
                    manager.PushState(b._state);
                    break;
                }
            }
        }
    }
    public void Draw()
    {
        Engine.DrawTexture(_background, Vector2.Zero, size: Game.Resolution);

        Engine.DrawRectSolid(new Bounds2(new Vector2(350, 50), new Vector2(400, 50)), new Color(112, 130, 153));
        Engine.DrawString("Level Select", new Vector2(Game.Resolution.X / 2, 40), Color.Black, Game.FontLarge, alignment: TextAlignment.Center);
        Engine.DrawString("ESC to go back", new Vector2(15, 10), Color.Black, Game.FontXSmall);

        Camera.DrawUI();
    }
    public void Enter(StateManager manager)
    {
        _background = Engine.LoadTexture("Sprites\\Backgrounds\\MenuBG.png");
        Camera.InitializeGameObjects();

        _buttons.Add(new Button(170, 275, 150, 100, "Tutorial", new ActiveLevel(Game.ScoreManager.GetName(), 0)));
        _buttons.Add(new Button(370, 275, 150, 100, "Level 1", new ActiveLevel(Game.ScoreManager.GetName(), 1)));
        _buttons.Add(new Button(570, 275, 150, 100, "Level 2", new ActiveLevel(Game.ScoreManager.GetName(), 2)));
        _buttons.Add(new Button(770, 275, 150, 100, "Level 3", new ActiveLevel(Game.ScoreManager.GetName(), 3)));
    }
}
