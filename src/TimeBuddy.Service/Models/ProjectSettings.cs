namespace TimeBuddy.Service.Models;

/// <summary>
/// Project settings e.g. hours per day
/// </summary>
public class ProjectSettings
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Id of the parent project
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Target hours to record per day
    /// </summary>
    public double TargetHoursPerDay { get; set; } = 8;

    /// <summary>
    /// Navigation property for project
    /// </summary>
    public virtual Project? Project { get; set; }
}