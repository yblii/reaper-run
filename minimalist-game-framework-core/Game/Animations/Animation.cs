// Stores an animation sequence's data

internal class Animation
{
    public Actions Action { get; }
    public int RowNum { get; }
    public int FrameCount { get; }
    public float Duration { get; }
    public float TimePerFrame { get; }
    public bool Interruptable { get; }

    public Animation(Actions Action, int RowNum, int Frames, float Duration, bool Interruptable)
    {
        this.Action = Action;
        this.RowNum = RowNum;
        this.FrameCount = Frames;
        this.Duration = Duration;
        this.Interruptable = Interruptable;

        TimePerFrame = Duration / FrameCount;
    }

    public override string ToString()
    {
        return "Action: " + Action + " | Row #: " + RowNum + " | # of Frames: " + FrameCount;
    }
}