using Fluxor;
using TimeBuddy.Core.Services;

namespace TimeBuddy.Core.Store.TimerUseCase;

public class Effects
{
    private const string StorageKey = "TimerState";
    private readonly ITimerService _timerService;
    private readonly IState<TimerState> _timerState;
    private readonly ILocalStorageService _localStorageService;

    public Effects(ITimerService timerService, IState<TimerState> timerState, ILocalStorageService localStorageService)
    {
        _timerService = timerService;
        _timerState = timerState;
        _localStorageService = localStorageService;
    }

    [EffectMethod]
    public async Task HandlePlayAction(PlayAction _, IDispatcher dispatcher)
    {
        var startedAt = await _timerService.PlayAsync();
        dispatcher.Dispatch(new SetLastStartAction(startedAt));
        dispatcher.Dispatch(new SetActivityAction(TimerActivity.Active));
    }
    
    [EffectMethod]
    public async Task HandlePauseAction(PauseAction _, IDispatcher dispatcher)
    {
        var recordedTimeFrame = await _timerService.PauseAsync();
        dispatcher.Dispatch(new AddRecordedTimeFrame(recordedTimeFrame));
        dispatcher.Dispatch(new SetActivityAction(TimerActivity.Paused));
    }
    
    [EffectMethod]
    public async Task HandleStopAction(StopAction _, IDispatcher dispatcher)
    {
        var recordedTimeFrames = await _timerService.StopAsync();
        dispatcher.Dispatch(new SaveAction(recordedTimeFrames));
        dispatcher.Dispatch(new SetActivityAction(TimerActivity.Stopped));
    }
    
    [EffectMethod]
    public async Task HandleSaveAction(SaveAction action, IDispatcher dispatcher)
    {
        
    }

    [EffectMethod(typeof(RefreshUiTimerAction))]
    public async Task HandleRefreshUiTimerAction(IDispatcher _)
    {
        await _localStorageService.SaveAsync(StorageKey, _timerState.Value);
    }

    [EffectMethod(typeof(LoadStateAction))]
    public async Task HandleLoadStateAction(IDispatcher dispatcher)
    {
        try
        {
            var state = await _localStorageService.LoadAsync<TimerState>(StorageKey);
            dispatcher.Dispatch(new SetLoadedStateAction(state));
        }
        catch (ArgumentNullException e)
        {
            // Todo logging
        }
    }
}