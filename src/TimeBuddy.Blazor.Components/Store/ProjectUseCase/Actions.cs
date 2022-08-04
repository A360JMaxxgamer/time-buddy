namespace TimeBuddy.Blazor.Components.Store.ProjectUseCase;

public record SetIsloadingAction(bool IsLoading);

public record LoadProjectAction(int ProjectId);

public record SetProjectAction(Project Project);