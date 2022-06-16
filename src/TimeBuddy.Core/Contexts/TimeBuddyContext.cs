using Microsoft.EntityFrameworkCore;
using TimeBuddy.Core.Models;

namespace TimeBuddy.Core.Contexts;

public class TimeBuddyContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    
    public DbSet<TimeFrame> TimeFrames { get; set; }
    
    public DbSet<ProjectSettings> ProjectSettings { get; set; }

    public TimeBuddyContext(DbContextOptions options) : base(options)
    {
    }
}