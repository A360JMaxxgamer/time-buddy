using TimeBuddy.Core.Models;
using TimeBuddy.Core.Store.TimerUseCase;

namespace TimeBuddy.Core.Services;

public interface ITimerService
{
    Task LoadAsync(TimerState state);
    
    Task<DateTime> PlayAsync();

    Task<TimeFrame> PauseAsync();

    Task<TimeFrame[]> StopAsync();
}