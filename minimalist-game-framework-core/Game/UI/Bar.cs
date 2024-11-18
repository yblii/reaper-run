using System;

class Bar : UIElement
{
    Texture _fill;
    Texture _background;
    Texture _overlay;

    Vector2 _size;
    Bounds2 _fillBounds;
    float _maxWidth;

    float _totalValue;

    public Bar(Vector2 pos, Vector2 size, string filePath, float totalValue, Bounds2 fillBounds) : base(pos)
    {
        _fill = Engine.LoadTexture(filePath + "\\Fill.png");
        _background = Engine.LoadTexture(filePath + "\\Background.png");
        _overlay = Engine.LoadTexture(filePath + "\\Overlay.png");

        _size = size;
        _fillBounds = fillBounds;
        _maxWidth = _fillBounds.Size.X;
        _totalValue = totalValue;
    }

    public void UpdateBar(float newValue)
    {
        float percent = newValue / _totalValue;
        _fillBounds.Size.X = _maxWidth * percent;
    }

    public override void Render()
    {
        Engine.DrawTexture(_background, position, null, _size);

        Bounds2 fillB = new Bounds2(0, 0, _fillBounds.Position.X + _fillBounds.Size.X, _fill.Size.Y);
        Vector2 fillSize = new Vector2((_size.X / _fill.Size.X) * fillB.Size.X, _size.Y);

        Engine.DrawTexture(_fill, position, null, fillSize, 0, null, TextureMirror.None, fillB);
        Engine.DrawTexture(_overlay, position, null, _size);
    }   
}