using TimeBuddy.Core.Models;

namespace TimeBuddy.Core.Store.ProjectListUseCase;

public record ProjectListState(IReadOnlyList<Project> Projects, bool IsLoading);