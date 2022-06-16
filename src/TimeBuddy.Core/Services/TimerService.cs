using Fluxor;
using TimeBuddy.Core.Models;
using TimeBuddy.Core.Store.TimerUseCase;

namespace TimeBuddy.Core.Services;

internal class TimerService : ITimerService
{
    private readonly List<TimeFrame> _currentTimeFrames = new();
    private DateTime _lastStartTime;

    /// <inheritdoc />
    public Task<TimeFrame> PauseAsync()
    {
        return Task.FromResult(RecordTimeFrame());
    }

    /// <inheritdoc />
    public Task<DateTime> PlayAsync()
    {
        _lastStartTime = DateTime.Now;
        return Task.FromResult(_lastStartTime);
    }

    /// <inheritdoc />
    public Task ReadStateFromStorage() => Task.CompletedTask;

    /// <inheritdoc />
    public Task<TimeFrame[]> StopAsync()
    {
        var timeFrame = RecordTimeFrame();
        _currentTimeFrames.Add(timeFrame);
        return Task.FromResult(_currentTimeFrames.ToArray());
    }

    private TimeFrame RecordTimeFrame()
    {
        var timeFrame = new TimeFrame
        {
            StartDate = _lastStartTime,
            Duration = DateTime.UtcNow - _lastStartTime.ToUniversalTime()
        };
        _currentTimeFrames.Add(timeFrame);
        return timeFrame;
    }
}