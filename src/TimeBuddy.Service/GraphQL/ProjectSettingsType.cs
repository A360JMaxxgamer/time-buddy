using TimeBuddy.Service.Models;

namespace TimeBuddy.Service.GraphQL;

public class ProjectSettingsType : ObjectType<ProjectSettings>
{
    /// <inheritdoc />
    protected override void Configure(IObjectTypeDescriptor<ProjectSettings> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Field(s => s.Project)
            .Ignore();
        descriptor.Field(s => s.ProjectId)
            .Ignore();
    }
}