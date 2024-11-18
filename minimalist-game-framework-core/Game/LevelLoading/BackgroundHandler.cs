using System;

class BackgroundHandler
{
	private GameObject[] _leadingBgs;
	private GameObject[] _trailingBgs;

	public BackgroundHandler(String[] bgFiles)
	{
		Bounds2 defaultBounds = new Bounds2(Vector2.Zero, Game.Resolution);

		_leadingBgs = new GameObject[bgFiles.Length];
		_trailingBgs = new GameObject[bgFiles.Length];

        _leadingBgs[0] = new GameObject(defaultBounds, Engine.LoadTexture(bgFiles[0]), Layers.Background3);
		_leadingBgs[1] = new GameObject(defaultBounds, Engine.LoadTexture(bgFiles[1]), Layers.Background2);
		_leadingBgs[2] = new GameObject(defaultBounds, Engine.LoadTexture(bgFiles[2]), Layers.Background1);
        _leadingBgs[3] = new GameObject(defaultBounds, Engine.LoadTexture(bgFiles[3]), Layers.SolidBackground);

        defaultBounds = new Bounds2(Vector2.Zero + new Vector2(Game.Resolution.X + 1f, 0), Game.Resolution);

        _trailingBgs[0] = new GameObject(defaultBounds, Engine.LoadTexture(bgFiles[0]), Layers.Background3);
        _trailingBgs[1] = new GameObject(defaultBounds, Engine.LoadTexture(bgFiles[1]), Layers.Background2);
        _trailingBgs[2] = new GameObject(defaultBounds, Engine.LoadTexture(bgFiles[2]), Layers.Background1);
        _trailingBgs[3] = new GameObject(defaultBounds, Engine.LoadTexture(bgFiles[3]), Layers.SolidBackground);
    }

    public void Update()
	{
		for(int i = 0; i < _leadingBgs.Length; i++)
		{
			GameObject curr = _leadingBgs[i];
			if (curr.Position.X + curr.Scale.X < 0)
			{
				curr.Position = new Vector2(_trailingBgs[i].Position.X + _trailingBgs[i].Scale.X + 1f, 0);
				GameObject temp = curr;

				_leadingBgs[i] = _trailingBgs[i];
				_trailingBgs[i] = temp;
			}
		}
	}
}
