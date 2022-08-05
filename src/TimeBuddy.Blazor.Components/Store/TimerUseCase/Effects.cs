using Fluxor;
using Microsoft.Extensions.Logging;
using StrawberryShake;
using TimeBuddy.Blazor.Components.Services;
using TimeBuddy.Blazor.Components.Store.ProjectUseCase;

namespace TimeBuddy.Blazor.Components.Store.TimerUseCase;

public class Effects
{
    private const string StorageKey = "TimerState";
    private readonly ILocalStorageService _localStorageService;
    private readonly ILogger<Effects> _logger;
    private readonly IApiClient _apiClient;
    private readonly ITimerService _timerService;
    private readonly IState<TimerState> _timerState;

    public Effects(
        ILogger<Effects> logger,
        IApiClient apiClient,
        ITimerService timerService, 
        IState<TimerState> timerState,
        ILocalStorageService localStorageService)
    {
        _logger = logger;
        _apiClient = apiClient;
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
        await _apiClient.AddTimeFramesToProject.ExecuteAsync(new AddTimeFramesToProjectInput
        {
            ProjectId = action.ProjectId,
            TimeFrames = action.RecordedTimeFrames
        });

        if (_timerState.Value.ActiveProject?.Id == action.ProjectId)
        {
            dispatcher.Dispatch(new LoadProjectAction(action.ProjectId));
        }
    }

    [EffectMethod]
    public async Task HandleStopAction(StopAction _, IDispatcher dispatcher)
    {
        var recordedTimeFrames = await _timerService.StopAsync();
        if (_timerState.Value.ActiveProject is not null)
            dispatcher.Dispatch(new SaveAction(_timerState.Value.ActiveProject.Id, recordedTimeFrames));
        dispatcher.Dispatch(new SetActivityAction(TimerActivity.Stopped));
    }

    [EffectMethod]
    public async Task HandleSetProjectAction(SetProjectAction action, IDispatcher dispatcher)
    {
        var projectDetailsResult = await _apiClient.GetProjectTimerData.ExecuteAsync(
            action.Project.Id, 
            DateTime.Today,
            DateTime.Today + TimeSpan.FromHours(24));

        if (projectDetailsResult.IsSuccessResult() && projectDetailsResult.Data?.Project is not null)
        {
            dispatcher.Dispatch(projectDetailsResult.Data.Project);
        }
    }

    private async Task SaveStateLocally(TimerState state)
    {
        await _localStorageService.SaveAsync(StorageKey, state);
    }
}