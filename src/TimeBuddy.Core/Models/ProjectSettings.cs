namespace TimeBuddy.Core.Models;

public class ProjectSettings
{
    public int Id { get; set; }
    
    public int ProjectId { get; set; }

    public virtual Project? Project { get; set; }
}