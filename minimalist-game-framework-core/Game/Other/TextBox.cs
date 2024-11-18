using System;
using System.Collections.Generic;
using System.Text;

class TextBox : GameObject
{
    private string _text;
    private Font _font;
    private TextAlignment _alignment;

    // note: the base class call uses the reaper sprite as a dummy sprite - the actual DrawObj method is defined below
    public TextBox(string text, float x, float y, float width, float height, Font size, Color color, bool stationary) : base(x, y, width, height, Engine.LoadTexture("Sprites\\Reaper.png"), Layers.Text)
    {
        _text = text;
        Position = new Vector2(x, y);
        Scale = new Vector2(width, height);
        _font = size;
        this.color = color;
        IsStatic = stationary;
        _alignment = TextAlignment.Left;

        Collidable = false;
    }

    // includes text alignment
    public TextBox(string text, float x, float y, float width, float height, Font size, Color color, bool stationary, TextAlignment alignment) : base(x, y, width, height, Engine.LoadTexture("Sprites\\Reaper.png"), Layers.Text)
    {
        _text = text;
        Position = new Vector2(x, y);
        Scale = new Vector2(width, height);
        _font = size;
        this.color = color;
        IsStatic = stationary;
        _alignment = alignment;

        Collidable = false;
    }

    public void ChangeText(string s)
    {
        _text = s;
    }

    public override void DrawObj()
    {
        Engine.DrawString(_text, Position, color, _font, _alignment);
    }
}