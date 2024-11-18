using System;
using System.Collections.Generic;
using System.Text;

static class CollisionHandler
{
    class Cell {
        public Bounds2 Bound { get; set; }
        public List<GameObject> Objects = new List<GameObject>();
        public Cell(Bounds2 bound)
        {
            this.Bound = bound;
        }
    }
    
    private static readonly int _rowCount = 20;
    private static readonly int _colCount = 40;

    private static Cell[,] _grid = new Cell[_colCount, _rowCount];

    private static Bounds2 _increasedRes = new Bounds2(-200, -200, Game.Resolution.X + 500, Game.Resolution.Y + 400);

    private static float _cellWidth = (_increasedRes.Size.X) / _colCount;
    private static float _cellHeight = (_increasedRes.Size.Y) / _rowCount;

    // Generates a grid with the given row count and cell size,
    // stores in _grid
    public static void GenerateGrid()
    {
        Vector2 cellSize = new Vector2(_cellWidth + 1, _cellHeight + 1);

        for(int row = 0; row < _rowCount; row++)
        {
            for(int col = 0; col < _colCount; col++)
            {
                Vector2 pos = new Vector2((_cellWidth * col) - 200, (_cellHeight * row) - 200);

                Cell curr = new Cell(new Bounds2(pos, cellSize));
                _grid[col, row] = curr;
            }
        }
    }

    public static void Clear()
    {
        for (int row = 0; row < _rowCount; row++)
        {
            for (int col = 0; col < _colCount; col++)
            {
                _grid[col, row].Objects.Clear();
            }
        }
    }

    public static bool InBounds(GameObject obj)
    {
        return _increasedRes.Contains(obj.GetBounds().Max);
    }

    public static HashSet<GameObject> GetCollisions(Bounds2 obj)
    {
        HashSet<GameObject> potentialCollisions = new HashSet<GameObject>();

        Vector2 pos = obj.Position + new Vector2(200, 200);

        int startY = (int) Math.Abs(Math.Floor(pos.Y / _cellHeight));
        int startX = (int) Math.Abs(Math.Floor(pos.X / _cellWidth));

        int endY = (int) Math.Abs(Math.Floor((pos.Y + obj.Size.Y) / _cellHeight));
        int endX = (int) Math.Abs(Math.Floor((pos.X + obj.Size.X) / _cellWidth));

        if (startX - 1 >= 0 && startY - 1 >= 0 && endX + 1 < _colCount && endY + 1 < _rowCount)
        {
            for (int i = startX - 1; i <= endX + 1; i++)
            {
                for (int j = startY - 1; j <= endY + 1; j++)
                {
                    if (_grid[i, j].Objects.Count > 1)
                    {
                        foreach (GameObject o in _grid[i, j].Objects)
                        {
                            potentialCollisions.Add(o);
                        }
                    }
                }
            }
        }
        return potentialCollisions;
    }

    public static HashSet<GameObject> GetCollisions(GameObject obj)
    {
        HashSet<GameObject> potentialCollisions = new HashSet<GameObject>();

        Bounds2 bound = obj.GetBounds();
        Vector2 pos = bound.Position + new Vector2(200, 200);

        int startY = (int) Math.Abs(Math.Floor(pos.Y / _cellHeight));
        int startX = (int) Math.Abs(Math.Floor(pos.X / _cellWidth));

        int endY = (int) Math.Abs(Math.Floor((pos.Y + bound.Size.Y) / _cellHeight));
        int endX = (int) Math.Abs(Math.Floor((pos.X + bound.Size.X) / _cellWidth));

        if (startX - 1 >= 0 && startY - 1 >= 0 && endX + 1 < _colCount && endY + 1 < _rowCount)
        {
            for (int i = startX - 1; i <= endX + 1; i++)
            {
                for (int j = startY - 1; j <= endY + 1; j++)
                {
                    if (_grid[i, j].Objects.Count > 1)
                    {
                        foreach (GameObject o in _grid[i, j].Objects)
                        {
                            if(!obj.Equals(o))
                            {
                                potentialCollisions.Add(o);
                            }
                        }
                    }
                }
            }
        }

        return potentialCollisions;
    }

    public static void ShowGrid()
    {
        for (int row = 0; row < _rowCount; row++)
        {
            for (int col = 0; col < _colCount; col++)
            {
                Engine.DrawRectEmpty(_grid[col, row].Bound, Color.Black);
                Engine.DrawString(row + ", " + col, _grid[col,row].Bound.Position + new Vector2(_cellWidth / 2, 0), Color.Black, Game.FontTiny);
            }
        }
    }

    public static void ShowCollisions()
    {
        for (int row = 0; row < _rowCount; row++)
        {
            for (int col = 0; col < _rowCount; col++)
            {
                Cell curr = _grid[row, col];
                if (curr.Objects.Count > 1)
                {
                    foreach(GameObject obj in curr.Objects)
                    {
                        //obj.DebugDrawBounds();
                    }
                }
            }
        }
    }

    public static void Update(GameObject obj)
    {
        Remove(obj);
        Add(obj);
    }

    public static void Add(GameObject obj)
    {
        Bounds2 bound = obj.GetBounds();
        Vector2 pos = bound.Position + new Vector2(200, 200);

        int startY = (int) Math.Abs(Math.Floor(pos.Y / _cellHeight));
        int startX = (int) Math.Abs(Math.Floor(pos.X / _cellWidth));

        int endY = (int) Math.Abs(Math.Floor((pos.Y + bound.Size.Y) / _cellHeight));
        int endX = (int) Math.Abs(Math.Floor((pos.X + bound.Size.X) / _cellWidth));

        if (endX < _colCount && endY < _rowCount)
        {
            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    _grid[i, j].Objects.Add(obj);
                }
            }
        }
    }

    public static void Remove(GameObject obj)
    {
        Bounds2 bound = obj.GetPrevBounds();
        Vector2 pos = bound.Position + new Vector2(200, 200);

        int startY = (int) Math.Abs(Math.Floor(pos.Y / _cellHeight));
        int startX = (int) Math.Abs(Math.Floor(pos.X / _cellWidth));

        int endY = (int) Math.Abs(Math.Floor((pos.Y + bound.Size.Y) / _cellHeight));
        int endX = (int) Math.Abs(Math.Floor((pos.X + bound.Size.X) / _cellWidth));

        if (endX < _colCount && endY < _rowCount)
        {
            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    _grid[i, j].Objects.Remove(obj);
                }
            }
        }
    }
}
