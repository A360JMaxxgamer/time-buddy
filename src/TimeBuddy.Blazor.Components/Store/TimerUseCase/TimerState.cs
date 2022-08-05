namespace TimeBuddy.Blazor.Components.Store.TimerUseCase;

public record TimerState(
    TimerActivity Activity, 
    IReadOnlyList<TimeFrameInput> RecordTimeFrames, 
    DateTime LastStart,
    TimeSpan ElapsedTime, 
    IProjectBase? ActiveProject,
    IGetProjectTimerData_Project? ActiveProjectDetails)
{
    public static TimerState New() => new(TimerActivity.Stopped, new List<TimeFrameInput>(), DateTime.Now,
        TimeSpan.Zero, null, null);
}

public enum TimerActivity
{
    Stopped,
    Active,
    Paused
}