using TimeBuddy.Blazor.Components.Store.TimerUseCase;

namespace TimeBuddy.Blazor.Components.Services;

internal class TimerService : ITimerService
{
    private readonly List<TimeFrameInput> _currentTimeFrames = new();
    private DateTime _lastStartTime;

    /// <inheritdoc />
    public Task<TimeFrameInput> PauseAsync()
    {
        return Task.FromResult(RecordTimeFrame());
    }

    /// <inheritdoc />
    public Task LoadAsync(TimerState state)
    {
        _currentTimeFrames.Clear();
        _currentTimeFrames.AddRange(state.RecordTimeFrames);
        _lastStartTime = state.LastStart;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<DateTime> PlayAsync()
    {
        _lastStartTime = DateTime.Now;
        return Task.FromResult(_lastStartTime);
    }

    /// <inheritdoc />
    public Task<TimeFrameInput[]> StopAsync()
    {
        var timeFrame = RecordTimeFrame();
        _currentTimeFrames.Add(timeFrame);
        return Task.FromResult(_currentTimeFrames.ToArray());
    }

    private TimeFrameInput RecordTimeFrame()
    {
        var timeFrame = new TimeFrameInput
        {
            StartDate = _lastStartTime,
            Duration = DateTime.UtcNow - _lastStartTime.ToUniversalTime()
        };
        _currentTimeFrames.Add(timeFrame);
        return timeFrame;
    }
}