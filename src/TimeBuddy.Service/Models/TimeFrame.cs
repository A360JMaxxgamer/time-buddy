namespace TimeBuddy.Service.Models;

/// <summary>
/// A recorded time frame
/// </summary>
public class TimeFrame
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Start of recording
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Total duration
    /// </summary>
    public TimeSpan Duration { get; set; }
}