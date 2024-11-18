using System;

// stores information about how a GameObject should be rendered
internal class Sprite
{
	Texture _texture; // sprite sheet or image (if the object has no animations)
	Color _color;
	Vector2 _position;
	Vector2 _size;
	public float _rotation;
	Vector2 _pivot;
	TextureMirror _mirror;
	Bounds2 _source;
	TextureBlendMode _blendMode;
	TextureScaleMode _scaleMode;

	public Sprite(Texture texture)
	{
		this._texture = texture;
		_color = Color.White;
		_size = texture.Size;
		_rotation = 0;
		_mirror = TextureMirror.None;
		_source = new Bounds2(0, 0, texture.Width, texture.Height);
		_blendMode = TextureBlendMode.Normal;
		_scaleMode = TextureScaleMode.Nearest;
	}

	public void Render()
    {
		Engine.DrawTexture(_texture, _position, _color, _size, _rotation, _pivot, _mirror, _source, _blendMode, _scaleMode);
	}

	public void SetDirection(TextureMirror mirror)
    {
		this._mirror = mirror;
    }

	public void SetSource(Bounds2 bounds)
    {
		this._source = bounds;
    }
	public void SetSize(Vector2 size)
    {
		this._size = size;
    }

	public void SetPosition(Vector2 pos)
    {
		this._position = pos;
    }
    public Vector2 GetPosition()
    {
		return _position;
    }

    public void SetBlendMode(TextureBlendMode mode)
    {
        this._blendMode = mode;
    }

	public Vector2 GetCenter()
	{
		return new Vector2(this._position.X + this._size.X / 2, this._position.Y + this._size.Y / 2);
	}

    public Vector2 GetSize()
    {
        return _size;
    }
}
