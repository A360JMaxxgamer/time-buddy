using TimeBuddy.Service.Contexts;
using TimeBuddy.Service.Models;

namespace TimeBuddy.Service.GraphQL;

public class Query
{
    /// <summary>
    /// Get a list of projects
    /// </summary>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    [UsePaging]
    [UseProjection]
    [UseSorting]
    [UseFiltering]
    public IQueryable<Project> GetProjects(TimeBuddyContext dbContext) => dbContext.Projects;
}