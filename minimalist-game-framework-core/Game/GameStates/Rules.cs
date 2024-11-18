using System;
using System.Collections.Generic;
using System.Text;

class Rules : IGameState
{
    private Texture _background;

    public void Update(StateManager manager)
    {
        if (Engine.GetKeyDown(Key.Escape) || Engine.GetKeyDown(Key.Backspace))
        {
            manager.PopState();
        }
    }

    public void Draw()
    {
        Engine.DrawTexture(_background, Vector2.Zero, size: Game.Resolution);

        string[] rules = {  "I. Run through each level to capture the spirit at the end",
                            "II. Hellhounds and other reapers are on guard, make sure to avoid them!",
                                "    i. You can kill hellhounds, but reapers are invulernable",
                                "    ii. Some enemies will drop potions or shields",
                            "III. Some walls and ladders are climbable, but watch out for brick walls",
                            "IV. Jump on platforms as you go",
                                "    i. Cracked platforms will break if you stand on them too long",
                                "    ii. Other platforms may change your speed",
                            "V. Avoid fireballs, spikes, lasers, and the pit!",
                            "VI. If you come across green terrian, try destroying it",
                            "VII. Collect as many coins as you can to get on the scoreboard!"   };

        string[] controls = {   "I. Use arrow keys to move left/right and jump",
                                "II. Hold spacebar for long jump",
                                "III. For combat use Z/X/C for high/medium/low kick (respectively)"  };

        string title1 = "Gameplay Rules";
        Engine.DrawString(title1, new Vector2(Game.Resolution.X / 2, 30), Color.Black, Game.FontLarge, TextAlignment.Center);
        for (int i = 0; i < rules.Length; i++) {
            Engine.DrawString(rules[i], new Vector2(20, 100 + 30 * i), Color.Black, Game.FontSmall);
        }
        string title2 = "Controls";
        Engine.DrawString(title2, new Vector2(Game.Resolution.X / 2, 420), Color.Black, Game.FontLarge, TextAlignment.Center);
        for(int i = 0; i < controls.Length; i++)
        {
            Engine.DrawString(controls[i], new Vector2(20, 490 + 30 * i), Color.Black, Game.FontSmall);
        }

        Engine.DrawString("ESC to go back", new Vector2(15, 10), Color.Black, Game.FontXSmall);
    }

    void IGameState.Enter(StateManager manager)
    {
        _background = Engine.LoadTexture("Sprites\\Backgrounds\\MenuBG.png");
    }
}
