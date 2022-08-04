using TimeBuddy.Blazor.Components.Store.TimerUseCase;

namespace TimeBuddy.Blazor.Components.Services;

public interface ITimerService
{
    Task LoadAsync(TimerState state);
    
    Task<DateTime> PlayAsync();

    Task<TimeFrame> PauseAsync();

    Task<TimeFrame[]> StopAsync();
}