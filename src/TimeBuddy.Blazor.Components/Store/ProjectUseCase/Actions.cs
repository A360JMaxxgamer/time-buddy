namespace TimeBuddy.Blazor.Components.Store.ProjectUseCase;

public record SetIsloadingAction(bool IsLoading);

public record LoadProjectAction(Guid ProjectId);

public record SetProjectAction(IProjectDetailed Project);