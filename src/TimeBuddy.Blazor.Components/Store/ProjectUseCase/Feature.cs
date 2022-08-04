namespace TimeBuddy.Blazor.Components.Store.ProjectUseCase;

public class Feature : Feature<ProjectState>
{
    /// <inheritdoc />
    public override string GetName() => nameof(ProjectState);

    /// <inheritdoc />
    protected override ProjectState GetInitialState() => new ProjectState(null, false);
}