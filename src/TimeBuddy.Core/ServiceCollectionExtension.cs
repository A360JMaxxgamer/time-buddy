using Fluxor;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeBuddy.Core.Contexts;
using TimeBuddy.Core.Models;
using TimeBuddy.Core.Services;

namespace TimeBuddy.Core;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddTimeBuddyContext(this IServiceCollection services, string fileName)
    {
        var connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = fileName
        }.ToString();
        services.AddDbContext<TimeBuddyContext>(opt => opt.UseSqlite(connectionString));
        var context = services
            .BuildServiceProvider()
            .GetRequiredService<TimeBuddyContext>();

        context.Database.EnsureCreated();

        if (!context.Projects.Any())
        {
            context.AddRange(CreateTestProjects());
            context.SaveChanges();
        }
        
        return services;
    }

    public static IServiceCollection AddStateManagement(this IServiceCollection services)
    {
        services.AddFluxor(options => options.ScanAssemblies(typeof(ServiceCollectionExtension).Assembly));

        return services;
    }

    public static IServiceCollection AddTimeBuddyServices(this IServiceCollection services)
    {
        services.AddSingleton<ITimerService, TimerService>();
        return services;
    }

    private static List<Project> CreateTestProjects() => new()
    {
        new Project()
        {
            Name = "Homework",
            CreatedAd = DateTime.Now,
            TimeFrames = new List<TimeFrame>()
            {
                {
                    new()
                    {
                        StartDate = new DateTime(1999,9,10, 16,40,0),
                        Duration = TimeSpan.FromMinutes(30)
                    }
                }
            }
        },
        new Project()
        {
            Name = "Work",
            CreatedAd = DateTime.Now
        }
    };
}