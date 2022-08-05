namespace TimeBuddy.Blazor.Components.Store.ProjectListUseCase;

public record ProjectListState(IReadOnlyList<IProjectBase> Projects, bool IsLoading);