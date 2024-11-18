class CritPoint : GameObject
{
    public Actions AtkPosition { get; }
    public CritPoint(float x, float y, Actions attack) : base(x, y, 30, 30, Engine.LoadTexture("Sprites\\Laser.png"), Layers.Collectible)
    {
        Collidable = false;
        AtkPosition = attack;
    }
}