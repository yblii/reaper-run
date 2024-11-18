class AchievementDisplay : UIElement
{
    private Achievement _achievement;
    private ResizableTexture _t;

    public AchievementDisplay(Vector2 pos, Vector2 size, Achievement a) : base(pos, size)
    {
        _achievement = a;
        _t = Engine.LoadResizableTexture("Sprites\\Button.png", 8, 8, 8, 8);
    }

    public override void Render()
    {
        Engine.DrawResizableTexture(_t, new Bounds2(this.position, this.size));
        Engine.DrawString(_achievement.DisplayName, position + new Vector2(15, 0), Color.White, Game.FontMedium);
        Engine.DrawString(_achievement.Description, position + new Vector2(15, 40), Color.White, Game.FontSmall);

        if (!_achievement.Unlocked)
        {
            Engine.DrawRectSolid(new Bounds2(this.position, this.size), new Color(0, 0, 0, 120));
        }
    }
}