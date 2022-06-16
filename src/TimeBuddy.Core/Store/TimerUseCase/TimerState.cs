using TimeBuddy.Core.Models;

namespace TimeBuddy.Core.Store.TimerUseCase;

public record TimerState(TimerActivity Activity, IReadOnlyList<TimeFrame> RecordTimeFrames, DateTime LastStart, TimeSpan ElapsedTime)
{
    public static TimerState New() => new TimerState(TimerActivity.Stopped, new List<TimeFrame>(), DateTime.Now, TimeSpan.Zero);
}

public enum TimerActivity
{
    Stopped,
    Active,
    Paused
}