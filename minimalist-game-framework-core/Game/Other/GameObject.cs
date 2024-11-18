using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Numerics;

class GameObject
{
    public const float GRAVITY = 0.5f;
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public Bounds2 PrevBound { get; set; }
    public Vector2 SpriteOffset;

    public Color color;
    public Sprite sprite;

    public bool Collidable;
    public bool IsStatic { get; set; }

    // automatically assigned to layer 0 unless otherwise stated
    public int Layer = 0;

    public float _dX;
    public float _dY;
    public float _aX = 0;
    public float _aY= GRAVITY;

    public bool Debugging = false;

    public static List<GameObject> toDestroy = new List<GameObject>();

    public GameObject(float x, float y, float width, float height, Texture t, int layer)
    {
        Position = new Vector2(x, y);
        Scale = new Vector2(width, height);
        Layer = layer;

        sprite = new Sprite(t);
        sprite.SetSize(Scale);
        Camera.AddGameObject(this);
    }

    public GameObject(Bounds2 bounds, Texture t, int layer)
    {
        Position = bounds.Position;
        Scale = bounds.Size;
        Layer = layer;

        sprite = new Sprite(t);
        sprite.SetSize(Scale);
        Camera.AddGameObject(this);
    }

    public void Destroy()
    {
        Camera.RemoveGameObject(this);
        CollisionHandler.Remove(this);
    }

    public virtual void MoveX(float dx)
    {
        PrevBound = GetBounds();
        Position = new Vector2(Position.X + dx, Position.Y);
      
        if (Collidable)
        {
            CollisionHandler.Update(this);
        }
    }

    public void MoveY(float dy)
    {
        PrevBound = GetBounds();
        Position = new Vector2(Position.X, Position.Y + dy);

        if (Collidable)
        {
            CollisionHandler.Update(this);
        }
    }

    public virtual void DrawObj()
    {
        sprite.SetPosition(this.Position + SpriteOffset);
        sprite.Render();
    }

    public Bounds2 GetBounds()
    {
        return new Bounds2(Position, Scale);
    }

    public Bounds2 NewBounds(float dX, float dY)
    {
        Vector2 pos = new Vector2(this.Position.X + dX, this.Position.Y + dY);
        return new Bounds2(pos, Scale);
    }

    public Vector2 GetCenter()
    {
        return new Vector2(this.Position.X + this.Scale.X / 2, this.Position.Y + this.Scale.Y);
    }

    public bool WillOverlap(float dX, float dY, HashSet<GameObject> other)
    {
        Vector2 pos = new Vector2(this.Position.X + dX, this.Position.Y + dY);
        Bounds2 newBounds = new Bounds2(pos, Scale);

        foreach (GameObject o in other)
        {
            if (!this.Equals(o) && o.Collidable && newBounds.OverlapsT(o.GetBounds()))
            {
                return true;
            }
        }

        return false;
    }

    public bool Overlaps(HashSet<GameObject> other)
    {
        foreach (GameObject o in other)
        {
            if (!this.Equals(o) && o.Collidable && this.GetBounds().OverlapsT(o.GetBounds()))
            {
                return true;
            }
        }

        return false;
    }

    public bool WillOverLapX(float dX, HashSet<GameObject> other)
    {
        Vector2 pos = new Vector2(this.Position.X + dX, this.Position.Y);
        Bounds2 newBounds = new Bounds2(pos, Scale);

        foreach (GameObject o in other)
        {
            if (!this.Equals(o) && o.Collidable && newBounds.OverlapsX(o.GetBounds()))
            {
                return true;
            }
        }
        return false;
    }
  
    public float HorizontalCollision(float dX)
    {
        HashSet<GameObject> other = CollisionHandler.GetCollisions(this);
        Vector2 pos = new Vector2(this.Position.X + dX, this.Position.Y);
        Bounds2 newBounds = new Bounds2(pos, Scale);

        foreach (GameObject o in other)
        {
            if(o.GetBounds().Overlaps(newBounds))
            {
                return this.GetBounds().GetXOverlap(o.GetBounds());
            }
        }
        return 0;
    }

    public float UnclipVertical()
    {
        HashSet<GameObject> other = CollisionHandler.GetCollisions(this);

        foreach (GameObject o in other)
        {
            if (o.GetBounds().Overlaps(this.GetBounds()) && !(o is Player))
            {
                return o.GetBounds().FixYOverlap(this.GetBounds());
            }
        }
        return 0;
    }

    public Bounds2 GetPrevBounds()
    {
        return PrevBound;
    }

    public virtual void Update()
    {
    }

    public void ChangeColor()
    {
        Engine.DrawRectSolid(new Bounds2(Position, Scale), Color.Orange);
    }

    public static void UpdateGameObjects()
    {
        foreach (GameObject o in toDestroy)
        {
            o.Destroy();
        }
        toDestroy.Clear();
    }
}

