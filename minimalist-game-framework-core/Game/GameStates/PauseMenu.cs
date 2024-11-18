class PauseMenu : IGameState
{
    public IGameState LowerScreen { get; }
    public void Draw()
    {
        LowerScreen.Draw();
        Engine.DrawRectSolid(new Bounds2(100, 100, 900, 500), Color.AliceBlue);
    }

    public PauseMenu(IGameState g)
    {
        LowerScreen = g;
    }

    public void Enter(StateManager manager)
    {
    }

    public void Update(StateManager manager)
    {
        if(Engine.GetKeyDown(Key.Escape))
        {
            manager.PopState();
        }
    }
}