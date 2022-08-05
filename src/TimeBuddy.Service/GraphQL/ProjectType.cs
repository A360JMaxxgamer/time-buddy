using TimeBuddy.Service.Models;

namespace TimeBuddy.Service.GraphQL;

public class ProjectType : ObjectType<Project>
{
    /// <inheritdoc />
    protected override void Configure(IObjectTypeDescriptor<Project> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Field(proj => proj.TimeFrames)
            .UseFiltering()
            .UseSorting();
    }
}