using System;
using System.IO;

internal class Animator
{
    private Sprite _sprite;

    private Vector2 _gridSize;
    private Vector2 _cellSize;

    private Animation[] _anims;
    private Animation _current;
    private int _currentFrame;
    private float _frameTimer;

    /*
     * Parameters:
     * Sprite sprite - reference to sprite in a GameObject that will be changed
     * String metadata - path to file with data about sprite sheet and animations for sprite
    */
    public Animator(Sprite sprite, String metadata)
    {
        this._sprite = sprite;

        ReadData(metadata);
        _current = _anims[0];
        _currentFrame = 0;

        _sprite.SetSource(new Bounds2(Vector2.Zero, _cellSize));
    }

    // changes frames of animation
    public void Update()
    {
        // keeping track of how long the current frame has been shown
        _frameTimer += Engine.TimeDelta;

        // updating frame based on animation duration
        if (_frameTimer > _current.TimePerFrame)
        {
            _frameTimer = 0;
            _currentFrame++;
            if(_currentFrame > _current.FrameCount - 1)
            {
                _currentFrame = 0;
            }

            // calculating location of new frame on the sprite sheet
            float yPos = _cellSize.Y * _current.RowNum;
            float xPos = _cellSize.X * _currentFrame;
            _sprite.SetSource(new Bounds2(new Vector2(xPos, yPos), _cellSize));
        }
    }

    // changes current animation
    public void ChangeAction(Actions action)
    {
        if(!_current.Action.Equals(action) && (_current.Interruptable || _currentFrame >= _current.FrameCount - 1)) {
            foreach (Animation a in _anims)
            {
                if (a.Action.Equals(action))
                {
                    _current = a;
                }
            }
            _currentFrame = 0;
            _frameTimer = 0;
        } 
    }

    public bool AnimationComplete()
    {
        return _currentFrame >= _current.FrameCount - 1;
    }

    // reads data from file and stores it
    private void ReadData(string file)
    {
        try
        {
            StreamReader sr = new StreamReader(file);

            // gridSize gives how many rows and columns of sprites there are
            // cell size is the pixel size of each individual sprite image
            String[] data = sr.ReadLine().Split(" ");
            _gridSize = new Vector2(int.Parse(data[0]), int.Parse(data[1]));
            _anims = new Animation[(int)_gridSize.Y];

            data = sr.ReadLine().Split(" ");
            _cellSize = new Vector2(int.Parse(data[0]), int.Parse(data[1]));

            // skipping unnecessary lines
            string line = "";
            while (!line.Equals("START"))
            {
                line = sr.ReadLine();
            }

            int rowNum = 0;

            while (sr.Peek() != -1)
            {
                Animation curr;
                
                // getting "title" of animation and converting it to the corresponding Action
                String title = sr.ReadLine();
                Actions currAction = (Actions)Enum.Parse(typeof(Actions), title, true);

                // getting other data about animation
                int frames = int.Parse(sr.ReadLine());
                float duration = float.Parse(sr.ReadLine());
                bool interruptable = bool.Parse(sr.ReadLine());

                curr = new Animation(currAction, rowNum, frames, duration, interruptable);
                _anims[rowNum] = curr;
                //Console.WriteLine(curr);

                rowNum++;
                sr.ReadLine();
            }
        }
        catch(FileNotFoundException e)
        {
            Console.WriteLine("Animation data file not found");
        }
    }
}
