using Fluxor;
using TimeBuddy.Core.Services;

namespace TimeBuddy.Core.Store.TimerUseCase;

public class Effects
{
    private readonly ITimerService _timerService;

    public Effects(ITimerService timerService)
    {
        _timerService = timerService;
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
}