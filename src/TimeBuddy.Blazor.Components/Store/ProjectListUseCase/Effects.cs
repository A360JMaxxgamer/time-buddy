using Fluxor;
using Microsoft.Extensions.Logging;
using StrawberryShake;

namespace TimeBuddy.Blazor.Components.Store.ProjectListUseCase;

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
    public async Task HandleFetchProjectsAsync(FetchProjectsAction _, IDispatcher dispatcher)
    {
        try
        {
            dispatcher.Dispatch(new SetIsLoadingAction(true));
            var projects = await _apiClient.GetProjectBases.ExecuteAsync();
            
            projects.EnsureNoErrors();
            if (projects.IsSuccessResult() && projects.Data?.Projects?.Nodes is not null)
            {
                dispatcher.Dispatch(new SetProjectsAction(projects.Data.Projects.Nodes));
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Fetching projects failed");
        }
        finally
        {
            dispatcher.Dispatch(new SetIsLoadingAction(false));
        }
    }

    [EffectMethod]
    public async Task HandleCreateProjectAction(CreateProjectAction action, IDispatcher dispatcher)
    {
        await _apiClient.CreateProject.ExecuteAsync(new CreateProjectInput()
        {
            ProjectName = action.Name
        });
        dispatcher.Dispatch(new FetchProjectsAction());
    }
}