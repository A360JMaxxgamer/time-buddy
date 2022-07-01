using TimeBuddy.Core.Models;

namespace TimeBuddy.Core.Store.TimerUseCase;

public record TimerState(TimerActivity Activity, IReadOnlyList<TimeFrame> RecordTimeFrames, DateTime LastStart,
    TimeSpan ElapsedTime, Project? ActiveProject)
{
    public static TimerState New() => new(TimerActivity.Stopped, new List<TimeFrame>(), DateTime.Now, TimeSpan.Zero, null);
}

public enum TimerActivity
{
    Stopped,
    Active,
    Paused
}