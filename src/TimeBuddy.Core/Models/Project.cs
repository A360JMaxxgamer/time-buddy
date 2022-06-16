namespace TimeBuddy.Core.Models;

public class Project
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedAd { get; set; }

    public virtual List<TimeFrame> TimeFrames { get; set; } = new();

    public virtual ProjectSettings Settings { get; set; } = new();
}