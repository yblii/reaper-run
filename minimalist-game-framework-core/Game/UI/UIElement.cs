using System;
using System.Collections.Generic;
using System.Text;

class UIElement
{
    public Vector2 position;
    public Texture texture;
    public Vector2 size;

    public UIElement(Vector2 pos, Texture t)
    {
        position = pos;
        texture = t;   

        Camera.AddUIElement(this);
    }

    public UIElement(Vector2 pos, Vector2 size)
    {
        position = pos;
        this.size = size;

        Camera.AddUIElement(this);
    }

    public UIElement(Vector2 pos)
    {
        position = pos;

        Camera.AddUIElement(this);
    }

    public virtual void Render()
    {
        if(texture != null) {
            Engine.DrawTexture(texture, position, null, size);
        }
    }
}
