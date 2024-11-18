using System;
using System.Collections.Generic;
using System.Text;

static class Camera
{
    // stores all the game objects in layers (the order in which they're drawn)
    // set 0 is the bottom layer (furthest back), the last set is the top layer
    private static List<HashSet<GameObject>> _gameObjects;
    private static List<UIElement> _UIElements;

    public static void UpdateAll()
    {
        //// old comments
        //Yea, idk if this is the best way to do this.
        //If u use a try/catch, everything flickers when an object is destroyed
        //Need the try catch because u can't edit the list of GameObjects 
        //while the foreach loop is running. Next best solution is 
        //duplicating the list and not changing it.

        for (int i = 0; i < _gameObjects.Count; i++)
        {
            GameObject[] layer = new GameObject[_gameObjects[i].Count];
            _gameObjects[i].CopyTo(layer);
            foreach (GameObject obj in layer) 
            {
                if(CollisionHandler.InBounds(obj)) obj.Update();
            }
        }

        Alive.UpdateAlive();
        GameObject.UpdateGameObjects();
    }

    public static void DrawAll()
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            foreach (GameObject obj in _gameObjects[i])
            {
                obj.DrawObj();
            }
        }

        for(int i = 0; i < _UIElements.Count; i++)
        {
            _UIElements[i].Render();
        }
    }

    public static void DrawUI()
    {
        for (int i = 0; i < _UIElements.Count; i++)
        {
            _UIElements[i].Render();
        }
    }

    public static void InitializeGameObjects()
    {
        _gameObjects = new List<HashSet<GameObject>>(Layers.NumLayers);
        for (int i = 0; i < Layers.NumLayers; i++)
        {
            _gameObjects.Add(new HashSet<GameObject>());
        }

        _UIElements = new List<UIElement>();
    }

    // moves all the game objects to the left
    // the top layer moves by dx, each layer below it moves by some appropriate fraction of that
    public static void Scroll(double dx)
    {
        // NOTE: while this piece of code may seem useless it's the only thing keeping our collisions working
        // DO NOT DELETE >:(
        if(dx == 0)
        {
            foreach(HashSet<GameObject> layer in _gameObjects)
            {
                foreach(GameObject g in layer)
                {
                    g.MoveX(0);
                }
            }
            return;
        }

        int currentLayer = 0;
        // inital scroll amount: 1/4 of the (layer - 0.2)
        double currentScroll = (-0.25 * currentLayer - 0.2);
        // based on player location, scale the scroll speed
        double dynamicScroll = CalcDynamicScrollExponential(currentScroll);

        // for each layer of game objects
        foreach (HashSet<GameObject> layer in _gameObjects)
        {
            // for each game object in the layer
            foreach (GameObject g in layer)
            {
                // if the object is not a stationary object
                if (!g.IsStatic)
                {
                    g.MoveX((float) (currentScroll * dynamicScroll));
                }
            }
            currentLayer++;

            // parallax calculation
            if (currentLayer < Layers.NumBackground)
            {
                currentScroll = (-0.25 * currentLayer - 0.2);
            }
            else
            {
                currentScroll = dx;
            }
        }
    }

    // based on player location, determine scroll speed scale linearlly
    public static double CalcDynamicScrollLinear(double currentScroll)
    {
        // get player x location
        double playerX = Game.StateManager.GetCurrentState().GetPlayer().Position.X;
        // find how far across the screen the player is (%)
        double totalX = Game.Resolution.X;
        double percentAcross = playerX / totalX;

        // if the player is within the last 25% of the screen, speed up the scroll speed
        if (percentAcross > 0.75)
        {
            return 1.5;
        }

        // if the player is within the first 75% of the screen, don't scale the scroll speed
        return 1;
    }

    // based on player location, determine scroll speed scale exponentially
    // exponential function y = ab^x
    // a is the "currentScroll" determined by the parallax (in the Scroll method)
    // b^x is what this function returns, with x representing the player position
    public static double CalcDynamicScrollExponential(double currentScroll)
    {
        // get player x location
        double playerX = Player.player.Position.X;

        // decimal from 0 to 2 that represents how far across the screen the player is
        double x = playerX / (Game.Resolution.X / 2);
        return Math.Exp(x); // returns e^x (where e = b)
    }

    // deletes all current game objects
    public static void Clear()
    {
        _gameObjects.Clear();
        _UIElements.Clear();
        CollisionHandler.Clear();
    }

    public static void ClearUI()
    {
        _UIElements.Clear();
    }

    // adds a game object to the game
    public static void AddGameObject(GameObject g)
    {
        List<HashSet<GameObject>> list = _gameObjects;
        _gameObjects[g.Layer].Add(g);
    }

    // removes a game object from the game
    public static void RemoveGameObject(GameObject g)
    {
        _gameObjects[g.Layer].Remove(g);
    }

    public static void AddUIElement(UIElement element)
    {
        _UIElements.Add(element);
    }

    // removes a game object from the game
    public static void RemoveUIElement(UIElement element)
    {
        _UIElements.Remove(element);
    }
}