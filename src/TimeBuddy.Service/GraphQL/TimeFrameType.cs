using TimeBuddy.Service.Models;

namespace TimeBuddy.Service.GraphQL;

public class TimeFrameType : ObjectType<TimeFrame>
{
    /// <inheritdoc />
    protected override void Configure(IObjectTypeDescriptor<TimeFrame> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Field("EndDate")
            .Type("DateTime!")
            .Resolve(ctx =>
            {
                var timeFrame = ctx.Parent<TimeFrame>();
                return timeFrame.StartDate + timeFrame.Duration;
            });
    }
}