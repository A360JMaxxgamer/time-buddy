using TimeBuddy.Core.Models;

namespace TimeBuddy.Core.Store.ProjectUseCase;

public record ProjectState(Project? Project, bool IsLoading);