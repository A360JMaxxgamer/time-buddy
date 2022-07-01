using Fluxor;

namespace TimeBuddy.Core.Store.ProjectUseCase;

public static class Reducer
{
    [ReducerMethod]
    public static ProjectState ReduceSetIsLoadingAction(ProjectState state, SetIsloadingAction action) =>
        state with
        {
            IsLoading = action.IsLoading
        };

    [ReducerMethod]
    public static ProjectState ReduceSetProjectAction(ProjectState state, SetProjectAction action) =>
        state with
        {
            Project = action.Project
        };
}