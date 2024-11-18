using System;

class LoadingScreen : IGameState
{
    float _opacity;
    ActiveLevel _level;

    float _duration;
    float _timer;
    float _durationT;
    public LoadingScreen(ActiveLevel lvl)
    {
        this._level = lvl;
        _opacity = 255;
        _duration = 10f;
        _durationT = 1.5f;

        _timer = 0;
    }

    public void Draw()
    {
        Engine.DrawRectSolid(new Bounds2(Vector2.Zero, Game.Resolution), new Color(23, 23, 31, (byte) _opacity));

        if(_opacity > 100)
        {
            if(_level._lvlManager._currLevelNum == 0)
            {
                Engine.DrawString("Tutorial", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / 2 - 50), Color.White, Game.FontXLarge, TextAlignment.Center);
            }
            else if (_level._lvlManager._currLevelNum == 1)
            {
                Engine.DrawString("Level " + _level._lvlManager._currLevelNum, new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / 3 - 50), Color.White, Game.FontXLarge, TextAlignment.Center);
                Engine.DrawString("It's your first day on the job at Reaper Inc. Your boss gave you one simple task; transport some spirits.", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / 2), Color.White, Game.FontSmall, TextAlignment.Center);
                Engine.DrawString("Unfortunately, you happen to be a pretty clumsy reaper.", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / (float) 1.8), Color.White, Game.FontSmall, TextAlignment.Center);
                Engine.DrawString("The second your boss leaves, you drop the sack of spirits, and they escape into the underworld.", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / (float)1.6), Color.White, Game.FontSmall, TextAlignment.Center);
                Engine.DrawString("Now, you have to sneak through the underworld, and recover those spirits.", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / (float) 1.4), Color.White, Game.FontSmall, TextAlignment.Center);
                Engine.DrawString("Your reaper coworkers are snitches, and you never were very good with dogs...", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / (float)1.2), Color.White, Game.FontSmall, TextAlignment.Center);
                Engine.DrawString("Press space to continue.", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / (float)1.1), Color.White, Game.FontSmall, TextAlignment.Center);
            }
            else if (_level._lvlManager._currLevelNum == 2)
            {
                Engine.DrawString("Level " + _level._lvlManager._currLevelNum, new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / 3 - 50), Color.White, Game.FontXLarge, TextAlignment.Center);
                Engine.DrawString("Congrats, you recovered a spirit!", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / 2), Color.White, Game.FontSmall, TextAlignment.Center);
                Engine.DrawString("So far, no one seems to be on to the fact that you screwed up on day 1.", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / (float)1.8), Color.White, Game.FontSmall, TextAlignment.Center);
                Engine.DrawString("You better find the next spirit soon, before your boss notices you're gone...", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / (float)1.6), Color.White, Game.FontSmall, TextAlignment.Center);
                Engine.DrawString("Press space to continue.", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / (float)1.4), Color.White, Game.FontSmall, TextAlignment.Center);
            }
            else if (_level._lvlManager._currLevelNum == 3)
            {
                Engine.DrawString("Level " + _level._lvlManager._currLevelNum, new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / 3 - 50), Color.White, Game.FontXLarge, TextAlignment.Center);
                Engine.DrawString("Uh oh. It looks like your boss found out about what you did.", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / 2), Color.White, Game.FontSmall, TextAlignment.Center);
                Engine.DrawString("Did I mention your boss is Death?", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / (float)1.8), Color.White, Game.FontSmall, TextAlignment.Center);
                Engine.DrawString("He's a bit angry. Hopefully, he doesn't fire you. As in light you on fire. Good luck!", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / (float)1.6), Color.White, Game.FontSmall, TextAlignment.Center);
                Engine.DrawString("Press space to continue.", new Vector2(Game.Resolution.X / 2, Game.Resolution.Y / (float)1.4), Color.White, Game.FontSmall, TextAlignment.Center);
            }


        }
    }

    public void Enter(StateManager manager)
    {
        Engine.DrawRectSolid(new Bounds2(Vector2.Zero, Game.Resolution), new Color(23, 23, 31, (byte)_opacity));
        _level.InitiateLevel();
    }

    public void Update(StateManager manager)
    {
        _timer += Engine.TimeDelta;
        if(_level._lvlManager._currLevelNum == 0)
        {
            if (_timer > _durationT)
            {
                _level.Draw();
                _level.Update(manager);
                _opacity -= 5;

                if (_opacity <= 0)
                {
                    manager.PopStateNoEnter();
                }
            }
        } else
        {
            if (Engine.GetKeyDown(Key.Space, true))
            {
                _level.Draw();
                _level.Update(manager);
                _opacity = 0;

                if (_opacity <= 0)
                {
                    manager.PopStateNoEnter();
                }
            }
        }

    }
}