using Fluxor;
using StrawberryShake;

namespace TimeBuddy.Blazor.Components.Store.ProjectUseCase;

public class Effects
{
    private readonly IApiClient _apiClient;

    public Effects(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [EffectMethod]
    public async Task HandleLoadProjectAction(LoadProjectAction action, IDispatcher dispatcher)
    {
        try
        {
            dispatcher.Dispatch(new SetIsloadingAction(true));
            var project = await _apiClient.GetProjectById.ExecuteAsync(action.ProjectId);
            if (project.IsSuccessResult() && project.Data?.Project is not null)
            {
                dispatcher.Dispatch(new SetProjectAction(project.Data.Project));
            }
        }
        catch (Exception)
        {
            // Todo add logging
        }
        finally
        {
            dispatcher.Dispatch(new SetIsloadingAction(false));
        }
    }
}