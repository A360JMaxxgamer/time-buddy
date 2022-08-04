using Fluxor;
using Microsoft.Extensions.Logging;
using TimeBuddy.Blazor.Components.Services;

namespace TimeBuddy.Blazor.Components.Store.TimerUseCase;

public class Effects
{
    private const string StorageKey = "TimerState";
    private readonly ILocalStorageService _localStorageService;
    private readonly ILogger<Effects> _logger;
    private readonly TimeBuddyContext _timeBuddyContext;
    private readonly ITimerService _timerService;
    private readonly IState<TimerState> _timerState;

    public Effects(
        ILogger<Effects> logger,
        TimeBuddyContext timeBuddyContext, 
        ITimerService timerService, 
        IState<TimerState> timerState,
        ILocalStorageService localStorageService)
    {
        _logger = logger;
        _timeBuddyContext = timeBuddyContext;
        _timerService = timerService;
        _timerState = timerState;
        _localStorageService = localStorageService;
    }

    [EffectMethod(typeof(LoadStateAction))]
    public async Task HandleLoadStateAction(IDispatcher dispatcher)
    {
        try
        {
            var state = await _localStorageService.LoadAsync<TimerState>(StorageKey);
            await _timerService.LoadAsync(state);
            dispatcher.Dispatch(new SetLoadedStateAction(state));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Loading timer state failed");
        }
    }

    [EffectMethod]
    public async Task HandlePauseAction(PauseAction _, IDispatcher dispatcher)
    {
        var recordedTimeFrame = await _timerService.PauseAsync();
        dispatcher.Dispatch(new AddRecordedTimeFrame(recordedTimeFrame));
        dispatcher.Dispatch(new SetActivityAction(TimerActivity.Paused));
    }

    [EffectMethod]
    public async Task HandlePlayAction(PlayAction _, IDispatcher dispatcher)
    {
        var startedAt = await _timerService.PlayAsync();
        dispatcher.Dispatch(new SetLastStartAction(startedAt));
        dispatcher.Dispatch(new SetActivityAction(TimerActivity.Active));
    }

    [EffectMethod(typeof(RefreshUiTimerAction))]
    public async Task HandleRefreshUiTimerAction(IDispatcher _)
    {
        await SaveStateLocally(_timerState.Value);
    }

    [EffectMethod]
    public async Task HandleSaveAction(SaveAction action, IDispatcher dispatcher)
    {
        var project = await _timeBuddyContext.Projects.FirstOrDefaultAsync(p => p.Id == action.Project.Id);
        if (project != null)
        {
            project.TimeFrames.AddRange(action.RecordedTimeFrames);
            await _timeBuddyContext.SaveChangesAsync();
            var state = TimerState.New();
            await SaveStateLocally(state with
            {
                ActiveProject = project
            });
            dispatcher.Dispatch(new SetActiveProjectAction(project));
        }
    }

    [EffectMethod]
    public async Task HandleStopAction(StopAction _, IDispatcher dispatcher)
    {
        var recordedTimeFrames = await _timerService.StopAsync();
        if (_timerState.Value.ActiveProject is not null)
            dispatcher.Dispatch(new SaveAction(_timerState.Value.ActiveProject, recordedTimeFrames));
        dispatcher.Dispatch(new SetActivityAction(TimerActivity.Stopped));

    }

    private async Task SaveStateLocally(TimerState state)
    {
        await _localStorageService.SaveAsync(StorageKey, state);
    }
}