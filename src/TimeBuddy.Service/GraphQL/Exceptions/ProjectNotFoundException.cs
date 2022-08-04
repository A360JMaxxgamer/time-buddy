namespace TimeBuddy.Service.GraphQL.Exceptions;

public class ProjectNotFoundException : Exception
{
    public ProjectNotFoundException(Guid projectId) : base($"Project with the id {projectId} not found") 
    {
    }
}