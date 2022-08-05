using Fluxor;
using StrawberryShake;

namespace TimeBuddy.Blazor.Components.Store.ProjectListUseCase;

public class Effects
{
    private readonly IApiClient _apiClient;

    public Effects(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [EffectMethod]
    public async Task HandleFetchProjectsAsync(FetchProjectsAction _, IDispatcher dispatcher)
    {
        try
        {
            dispatcher.Dispatch(new SetIsLoadingAction(true));
            var projects = await _apiClient.GetProjectBases.ExecuteAsync();

            if (projects.IsSuccessResult() && projects.Data?.Projects?.Nodes is not null)
            {
                dispatcher.Dispatch(new SetProjectsAction(projects.Data.Projects.Nodes));
            }
        }
        catch (Exception)
        {
            // Todo add logging
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