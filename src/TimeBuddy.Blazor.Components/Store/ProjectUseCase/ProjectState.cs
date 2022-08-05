namespace TimeBuddy.Blazor.Components.Store.ProjectUseCase;

public record ProjectState(IProjectDetailed? Project, bool IsLoading);