using TimeBuddy.Core.Models;

namespace TimeBuddy.Core.Store.TimerUseCase;

public record PlayAction;

public record PauseAction;

public record StopAction;

public record SetActiveProjectAction(Project Project);

public record SetActivityAction(TimerActivity Activity);

public record SetLastStartAction(DateTime StartedAt);

public record AddRecordedTimeFrame(TimeFrame TimeFrame);

public record SaveAction(Project Project, TimeFrame[] RecordedTimeFrames);

public record SetLoadedStateAction(TimerState State);

public record LoadStateAction;

public record RefreshUiTimerAction;

public record ClearAction;
