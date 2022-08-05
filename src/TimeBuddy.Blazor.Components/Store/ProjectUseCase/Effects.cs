using Fluxor;
using Microsoft.Extensions.Logging;
using StrawberryShake;

namespace TimeBuddy.Blazor.Components.Store.ProjectUseCase;

public class Effects
{
    private readonly ILogger<Effects> _logger;
    private readonly IApiClient _apiClient;

    public Effects(ILogger<Effects> logger, IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    [EffectMethod]
    public async Task HandleLoadProjectAction(LoadProjectAction action, IDispatcher dispatcher)
    {
        _logger.LogDebug("Load project {ProjectId}", action.ProjectId);
        try
        {
            dispatcher.Dispatch(new SetIsloadingAction(true));
            var project = await _apiClient.GetProjectById.ExecuteAsync(action.ProjectId);
            project.EnsureNoErrors();
            if (project.IsSuccessResult() && project.Data?.Project is not null)
            {
                dispatcher.Dispatch(new SetProjectAction(project.Data.Project));
            }
        }
        catch (Exception e)
        {
            _logger.LogError( e, "Failed loading project {ProjectId}", action.ProjectId);
        }
        finally
        {
            dispatcher.Dispatch(new SetIsloadingAction(false));
        }
    }
}