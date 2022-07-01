using TimeBuddy.Core.Models;

namespace TimeBuddy.Core.Store.ProjectListUseCase;

public record FetchProjectsAction();

public record SetProjectsAction(IReadOnlyList<Project> Projects);

public record SetIsLoadingAction(bool IsLoading);

public record CreateProjectAction(string Name);