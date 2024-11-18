using System;
using System.Collections.Generic;
using System.Text;

class Button : UIElement
{
    private string _text;
    private ResizableTexture _t;
    private Texture _img;
    public IGameState _state { get; }

    private bool _highlighted;

    public Button(float x, float y, float width, float height, string display, IGameState state) : base(new Vector2(x, y), new Vector2(width, height))
    {
        _t = Engine.LoadResizableTexture("Sprites\\Button.png", 8, 8, 8, 8);
        _text = display;
        this._state = state;
    }

    public void SetImage(Texture t)
    {
        this._img = t;
    }

    public void Update(Vector2 mouse)
    {
        if(MouseOverlap(mouse))
        {
            _highlighted = true;
        } 
        else
        {
            _highlighted = false;
        }
    }

    public override void Render()
    {
        Engine.DrawResizableTexture(_t, new Bounds2(this.position, this.size), scaleMode: TextureScaleMode.Nearest);

        if (_highlighted)
        {
            Engine.DrawRectSolid(new Bounds2(this.position, this.size), new Color(0, 0, 0, 120));
        }

        Engine.DrawString(_text, GetCenter() - new Vector2(0, 16), alignment: TextAlignment.Center, color: Color.White, font: Game.FontSmall);
        
        if(_img != null) Engine.DrawTexture(_img, this.position + new Vector2(6, 3), size: this.size * 0.8f);
        
        base.Render();
    }

    private Vector2 GetCenter()
    {
        return new Vector2(this.position.X + this.size.X / 2, this.position.Y + this.size.Y / 2);
    }

    public Boolean MouseOverlap(Vector2 mouse)
    {
        if (position.X < mouse.X && mouse.X < position.X + size.X)
        {
            if (position.Y < mouse.Y && mouse.Y < position.Y + size.Y)
            {
                return true;
            }
        }
        return false;
    }
}

