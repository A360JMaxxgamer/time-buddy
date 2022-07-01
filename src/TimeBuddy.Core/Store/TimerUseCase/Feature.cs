using Fluxor;

namespace TimeBuddy.Core.Store.TimerUseCase;

public class Feature : Feature<TimerState>
{
    /// <inheritdoc />
    public override string GetName() => nameof(TimerState);

    /// <inheritdoc />
    protected override TimerState GetInitialState() => TimerState.New();
}