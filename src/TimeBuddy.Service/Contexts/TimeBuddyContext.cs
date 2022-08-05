using Microsoft.EntityFrameworkCore;
using TimeBuddy.Service.Models;

namespace TimeBuddy.Service.Contexts;

public class TimeBuddyContext : DbContext
{
    public DbSet<Project> Projects => Set<Project>();
    
    public DbSet<TimeFrame> TimeFrames => Set<TimeFrame>();
    
    public DbSet<ProjectSettings> ProjectSettings => Set<ProjectSettings>();

    public TimeBuddyContext(DbContextOptions options) : base(options)
    {
    }
}