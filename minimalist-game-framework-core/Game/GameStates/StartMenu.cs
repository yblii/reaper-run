using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

class StartMenu : IGameState
{
    private List<Button> _buttons = new List<Button>();
    private Texture _background;

    private Sprite _sprite;
    private Animator _anim;
    public StartMenu()
	{
        _background = Engine.LoadTexture("Sprites\\MainMenu.png");
    }

	public void Update(StateManager manager)
    {
        foreach(Button b in _buttons)
        {
            b.Update(Engine.MousePosition);

            if(Engine.GetMouseButtonDown(MouseButton.Left))
            {
                if(b.MouseOverlap(Engine.MousePosition))
                {
                    manager.PushState(b._state);
                }
            }
        }
    }

    public void Draw()
    {
        // draw background
        Engine.DrawTexture(_background, Vector2.Zero, size: Game.Resolution);
        _anim.ChangeAction(Actions.Idle);

        _anim.Update();
        _sprite.Render();

        // draw current name
        //string displayName;
        if (Game.ScoreManager.GetName().Equals(""))
        {
            //displayName = "You are currently playing as a guest. Change your name to save your score.";
            Engine.DrawString("guest", _sprite.GetCenter() - new Vector2(24, 85), alignment: TextAlignment.Center, font: Game.FontSmall, color:Color.White);
        } 
        else
        {
            //displayName = "You are currently playing as " + Game.ScoreManager.GetName();
            Engine.DrawString(Game.ScoreManager.GetName(), _sprite.GetCenter() - new Vector2(24, 85), alignment: TextAlignment.Center, font: Game.FontSmall, color: Color.White);
        }
        
        //Engine.DrawString(displayName, new Vector2(60, 550), Color.White, Game.FontSmall);
        Camera.DrawUI();
    }

    public void Enter(StateManager manager)
    {
        Camera.InitializeGameObjects();
        _buttons.Clear();

        // fake player set up
        _sprite = new Sprite(Engine.LoadTexture("Sprites\\Player\\Player.png"));
        _sprite.SetPosition(new Vector2(800, 495));
        _anim = new Animator(_sprite, "Assets\\AnimationData\\PlayerAnimData.txt");
        _sprite.SetSize(new Vector2(48 * 3, 96));

        // button setup
        int bWidth = 180;
        int bHeight = 40;
        int startY = 320;
        float xPos = Game.Resolution.X / 2 - bWidth / 2;

        _buttons.Add(new Button(xPos, _buttons.Count * 50 + startY, bWidth, bHeight, "Start", new LevelSelector()));
        _buttons.Add(new Button(xPos, _buttons.Count * 50 + startY, bWidth, bHeight, "Rules", new Rules()));
        _buttons.Add(new Button(xPos, _buttons.Count * 50 + startY, bWidth, bHeight, "Credits", new Credits()));
        _buttons.Add(new Button(xPos, _buttons.Count * 50 + startY, bWidth, bHeight, "Scoreboard", new Scoreboard()));
        _buttons.Add(new Button(xPos, _buttons.Count * 50 + startY, bWidth, bHeight, "Change Name", new ChangeName()));

        Button a = new Button(30, 520, 64, 64, "", new AchievementMenu());
        a.SetImage(Engine.LoadTexture("Sprites\\AchievementIcon.png"));
        _buttons.Add(a);
    }
}
