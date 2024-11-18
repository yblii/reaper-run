using System.Collections.Generic;

class AchievementMenu : IGameState
{
    private Vector2 size;
    private Vector2 padding;
    private Texture _background;

    private List<AchievementDisplay> achievements = new List<AchievementDisplay>();

    public void Enter(StateManager manager)
    {
        Camera.InitializeGameObjects();
        padding = new Vector2(10, 10);
        size = new Vector2(Game.Resolution.X - padding.X * 2, 80);

        for (int i = 0; i < AchievementManager.Instance().GetAchievements().Count; i++)
        {
            Vector2 pos = new Vector2(padding.X, (size.Y + padding.Y) * i + padding.Y);
            new AchievementDisplay(pos, size, AchievementManager.Instance().GetAchievements()[i]);
        }
        _background = Engine.LoadTexture("Sprites\\Backgrounds\\MenuBG.png");

        Engine.DrawString("ESC to go back", new Vector2(15, 10), Color.Black, Game.FontXSmall);
    }

    public void Draw()
    {
        Engine.DrawTexture(_background, Vector2.Zero, size: Game.Resolution);

        Camera.DrawUI();
    }

    public void Update(StateManager manager)
    {
        if (Engine.GetKeyDown(Key.Escape))
        {
            Camera.ClearUI();
            manager.PopState();
        }
    }
}