namespace TimeBuddy.Blazor.Components.Store.ProjectListUseCase;

public record ProjectListState(IReadOnlyList<Project> Projects, bool IsLoading);