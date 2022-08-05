namespace TimeBuddy.Blazor.Components.Store.TimerUseCase;

public record PlayAction;

public record PauseAction;

public record StopAction;

public record SetActiveProjectAction(IProjectBase Project);

public record SetActiveProjectDetailsAction(IGetProjectTimerData_Project ProjectDetails);

public record SetActivityAction(TimerActivity Activity);

public record SetLastStartAction(DateTime StartedAt);

public record AddRecordedTimeFrame(TimeFrameInput TimeFrame);

public record SaveAction(Guid ProjectId, TimeFrameInput[] RecordedTimeFrames);

public record SetLoadedStateAction(TimerState State);

public record LoadStateAction;

public record RefreshUiTimerAction;

public record ClearAction;
