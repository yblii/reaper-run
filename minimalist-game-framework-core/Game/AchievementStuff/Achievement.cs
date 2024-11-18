class Achievement
{
    public string DisplayName { get; set; }
    public AchievementName Name { get; set; }
    public string Description { get; set; }
    public bool Unlocked { get; set; }

    public Achievement(string display, string description, AchievementName name)
    {
        DisplayName = display;
        Name = name;
        Description = description;
        Unlocked = false;
    }
}