namespace TimeBuddy.Service.Models;

/// <summary>
/// A project contains various settings and a list of recorded timeframes
/// </summary>
public class Project
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// A user set name
    /// </summary>
    public string Name { get; set; } = "Default";

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// All recorded timeframes
    /// </summary>
    public virtual List<TimeFrame> TimeFrames { get; set; } = new();

    /// <summary>
    /// Project settings e.g. hours per day
    /// </summary>
    public virtual ProjectSettings Settings { get; set; } = new();
}