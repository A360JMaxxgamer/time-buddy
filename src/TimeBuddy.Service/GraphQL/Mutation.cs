using Microsoft.EntityFrameworkCore;
using TimeBuddy.Service.Contexts;
using TimeBuddy.Service.GraphQL.Exceptions;
using TimeBuddy.Service.Models;

namespace TimeBuddy.Service.GraphQL;

public class Mutation
{
    /// <summary>
    /// Create a new project
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="projectName">Name of the project</param>
    /// <param name="timeFrameInputs"></param>
    /// <returns></returns>
    public async Task<Project> CreateProjectAsync(TimeBuddyContext dbContext, string projectName, TimeFrameInput[]? timeFrameInputs)
    {
        var project = new Project()
        {
            Name = projectName,
            CreatedAt = DateTime.UtcNow
        };

        if (timeFrameInputs is not null)
        {
            project.TimeFrames.AddRange(timeFrameInputs.Select(ConvertToTimeFrame));
        }
        
        var tracking = await dbContext.AddAsync(project);
        await dbContext.SaveChangesAsync();
        return tracking.Entity;
    }

    /// <summary>
    /// Add timeframes to a project
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="projectId">Id of the project</param>
    /// <param name="timeFrames">TimeFrames to add</param>
    /// <returns></returns>
    /// <exception cref="ProjectNotFoundException"></exception>
    [Error(typeof(ProjectNotFoundException))]
    public async Task<Project> AddTimeFramesToProject(TimeBuddyContext dbContext, Guid projectId, TimeFrameInput[] timeFrames)
    {
        var project = await dbContext.Projects.FirstOrDefaultAsync(proj => proj.Id == projectId);

        if (project is null)
            throw new ProjectNotFoundException(projectId);
        
        project.TimeFrames.AddRange(timeFrames.Select(ConvertToTimeFrame));
        await dbContext.SaveChangesAsync();
        return project;
    }

    private static TimeFrame ConvertToTimeFrame(TimeFrameInput input) => new()
    {
        Duration = input.Duration,
        StartDate = input.StartDate
    };
}

public record TimeFrameInput(DateTime StartDate, TimeSpan Duration);