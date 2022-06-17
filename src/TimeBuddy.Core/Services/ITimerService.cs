using TimeBuddy.Core.Models;

namespace TimeBuddy.Core.Services;

public interface ITimerService
{
    Task<DateTime> PlayAsync();

    Task<TimeFrame> PauseAsync();

    Task<TimeFrame[]> StopAsync();
}