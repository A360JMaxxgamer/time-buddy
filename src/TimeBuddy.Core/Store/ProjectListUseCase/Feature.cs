using Fluxor;
using TimeBuddy.Core.Models;

namespace TimeBuddy.Core.Store.ProjectListUseCase;

public class Feature : Feature<ProjectListState>
{
    /// <inheritdoc />
    public override string GetName() => nameof(ProjectListState);

    /// <inheritdoc />
    protected override ProjectListState GetInitialState() => new(new List<Project>(), false);
}