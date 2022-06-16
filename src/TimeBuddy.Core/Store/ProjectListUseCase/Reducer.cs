using Fluxor;

namespace TimeBuddy.Core.Store.ProjectListUseCase;

public static class Reducer
{
    [ReducerMethod]
    public static ProjectListState ReduceSetIsLoadingAction(ProjectListState state, SetIsLoadingAction action) =>
        state with
        {
            IsLoading = action.IsLoading
        };

    [ReducerMethod]
    public static ProjectListState ReduceSetProjectsAction(ProjectListState state, SetProjectsAction action) =>
        state with
        {
            Projects = action.Projects
        };
}