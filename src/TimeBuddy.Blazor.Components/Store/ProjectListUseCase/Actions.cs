namespace TimeBuddy.Blazor.Components.Store.ProjectListUseCase;

public record FetchProjectsAction();

public record SetProjectsAction(IReadOnlyList<IProjectBase> Projects);

public record SetIsLoadingAction(bool IsLoading);

public record CreateProjectAction(string Name);